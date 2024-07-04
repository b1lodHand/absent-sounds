using com.absence.soundsystem.imported;
using UnityEngine;
using UnityEngine.Audio;

namespace com.absence.soundsystem
{
    [System.Serializable]
    public class SoundData
    {
        [SerializeField] private AudioClip m_clip = null;
        [SerializeField] private AudioMixerGroup m_mixerGroup = null;
        [SerializeField] private bool m_loop = false;
        [SerializeField] private bool m_isFrequent = false;
        [SerializeField, MinMaxSlider(0f, 1f)] private Vector2 m_volume = new Vector2(1f, 1f);
        [SerializeField, MinMaxSlider(-3f, 3f)] private Vector2 m_pitch = new Vector2(1f, 1f);

        public AudioClip Clip => m_clip;
        public AudioMixerGroup TargetMixerGroup => m_mixerGroup;
        public bool Looping => m_loop;
        public bool IsFrequent => m_isFrequent;
        public float Volume => Random.Range(m_volume.x, m_volume.y);
        public float Pitch => Random.Range(m_pitch.x, m_pitch.y);

        public SoundData()
        {
            m_volume = new Vector2(1f, 1f);
            m_pitch = new Vector2(1f, 1f);
        }
    }

}