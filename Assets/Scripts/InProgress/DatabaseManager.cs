using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Database))]
public class DatabaseManager : Editor
{
    private Database database;

    private void OnEnable()
    {
        database = target as Database;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Add Achievements"))
        {
            AddAchievements();
        }
    }

    void AddAchievements()
    {
        string file = Path.Combine(Application.dataPath, "AchievementsList.cs");
        string code = "public enum AchievementsEnum {";
        foreach (Achievements achievements in database.achievementlist)
        {
            code += achievements.id + ",";
        }
        code += "}";
        File.WriteAllText(file, code);
        AssetDatabase.ImportAsset("Assets/AchievementList.cs");
    }
}
