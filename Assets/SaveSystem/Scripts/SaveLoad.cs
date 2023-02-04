using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoad
{
    public static bool Encrypt;
    public static string Extention = "save";

    public static SaveFile New(string filename)
    {
        SaveFile save = new SaveFile();

        save.FileName = filename;
        save.createdDate = System.DateTime.Now;

        return save;
    }

    public static void Save(SaveFile file)
    {        
        file.lastSaved = System.DateTime.Now;

        string json = JsonUtility.ToJson(file, !Encrypt);
        if (Encrypt)
        {
            json = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(json));
        }

        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + file.FileName + "." + Extention, json);
    }

    public static SaveFile Load(string filename)
    {
        string filetext = System.IO.File.ReadAllText(Application.persistentDataPath + "/" + filename + "." + Extention);

        if(Encrypt)
        {
            filetext = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(filetext));
        }

        return JsonUtility.FromJson<SaveFile>(filetext);
    }

    public static string[] GetAllSaves()
    {
        string[] fullpaths = System.IO.Directory.GetFiles(Application.persistentDataPath);
        string[] files = new string[fullpaths.Length];

        for(int i = 0; i < fullpaths.Length; i++)
        {
            files[i] = System.IO.Path.GetFileNameWithoutExtension(fullpaths[i]);
        }

        return files;
    }
}
