﻿using System;
using System.IO;

namespace CodeLabsGenerator;

class Program
{
    private static readonly string basePagePath = @"C:\TRMO\RiderProjects\CodeLabs\BasePageSource\extralevelfolder\BasePage.html";

    static void Main(string[] args)
    {
        GenerateAllModifiedTutorials();
        // ReGenerateAllTutorials();
        // GenerateOne("C:\\TRMO\\RiderProjects\\CodeLabs\\Tutorials\\Sandbox");
    }

    private static void ReGenerateAllTutorials()
    {
        var tutorialDirectories = GetAllTutorialDirectories();
        foreach (string directory in tutorialDirectories)
        {
            // var shouldGenerate = ShouldGenerateTutorial(directory);
            // if (shouldGenerate)
            // {
            GenerateOne(directory);
            // }
        }
    }

    private static void GenerateAllModifiedTutorials()
    {
        var tutorialDirectories = GetAllTutorialDirectories();
        foreach (string directory in tutorialDirectories)
        {
            var shouldGenerate = ShouldGenerateTutorial(directory);
            if (shouldGenerate)
            {
                GenerateOne(directory);
            }
        }
    }

    private static bool ShouldGenerateTutorial(string directory)
    {
        DateTime directoryWriteTime = Directory.GetLastWriteTime(directory);
        DateTime pageWriteTime = File.GetLastWriteTime(directory + "/Page.html");
        DateTime basePageWriteTime = File.GetLastWriteTime(basePagePath);

        bool directoryIsNewerThanPage = directoryWriteTime > pageWriteTime;
        bool basePageIsNewerThanPage = basePageWriteTime > pageWriteTime;

        return directoryIsNewerThanPage || basePageIsNewerThanPage;
    }

    private static string[] GetAllTutorialDirectories()
    {
        string tutorialsFolderPath = @"C:\TRMO\RiderProjects\CodeLabs\Tutorials\";
        var tutorialDirectories = Directory.GetDirectories(tutorialsFolderPath);
        return tutorialDirectories;
    }

    private static void GenerateOne(string tutorial)
    {
        // string folderPathToMdSteps = @"C:\TRMO\RiderProjects\CodeLabs\Tutorials\" + tutorial;
        string folderPathToMdSteps = tutorial;
        Console.WriteLine("Generating \"" + folderPathToMdSteps.Split("\\")[^1] + "\" ...");
        Generator.GenerateHtmlPageFromMdFiles(folderPathToMdSteps, basePagePath);
        Console.WriteLine("Done!");
    }
}