using System.Collections;
using UnityEngine;

namespace com.absence.soundsystem.internals
{
    /// <summary>
    /// The component responsible for playing sounds. Don't use it manually.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class SoundInstance : MonoBehaviour
    {
        [SerializeField] internal SoundData m_soundData = null;
        [SerializeField] internal AudioSource m_audioSource = null;
        [SerializeField] internal Transform m_followingTarget = null;

        Coroutine m_playingCoroutine = null;

        private void Update()
        {
            if (m_soundData == null) return;
            if (m_followingTarget == null) return;

            transform.position = m_followingTarget.position;
        }

        #region Public API

        /// <summary>
        /// Use to force-stop this instance.
        /// </summary>
        public void ForceStop() => ForceStop_Internal(true);
        /// <summary>
        /// Use to move this instance to a position. Be warned: <see cref="WithFollowingTarget(Transform)"/> function
        /// will override this setting. So don't use this and that together.
        /// </summary>
        /// <param name="position">Target position.</param>
        /// <returns>Returns the same sound instance.</returns>
        public SoundInstance AtPosition(Vector3 position)
        {
            transform.position = position;
            return this;
        }
        /// <summary>
        /// Use to randomize volume of the audio source this instance uses. This
        /// function will override the volume range you defined at the <see cref="SoundData.Volume"/>.
        /// </summary>
        /// <returns>Returns the same sound instance.</returns>
        public SoundInstance WithRandomVolume()
        {
            m_audioSource.volume = Random.Range(0f, 1.0f);
            return this;
        }
        /// <summary>
        /// Use to randomize pitch of the audio source this instance uses. This
        /// function will override the pitch range you defined at the <see cref="SoundData.Volume"/>.
        /// </summary>
        /// <returns>Returns the same sound instance.</returns>
        public SoundInstance WithRandomPitch()
        {
            m_audioSource.pitch = Random.Range(-3.0f, 3.0f);
            return this;
        }
        /// <summary>
        /// Use to make the instance follow a target starting the moment you call this function, until
        /// it stops playing.
        /// </summary>
        /// <param name="target">Target to follow.</param>
        /// <returns>Returns the same sound instance.</returns>
        public SoundInstance WithFollowingTarget(Transform target)
        {
            m_followingTarget = target;
            return this;
        }
        /// <summary>
        /// Use to play the instance.
        /// </summary>
        public void Play()
        {
            if (m_soundData.Clip == null)
            {
                ForceStop();
                return;
            }

            if (m_playingCoroutine != null) StopCoroutine(m_playingCoroutine);

            m_audioSource.Play();
            m_playingCoroutine = StartCoroutine(C_WaitUntilEnd());
        }

        #endregion

        #region Internal API

        internal void Initialize(SoundData soundData)
        {
            m_soundData = soundData;
            m_audioSource.SetupWithData(m_soundData);
        }
        internal void FindRealSource()
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
        internal void SetActive(bool active) => gameObject.SetActive(active);
        internal void Destroy()
        {
            m_soundData = null;
            Destroy(gameObject);
        }

        #endregion

        private IEnumerator C_WaitUntilEnd()
        {
            yield return new WaitWhile(() => (m_audioSource.isPlaying || m_audioSource.loop));
            ForceStop();
        }
    }

}