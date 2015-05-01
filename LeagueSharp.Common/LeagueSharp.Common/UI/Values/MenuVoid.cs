using SharpDX;

namespace LeagueSharp.Common.UI.Values
{
    public class MenuVoid : AMenuValue
    {
        public override int Width
        {
            get { return 0; }
        }

        public override void Draw()
        {
            //do nothing
        }


        public override void OnWndProc(InputEventArgs args)
        {
            //do nothing
        }
    }
}