#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyConfig))]
public class EnemyConfigEditor : Editor
{
    public EnemyConfig card;

    void OnEnable()
    {
        card = (EnemyConfig) target;
    }

    public override void OnInspectorGUI()
    {
        //  var styleLabel = new GUIStyle { alignment = TextAnchor.MiddleCenter };
        //  styleLabel.normal.textColor = Color.gray;
        //  EditorGUILayout.LabelField("Create action", styleLabel);
        //      GUI.backgroundColor = Color.gray;
        //    GUI.contentColor = Color.gray;
        EditorGUILayout.Space();
        EditorGUILayout.BeginVertical("helpbox");
        EditorGUILayout.ObjectField("Icon", card.enemyIcon, typeof(Sprite), false);
        EditorGUILayout.EndVertical();

        GUI.backgroundColor = Color.white;
        GUI.contentColor = Color.white;
        EditorGUILayout.Space();        EditorGUILayout.Space();        EditorGUILayout.Space();
        base.OnInspectorGUI();
        if (GUI.changed) SetObjectDirty(card);
    }

    static void SetObjectDirty(Object obj)
    {
        EditorUtility.SetDirty(obj);
    }
}
#endif