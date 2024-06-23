using UnityEngine;

namespace com.absence.soundsystem
{
    public abstract class SoundAsset : ScriptableObject
    {
        public abstract void Play(AudioSource source);
        public abstract SoundData GetData();
    }

}