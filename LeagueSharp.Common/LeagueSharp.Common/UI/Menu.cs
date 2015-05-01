using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LeagueSharp.Common.D3DX;
using SharpDX;

namespace LeagueSharp.Common.UI
{
    public class Menu : AMenuComponent
    {
        private AMenuComponent[] _childrenCached;
        private bool _toggled;
        private bool _visible;
        private readonly IDictionary<string, AMenuComponent> _children = new Dictionary<string, AMenuComponent>();

        public Menu(string name, string displayName, string uniqueIdentifier = null)
            : base(name, displayName, uniqueIdentifier) {}

        public override AMenuComponent this[string name]
        {
            get
            {
                if (_children.ContainsKey(name))
                {
                    return _children[name];
                }
                throw new Exception("No Child with name +" + name + " is found.");
            }
        }

        public AMenuComponent[] Children
        {
            get { return _childrenCached ?? (_childrenCached = _children.Values.ToArray()); }
        }

        public override int Width
        {
            get
            {
                return D3Dx.m_font.MeasureText(GlobalMenu.Instance.PpSprite, DisplayName, 0).Width + MenuSettings.HorizontalPadding * 2;
            }
        }

        public bool Toggled
        {
            get { return _toggled; }
            set
            {
                _toggled = value;
                foreach (var child in Children)
                {
                    child.Visible = value;
                }
            }
        }

        public override bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        /// <summary>
        ///     Add a AMenuComponent to this Menu.
        /// </summary>
        /// <param name="component"></param>
        public void Add(AMenuComponent component)
        {
            component.Parent = this;
            _children[component.Name] = component;
            _childrenCached = null;
        }

        /// <summary>
        ///     Remove an existing AMenuComponent from this Menu.
        /// </summary>
        /// <param name="component"></param>
        public void Remove(AMenuComponent component)
        {
            if (_children.ContainsKey(component.Name))
            {
                component.Parent = null;
                _children.Remove(component.Name);
                _childrenCached = null;
            }
        }

        internal override void OnDraw()
        {
            if (!_visible)
            {
                return;
            }

            #region Main Draw

            // => Menu Color
            var color = new ColorBGRA(
                MenuSettings.BackgroundColor.R, MenuSettings.BackgroundColor.G, MenuSettings.BackgroundColor.B,
                MenuSettings.BackgroundColor.A);

            // => Draw Rectangle for parent only.
            if (Parent == null)
            {
                Primitive.g_pLine.DrawRectangle(Position.X, Position.Y, MenuWidth, MenuSettings.DefaultHeight, color);
            }

            // => Draw Outline
            if (Toggled)
            {
                Primitive.g_pLine.DrawRectangleOutline(
                    Position.X, Position.Y, MenuWidth, MenuSettings.DefaultHeight,
                    new ColorBGRA(0xff /*255*/, 0x69 /*105*/, 0xb4 /*180*/, 0xff /*255*/));

                // TODO: Replace with a colorchangeable value.
            }

            // => Draw Text
            var rec = new Rectangle(
                (int) (Position.X + MenuSettings.HorizontalPadding), (int) Position.Y,
                MenuWidth - 2 * MenuSettings.HorizontalPadding, MenuSettings.DefaultHeight);
            D3Dx.m_font.DrawCenteredText(
                GlobalMenu.Instance.PpSprite, DisplayName, rec,
                Primitive.CenteredTextFlags.HorizontalLeft | Primitive.CenteredTextFlags.VerticalCenter,
                new ColorBGRA(255, 255, 255, 255));
            D3Dx.m_font.DrawCenteredText(
                GlobalMenu.Instance.PpSprite, "\x3e\x3e", rec,
                Primitive.CenteredTextFlags.HorizontalRight | Primitive.CenteredTextFlags.VerticalCenter,
                new ColorBGRA(255, 255, 255, 255));

            #endregion

            #region Children Draw

            if (Children.Length == 0 || !Toggled)
            {
                return;
            }
            var blockHeight = MenuSettings.DefaultHeight * Children.FindAll(c => c.Visible).Count();
            var blockWidth = Children[0].MenuWidth;
            var blockX = Position.X + MenuWidth + ((Toggled) ? 1 : 0);

            Primitive.g_pLine.DrawRectangle(blockX, Position.Y, blockWidth, blockHeight, color);
            foreach (var t in Children)
            {
                t.OnDraw();
            }

            #endregion
        }

        public override T GetValue<T>(string name)
        {
            if (_children.ContainsKey(name))
            {
                return ((MenuItem<T>) _children[name]).Value;
            }
            throw new Exception("Could not find child with name " + name);
        }

        public override void OnWndProc(InputEventArgs args)
        {
            if (Parent == null)
            {
                if ((args.Msg == WindowsMessages.WM_KEYDOWN || args.Msg == WindowsMessages.WM_KEYUP) && args.SingleKey == (Keys)(Config.ShowMenuPressKey))
                {
                    Visible = args.Msg == WindowsMessages.WM_KEYDOWN;
                }

                if (args.Msg == WindowsMessages.WM_KEYUP && args.SingleKey == (Keys)(Config.ShowMenuToggleKey))
                {
                    Visible = !Visible;
                }
            }

            if (!Visible)
            {
                return;
            }

            if (Utils.IsUnderRectangle(args.Cursor, Position.X, Position.Y, MenuWidth, MenuSettings.DefaultHeight) &&
                args.Msg == WindowsMessages.WM_LBUTTONDOWN)
            {
                Toggled = !Toggled;
            }

            if (!Toggled)
            {
                return;
            }

            foreach (var child in Children.Where(c => c.Visible))
            {
                child.OnWndProc(args);
            }
        }
    }
}