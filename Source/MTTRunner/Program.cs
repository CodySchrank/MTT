using System;
using MTT;

namespace MTTRunner
{
    class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Arguments should contain the ConvertDirectory and the WorkingDirectory");
            }

            var convertService = new ConvertService((logString, logArgs) => Console.WriteLine(logString, logArgs))
            {
                ConvertDirectory = args[0],
                WorkingDirectory = args[1]
            };
            convertService.Execute();

            Console.ReadLine();
        }
    }
}
