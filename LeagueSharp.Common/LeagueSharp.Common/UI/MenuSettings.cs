using SharpDX;
using Color = System.Drawing.Color;

namespace LeagueSharp.Common.UI
{
    public class MenuSettings
    {
        private static readonly Vector2 Corner = new Vector2(50, 50);
        private static readonly Color Background = Color.FromArgb(200, Color.Black);

        public static Vector2 UpperLeftCorner
        {
            get { return Corner; }
        }

        public static int DefaultWidth
        {
            get { return 160 + 2 * HorizontalPadding; }
        }

        public static int DefaultHeight
        {
            get { return 30; }
        }

        public static Color BackgroundColor
        {
            get { return Background; }
        }

        public static Color ActiveBackgroundColor
        {
            get { return Color.DimGray; }
        }

        public static int HorizontalPadding
        {
            get { return 10; }
        }

        public static ColorBGRA DefaultTextColor
        {
            get { return new ColorBGRA(255, 255, 255, 255); }
        }
    }
}