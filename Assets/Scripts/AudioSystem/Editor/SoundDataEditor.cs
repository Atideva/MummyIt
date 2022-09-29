using AudioSystem;
using UnityEditor;
using UnityEngine;

namespace Audio.Editor
{
    [CustomEditor(typeof(AudioData), true)]
    public class SoundDataEditor : UnityEditor.Editor
    {

        [SerializeField]   AudioSource previewer;

        public void OnEnable()
        {
            previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
        }

        public void OnDisable()
        {
            DestroyImmediate(previewer.gameObject);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);

            if (GUILayout.Button("Preview"))
            {
                ((AudioData)target).EditorTest(previewer);
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}
