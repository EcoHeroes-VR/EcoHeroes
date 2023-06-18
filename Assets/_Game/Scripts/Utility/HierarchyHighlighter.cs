#if UNITY_EDITOR
using UnityEngine;

namespace Scripts.Utility
{
    public class HierarchyHighlighter : MonoBehaviour
    {

        public static readonly Color DEFAULT_BACKGROUND_COLOR = new Color(0.76f, 0.76f, 0.76f, 1f);
        public static readonly Color DEFAULT_BACKGROUND_COLOR_INACTIVE = new Color(0.306f, 0.396f, 0.612f, 1f);
        public static readonly Color DEFAULT_TEXT_COLOR = Color.black;

        public HierarchyHighlighter() { }

        public HierarchyHighlighter(Color inBackgroundColor)
        {
            this.Background_Color = inBackgroundColor;
        }

        public HierarchyHighlighter(Color inBackgroundColor, Color inTextColor, FontStyle inFontStyle = FontStyle.Normal)
        {
            this.Background_Color = inBackgroundColor;
            this.Text_Color = inTextColor;
            this.TextStyle = inFontStyle;
        }


        [Header("Color Settings")]
        public Color Text_Color = DEFAULT_TEXT_COLOR;

        public FontStyle TextStyle = FontStyle.Normal;

        public Color Background_Color = DEFAULT_BACKGROUND_COLOR;

        [HideInInspector]
        public bool Custom_Inactive_Colors = false;
        [HideInInspector]
        public Color Text_Color_Inactive = DEFAULT_TEXT_COLOR;
        [HideInInspector]
        public FontStyle TextStyle_Inactive = FontStyle.Normal;
        [HideInInspector]
        public Color Background_Color_Inactive = DEFAULT_BACKGROUND_COLOR_INACTIVE;
    }
}
#endif
