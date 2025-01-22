using System.Collections.Generic;

namespace com.absence.soundsystem
{
    /// <summary>
    /// Mark the classes that contain multiple sound datas with this interface.
    /// </summary>
    public interface IMultiSoundAsset
    {
        /// <summary>
        /// All the sound data this class has.
        /// </summary>
        List<SoundData> DataList { get; }
        /// <summary>
        /// Use to get data at an index from the data list.
        /// </summary>
        /// <param name="index">Index to use.</param>
        /// <returns>Returns null if something goes wrong, true otherwise.</returns>
        virtual SoundData GetDataAt(int index)
        {
            if (DataList.Count == 0) return null;
            if (index < 0) return null;
            if (index >= DataList.Count) return null;

            return DataList[index];
        }
    }
}
