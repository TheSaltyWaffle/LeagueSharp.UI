using System;
using SharpDX;

namespace LeagueSharp.Common.UI.Values
{
    public class MenuInputText : AMenuValue
    {
        public MenuInputText(string text)
        {
            Text = text;
        }

        public string Text { get; set; }

        public override int Width
        {
            get { return MenuSettings.DefaultHeight; }
        }

        public override void Draw()
        {
        }

        public override void OnWndProc(InputEventArgs args)
        {
        }
    }
}