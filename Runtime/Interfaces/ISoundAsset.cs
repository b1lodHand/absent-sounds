namespace com.absence.soundsystem
{
    /// <summary>
    /// Mark classes that can return a sound data with this interface.
    /// </summary>
    public interface ISoundAsset
    {
        SoundData GetData();
    }
}