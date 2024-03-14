using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.dataPath + "/Saves3/";

    public static void Init() 
    {
        if(!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save(int sceneNumber, string saveString)
    {
        File.WriteAllText(SAVE_FOLDER + "/scene_" + sceneNumber + ".txt", saveString);
    }

    public static string Load(int sceneNumber)
    {
        if(File.Exists(SAVE_FOLDER + "/scene_" + sceneNumber + ".txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/scene_" + sceneNumber + ".txt");
            return saveString;
        }
        else
        {
            return null;
        }
    }
}
