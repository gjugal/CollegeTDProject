using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeScriptableObjects{

	[MenuItem("Assets/Create/New Soldier Properties")]
    public static void CreateMyAssest()
    {
        ScriptableSoldierProps asset = ScriptableObject.CreateInstance<ScriptableSoldierProps>();
        AssetDatabase.CreateAsset(asset, "Assets/Soldiers/NewSoldierProps.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
