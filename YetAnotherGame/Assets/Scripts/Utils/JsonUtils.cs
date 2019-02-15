using System.IO;
using UnityEngine;

public static class JsonUtils
{
    /* 
    file must be located:
    - in Assets folder (in design mode)
    - in YetAnotherGameBuild/YetAnotherGame_Data folder (in standalone exe)
    */
    public static T ParseFromFile<T>(string relativePath) where T : class
    {
        var path = $"{Application.dataPath}/{relativePath}";
        if(!File.Exists(path))
        {
            Debug.LogError($"Filepath {path} does not exist.");
            return null;
        }

        string fileContent = File.ReadAllText(path);
        if(fileContent.Length == 0)
        {
            Debug.LogError($"File {path} is empty.");
            return null;
        }

        try
        {
            return JsonUtility.FromJson<T>(fileContent);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
            return null;
        }        
    }
}