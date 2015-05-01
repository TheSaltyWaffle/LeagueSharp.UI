using System;
using SharpDX;
using Color = System.Drawing.Color;

namespace LeagueSharp.Common.UI.Values
{
    public class MenuColor : AMenuValue
    {
        public MenuColor(Color color)
        {
            Color = color;
        }

        public Color Color { get; set; }

        public override int Width
        {
            get { return MenuSettings.DefaultHeight; }
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }


        public override void OnWndProc(InputEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}