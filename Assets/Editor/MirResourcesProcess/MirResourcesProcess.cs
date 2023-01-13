using System.IO;
using UnityEditor;
using UnityEngine;


public class MirResourcesProcess : EditorWindow {
    [SerializeField]
    public static string mirDataPath = "";
    [SerializeField]
    public static string mirMapPath = "";

    [MenuItem("工具/处理mir2资源")]
    private static void ShowWindow() {
        var window = GetWindow<MirResourcesProcess>();
        window.titleContent = new GUIContent("处理资源");
        window.Show();
    }

    private void OnGUI() {
        // Data
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Data的资源路径",EditorStyles.boldLabel);
        mirDataPath = EditorGUILayout.TextField(mirDataPath);
        if(GUILayout.Button("浏览")){
            EditorApplication.delayCall += ()=>{
               mirDataPath= EditorUtility.OpenFolderPanel("请选择", mirDataPath, "");
            };
        }
        EditorGUILayout.EndHorizontal();

        // Map
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Map的资源路径",EditorStyles.boldLabel);
        mirMapPath = EditorGUILayout.TextField(mirMapPath);
        if(GUILayout.Button("浏览")){
            EditorApplication.delayCall += ()=>{
               mirMapPath= EditorUtility.OpenFolderPanel("请选择", mirMapPath, "");
            };
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("------------------");
        //导出地图资源
        if (GUILayout.Button("导出地图资源"))
        {
            EditorApplication.delayCall += exportMapRes;
        }
        if (GUILayout.Button("导出怪物动画资源"))
        {
            EditorApplication.delayCall += exportMonsterRes;
        }
        if (GUILayout.Button("导出NPC动画资源"))
        {
            EditorApplication.delayCall += exportNpcRes;
        }
    }

    void exportMapRes(){

    }
    void exportMonsterRes(){

    }
    void exportNpcRes(){

    }
}
