using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Build.Framework;
using MTT;
using MSBuildTask = Microsoft.Build.Utilities.Task;


/**
    TODO:
    Add inheritance
 */

namespace MSBuildTasks
{
    public class ConvertMain : MSBuildTask
    {
        /// <summary>
        /// The current working directory for the convert process
        /// </summary>
        public string WorkingDirectory { get; set; }

        /// <summary>
        /// The directory to save the ts models
        /// </summary>
        public string ConvertDirectory { get; set; }

        protected MessageImportance LoggingImportance { get; } = MessageImportance.High;

        private List<ModelFile> Models {get; set;}

        private string LocalWorkingDir {get; set;}

        private string LocalConvertDir { get; set; }

        public ConvertMain() {
            Models = new List<ModelFile>();
        }

        public override bool Execute()
        {
            GetWorkingDirectory();
            GetConvertDirectory();
            LoadModels();
            BreakDown();
            Convert();
            return true;
        }

        public void GetWorkingDirectory() {
            var dir = Directory.GetCurrentDirectory();

            if(string.IsNullOrEmpty(WorkingDirectory)) {
                Log.LogMessage(LoggingImportance, "Using Default Working Directory {0}", dir);
                LocalWorkingDir = dir;
                return;
            }

            var localdir = Path.Combine(dir, WorkingDirectory);

            if(!Directory.Exists(localdir)) {
                Log.LogError("Working Directory does not exist {0}, creating..", localdir);
                Directory.CreateDirectory(localdir).Create();
                LocalConvertDir = localdir;
                return;
            }

            Log.LogMessage(LoggingImportance, "Using User Directory {0}", localdir);
            LocalWorkingDir = localdir;
            return;
        }

        public void GetConvertDirectory() {
            var dir = Directory.GetCurrentDirectory();

            if (string.IsNullOrEmpty(ConvertDirectory))
            {
                Log.LogMessage(LoggingImportance, "Using Default Convert Directory {0} - this does not always update", dir);
                LocalConvertDir = dir;
                return;
            }

            var localdir = Path.Combine(dir, ConvertDirectory);

            if (!Directory.Exists(localdir))
            {
                Log.LogError("Convert Directory does not exist {0}, creating..", localdir);
            } else {
                Log.LogMessage(LoggingImportance, "Using User Directory {0}", localdir);
                Directory.Delete(localdir, true);
            }

            Directory.CreateDirectory(localdir).Create();
            LocalConvertDir = localdir;
            return;
        }

        public void LoadModels() {
            var files = Directory.GetFiles(LocalWorkingDir);
            var dirs = Directory.GetDirectories(LocalWorkingDir);

            foreach (var dir in dirs)
            {
                string dirName = dir.Replace(LocalWorkingDir, String.Empty);

                if(!String.IsNullOrEmpty(dirName))
                {
                    var innerFiles = Directory.GetFiles(dir);

                    foreach (var file in innerFiles)
                    {
                        AddModel(file, dirName);
                    }

                }
            }

            foreach (var file in files)
            {
                AddModel(file, "");
            }
        }

        public void AddModel(string file, string structure = "") {
            string[] explodedDir = file.Split('/');
            string fileName = explodedDir[explodedDir.Length -1];

            string[] fileInfo = File.ReadAllLines(file);

            var modelFile = new ModelFile()
            {
                Name = ToPascalCase(fileName.Replace("Resource", String.Empty).Replace(".cs", String.Empty)),
                Info = fileInfo,
                Structure = structure
            };

            Models.Add(modelFile);
        }

