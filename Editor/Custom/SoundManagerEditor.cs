using com.absence.soundsystem.internals;
using UnityEditor;
using UnityEngine;

namespace com.absence.soundsystem.editor
{
    [CustomEditor(typeof(SoundManager))]
    public class SoundManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            SoundManager manager = (SoundManager)target;

            GUIStyle headerStyle = new GUIStyle(GUI.skin.label);
            headerStyle.fontSize= 18;
            headerStyle.margin.bottom = 10;
            headerStyle.alignment = TextAnchor.MiddleCenter;
            headerStyle.fontStyle = FontStyle.Bold;

            GUILayout.Label("Sound Manager", headerStyle);

            Undo.RecordObject(manager, "Sound Manager (Editor)");

            EditorGUI.BeginChangeCheck();

            if (Application.isPlaying) GUI.enabled = false;

            bool dontDestroyOnLoad = manager.m_dontDestroyOnLoad;

            GUIContent ddolContent = new GUIContent()
            {
                text = "Don't Destroy On Load",
                tooltip = "If true, this object will try to move itself to DontDestroyOnLoad scene on awake.",
            };

            dontDestroyOnLoad = EditorGUILayout.Toggle(ddolContent, dontDestroyOnLoad);

            int maxFrequents = manager.MaxFrequentInstances;
            maxFrequents = EditorGUILayout.IntField("Max Frequent Instances", maxFrequents);

            GUI.enabled = true;

            if (EditorGUI.EndChangeCheck())
            {
                manager.m_dontDestroyOnLoad = dontDestroyOnLoad;
                manager.MaxFrequentInstances = maxFrequents;
                EditorUtility.SetDirty(manager);
            }
        }
    }
}
