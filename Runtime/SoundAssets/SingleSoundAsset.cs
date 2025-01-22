using UnityEngine;

namespace com.absence.soundsystem
{
    [CreateAssetMenu(menuName = "absencee_/absent-sounds/Sound Asset/Single Sound Asset", fileName = "New Single Sound")]
    public class SingleSoundAsset : SoundAsset
    {
        [SerializeField] private SoundData m_audioData = new();

        public override SoundData GetData()
        {
            return m_audioData;
        }
    }

}