using LeagueSharp.Common.D3DX;
using SharpDX;

namespace LeagueSharp.Common.UI.Values
{
    public class MenuBool : AMenuValue
    {
        public bool Bool { get; set; }

        public override int Width
        {
            get { return MenuSettings.DefaultHeight; }
        }

        public override void Draw()
        {
            // => Drawing colored box (data)
            var x = Container.Position.X + Container.MenuWidth - Width;
            var y = Container.Position.Y;
            var width = Width;
            var height = MenuSettings.DefaultHeight;
            var boxColor = (Bool) ? new ColorBGRA(0, 100, 0, 255) : new ColorBGRA(100, 0, 0, 255);

            // => Drawing colored box
            Primitive.g_pLine.DrawRectangle(x, y, width, height, boxColor);

            // => Drawing text (data)
            var text = (Bool) ? "ON" : "OFF";
            var rec = new Rectangle((int) x, (int) y, width, height);
            var sprite = GlobalMenu.Instance.PpSprite;
            const Primitive.CenteredTextFlags flags =
                Primitive.CenteredTextFlags.HorizontalCenter | Primitive.CenteredTextFlags.VerticalCenter;

            // => Drawing text
            D3Dx.m_font.DrawCenteredText(sprite, text, rec, flags, MenuSettings.DefaultTextColor);
        }

        public override void OnWndProc(InputEventArgs args)
        {
            var x = Container.Position.X + Container.MenuWidth - Width;
            var y = Container.Position.Y;
            var width = Width;
            var height = MenuSettings.DefaultHeight;

            if (Utils.IsUnderRectangle(args.Cursor, x, y, width, height) && args.Msg == WindowsMessages.WM_LBUTTONDOWN)
            {
                Bool = !Bool;
                args.Process = false;
            }
        }
    }
}