using com.absence.soundsystem.imported;
using UnityEngine;
using UnityEngine.Audio;

namespace com.absence.soundsystem
{
    /// <summary>
    /// The class that holds all the data needed for a sound to get played.
    /// </summary>
    [System.Serializable]
    public class SoundData
    {
        [SerializeField, Tooltip("Target clip.")] 
        private AudioClip m_clip = null;

        [SerializeField, Tooltip("Target mixer group.")] 
        private AudioMixerGroup m_mixerGroup = null;

        [SerializeField, Tooltip("If true, any sound instance created with this data will stay looped until you force-stop them.")] 
        private bool m_loop = false;

        [SerializeField, Tooltip("If true, any sound instance created with this data will marked as frequent.")] 
        private bool m_isFrequent = false;

        [SerializeField, Tooltip("The volume range which will be randomized between two values."), MinMaxSlider(0f, 1f)] 
        private Vector2 m_volume = new Vector2(1f, 1f);

        [SerializeField, Tooltip("The Pitch range which will be randomized between two values."), MinMaxSlider(-3f, 3f)] 
        private Vector2 m_pitch = new Vector2(1f, 1f);

        public AudioClip Clip => m_clip;
        public AudioMixerGroup TargetMixerGroup => m_mixerGroup;
        public bool Loop => m_loop;
        public bool IsFrequent => m_isFrequent;
        public float Volume => Random.Range(m_volume.x, m_volume.y);
        public float Pitch => Random.Range(m_pitch.x, m_pitch.y);

        public SoundData()
        {
            m_volume = new Vector2(1f, 1f);
            m_pitch = new Vector2(1f, 1f);
        }

        public SoundData(SoundData copyFrom)
        {
            m_clip = copyFrom.Clip;
            m_mixerGroup = copyFrom.TargetMixerGroup;
            m_isFrequent = copyFrom.IsFrequent;
            m_volume = copyFrom.m_volume;
            m_pitch = copyFrom.m_pitch;
        }

        public SoundData WithAudioClip(AudioClip clip)
        {
            SoundData copy = new SoundData(this);
            copy.m_clip = clip;

            return copy;
        }
    }

}