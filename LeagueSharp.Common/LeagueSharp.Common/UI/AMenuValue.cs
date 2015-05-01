namespace LeagueSharp.Common.UI
{
    public abstract class AMenuValue
    {
        public AMenuComponent Container { get; set; }

        public abstract int Width { get; }
        public abstract void Draw();
        public abstract void OnWndProc(InputEventArgs args);
    }
}