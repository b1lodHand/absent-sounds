using UnityEngine;

namespace com.absence.soundsystem
{
    /// <summary>
    /// A sound asset that holds a single sound data.
    /// </summary>
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