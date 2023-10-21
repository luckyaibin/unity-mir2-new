using UnityEngine;

using UnityEditor;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
public class ChangeMetaFile : ScriptableObject
{



    [MenuItem("工具/改变meta/直接修改meta为mir格式")]

    static void ChangeTextureFormat_AutoCompressed()
    {

        // You can either filter files to get only neccessary files by its file extension using LINQ.
        // It excludes .meta files from all the gathers file list.
        var assetFiles = GetFiles(
            GetSelectedPathOrFallback()).Where(s => s.Contains(".meta") == true).OrderBy(go => go);

        foreach (string f in assetFiles)
        {
            Debug.Log("Files: " + f);
        }

        if (true)
        {
            
            foreach (string filename in assetFiles)
            {
                
                var fileContent = File.ReadAllText(filename);
                var newFileContent = fileContent;
                newFileContent = Regex.Replace(newFileContent, "isReadable: 0", "isReadable: 1");
                newFileContent = Regex.Replace(newFileContent, "nPOTScale: 1", "nPOTScale: 0");
                newFileContent = Regex.Replace(newFileContent, "spriteMode: 0", "spriteMode: 1");
                newFileContent = Regex.Replace(newFileContent, "alignment: 0", "alignment: 1");
                newFileContent = Regex.Replace(newFileContent, "spritePivot: {x: 0.5, y: 0.5}", "spritePivot: {x: 0, y: 1}");
                newFileContent = Regex.Replace(newFileContent, "spritePixelsToUnits: 100", "spritePixelsToUnits: 1");
                newFileContent = Regex.Replace(newFileContent, "spritePixelsToUnits: 11", "spritePixelsToUnits: 1");

                File.WriteAllText(filename, newFileContent);
               
            }
            // Refresh again to update import settings
            //AssetDatabase.Refresh();
        }

    }

    /// <summary>
    /// Retrieves selected folder on Project view.
    /// </summary>
    /// <returns></returns>
    public static string GetSelectedPathOrFallback()
    {
        string path = "Assets";

        foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
                break;
            }
        }
        return path;
    }

    /// <summary>
    /// Recursively gather all files under the given path including all its subfolders.
    /// </summary>
    static IEnumerable<string> GetFiles(string path)
    {
        Queue<string> queue = new Queue<string>();
        queue.Enqueue(path);
        while (queue.Count > 0)
        {
            path = queue.Dequeue();
            try
            {
                foreach (string subDir in Directory.GetDirectories(path))
                {
                    queue.Enqueue(subDir);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            string[] files = null;
            try
            {
                files = Directory.GetFiles(path);
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            if (files != null)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    yield return files[i];
                }
            }
        }
    }


}

