using com.absence.soundsystem.internals;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.absence.soundsystem
{
    [CreateAssetMenu(menuName = "absencee_/absent-sounds/Sound Asset/Multi Sound Asset", fileName = "New Multi Sound")]
    public class MultiSoundAsset : SoundAsset
    {
        [SerializeField] private List<SoundData> m_dataList = new();
        public List<SoundData> DataList => m_dataList;

        public override void Play(AudioSource source)
        {
            if (m_dataList.Count == 0) return;

            var data = GetData();
            if (data.Clip == null) return;

            source.SetupWithData(data);

            source.Play();
        }

        public override SoundData GetData()
        {
            if (m_dataList.Count == 0) return null;

            return m_dataList.OrderBy(r => Random.insideUnitCircle.x).FirstOrDefault();
        }
    }

}