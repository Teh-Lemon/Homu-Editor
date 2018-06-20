// Created by Teh Lemon on 2018/06/09

// Creates new scripts based on your script templates
// Use by right-clicking your project view and navigating to Create/[MENU_ITEM_PATH]

// The following keywords can be used in the templates by default:
// It's easy to add more, check out the CreateScript function.
// #AUTHOR#, #DATE#, #NAMESPACE#, #FILENAME#

// Customize your author and namespace
// Set where you've saving your templates. These have to be in Assets/Editor Default Resources.
// Add a string for each of your templates and assign the template filename to them.
// Create the right click menus. Make sure they assign the template filename to WhichTemplateToMake and calls CreateEditorWindow().
// Customize CreateScript() as you see fit

using System.IO;
using UnityEditor;
using UnityEngine;

namespace WakabaGames.Editor
{
    public class ScriptGenerator : EditorWindow
    {
        //////
        #region Customize these
        const string AUTHOR = "Teh Lemon";
        const string NAMESPACE = "WakabaGames";

        // The path to your templates relative to the "Assets/Editor Default Resources/" folder        
        const string TEMPLATE_PATH = "WakabaGames/Homu-Editor/Script Templates/";

        // Your templates and their filenames. Don't forget the file extension.
        const string TEMPLATE_COMPONENT = "ComponentTemplate.txt";
        const string TEMPLATE_EMPTY = "EmptyTemplate.txt";

        // Generate the script contents and creates the file. Customize as you see fit.
        void CreateScript(string filename)
        {
            // Load the text from the template
            string scriptText = LoadTemplate(WhichTemplateToCreate);

            // Replace placeholders in the script text. Customize this as you wish
            scriptText = scriptText.Replace("#AUTHOR#", AUTHOR);
            scriptText = scriptText.Replace("#DATE#", System.DateTime.Today.ToString("yyyy/MM/dd"));
            scriptText = scriptText.Replace("#NAMESPACE#", NAMESPACE);
            scriptText = scriptText.Replace("#FILENAME#", filename);

            // I want my component templates to always be named  [the class] + "Wrapper"
            filename = (WhichTemplateToCreate == TEMPLATE_COMPONENT) ? $"{filename}Wrapper" : filename;

            // Create the script file
            WriteFile(filename, scriptText);
            // Remove this if you don't want Unity importing and recompiling immediately afterwards
            ImportAndReload();
        }

        // Create your right-click menus for each of your templates
        // The function names don't matter.
        [MenuItem("Assets/Create/Wakaba Games/Empty Script")]
        static void CreateEmptyScript()
        {
            WhichTemplateToCreate = TEMPLATE_EMPTY;
            ShowInputFilenameWindow();
        }
        
        [MenuItem("Assets/Create/Wakaba Games/Component Script")]
        static void CreateComponentScript()
        {
            WhichTemplateToCreate = TEMPLATE_COMPONENT;
            ShowInputFilenameWindow();
        }
        #endregion
        //////

        //////
        /// You can ignore all this
        #region Backend
        static string WhichTemplateToCreate;

        string LoadTemplate(string templateFilename)
        {
            // Add the trailing slash if the user forgets it
            string path = TEMPLATE_PATH.EndsWith("/") ? TEMPLATE_PATH : TEMPLATE_PATH + "/";
            // Load DefaultTemplate.txt from the folder...
            // "Editor Default Resources/TEMPLATE_PATH"
            TextAsset template = EditorGUIUtility.Load($"{path}{templateFilename}") as TextAsset;
            return template.text;
        }

        void WriteFile(string filename, string text)
        {
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
            using(StreamWriter sw = File.CreateText(Path.Combine(path, $"{filename}.cs")))
            {
                sw.Write(text);
            }
        }

        void ImportAndReload()
        {
            // Import scripts into Unity
            AssetDatabase.Refresh();
        }

        // Editor Window
        const string INPUT_LABEL = "Filename: ";
        string Filename = "NewScript";
        static bool Focused = false; // Used to focus on the text field
        static void ShowInputFilenameWindow()
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
            if (GUILayout.Button("Create") || KeebEnter)
            {
                CreateScript(Filename);
                Close();
            }
            if (GUILayout.Button("Cancel") || KeebEscape)
            {
                Close();
            }
        }

        bool KeebEnter
        {
            get { return Event.current.isKey && (Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter); }
        }
        bool KeebEscape
        {
            get { return Event.current.isKey && Event.current.keyCode == KeyCode.Escape; }
        }
        #endregion
        //////
    }
}