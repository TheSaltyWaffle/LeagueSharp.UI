using System;
using LeagueSharp;
using LeagueSharp.Common;
using LeagueSharp.Common.UI;
using LeagueSharp.Common.UI.Values;
using Menu = LeagueSharp.Common.UI.Menu;

namespace TestAssembly
{
    public class Program
    {
        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        public static void Game_OnGameLoad(EventArgs args)
        {
            var root = new MenuRoot("GUITest", "LeagueSharp.UI Test", "TheSaltyWaffle_L33t");
            var b0 = new MenuItem<MenuBool>("bool_test", "Test bool #1")
            {
                Value = new MenuBool { Bool = true },
                Visible = true
            };
            var b1 = new MenuItem<MenuBool>("bool_test2", "Test bool #2")
            {
                Value = new MenuBool { Bool = false },
                Visible = true
            };
            var s0 = new MenuItem<MenuSlider>("slider_test", "Slider test #1")
            {
                Value = new MenuSlider(50, 0, 100),
                Visible = true
            };
            var sub = new Menu("menu_test", "Test Menu #1") { Visible = true };

            var b2 = new MenuItem<MenuBool>("bool_test", "Test bool #1")
            {
                Value = new MenuBool { Bool = true },
                Visible = true
            };
            var b3 = new MenuItem<MenuBool>("bool_test2", "Test bool #2")
            {
                Value = new MenuBool { Bool = false },
                Visible = true
            };

            sub.Add(b2);
            sub.Add(b3);
            
            
            root.Add(b0);
            root.Add(b1);
            root.Add(sub);
            root.Add(s0);

            root.Attach();

            var root2 = new MenuRoot("GUITest2", "LeagueSharp.UI Test", "TheSaltyWaffle_L33t");
            root2.Attach();

            /*dynamic droot = root;
            bool booltest = droot.bool2.Bool;
            Game.PrintChat(booltest.ToString());
            //Example usage rootmenu["testmenu"].GetValue<MenuBool>("bool1").Bool*/
        }
    }
}