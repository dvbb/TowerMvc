using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Plastic.Antlr3.Runtime.Debug;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Map))]
public class MapEditor : Editor
{
    [HideInInspector]
    private Map map = null;
    private List<FileInfo> m_files = new List<FileInfo>();
    private int m_selectedIndex = 0;
    private Level level = new Level();

    private string selectedFileName;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        map = target as Map;

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
            map.ClearHolder();
        }
        if (GUILayout.Button("Clear Path"))
        {
            map.ClearRoad();
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

        Tools.ParseXml(file.FullName, ref level);
        selectedFileName = file.FullName;
        map.LoadLevel(level);
    }

    private void SaveLevel()
    {
        level.Path.Clear();
        level.Holder.Clear();

        foreach (var item in map.Path)
        {
            level.Path.Add(new Point(item.X, item.Y));
        }
        foreach (var item in map.Grids)
        {
            if (item.IsHolder)
                level.Holder.Add(new Point(item.X, item.Y));
        }

        Tools.SaveXml(selectedFileName, level);
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
