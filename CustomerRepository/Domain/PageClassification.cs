using System.Collections.Generic;
using System.IO;

namespace CustomerRepository.Domain
{
    public class PageClassification
    {
        Dictionary<string, HashSet<string>> classDictionaries;
        List<string> allFiles;

        //public static string GetClassification(string searchWord)
        //{
        //    string currentpath = HostingEnvironment.MapPath("~");
        //    string cat = "";

        //    string sourceFolder = currentpath + @"\PageClass";
        //    List<string> allFiles = new List<string>();
        //    AddFileNamesToList(sourceFolder, allFiles);
        //    foreach (string fileName in allFiles)
        //    {
        //        foreach (var contents in File.ReadLines(fileName))
        //        {
        //            if (contents==searchWord)
        //            {
        //                int count = fileName.Split('\\').Length - 1;
        //                cat = cat + ";" + fileName.Split('\\')[count - 1];

        //                // +":" + fileName.Split('\\')[count - 1];
        //                //return cat;
        //            }
        //        }
        //    }

        //    if (cat == "") cat = "notfound";
        //    return cat;
        //}


    public string GetClassification(string searchWord)
    {

        return "";
    }

    private void FillClassDictionaries()
    {
        classDictionaries = new Dictionary<string, HashSet<string>>();
        string cat;
        foreach (string fileName in allFiles)
        {
            int count = fileName.Split('\\').Length - 1;
            cat = fileName.Split('\\')[count - 1];
            if (!classDictionaries.ContainsKey(cat))
            {
                classDictionaries.Add(cat, new HashSet<string>());
            }
            foreach (var contents in File.ReadLines(fileName))
            {
                classDictionaries[cat].Add(contents);
            }
        }
    }

    public static void AddFileNamesToList(string sourceDir, List<string> allFiles)
    {

            string[] fileEntries = Directory.GetFiles(sourceDir);
            foreach (string fileName in fileEntries)
            {
                allFiles.Add(fileName);
            }

            //Recursion    
            string[] subdirectoryEntries = Directory.GetDirectories(sourceDir);
            foreach (string item in subdirectoryEntries)
            {
                // Avoid "reparse points"
                if ((File.GetAttributes(item) & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint)
                {
                    AddFileNamesToList(item, allFiles);
                }
            }
    }

    }
}