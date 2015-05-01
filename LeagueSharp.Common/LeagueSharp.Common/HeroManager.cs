﻿#region LICENSE

/*
 Copyright 2014 - 2015 LeagueSharp
 HeroManager.cs is part of LeagueSharp.Common.
 
 LeagueSharp.Common is free software: you can redistribute it and/or modify
 it under the terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 (at your option) any later version.
 
 LeagueSharp.Common is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 GNU General Public License for more details.
 
 You should have received a copy of the GNU General Public License
 along with LeagueSharp.Common. If not, see <http://www.gnu.org/licenses/>.
*/

#endregion

#region

using System;
using System.Collections.Generic;
using System.Linq;
using LeagueSharp;

#endregion

namespace LeagueSharp.Common
{
    public class HeroManager
    {
        /// <summary>
        ///     A list containing all heroes in the current match
        /// </summary>
        public static List<Obj_AI_Hero> AllHeroes { get; private set; }
        /// <summary>
        ///     A list containing only ally heroes in the current match
        /// </summary>
        public static List<Obj_AI_Hero> Allies { get; private set; }
        /// <summary>
        ///     A list containing only enemy heroes in the current match
        /// </summary>
        public static List<Obj_AI_Hero> Enemies { get; private set; }

        static HeroManager()
        {
            if (Game.Mode == GameMode.Running)
            {
                Game_OnStart(new EventArgs());
            }
            Game.OnStart += Game_OnStart;
        }

        static void Game_OnStart(EventArgs args)
        {
            AllHeroes = ObjectManager.Get<Obj_AI_Hero>().ToList();
            Allies = AllHeroes.FindAll(o => o.IsAlly);
            Enemies = AllHeroes.FindAll(o => o.IsEnemy);
        }
    }
}
