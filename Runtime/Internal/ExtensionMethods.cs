using UnityEngine;

namespace com.absence.soundsystem.internals
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Use to setup this audio source with a sound data.
        /// </summary>
        /// <param name="source">Target audio source.</param>
        /// <param name="data">Data to use.</param>
        public static void SetupWithData(this AudioSource source, SoundData data)
        {
            source.clip = data.Clip;
            source.outputAudioMixerGroup = data.TargetMixerGroup;
            source.loop = data.Loop;
            source.volume = data.Volume;
            source.pitch = data.Pitch;
        }
    }

}