// Originally taken from Unity's Book Of The Dead: Environment 1.0 asset
// When enabled this keeps the game view camera synced with the scene view
// Useful when testing visual effects that only show up in game view

// Modified so it sets the camera back to where it was before instead of just zero-ing it
// Enable in the Tools menu or by using the ctrl+T shortcut

using UnityEngine;
using UnityEditor;

namespace WakabaGames
{
    public static class GameViewTracker
    {
        [MenuItem(k_MenuName, true)]
        public static bool ToggleGameViewTrackingValidate()
        {
            Menu.SetChecked(k_MenuName, s_Enabled);
            return true;
        }

        [MenuItem(k_MenuName, priority = 1050)]
        public static void ToggleGameViewTracking()
        {
            SetEnabled(!s_Enabled);
        }

        static void SetEnabled(bool enabled)
        {
            if (enabled && !s_Enabled)
            {
                SceneView.onSceneGUIDelegate += sceneGUICallback;
                s_Enabled = true;

                // Modified code - Teh Lemon 2018/07/01
                originalPosition = Camera.main.transform.localPosition;
                originalRotation = Camera.main.transform.localRotation;
            }
            else if (!enabled && s_Enabled)
            {
                SceneView.onSceneGUIDelegate -= sceneGUICallback;
                s_Enabled = false;

                // Original Book of the Dead code
                //Camera.main.transform.localPosition = Vector3.zero;
                //Camera.main.transform.localEulerAngles = Vector3.zero;

                // Modified code - Teh Lemon 2018/07/01
                Camera.main.transform.localPosition = originalPosition;
                Camera.main.transform.localRotation = originalRotation;
            }
        }

        static void sceneGUICallback(SceneView s)
        {
            if (Camera.main == null)
                return;

            if (!s.camera.orthographic)
            {
                Camera.main.transform.SetPositionAndRotation(s.camera.transform.position - 0.1f * s.camera.transform.forward, s.camera.transform.rotation);
            }
        }

        static bool s_Enabled;
        const string k_MenuName = "Tools/Toggle GameView tracking %T";
        // Modified code
        static Vector3 originalPosition = Vector3.zero;
        static Quaternion originalRotation = Quaternion.identity;
    }
}