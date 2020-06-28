using System.IO;
using UnityEngine;

public static class FileUtils
{
    public static string Read (string pathToFile) 
    {
        try 
        {
            FileStream file = new FileStream (pathToFile, FileMode.Open, FileAccess.Read);
            StreamReader streamReader = new StreamReader (file);
            string dataAsString = streamReader.ReadToEnd ();

            streamReader.Close ();
            file.Close ();

            return dataAsString;
        } 
        catch (FileNotFoundException error) 
        {
            Debug.LogError (error);
        }

        return "";
    }
}