using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp.Common.D3DX;
using LeagueSharp.Common.UI.Theme;
using SharpDX;
using SharpDX.Direct3D9;

namespace LeagueSharp.Common.UI
{
    class GlobalMenu
    {

        private static readonly GlobalMenu _instance = new GlobalMenu();

        internal bool PpSpriteDrawnProtection;
        internal readonly Sprite PpSprite = new Sprite(D3Dx.m_pD3Ddev);

        private readonly List<Menu> menus = new List<Menu>(); 

        public static GlobalMenu Instance
        {
            get { return _instance; }
        }

        public ITheme Theme { get; set; }

        public Vector2 Position { get; set; }

        private GlobalMenu()
        {
            Theme = new DefaultTheme();
            Position = new Vector2(50, 50);
            Drawing.OnDraw += Drawing_OnDraw;
            Game.OnWndProc += Game_OnWndProc;
        }

        void Game_OnWndProc(WndEventArgs args)
        {
            var inputArgs = new InputEventArgs(args);
            foreach (Menu menu in menus)
            {
                menu.OnWndProc(inputArgs);
            }
        }

        private void Drawing_OnDraw(EventArgs args)
        {
            // => Check Protection
            if (!PpSpriteDrawnProtection)
            {
                // => Sprite Begin
                PpSprite.Begin(SpriteFlags.AlphaBlend); // => Enable AlphaBlend for transparency support.

                // => Enable Protection
                PpSpriteDrawnProtection = true;
            }

            foreach (Menu menu in menus)
            {
                // => Drawing
                menu.OnDraw();
            }

            // => Check Protection
            if (PpSpriteDrawnProtection)
            {
                // => Sprite End
                PpSprite.End();

                // => Disable Protection
                PpSpriteDrawnProtection = false;
            }
        }

        public void Add(Menu menu)
        {
            menu.Position = new Vector2(
                MenuSettings.UpperLeftCorner.X,
                MenuSettings.UpperLeftCorner.Y + MenuSettings.DefaultHeight * menus.Count);
            menus.Add(menu);
        }
    }
}
