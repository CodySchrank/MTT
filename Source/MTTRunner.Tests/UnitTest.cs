using NUnit.Framework;
using MTTRunner;
using System.IO;
using System;
using System.Text.RegularExpressions;

namespace MTTRunner.Tests
{
    public class BasicTests
    {
        private readonly string CurrentDir = Directory.GetCurrentDirectory().Replace("\\", "/");
        private readonly string WorkingDir = "workingDir/";
        private readonly string ConvertDir = "convertDir/";
        private string VehicleFile;
        private string VehicleStateFile;

        [SetUp]
        public void Setup()
        {
            VehicleFile = Path.Combine(CurrentDir, ConvertDir, "Vehicles/vehicle.ts");
            VehicleStateFile = Path.Combine(CurrentDir, ConvertDir, "Vehicles/vehicleState.ts");

            var resources = CurrentDir.Replace("Source/MTTRunner.Tests/bin/Debug/netcoreapp2.2", "example/Resources");

            if(!Directory.Exists(resources)) {
                throw new Exception("Resources Directory does not exist");
            }

            if(Directory.Exists(WorkingDir)) {
                Directory.Delete(WorkingDir, true);
            }

            DirectoryCopy(resources, WorkingDir, true);

            var dirs = new string[] {WorkingDir, ConvertDir};

            MTTRunner.Program.StartService(dirs);
        }

        [Test]
        public void WorkingDirExists()
        {
            Assert.That(Directory.Exists(Path.Combine(CurrentDir, WorkingDir)));
        }

        [Test]
        public void ConvertDirExists()
        {
           Assert.That(Directory.Exists(Path.Combine(CurrentDir, ConvertDir)));
        }

        [Test]
        public void ConvertedFileExists()
        {
            Assert.That(File.Exists(VehicleFile));
        }

        [Test]
        public void AutoGeneratedExists() {
            string[] lines = System.IO.File.ReadAllLines(VehicleFile);

            Assert.That(lines[0], Is.EqualTo("/* Auto Generated */"));
        }

        [Test]
        public void DifferentDirImportStatementExists() {
            string[] lines = System.IO.File.ReadAllLines(VehicleFile);

            Assert.That(lines[2], Is.EqualTo("import { Entity } from \"./../entity\";"));
        }

        [Test]
        public void SameDirImportStatementExists() {
            string[] lines = System.IO.File.ReadAllLines(VehicleFile);

            Assert.That(lines[4], Is.EqualTo("import { VehicleState } from \"./vehicleState\";"));
        }

        [Test]
        public void ClassTransformationExists() {
            string[] lines = System.IO.File.ReadAllLines(VehicleFile);

            Assert.That(lines[7], Is.EqualTo("export interface Vehicle extends Entity {"));
        }

        [Test]
        public void PropertyExists() {
            string[] lines = System.IO.File.ReadAllLines(VehicleFile);

            Assert.That(lines[8], Is.EqualTo("    year: number;"));
        }

        [Test]
        public void OptionalPropertyExists() {
            string[] lines = System.IO.File.ReadAllLines(VehicleFile);

            Assert.That(lines[11], Is.EqualTo("    mileage?: number;"));
        }

        [Test]
        public void CheckMapExists() {
            string[] lines = System.IO.File.ReadAllLines(VehicleFile);

            Assert.That(lines[12], Is.EqualTo("    options: Map<string, Units>;"));
        }

        [Test]
        public void EnumPropertyExists() {
            string[] lines = System.IO.File.ReadAllLines(VehicleFile);

            Assert.That(lines[13], Is.EqualTo("    condition: VehicleState;"));
        }

        [Test]
        public void ArrayExists() {
            string[] lines = System.IO.File.ReadAllLines(VehicleFile);

            Assert.That(lines[14], Is.EqualTo("    parts: Part[];"));
        }

        [Test]
        public void InitializedListExists()
        {
            string[] lines = System.IO.File.ReadAllLines(VehicleFile);
            Assert.That(lines[15], Is.EqualTo("    spareParts: Part[];"));
        }
        
        [Test]
        public void GuidExists()
        {
            string[] lines = System.IO.File.ReadAllLines(VehicleFile);
            Assert.That(lines[16], Is.EqualTo("    id: string;"));
        }

        [Test]
        public void CommentsDoNotExist() {
            string[] lines = System.IO.File.ReadAllLines(VehicleFile);
            
            foreach(string line in lines) {
                Assert.That(line, Does.Not.Contain("//"));
            }
        }

        [Test]
        public void PreprocessorDirectivesDoNotExist() {
            string[] lines = System.IO.File.ReadAllLines(VehicleFile);
            
            foreach(string line in lines) {
                Assert.That(line.IsNotPreProcessorDirective());
            }
        }

        [Test]
        public void UsingStatementDoesNotExist() {
            string[] lines = System.IO.File.ReadAllLines(VehicleFile);
            
            foreach(string line in lines) {
                Assert.That(line.DoesNotStrictContain("using"));
            }
        }

        [Test]
        public void NamespaceStatementDoesNotExist() {
            string[] lines = System.IO.File.ReadAllLines(VehicleFile);
            
            foreach(string line in lines) {
                Assert.That(line.DoesNotStrictContain("namespace"));
            }
        }

        
        [Test]
        public void EnumTransformationExists() {
            string[] lines = System.IO.File.ReadAllLines(VehicleStateFile);

            Assert.That(lines[2], Is.EqualTo("export enum VehicleState {"));
        }

                
        [Test]
        public void EnumPropertyWithValueExists() {
            string[] lines = System.IO.File.ReadAllLines(VehicleStateFile);

            Assert.That(lines[3], Is.EqualTo("    broken = 1,"));
        }

        [Test]
        public void EnumPropertyWithoutValueExists() {
            string[] lines = System.IO.File.ReadAllLines(VehicleStateFile);

            Assert.That(lines[4], Is.EqualTo("    used,"));
        }

        /**
        
            Helper Methods
        
         */
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }
            
            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}

public static class StringExtension
    {
        public static bool DoesNotStrictContain(this string str, string match)
        {
            string reg = "(^|\\s)" + match + "(\\s|$)";
            return !Regex.IsMatch(str, reg);
        }

        public static bool IsNotPreProcessorDirective(this string str)
        {
            return !Regex.IsMatch(str, @"^#\w+");
        }
    }