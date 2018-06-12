// Created by Teh Lemon
// Creates a panel in the Unity Editor that displays an image

// Image and material needs to be called "mascot" and placed in "Assets/Editor Default Resources/WakabaGames/Homu-Editor/"
// You can customize this by editing the constants below

// Your image file can be any type supported by Unity's texture importer
// The material isn't needed (though you'll get a warning when you first open the window) 
// but the default material does not support transparencies

using UnityEngine;
using UnityEditor;
using UnityEngine.Assertions;

namespace WakabaGames.Editor
{
    public class MascotPanel : EditorWindow
    {
        // Where you're storing the mascot image file. This path needs to be in Assets/Editor Default Resources
        // Don't forget the trailing slash
        const string IMAGE_PATH = "WakabaGames/Homu-Editor/Mascot/";
        // The filename of your mascot image file without the file extensions
        const string IMAGE_FILENAME = "mascot";
        // Same for the texture's material
        const string MATERIAL_PATH = "WakabaGames/Homu-Editor/Mascot/";
        const string MATERIAL_FILENAME = "mascot";

        // Holds the loaded image
        static Texture2D m_texture;
        // The material used to draw the image. Unity will use a default one if not specified but the default doesn't support transparencies.
        static Material m_mat;
        // All the supported file types by the Unity texture importer
        static string[] fileTypes = new string[] { "jpg", "png", "gif", "bmp", "exr", "hdr", "iff", "pict", "psd", "tga", "tiff" };

        // Adds the button to Unity Editor's Window menu
        [MenuItem ( "Window/Mascot")]
        static void Open()
        {
            EditorWindow.GetWindow<MascotPanel>("Mascot");
            
            // Load the mascot image from the "Assets/Editor Default Resources/WakabaGames/Homu-Editor/" folder
            // Try all the supported filetypes until the right one is found
            for (int i = 0; i < fileTypes.Length; i++)
            {
                m_texture = EditorGUIUtility.Load($"{IMAGE_PATH}{IMAGE_FILENAME}.{fileTypes[i]}") as Texture2D;
                if (m_texture != null)
                {
                    break;
                }
            }
            Assert.IsNotNull(m_texture, $"Mascot Panel: Editor Default Resources/{IMAGE_PATH}{IMAGE_FILENAME} image not found or is not a supported file type.");

            // Load the mascot material
            m_mat = EditorGUIUtility.Load($"{MATERIAL_PATH}{MATERIAL_FILENAME}.mat") as Material;
            Assert.IsNotNull(m_mat, $"Mascot Panel: Editor Default Resources/{MATERIAL_PATH}{MATERIAL_FILENAME}.mat material not found");
        }

        void OnGUI()
        {
            if (m_texture == null)
            {
                return;
            }

            // Draw the image
            EditorGUI.DrawPreviewTexture(new Rect(0, 0, position.width, position.height), m_texture, m_mat, ScaleMode.ScaleToFit);
        }
    }
}
