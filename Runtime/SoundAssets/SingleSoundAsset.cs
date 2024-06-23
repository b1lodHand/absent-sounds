using com.absence.soundsystem.internals;
using UnityEngine;

namespace com.absence.soundsystem
{

    [CreateAssetMenu(menuName = "absencee_/absent-sounds/Sound Asset/Single Sound Asset", fileName = "New Single Sound")]
    public class SingleSoundAsset : SoundAsset
    {
        [SerializeField] private SoundData m_audioData = new();

        public override void Play(AudioSource source)
        {
            if (m_audioData.Clip == null) return;

            source.SetupWithData(m_audioData);

            source.Play();
        }

        public override SoundData GetData()
        {
            return m_audioData;
        }
    }

}