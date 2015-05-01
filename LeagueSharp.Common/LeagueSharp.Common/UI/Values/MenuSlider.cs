using System;
using System.Windows.Forms;
using LeagueSharp.Common.D3DX;
using SharpDX;

namespace LeagueSharp.Common.UI.Values
{
    public class MenuSlider : AMenuValue
    {
        public MenuSlider(int value, int minValue, int maxValue)
        {
            CurrentValue = value;
            Min = minValue;
            Max = maxValue;
        }

        public int CurrentValue { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }

        public override int Width
        {
            get { return 50; }
        }

        public override void Draw()
        {
            DrawSlider();
            DrawCursor();
        }

        public override void OnWndProc(InputEventArgs args)
        {
            // TODO
        }

        public void DrawSlider()
        {
            // TODO
        }

        public void DrawCursor()
        {
            var x = Container.Position.X;
            var y = Container.Position.Y;
            var width = Container.MenuWidth;
            var height = MenuSettings.DefaultHeight;
            var vec = new Vector2(Cursor.Position.X, Cursor.Position.Y);

            if (Utils.IsUnderRectangle(vec, x, y, width, height))
            {
                Primitive.g_pLine.DrawLine(vec.X, y, vec.X, y+height, 1, new ColorBGRA(255, 0, 0, 255));
            }
        }
    }
}