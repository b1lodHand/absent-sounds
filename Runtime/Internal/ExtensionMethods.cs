using UnityEngine;

namespace com.absence.soundsystem.internals
{
    public static class ExtensionMethods
    {
        public static void SetupWithData(this AudioSource source, SoundData data)
        {
            source.clip = data.Clip;
            source.outputAudioMixerGroup = data.TargetMixerGroup;
            source.loop = data.Looping;
            source.volume = data.Volume;
            source.pitch = data.Pitch;
        }
    }

}