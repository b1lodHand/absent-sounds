using com.absence.soundsystem.internals;

namespace com.absence.soundsystem
{
    /// <summary>
    /// The wrapper static class for the system.
    /// </summary>
    public static class Sound
    {
        /// <summary>
        /// Use to instantly play a sound without any custom settings.
        /// </summary>
        /// <param name="data">Sound data.</param>
        /// <param name="manager">If you want to use the singleton, leave as null.</param>
        public static void Play(SoundData data, SoundManager manager = null)
        {
            Create(data, manager).Play();
        }

        /// <summary>
        /// Use to instantly play a sound without any custom settings.
        /// </summary>
        /// <param name="asset">Sound asset to use.</param>
        /// <param name="manager">If you want to use the singleton, leave as null.</param>
        public static void Play(ISoundAsset asset, SoundManager manager = null)
        {
            SoundData data = asset.GetData();

            Play(data, manager);
        }

        /// <summary>
        /// Use to create a sound instance. It will not play directly 
        /// so that you can apply some settings (with a chain like method structure) 
        /// to it before playing. 
        /// To play it, call <see cref="SoundInstance.Play"/> method.
        /// </summary>
        /// <param name="data">Sound data.</param>
        /// <param name="manager">If you want to use the singleton, leave as null.</param>
        /// <returns>Returns the sound instance created. Press 'dot' to see the settings you can change.</returns>
        public static SoundInstance Create(SoundData data, SoundManager manager = null)
        {
            if (manager == null) manager = SoundManager.Instance;

            SoundInstance ai = manager.Get(data.IsFrequent);
            ai.Initialize(data);

            return ai;
        }

        /// <summary>
        /// Use to create a sound instance. It will not play directly 
        /// so that you can apply some settings (with a chain like method structure) 
        /// to it before playing. 
        /// To play it, call <see cref="SoundInstance.Play"/> method.
        /// </summary>
        /// <param name="asset">Sound asset to use.</param>
        /// <param name="manager">If you want to use the singleton, leave as null.</param>
        /// <returns>Returns the sound instance created. Press 'dot' to see the settings you can change.</returns>
        public static SoundInstance Create(ISoundAsset asset, SoundManager manager = null)
        {
            SoundData data = asset.GetData();

            return Create(data, manager);
        }
    }

}