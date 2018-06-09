// Created by Teh Lemon on 2018/06/09
// Creates new scripts based on the script template found in...
// "Assets/Editor Default Resources/WakabaGames/Homu-Editor/Script Templates/ScriptTemplate.txt"
// Use by right-clicking your project view and navigating to Create/WakabaGames/New Script
// The following keywords can be used in the templates:
// #AUTHOR#, #DATE#, #NAMESPACE_OPEN#, #NAMESPACE_END#, #FILENAME#
// You can customize the author and namespace by editing the constants below
// Maybe don't commit the template to the repo if working in a team

using UnityEngine;
using UnityEditor;
using System.IO;

namespace WakabaGames
{
    public class ScriptGenerator : EditorWindow
    {
        // Customize these
        const string AUTHOR = "Teh Lemon";
        const string NAMESPACE = "WakabaGames";

        // Editor Window
        const string INPUT_LABEL = "Filename: ";
        string Filename = "NewScript";
        static bool Focused = false; // Used to focus on the text field

        // Show the filename user prompt window
        [MenuItem("Assets/Create/Wakaba Games/New Script")]
        static void ShowCreateScriptWindow()
        {
            ScriptGenerator window = ScriptableObject.CreateInstance(typeof(ScriptGenerator)) as ScriptGenerator;

            window.minSize = new Vector2(225, 65);
            window.maxSize = new Vector2(1000, 65);            
            Focused = false;

            window.ShowUtility();            
        }

        // Draw the window the the menu item is clicked
        void OnGUI()
        {
            // The text field
            GUI.SetNextControlName("InputField");
            EditorGUIUtility.labelWidth = 65;
            Filename = EditorGUILayout.TextField(INPUT_LABEL, Filename);

            // Focus the text field when the window is first opened
            if (!Focused)
            {
                EditorGUI.FocusTextInControl("InputField");
                Focused = true;
            }

            // Buttons
            if (GUILayout.Button("Create"))
            {   
                CreateScript(Filename);
                Close();
            }
            if (GUILayout.Button("Cancel"))
            {
                Close();
            }

            // Keyboard enter
            if (Event.current.isKey && (Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter))
            {
                CreateScript(Filename);
                Close();
            }
        }

        // Create the file
        void CreateScript(string filename)
        {            
            // Load DefaultTemplate.txt from the folder...
            // "Editor Default Resources/WakabaGames/Homu-Editor/Script Templates"
            TextAsset script = EditorGUIUtility.Load("WakabaGames/Homu-Editor/Script Templates/ScriptTemplate.txt") as TextAsset;
            string newScript = script.text;

            // Replace placeholders in the script text
            newScript = newScript.Replace("#AUTHOR#", AUTHOR);            
            newScript = newScript.Replace("#DATE#", System.DateTime.Today.ToString("yyyy/MM/dd"));
            newScript = newScript.Replace("#FILENAME#", filename);
            if (NAMESPACE.Length > 0)
            {
                newScript = newScript.Replace("#NAMESPACE_OPEN#", $"namespace {NAMESPACE}\n{{");
                newScript = newScript.Replace("#NAMESPACE_END#", "}");
            }
            else
            {
                newScript = newScript.Replace("#NAMESPACE_OPEN#", "");
                newScript = newScript.Replace("#NAMESPACE_END#", "");
            }

            // Where to create the file
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
            }
            if (string.IsNullOrEmpty(path))
            {
                path = "Assets/";
            }

            // Create the file
            using (StreamWriter sw = File.CreateText(Path.Combine(path, $"{filename}.cs")))
            {
                sw.Write(newScript);
            }

            // Import scripts into Unity
            AssetDatabase.Refresh();
        }
    }
}