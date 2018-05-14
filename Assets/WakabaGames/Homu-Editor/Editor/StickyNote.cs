using UnityEngine;

namespace WakabaGames.Editor
{
    public class StickyNote : MonoBehaviour
    {
        public enum Colors
        {
            White, Red, Yellow, Green, Blue, Purple, Pink
        }

        public Colors GizmoColor;
        public string Author;
        [TextArea(3, 30)]
        public string Text;

        void OnDrawGizmos()
        {
            switch ( GizmoColor )
            {
                case Colors.White:
                    Gizmos.DrawIcon(transform.position, "WakabaGames/Homu-Editor/StickyNotes/stickynote_white.png");
                    break;
                case Colors.Red:
                    Gizmos.DrawIcon(transform.position, "WakabaGames/Homu-Editor/StickyNotes/stickynote_red.png");
                    break;
                case Colors.Yellow:
                    Gizmos.DrawIcon(transform.position, "WakabaGames/Homu-Editor/StickyNotes/stickynote_yellow.png");
                    break;
                case Colors.Green:
                    Gizmos.DrawIcon(transform.position, "WakabaGames/Homu-Editor/StickyNotes/stickynote_green.png");
                    break;
                case Colors.Blue:
                    Gizmos.DrawIcon(transform.position, "WakabaGames/Homu-Editor/StickyNotes/stickynote_blue.png");
                    break;
                case Colors.Purple:
                    Gizmos.DrawIcon(transform.position, "WakabaGames/Homu-Editor/StickyNotes/stickynote_purple.png");
                    break;
                case Colors.Pink:
                    Gizmos.DrawIcon(transform.position, "WakabaGames/Homu-Editor/StickyNotes/stickynote_pink.png");
                    break;

                default:
                    Gizmos.DrawCube(transform.position, Vector3.one);
                    break;
            }
        }
    }
}