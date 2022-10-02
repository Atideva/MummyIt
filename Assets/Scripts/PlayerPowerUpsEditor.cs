#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerPowerUps))]
public class PlayerPowerUpsEditor : Editor
{
    public PlayerPowerUps script;
      Vector2 _scroll;

    void OnEnable()
    {
        script = (PlayerPowerUps) target;
    }

    public override void OnInspectorGUI()
    {
        // var styleLabel = new GUIStyle
        // {
        //     alignment = TextAnchor.MiddleCenter,
        //     normal =
        //     {
        //         textColor = Color.gray
        //     }
        // };
        // EditorGUILayout.LabelField("Create action", styleLabel);
        // GUI.backgroundColor = Color.gray;
        // GUI.contentColor = Color.gray;
        // EditorGUILayout.Space();
        // EditorGUILayout.BeginVertical("helpbox");
        // EditorGUILayout.ObjectField("Icon", script.Sprite, typeof(Sprite), false);
        // EditorGUILayout.EndVertical();
        //
        // GUI.backgroundColor = Color.white;
        // GUI.contentColor = Color.white;

        base.OnInspectorGUI();
        EditorGUILayout.Space();

        if (script.availablePowerUps.Count > 0)
        {
            _scroll = EditorGUILayout.BeginScrollView(_scroll, GUILayout.MaxHeight(300));
            for (var i = 0; i < script.availablePowerUps.Count; i++)
            {
                EditorGUILayout.BeginHorizontal("box");
                var nam = script.availablePowerUps[i] ? script.availablePowerUps[i].name : "None";
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