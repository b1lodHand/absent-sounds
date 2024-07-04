using System.Collections;
using UnityEngine;

namespace com.absence.soundsystem.internals
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundInstance : MonoBehaviour
    {
        [SerializeField] internal SoundData m_soundData = null;
        [SerializeField] internal AudioSource m_audioSource = null;
        [SerializeField] internal Transform m_followingTarget = null;

        Coroutine m_playingCoroutine = null;

        public void ForceStop() => ForceStop_Internal(true);

        public SoundInstance AtPosition(Vector3 position)
        {
            transform.position = position;
            return this;
        }
        public SoundInstance WithRandomVolume()
        {
            m_audioSource.volume = Random.Range(0f, 1.0f);
            return this;
        }
        public SoundInstance WithRandomPitch()
        {
            m_audioSource.pitch = Random.Range(-3.0f, 3.0f);
            return this;
        }
        public SoundInstance WithFollowingTarget(Transform target)
        {
            m_followingTarget = target;
            return this;
        }

        private void Update()
        {
            if (m_soundData == null) return;
            if (m_followingTarget == null) return;

            transform.position = m_followingTarget.position;
        }

        internal void Initialize(SoundData audioData)
        {
            m_soundData = audioData;
            m_audioSource.SetupWithData(m_soundData);
        }
        internal void Play()
        {
            if (m_soundData.Clip == null)
            {
                ForceStop();
                return;
            }

            if (m_playingCoroutine != null) StopCoroutine(m_playingCoroutine);

            m_audioSource.Play();
            m_playingCoroutine = StartCoroutine(WaitUntilEnd());
        }

        internal void Initialize_Internal()
        {
            if (m_audioSource != null) return;

            if (gameObject.TryGetComponent(out AudioSource source)) m_audioSource = source;
            else m_audioSource = gameObject.AddComponent<AudioSource>();
        }
        internal void ForceStop_Internal(bool releaseToPool = true)
        {
            if (m_playingCoroutine != null)
            {
                StopCoroutine(m_playingCoroutine);
                m_playingCoroutine = null;
            }

            m_audioSource.Stop();
            if (releaseToPool) SoundManager.Instance.Release(this);
        }

        private IEnumerator WaitUntilEnd()
        {
            yield return new WaitWhile(() => m_audioSource.isPlaying);
            ForceStop();
        }

        internal void SetActive(bool active) => gameObject.SetActive(active);
        internal void Destroy()
        {
            m_soundData = null;
            Destroy(gameObject);
        }
    }

}