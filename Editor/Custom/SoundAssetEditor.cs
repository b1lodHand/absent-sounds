using UnityEditor;
using UnityEngine;

namespace com.absence.soundsystem.editor
{
    [CustomEditor(typeof(SoundAsset), true)]
    public class SoundAssetEditor : Editor
    {
        private AudioSource m_previewAudioSource;

        private void OnEnable()
        {
            m_previewAudioSource = EditorUtility.CreateGameObjectWithHideFlags("Sound Asset Audio Source [Editor]", HideFlags.HideAndDontSave).
                AddComponent<AudioSource>();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Preview"))
            {
                SoundAsset rsa = (SoundAsset)target;
                rsa.Play(m_previewAudioSource);
            }
        }

        private void OnDisable()
        {
            DestroyImmediate(m_previewAudioSource.gameObject);
        }
    }

}