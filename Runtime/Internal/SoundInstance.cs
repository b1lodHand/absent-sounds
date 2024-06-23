using System.Collections;
using UnityEngine;

namespace com.absence.soundsystem.internals
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundInstance : MonoBehaviour
    {
        [SerializeField] internal SoundData m_audioData = null;
        [SerializeField] internal AudioSource m_audioSource;

        Coroutine m_playingCoroutine = null;

        public void ForceStop() => ForceStop_Internal(true);

        internal void Initialize(SoundData audioData)
        {
            m_audioData = audioData;
            m_audioSource.SetupWithData(m_audioData);
        }
        internal void Play()
        {
            if (m_audioData.Clip == null)
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
            m_audioData = null;
            Destroy(gameObject);
        }
    }

}