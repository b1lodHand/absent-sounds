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
            serializedObject.Update();

            GUIStyle headerStyle = new GUIStyle(GUI.skin.label);
            headerStyle.fontSize= 18;
            headerStyle.margin.bottom = 10;
            headerStyle.alignment = TextAnchor.MiddleCenter;
            headerStyle.fontStyle = FontStyle.Bold;

            GUILayout.Label("Sound Manager", headerStyle);

            EditorGUI.BeginChangeCheck();

            if (Application.isPlaying) GUI.enabled = false;

            bool useSingleton = manager.m_useSingleton;
            bool dontDestroyOnLoad = manager.m_dontDestroyOnLoad;
            int maxFrequents = manager.MaxFrequentInstances;
            AudioSource prefab = manager.m_prefab;

            DrawPrefabField();
            DrawFreqField();
            DrawSingletonToggle();
            DrawDDOLToggle();

            if (Application.isPlaying) GUI.enabled = true;

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(manager, "Sound Manager (Editor)");

                manager.m_useSingleton = useSingleton;
                manager.m_dontDestroyOnLoad = dontDestroyOnLoad;
                manager.MaxFrequentInstances = maxFrequents;
                manager.m_prefab = prefab;

                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(manager);
            }

            return;

            void DrawSingletonToggle()
            {
                GUIContent content = new GUIContent()
                {
                    text = "Use Singleton",
                    tooltip = "If true, this SoundManager will try to be the singleton.",
                };

                useSingleton = EditorGUILayout.Toggle(content, useSingleton);
            }

            void DrawDDOLToggle()
            {
                GUIContent content = new GUIContent()
                {
                    text = "Don't Destroy On Load",
                    tooltip = "If true, this object will try to move itself to DontDestroyOnLoad scene on awake.",
                };

                if (!useSingleton)
                {
                    GUI.enabled = false;
                    dontDestroyOnLoad = false;
                }
                dontDestroyOnLoad = EditorGUILayout.Toggle(content, dontDestroyOnLoad);
                if (!useSingleton) GUI.enabled = true;
            }

            void DrawFreqField()
            {
                GUIContent content = new GUIContent()
                {
                    text = "Max Frequent Instances",
                    tooltip = "Max amount of frequent instances allowed to play at the same time. If this value gets exceeded, oldest playing frequent instance will be used instead of a new instance.",
                };

                maxFrequents = EditorGUILayout.IntField(content, maxFrequents);
            }

            void DrawPrefabField()
            {
                GUIContent content = new GUIContent()
                {
                    text = "Prefab Override",
                    tooltip = "The audio source to copy settings from. Leave empty if you have no use of it.",
                };

                prefab = EditorGUILayout.ObjectField(content, prefab, typeof(AudioSource), allowSceneObjects: false)
                    as AudioSource;
            }
        }
    }
}
