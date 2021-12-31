using System;

namespace CodeLabsGenerator
{
    class Program
    {
        private static readonly string basePagePath = @"C:\TRMO\RiderProjects\CodeLabs\BasePageSource\extralevelfolder\BasePage.html";

        static void Main(string[] args)
        {
            // string folderPathToMdSteps = @"C:\TRMO\RiderProjects\CodeLabs\Tutorials\BlazorTodoTutorialPart1";
            string folderPathToMdSteps = @"C:\TRMO\RiderProjects\CodeLabs\Tutorials\CodelabsDoc";
            Console.WriteLine("Generating \"" + folderPathToMdSteps.Split("\\")[^1] + "\" ...");
            Generator.GenerateHtmlPageFromMdFiles(folderPathToMdSteps, basePagePath);
            Console.WriteLine("Done!");
        }
    }
}