using com.absence.soundsystem.internals;
using UnityEditor;
using UnityEngine;

namespace com.absence.soundsystem.editor
{
    public static class EditorJobsHelper
    {
        [MenuItem("GameObject/absencee_/absent-sounds/Sound Manager", priority = 0)]
        static void CreateSoundManager_MenuItem()
        {
            GameObject obj = new GameObject("Sound Manager");
            obj.AddComponent<SoundManager>();

            Undo.RegisterCreatedObjectUndo(obj, "Sound Manager Created via MenuItem");

            Selection.activeGameObject = obj;
        }
    }
}
