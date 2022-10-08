#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GunConfig))]
public class GunConfigEditor : Editor
{
    public GunConfig script;
    private Vector2 _scroll;

    void OnEnable()
    {
        script = (GunConfig) target;
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
        EditorGUILayout.ObjectField("Icon", script.Sprite, typeof(Sprite), false);
        EditorGUILayout.EndVertical();

        GUI.backgroundColor = Color.white;
        GUI.contentColor = Color.white;

        base.OnInspectorGUI();
        EditorGUILayout.Space();

        

        if (GUI.changed) SetObjectDirty(script);
    }

    static void SetObjectDirty(Object obj)
    {
        EditorUtility.SetDirty(obj);
    }
}

#endif