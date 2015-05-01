using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX;

namespace LeagueSharp.Common.UI.Values
{
    public class MenuList<T> : AMenuValue
    {
        public MenuList(IList<T> list)
        {
            List = list;
            SelectedValue = list.First();
        }

        public IList<T> List { get; set; }
        public T SelectedValue { get; set; }

        public override int Width
        {
            get
            {
                //TODO calculate with of SelectedValue.ToString() + left and right arrows
                throw new NotImplementedException();
            }
        }

        public override void Draw()
        {
            //TODO draw SelectedValue.ToString()
            throw new NotImplementedException();
        }


        public override void OnWndProc(InputEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}