        public void BreakDown() {
            foreach (var file in Models)
            {
                //TODO:  Could check class inheritence here

                foreach (var line in file.Info)
                {
                    if(line.Contains("public") && !line.Contains("class") && !IsContructor(line)) {
                        string[] modLine = line.Replace("public", "").Trim().Split(' ');

                        string type = modLine[0];

                        bool IsArray = CheckIsArray(type);
                        
                        type = CleanType(type);

                        bool isUserDefined = false;
                        var userDefinedImport = "";

                        foreach (var f in Models)
                        {
                            if(f.Name.Equals(type)) {
                                isUserDefined = true;
                                
                                if (file.Structure.Equals(f.Structure)){
                                    userDefinedImport = "./" + ToCamelCase(type); //same dir
                                } else if(String.IsNullOrEmpty(file.Structure)) {
                                    userDefinedImport = "./" + f.Structure + "/" + ToCamelCase(type);  //top level
                                } else {
                                    userDefinedImport = "../" + f.Structure + "/" + ToCamelCase(type); //different dir
                                }
                                
                            }
                        }

                        string varName = modLine[1];

                        LineObject obj = new LineObject() {
                            VariableName = varName,
                            Type = isUserDefined ? type : TypeOf(type),
                            IsArray = IsArray,
                            UserDefined = isUserDefined,
                            UserDefinedImport = userDefinedImport
                        };

                        file.Objects.Add(obj);
                    }
                }
            }
        }

        private string TypeOf(string type) {
            switch (type)
            {
                case "byte":
                case "sbyte":
                case "decimal":
                case "double":
                case "float":
                case "int":
                case "uint":
                case "long":
                case "ulong":
                case "short":
                case "ushort":
                return "number";
                
                case "bool":
                return "boolean";

                case "string":
                return "string";

                case "DateTime":
                return "Date";

                default: return "any";
            }
        }

        public void Convert() {
            Log.LogMessage(LoggingImportance, "Converting..");

            foreach (var file in Models)
            {
                DirectoryInfo di = Directory.CreateDirectory(Path.Combine(LocalConvertDir, file.Structure));
                di.Create();

                string fileName = ToCamelCase(file.Name + ".ts");
                Log.LogMessage(LoggingImportance, "Creating file {0}", fileName);
                string saveDir = Path.Combine(di.FullName,fileName);
                
                using (var stream = new FileStream(saveDir, FileMode.Create, FileAccess.Write, FileShare.Read, 4096, FileOptions.SequentialScan))
                using (StreamWriter f =
                    new StreamWriter(stream, System.Text.Encoding.UTF8, 1024, false))
                        {   
                            var importing = false;  //only used for formatting

                            foreach (var obj in file.Objects)
                            {
                                if(obj.UserDefined) {
                                    importing = true;
                                    f.WriteLine("import { " + obj.Type + " } from \"" + obj.UserDefinedImport + "\"");  //need to check for duplicate imports
                                }
                            }

                            if(importing) {
                                f.WriteLine("");
                            }

                            f.WriteLine("export interface " + file.Name + " {");
                            foreach (var obj in file.Objects)
                            {
                                var str = 
                                    ToCamelCase(obj.VariableName) 
                                    + ": " 
                                    + obj.Type 
                                    + (obj.IsArray ? "[]" : String.Empty) 
                                    + ";";

                                f.WriteLine("\t" + str);
                            }
                            f.WriteLine("}");
                        }
            }
        }

        private string ToCamelCase(string str)
        {
            if (String.IsNullOrEmpty(str) || Char.IsLower(str, 0))
                return str;

            bool isCaps = true;

            foreach (var c in str)
            {
                if(Char.IsLetter(c) && Char.IsLower(c)) isCaps = false;
            }

            if(isCaps) return str.ToLower();

            return Char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        private string ToPascalCase(string str) {
            if (String.IsNullOrEmpty(str) || Char.IsUpper(str, 0))
                return str;

            return Char.ToUpperInvariant(str[0]) + str.Substring(1);
        }

        public bool CheckIsArray(string type)
        {
            return type.Contains("[]") || type.Contains("ICollection") || type.Contains("IEnumerable");
        }

        public string CleanType(string type) {
            return type.Replace("[]", String.Empty)
                .Replace("ICollection", String.Empty)
                .Replace("IEnumerable", String.Empty)
                .Replace("<", String.Empty)
                .Replace(">", String.Empty);
        }

        public bool IsContructor(string line) {
            return line.Contains("()") || ((line.Contains("(") && line.Contains(")")));
        }
    }
}
