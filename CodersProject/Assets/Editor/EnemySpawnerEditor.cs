using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemySpawnerEditor : EditorWindow
{
    [MenuItem("Window/Spawner")]
    public static void ShowWindow()
    {
        GetWindow<EnemySpawnerEditor>("SpawnerEditor");
    }

    static int enemiesScrollbar;
    static int mobTypeToGenerate;

    static bool customizeEnemies;
    static bool cubeEnabled;
    static bool capsuleEnabled;
    static bool archerEnabled;

    public void OnGUI()
    {
        GUILayout.Label("Number of enemies to spawn");
        enemiesScrollbar = EditorGUILayout.IntSlider(enemiesScrollbar, 1, 5, null);

        GUIContent[] _dropContent = new GUIContent[3];
        _dropContent[0] = new GUIContent("Cube");
        _dropContent[1] = new GUIContent("Capsule");
        _dropContent[2] = new GUIContent("Archer");

        mobTypeToGenerate = EditorGUILayout.Popup(0, _dropContent);

        customizeEnemies = EditorGUILayout.BeginToggleGroup("Customize enemies", customizeEnemies);
        cubeEnabled = EditorGUILayout.Toggle("Cube", cubeEnabled);
        capsuleEnabled = EditorGUILayout.Toggle("Capsule", capsuleEnabled);
        archerEnabled = EditorGUILayout.Toggle("Archer", archerEnabled);
        EditorGUILayout.EndToggleGroup();

        if (GUILayout.Button("Spawn Enemies"))
        {
            foreach (GameObject _item in Selection.gameObjects)
            {
                if (_item.GetComponent<EnemySpawner>() == null)
                    continue;

                _item.GetComponent<EnemySpawner>().SetStartAiToSpawnVal(enemiesScrollbar);
                _item.GetComponent<EnemySpawner>().StartSpawn();
            }
        }

        if(GUILayout.Button("Destroy First Enemy"))
        {
            foreach (GameObject _item in Selection.gameObjects)
            {
                if (_item.GetComponent<EnemySpawner>() == null)
                    continue;

                _item.GetComponent<EnemySpawner>().DestroyAllSpawnedEnemies();
            }
        }
    }
}
