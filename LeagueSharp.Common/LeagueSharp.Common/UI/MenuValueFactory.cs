using System;
using System.Collections.Generic;
using LeagueSharp.Common.UI.Values;

namespace LeagueSharp.Common.UI
{
    internal class MenuValueFactory
    {
        private static readonly IDictionary<Type, Func<AMenuValue>> DefaultMenuValueByType =
            new Dictionary<Type, Func<AMenuValue>>
            {
                { typeof(MenuBool), () => new MenuBool() },
                { typeof(MenuSlider), () => new MenuSlider(0, 0, 100) },
                { typeof(MenuInputText), () => new MenuInputText("")}
                //... add more IMenuValues
            };

        public static T Create<T>() where T : AMenuValue
        {
            if (DefaultMenuValueByType.ContainsKey(typeof(T)))
            {
                return (T) DefaultMenuValueByType[typeof(T)].Invoke();
            }
            return default(T);
        }
    }
}