using com.absence.soundsystem.internals;
using System.Collections.Generic;
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
            int poolCapacity = manager.m_initialPoolCapacity;
            int maxFrequents = manager.MaxFrequentInstances;
            AudioSource prefab = manager.m_prefab;

            DrawPrefabField();
            DrawPoolCapacityField();
            DrawFreqField();
            DrawSingletonToggle();
            DrawDDOLToggle();

            if (Application.isPlaying) GUI.enabled = true;

            EditorGUILayout.Space();

            if (Application.isPlaying) DrawInfo();
            else
            {
                EditorGUILayout.HelpBox("You need to start the game to see any runtime data.",
                    MessageType.Info);
            }

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(manager, "Sound Manager (Editor)");

                manager.m_useSingleton = useSingleton;
                manager.m_dontDestroyOnLoad = dontDestroyOnLoad;
                manager.MaxFrequentInstances = maxFrequents;
                manager.m_prefab = prefab;
                manager.m_initialPoolCapacity = poolCapacity;

                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(manager);
            }

            return;

            void DrawPoolCapacityField()
            {
                GUIContent content = new GUIContent()
                {
                    text = "Initial Pool Capacity",
                    tooltip = "This is the capacity that will be used when creating the pool.",
                };

                poolCapacity = EditorGUILayout.IntField(content, poolCapacity);
                if (poolCapacity < 0) poolCapacity = 0;
            }

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

                dontDestroyOnLoad = EditorGUILayout.Toggle(content, dontDestroyOnLoad);
            }

            void DrawFreqField()
            {
                GUIContent content = new GUIContent()
                {
                    text = "Max Frequent Instances",
                    tooltip = "Max amount of frequent instances allowed to play at the same time. If this value gets exceeded, oldest playing frequent instance will be used instead of a new instance.",
                };

                maxFrequents = EditorGUILayout.IntField(content, maxFrequents);
                if (maxFrequents < 0) maxFrequents = 0;
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

            void DrawInfo()
            {
                GUI.enabled = false;

                GUIStyle style = new(GUI.skin.label);
                style.richText = true;

                int totalInstances = manager.m_activeInstances;
                int freqInstances = 0;
                
                if (manager.m_frequentList != null)
                    freqInstances = manager.m_frequentList.Count;

                GUIContent content = new GUIContent()
                {
                    text = $"<i>Used Instances (Total): {totalInstances}</i>",
                };

                GUIContent content2 = new GUIContent()
                {
                    text = $"<i>Used Frequent Instances: {freqInstances}</i>",
                };

                EditorGUILayout.LabelField(content, style);
                EditorGUILayout.LabelField(content2, style);

                GUI.enabled = true;
            }
        }
    }
}
