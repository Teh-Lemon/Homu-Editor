// Adds extra hotkeys to the Unity Editor
// F5 to Play and Stop the game
// alt+D to deselect all gameobjects

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System;

namespace WakabaGames.Editor
{
    public class EditorShortCutKeys : ScriptableObject
    {
        // F5 to Play (and exit playmode)
        [MenuItem("Edit/Play _F5", false, 10001)] 
        static void PlayGame()
        {
            if (!Application.isPlaying)
            {
                // save before playing
                EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
            }
            EditorApplication.ExecuteMenuItem("Edit/Play");
        }

        // alt D to deselect gameobjects
        [MenuItem("Edit/Deselect All &d", false, 10001)]
        static void Deselect()
        {            
            Selection.activeGameObject = null;
        }
    }
}
#endif