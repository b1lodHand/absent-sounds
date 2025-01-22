using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace com.absence.soundsystem.internals
{
    /// <summary>
    /// The singleton class responsible for handling anything based on absent-sounds.
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("absencee_/absent-sounds/Sound Manager")]
    public class SoundManager : MonoBehaviour
    {
        internal const int DEFAULT_POOL_CAPACITY = 16;
        internal const int MAX_FREQ_COUNT = 16;
        internal const bool INSTANTIATE_AUTOMATICALLY = false;

        [SerializeField] internal bool m_useSingleton = true;
        [SerializeField] internal bool m_dontDestroyOnLoad = true;
        [SerializeField] internal int m_initialPoolCapacity = DEFAULT_POOL_CAPACITY;
        [SerializeField] private int m_maxFrequentInstances = MAX_FREQ_COUNT;
        [SerializeField] internal AudioSource m_prefab;

#if UNITY_EDITOR
        [SerializeField] internal int m_activeInstances = 0;
#endif

        internal IObjectPool<SoundInstance> m_pool;
        internal List<SoundInstance> m_frequentList;

        /// <summary>
        /// Max amount of frequent instances allowed to play at the same time. If this value gets exceeded, oldest playing frequent instance will be used instead of a new instance.
        /// </summary>
        public int MaxFrequentInstances
        {
            get
            {
                return m_maxFrequentInstances;
            }

            internal set
            {
                if (Application.isPlaying) throw new UnityException("You cannot change a property of SoundManager runtime!");

                m_maxFrequentInstances = value;
            }
        }

        #region Singleton
        private static SoundManager m_instance;
        public static SoundManager Instance => m_instance;
        #endregion

        private void Awake()
        {
            if (m_useSingleton) SetupSingleton();
            SetupPool();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void InstantiateSoundManager()
        {
#pragma warning disable CS0162 // Unreachable code detected
            if (!INSTANTIATE_AUTOMATICALLY) return;

            new GameObject("Sound Manager [absent-sounds]").AddComponent<SoundManager>();
#pragma warning restore CS0162 // Unreachable code detected
        }

        #region Pooling

        private void SetupPool()
        {
            m_pool = new ObjectPool<SoundInstance>(OnCreate, OnGet, OnRelease, PerformDestroy, true, DEFAULT_POOL_CAPACITY, 10000);
            m_frequentList = new();
        }

        private SoundInstance OnCreate()
        {
            SoundInstance ai = null;

            if (m_prefab == null) ai = new GameObject("Sound Instance").AddComponent<SoundInstance>();
            else ai = GameObject.Instantiate(m_prefab).gameObject.AddComponent<SoundInstance>();
            ai.transform.SetParent(SoundManager.Instance.transform);
            ai.transform.localPosition = Vector3.zero;
            ai.FindRealSource();

            return ai;
        }

        private void OnGet(SoundInstance instance)
        {
            instance.SetActive(true);

            m_activeInstances++;
        }

        private void OnRelease(SoundInstance instance)
        {
            instance.m_soundData = null;
            instance.m_followingTarget = null;
            instance.transform.localPosition = Vector3.zero;
            instance.SetActive(false);

            m_activeInstances--;
        }

        private void PerformDestroy(SoundInstance instance)
        {
            instance.Destroy();
        }

        #endregion

        private void SetupSingleton()
        {
            if (m_instance != null && m_instance != this)
            {
                Destroy(gameObject);
                return;
            }

            m_instance = this;
            if (m_dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
        }

        internal SoundInstance Get(bool isTargetDataFrequent)
        {
            if (!isTargetDataFrequent) return m_pool.Get();
            if (m_frequentList.Count < m_maxFrequentInstances)
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