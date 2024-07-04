using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace com.absence.soundsystem.internals
{
    internal class SoundManager : MonoBehaviour
    {
        internal const int DEFAULT_POOL_CAPACITY = 8;
        internal const int MAX_FREQ_COUNT = 16;
        internal static readonly bool INSTANTIATE_AUTOMATICALLY = true;

        #region Singleton
        private static SoundManager m_instance;
        public static SoundManager Instance => m_instance;

        private void SetupSingleton()
        {
            if (m_instance != null && m_instance != this)
            {
                Destroy(gameObject);
                return;
            }

            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion

        private void Awake()
        {
            SetupSingleton();
            SetupPool();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void InstantiateSoundManager()
        {
            if (!INSTANTIATE_AUTOMATICALLY) return;

            new GameObject("Audio Manager [absent-audio]").AddComponent<SoundManager>();
        }

        #region Pooling
        private IObjectPool<SoundInstance> m_pool;
        private List<SoundInstance> m_frequentList;

        private void SetupPool()
        {
            m_pool = new ObjectPool<SoundInstance>(OnCreate, OnGet, OnRelease, PerformDestroy, true, DEFAULT_POOL_CAPACITY, 10000);
            m_frequentList = new();
        }

        private SoundInstance OnCreate()
        {
            SoundInstance ai = new GameObject("Audio Instance").AddComponent<SoundInstance>();
            ai.transform.SetParent(SoundManager.Instance.transform);
            ai.Initialize_Internal();

            return ai;
        }

        private void OnGet(SoundInstance instance)
        {
            instance.SetActive(true);
        }

        private void OnRelease(SoundInstance instance)
        {
            instance.m_soundData = null;
            instance.SetActive(false);
        }

        private void PerformDestroy(SoundInstance instance)
        {
            instance.Destroy();
        }
        #endregion

        internal SoundInstance Get(bool isTargetDataFrequent)
        {
            if (!isTargetDataFrequent) return m_pool.Get();
            if (m_frequentList.Count < MAX_FREQ_COUNT)
            {
                SoundInstance ai1 = m_pool.Get();
                m_frequentList.Add(ai1);
                return ai1;
            }

            SoundInstance ai2 = m_frequentList.FirstOrDefault();
            ai2.ForceStop_Internal(false);
            m_frequentList.Remove(ai2);
            m_frequentList.Add(ai2);

            return ai2;
        }

        internal void Release(SoundInstance instance)
        {
            if (instance.m_soundData.IsFrequent) m_frequentList.Remove(instance);
            m_pool.Release(instance);
        }
    }

}