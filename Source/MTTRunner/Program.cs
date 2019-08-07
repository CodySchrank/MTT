using System;
using MTT;

namespace MTTRunner
{
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                throw new Exception("Arguments should contain the ConvertDirectory and the WorkingDirectory");
            }

            Program.StartService(args);
        }

        public static void StartService(string[] args) {
            var convertService = new ConvertService((logString, logArgs) => Console.WriteLine(logString, logArgs))
            {
                WorkingDirectory = args[0],
                ConvertDirectory = args[1]
            };
            
            convertService.Execute();
        }


    }
}
