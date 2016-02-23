using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeScriptableObjects{

	[MenuItem("Assets/Create/New Soldier Properties")]
    public static void CreateMyAssest()
    {
        SoldierProps asset = ScriptableObject.CreateInstance<SoldierProps>();
        AssetDatabase.CreateAsset(asset, "Assets/Soldiers/NewSoldierProps.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
