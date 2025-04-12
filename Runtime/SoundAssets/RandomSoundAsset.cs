using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.absence.soundsystem
{
    /// <summary>
    /// A sound asset that holds multiple sound datas and returns a random one when requested.
    /// </summary>
    [CreateAssetMenu(menuName = "absencee_/absent-sounds/Sound Asset/Random Sound Asset", fileName = "New Random Sound Asset")]
    public class RandomSoundAsset : SoundAsset, IMultiSoundAsset
    {
        [SerializeField] private List<SoundData> m_dataList = new();
        public List<SoundData> DataList => m_dataList;

        public override SoundData GetData()
        {
            if (m_dataList.Count == 0) return null;

            return m_dataList.OrderBy(r => Random.insideUnitCircle.x).FirstOrDefault();
        }
    }

}