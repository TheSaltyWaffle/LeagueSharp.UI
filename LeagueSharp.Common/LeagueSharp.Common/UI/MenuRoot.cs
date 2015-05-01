using System;
using System.Diagnostics.CodeAnalysis;
using LeagueSharp.Common.D3DX;
using SharpDX;
using SharpDX.Direct3D9;

namespace LeagueSharp.Common.UI
{
    public class MenuRoot : Menu
    {

        public MenuRoot(string name, string displayName, string uniqueIdentifier = null)
            : base(name, displayName, uniqueIdentifier)
        {
           
        }

        public void Attach()
        {
            GlobalMenu.Instance.Add(this);
        }
    }
}