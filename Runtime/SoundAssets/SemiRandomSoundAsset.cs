using System.Collections.Generic;
using UnityEngine;

namespace com.absence.soundsystem
{
    /// <summary>
    /// A sound asset that holds multiple audio clips and returns the sound preset specified in the inspector with a random clip.
    /// </summary>
    [CreateAssetMenu(menuName = "absencee_/absent-sounds/Sound Asset/Semi-Random Sound Asset", fileName = "New Semi-Random Sound Asset")]
    public class SemiRandomSoundAsset : SoundAsset
    {
        [SerializeField] private List<AudioClip> m_audioClips = new();
        [SerializeField] private SoundData m_soundData;

        public override SoundData GetData()
        {
            if (m_audioClips.Count == 0)
                return null;

            int random = Random.Range(0, m_audioClips.Count);
            AudioClip randomAudioClip = m_audioClips[random];

            return m_soundData.WithAudioClip(randomAudioClip);
        }
    }
}
