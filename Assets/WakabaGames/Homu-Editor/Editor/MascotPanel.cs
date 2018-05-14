// Creates a panel in the Unity Editor that displays an image

// Image needs to be called "mascot" and placed in "Assets/Editor Default Resources/WakabaGames/Homu-Editor/"
// Image needs to be png, jpg or gif. You can add more below if you need them but it has to be a Unity supported format.

// Material needs to be called "mascot" and placed in "Assets/Editor Default Resources/WakabaGames/Homu-Editor/"
// The material isn't needed (though you'll get a warning when you first open the window) 
// but the default material does not support transparencies

using UnityEngine;
using UnityEditor;
using UnityEngine.Assertions;

namespace WakabaGames.Editor
{
    public class MascotPanel : EditorWindow
    {
        static Texture2D m_texture;
        static Material m_mat;

        [MenuItem ( "Window/Mascot ")]
        static void Open()
        {
            EditorWindow.GetWindow<MascotPanel>("Mascot");
            
            // Load the mascot image from the "Assets/Editor Default Resources/WakabaGames/Homu-Editor/" folder
            m_texture = EditorGUIUtility.Load("WakabaGames/Homu-Editor/mascot.jpg") as Texture2D;
            // try png if jpg isn't found
            if (m_texture == null)
            {
                m_texture = EditorGUIUtility.Load("WakabaGames/Homu-Editor/mascot.png") as Texture2D;
            }
            // try gif if png isn't found either
            if ( m_texture == null )
            {
                m_texture = EditorGUIUtility.Load("WakabaGames/Homu-Editor/mascot.gif") as Texture2D;
            }

            Assert.IsNotNull(m_texture, "Mascot Panel: Editor Default Resources/WakabaGames/Homu-Editor/mascot.jpg/png/gif image not found");

            // Load the mascot material
            m_mat = EditorGUIUtility.Load("WakabaGames/Homu-Editor/mascot.mat") as Material;
            Assert.IsNotNull(m_mat, "Mascot Panel: Editor Default Resources/WakabaGames/Homu-Editor/mascot.mat material not found");
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
