#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AmmoMagazine))]
public class AmmoMagazineEditor : Editor
{
    public AmmoMagazine script;
    private Vector2 _scroll;

    void OnEnable()
    {
        script = (AmmoMagazine) target;
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

                var chance = (int) (script.GetAmmoChance(i) * 100f);

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