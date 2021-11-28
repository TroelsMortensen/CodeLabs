namespace CodeLabsGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string folderPathToMdSteps = @"C:\TRMO\RiderProjects\CodeLabs\MarkDownSources\CsharpSockets";
            string basePagePath = @"C:\TRMO\RiderProjects\CodeLabs\BasePageSource\BasePage.html";
            Generator.GenerateHtmlPageFromMdFiles(folderPathToMdSteps, basePagePath);
        }
    }
}