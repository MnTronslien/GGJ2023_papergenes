using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadDebug : MonoBehaviour
{
    string filename;
    string[] saves;
    SaveFile save;

    private void Start()
    {
        saves = SaveLoad.GetAllSaves();
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("New Save", GUILayout.Width(100)))
        {
            save = SaveLoad.New(filename);
        }
        filename = GUILayout.TextField(filename, GUILayout.Width(300));
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        GUILayout.Label("Current:");
        if(save == null)
        {
            GUILayout.Label("NONE");
        } else
        {
            DrawSave();
        }

        GUI.enabled = save != null;
        if (GUILayout.Button("Save"))
        {
            SaveLoad.Save(save);
            saves = SaveLoad.GetAllSaves();
        }
        GUI.enabled = true;

        GUILayout.Space(10);

        foreach(string s in saves)
        {
            GUILayout.BeginHorizontal();
            if(GUILayout.Button("Load", GUILayout.Width(100)))
            {
                save = SaveLoad.Load(s);
            }
            GUILayout.Label(s);
            GUILayout.EndHorizontal();
        }
    }

    void DrawSave()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("filename:", GUILayout.Width(100));
        save.FileName = GUILayout.TextField(save.FileName, GUILayout.Width(300));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("created:", GUILayout.Width(100));
        GUILayout.Label(save.createdDate.ToString());
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("updated:", GUILayout.Width(100));
        GUILayout.Label(save.lastSaved.ToString());
        GUILayout.EndHorizontal();
    }
}
