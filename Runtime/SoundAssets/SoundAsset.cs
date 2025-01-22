using com.absence.soundsystem.internals;
using UnityEngine;

namespace com.absence.soundsystem
{
    public abstract class SoundAsset : ScriptableObject, ISoundAsset
    {
        internal void Preview(AudioSource source)
        {
            if (Application.isPlaying)
            {
                Debug.LogWarning("You can't preview sound assets while playing the game.");
                return;
            }

            Preview_Internal(source);
        }

        protected virtual void Preview_Internal(AudioSource source)
        {
            var data = GetData();
            if (data.Clip == null) return;

            source.SetupWithData(data);
            source.Play();
        }

        public abstract SoundData GetData();
    }

}