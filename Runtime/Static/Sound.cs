using com.absence.soundsystem.internals;

namespace com.absence.soundsystem
{
    public static class Sound
    {
        public static SoundInstance Play(SoundData data)
        {
            SoundInstance ai = SoundManager.Instance.Get(data.IsFrequent);
            ai.Initialize(data);
            ai.Play();

            return ai;
        }
    }

}