using UnityEditor;
using UnityEngine;

namespace com.absence.soundsystem.editor
{
    [CustomEditor(typeof(SoundAsset), true)]
    public class SoundAssetEditor : Editor
    {
        private AudioSource m_previewAudioSource;
        private bool m_loop;

        private void OnEnable()
        {
            m_previewAudioSource = EditorUtility.CreateGameObjectWithHideFlags("Sound Asset Audio Source [Editor]", HideFlags.HideAndDontSave).
                AddComponent<AudioSource>();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (m_loop) DrawStopButton();
            else DrawPreviewButton();

            return;

            void DrawStopButton()
            {
                if (GUILayout.Button("Stop"))
                {
                    SoundAsset rsa = (SoundAsset)target;
                    m_previewAudioSource.Stop();
                    m_loop = false;
                }
            }

            void DrawPreviewButton() 
            {
                if (Application.isPlaying) GUI.enabled = false;

                if (GUILayout.Button("Preview"))
                {
                    SoundAsset rsa = (SoundAsset)target;
                    rsa.Preview(m_previewAudioSource);
                    m_loop = m_previewAudioSource.loop;
                }

                GUI.enabled = true;
            }
        }

        private void OnDisable()
        {
            DestroyImmediate(m_previewAudioSource.gameObject);
        }
    }

}