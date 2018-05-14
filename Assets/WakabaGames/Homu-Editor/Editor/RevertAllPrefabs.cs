// A tool for the Unity Editor
// Reverts all the selected gameobjects back to their prefab settings
// How to use
// Select the gameobjects in the hierarchy/scene view
// Navigate to the Tools submenu at the top of the editor
// Click Revert to Prefab

using UnityEditor;
using UnityEngine;

namespace WakabaGames.Editor
{
    public class RevertAllPrefabsEditor
    {
        [MenuItem("Tools/Revert to Prefab")]
        static void Revert()
        {
            var selection = Selection.gameObjects;

            if (selection.Length > 0)
            {
                Undo.SetCurrentGroupName("Revert to Prefab");

                for (int i = 0; i < selection.Length; i++)
                {
                    Undo.RecordObject(selection[i], "Revert to Prefab");
                    PrefabUtility.RevertPrefabInstance(selection[i]);
                }
            }
            else
            {
                Debug.Log("Cannot revert to prefab - nothing selected");
                return;
            }
        }
    }
}