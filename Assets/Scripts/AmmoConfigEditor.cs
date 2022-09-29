#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AmmoConfig))]
public class AmmoConfigEditor : Editor
{
    public AmmoConfig script;
    Vector2 _scroll;

    void OnEnable()
    {
        script = (AmmoConfig) target;
    }

    public override void OnInspectorGUI()
    {
        var styleLabel = new GUIStyle
        {
            alignment = TextAnchor.MiddleCenter,
            normal =
            {
                textColor = Color.gray
            }
        };
        EditorGUILayout.LabelField("Create action", styleLabel);
        GUI.backgroundColor = Color.gray;
        GUI.contentColor = Color.gray;
        EditorGUILayout.Space();
        EditorGUILayout.BeginVertical("helpbox");
        EditorGUILayout.ObjectField("Icon", script.Icon, typeof(Sprite), false);
        EditorGUILayout.EndVertical();

        GUI.backgroundColor = Color.white;
        GUI.contentColor = Color.white;

        base.OnInspectorGUI();

        if (GUI.changed) SetObjectDirty(script);
    }

    static void SetObjectDirty(Object obj)
    {
        EditorUtility.SetDirty(obj);
    }
}

#endif