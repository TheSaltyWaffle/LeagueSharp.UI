using System;
using SharpDX;
using Color = System.Drawing.Color;

namespace LeagueSharp.Common.UI.Values
{
    public class MenuCircle : AMenuValue
    {
        public MenuCircle(Color color)
        {
            Color = color;
        }

        public bool Active { get; set; }
        public Color Color { get; set; }
        public int Radius { get; set; }

        public override int Width
        {
            get
            {
                //TODO: width of slider + width of color square + width of active button
                throw new NotImplementedException();
            }
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