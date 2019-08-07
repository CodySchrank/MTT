using NUnit.Framework;
using MTTRunner;
using System.IO;
using System;

namespace MTTRunner.Tests
{
    public class BasicTests
    {
        private readonly string CurrentDir = Directory.GetCurrentDirectory();
        private readonly string WorkingDir = "workingDir/";
        private readonly string ConvertDir = "convertDir/";

        [SetUp]
        public void Setup()
        {
            var resources = CurrentDir.Replace("\\", "/").Replace("Source/MTTRunner.Tests/bin/Debug/netcoreapp2.2", "example/Resources");

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
            if(Directory.Exists(Path.Combine(CurrentDir, WorkingDir))) {
                Assert.Pass();
            } else {
                Assert.Fail();
            }

        }

        [Test]
        public void ConvertDirExists()
        {
            if(Directory.Exists(Path.Combine(CurrentDir, ConvertDir))) {
                Assert.Pass();
            } else {
                Assert.Fail();
            }
        }


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