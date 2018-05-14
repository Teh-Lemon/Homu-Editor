// Adds extra hotkeys to the Unity Editor
// F5 to Play and Stop the game
// alt+D to deselect all gameobjects
// F8 to screenshot

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
        [MenuItem("Edit/Run _F5")] // shortcut key F5 to Play (and exit playmode also)
        static void PlayGame()
        {
            if (!Application.isPlaying)
            {
                EditorSceneManager.SaveScene(SceneManager.GetActiveScene(), "", false); // optional: save before run
            }
            EditorApplication.ExecuteMenuItem("Edit/Play");
        }


        [MenuItem("Edit/Deselect All &d", false, -101)]
        static void Deselect()
        {
            Selection.activeGameObject = null;
        }

        [MenuItem("Edit/Take Screenshot _F8")]
        static void TakeScreenshot()
        {
            DateTime d = DateTime.Now;
            string game = Application.productName.Replace(" ", "_");
            string scene = SceneManager.GetActiveScene().name.Replace(" ", "_");
            string filename = $"{game}_{scene}_{d.Year}-{d.Month:d2}-{d.Day:d2}_{d.Hour:d2}-{d.Minute:d2}-{d.Second:d2}.png";
            string filePath = EditorPrefs.GetString("MelonScreenshotSavePathKey", "").Replace("\\","\\\\");            
            ScreenCapture.CaptureScreenshot(filePath + filename, 1);
            Debug.Log("Screenshot taken");
        }
    }

    public class MelonPreferences : MonoBehaviour
    {    
        // Have we loaded the prefs yet
        static bool prefsLoaded = false;

        // The Preferences
        public static string MelonScreenshotSavePath = "";

        // Add preferences section named "My Preferences" to the Preferences Window
        [PreferenceItem("Melon Core")]
        public static void PreferencesGUI()
        {
            // Load the preferences
            if ( !prefsLoaded )
            {
                MelonScreenshotSavePath = EditorPrefs.GetString("MelonScreenshotSavePathKey", "");
                prefsLoaded = true;
                //Debug.Log($"Loaded screenshot path as {MelonScreenshotSavePath}");
            }

            // Preferences GUI
            //GUI.Label("Leave blank to save in the project root folder");
            MelonScreenshotSavePath = EditorGUILayout.TextField("Screenshot Save Path", MelonScreenshotSavePath);

            // Save the preferences
            if ( GUI.changed )
            {
                EditorPrefs.SetString("MelonScreenshotSavePathKey", MelonScreenshotSavePath);
                //Debug.Log($"Melon settings saved. Screenshot path at {MelonScreenshotSavePath}.");
            }
        }
    }
}
#endif