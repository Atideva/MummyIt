#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaveEnemiesConfig))]
public class WaveEnemiesConfigEditor : Editor
{
    public WaveEnemiesConfig script;
    private Vector2 _scroll;

    void OnEnable()
    {
        script = (WaveEnemiesConfig) target;
    }

    public override void OnInspectorGUI()
    {
        
        base.OnInspectorGUI();
        EditorGUILayout.Space();

        if (script.AmmoTypes.Count > 0)
        {
            _scroll = EditorGUILayout.BeginScrollView(_scroll, GUILayout.MaxHeight(300));
            for (var i = 0; i < script.AmmoTypes.Count; i++)
            {
                EditorGUILayout.BeginHorizontal("box");
                var nam = script.AmmoTypes[i] ? script.AmmoTypes[i].name : "None";
                EditorGUILayout.LabelField(nam);

                var chance = (int) (script.GetChance(i) * 100f);

                EditorGUILayout.LabelField(chance + "%");
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
        }


        if (GUI.changed) SetObjectDirty(script);
    }

    static void SetObjectDirty(Object obj)
    {
        EditorUtility.SetDirty(obj);
    }
}

#endif