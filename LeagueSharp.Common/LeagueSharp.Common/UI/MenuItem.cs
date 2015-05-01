using System;
using LeagueSharp.Common.D3DX;
using SharpDX;

namespace LeagueSharp.Common.UI
{
    public abstract class MenuItem : AMenuComponent
    {
        protected MenuItem(string name, string displayName, string uniqueIdentifier = null)
            : base(name, displayName, uniqueIdentifier) {}

        public abstract object ValueAsObject { get; }
    }

    public class MenuItem<T> : MenuItem where T : AMenuValue
    {
        private T _value;

        public MenuItem(string name, string displayName) : base(name, displayName)
        {
            Value = MenuValueFactory.Create<T>();
        }

        public T Value
        {
            get { return _value; }
            set
            {
                _value = value;
                _value.Container = this;
            }
        }

        public override int Width
        {
            get
            {
                return D3Dx.m_font.MeasureText(GlobalMenu.Instance.PpSprite, DisplayName, 0).Width + Value.Width +
                       MenuSettings.HorizontalPadding * 5;
            }
        }

        public override bool Visible { get; set; }

        public override object ValueAsObject
        {
            get { return Value; }
        }

        internal override void OnDraw()
        {
            if (!Visible)
            {
                return;
            }

            // => Draw Display Name
            var rec = new Rectangle(
                (int)(Position.X + MenuSettings.HorizontalPadding), (int)Position.Y,
                MenuWidth - Value.Width - 2 * MenuSettings.HorizontalPadding, MenuSettings.DefaultHeight);
            D3Dx.m_font.DrawCenteredText(
                GlobalMenu.Instance.PpSprite, DisplayName, rec,
                Primitive.CenteredTextFlags.HorizontalLeft | Primitive.CenteredTextFlags.VerticalCenter,
                new ColorBGRA(255, 255, 255, 255));

            // => Draw the IMenuValue
            Value.Draw();

            // TODO: IsLast
            /*Primitive.g_pLine.DrawLine(
                Position.X + 1, Position.Y + MenuSettings.DefaultHeight, Position.X + 1 + MenuWidth,
                Position.Y + MenuSettings.DefaultHeight, 2, new ColorBGRA(0, 0, 0, 255));*/
        }

        public override void OnWndProc(InputEventArgs args)
        {
            // => Pass the keystroke
            Value.OnWndProc(args);
        }

        #region

        public override AMenuComponent this[string index]
        {
            get { throw new NotImplementedException("Cannot get child of a MenuItem"); }
        }

        public override T2 GetValue<T2>(string name)
        {
            throw new NotImplementedException("Cannot get child of a MenuItem");
        }

        #endregion
    }
}