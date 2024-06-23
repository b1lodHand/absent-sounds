using com.absence.soundsystem.internals;

namespace com.absence.soundsystem
{
    public static class Sound
    {
        public static void Play(SoundData data)
        {
            SoundInstance ai = SoundManager.Instance.Get(data.IsFrequent);
            ai.Initialize(data);
            ai.Play();
        }

        public static SoundInstance PlayWithReturn(SoundData data)
        {
            SoundInstance ai = SoundManager.Instance.Get(data.IsFrequent);
            ai.Initialize(data);
            ai.Play();

            return ai;
        }
    }

}