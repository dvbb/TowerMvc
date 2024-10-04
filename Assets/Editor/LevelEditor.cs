using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Plastic.Antlr3.Runtime.Debug;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    [HideInInspector]
    private Level level = null;
    private List<FileInfo> m_files = new List<FileInfo>();
    private int m_selectedIndex = 0;
    private LevelInfo levelinfo = new LevelInfo();

    private string selectedFileName;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        level = target as Level;

        // Init func
        EditorGUILayout.BeginHorizontal();
        int currentIndex = EditorGUILayout.Popup(m_selectedIndex, GetNames(m_files));
        if (currentIndex != m_selectedIndex)
        {
            m_selectedIndex = currentIndex;
            LoadLevel();
        }
        if (GUILayout.Button("Read List"))
        {
            LoadLevelFiles();
        }
        EditorGUILayout.EndHorizontal();

        // Editor holder
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Clear Holder"))
        {
            level.ClearHolder();
        }
        if (GUILayout.Button("Clear Path"))
        {
            level.ClearRoad();
        }
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Save Level"))
        {
            SaveLevel();
            EditorUtility.DisplayDialog("MapBuilder", "Save succeed", "Ok");
        }
    }

    private void LoadLevelFiles()
    {
        // Clear old data
        Clear();

        // Load
        m_files = Tools.GetLevelFiles();

        if (m_files.Count > 0)
        {
            m_selectedIndex = 0;
            LoadLevel();
        }
    }

    private void LoadLevel()
    {
        Debug.Log("load level");
        // Clear
        Clear();

        // Load
        FileInfo file = m_files[m_selectedIndex];

        Tools.ParseXml(file.FullName, ref levelinfo);
        selectedFileName = file.FullName;
        level.LoadLevel(levelinfo);
    }

    private void SaveLevel()
    {
        levelinfo.Path.Clear();
        levelinfo.Holder.Clear();

        foreach (var item in level.Path)
        {
            levelinfo.Path.Add(new Point(item.X, item.Y));
        }
        foreach (var item in level.Grids)
        {
            if (item.IsHolder)
                levelinfo.Holder.Add(new Point(item.X, item.Y));
        }

        Tools.SaveXml(selectedFileName, levelinfo);
    }

    private string[] GetNames(List<FileInfo> files)
    {
        List<string> names = new List<string>();
        foreach (FileInfo file in files)
        {
            names.Add(file.Name);
        }
        return names.ToArray();
    }

    private void Clear()
    {
    }
}
