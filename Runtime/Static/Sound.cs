using com.absence.soundsystem.internals;

namespace com.absence.soundsystem
{
    public static class Sound
    {
        public static void Play(SoundData data, SoundManager manager = null)
        {
            Create(data, manager).Play();
        }

        public static void Play(ISoundAsset asset, SoundManager manager = null)
        {
            SoundData data = asset.GetData();

            Play(data, manager);
        }

        public static SoundInstance Create(SoundData data, SoundManager manager = null)
        {
            if (manager == null) manager = SoundManager.Instance;

            SoundInstance ai = manager.Get(data.IsFrequent);
            ai.Initialize(data);

            return ai;
        }

        public static SoundInstance Create(ISoundAsset asset, SoundManager manager = null)
        {
            SoundData data = asset.GetData();

            return Create(data, manager);
        }
    }

}