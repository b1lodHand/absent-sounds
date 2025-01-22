using com.absence.soundsystem.internals;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.absence.soundsystem
{
    [CreateAssetMenu(menuName = "absencee_/absent-sounds/Sound Asset/Multi Sound Asset", fileName = "New Multi Sound")]
    public class MultiSoundAsset : SoundAsset
    {
        [SerializeField] private MultiSoundAssetDataSelectionType m_selectionType 
            = MultiSoundAssetDataSelectionType.Random;

        [SerializeField] private List<SoundData> m_dataList = new();

        public List<SoundData> DataList => m_dataList;

        public override SoundData GetData()
        {
            if (m_dataList.Count == 0) return null;

            switch (m_selectionType)
            {
                case MultiSoundAssetDataSelectionType.Random:
                    return GetData_Random();
                default:
                    return null;
            }
        }

        private SoundData GetData_Random()
        {
            return m_dataList.OrderBy(r => Random.insideUnitCircle.x).FirstOrDefault();
        }
    }

}