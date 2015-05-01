using System;
using System.Dynamic;
using System.Linq;
using SharpDX;

namespace LeagueSharp.Common.UI
{
    public abstract class AMenuComponent : DynamicObject
    {
        private int _menuWidth = -1;
        private Vector2 _position;

        protected AMenuComponent(string name, string displayName, string uniqueIdentifier)
        {
            Name = name;
            DisplayName = displayName;
            UniqueIdentifier = uniqueIdentifier;
        }

        /// <summary>
        ///     The internal name for this Component.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     The Display Name that is used to draw.
        /// </summary>
        public string DisplayName { get; private set; }

        public string UniqueIdentifier { get; private set; }

        /// <summary>
        ///     The parent of this AMenuComponent or null if not existing.
        /// </summary>
        public Menu Parent { get; set; }

        /// <summary>
        ///     The Width of this Component and all other Components with the same Parent.
        /// </summary>
        public int MenuWidth
        {
            get
            {
                if (_menuWidth == -1)
                {
                    _menuWidth = Parent != null ? Parent.Children.Max(child => child.Width) : MenuSettings.DefaultWidth;
                }
                return _menuWidth;
            }
            set { _menuWidth = value; }
        }

        /// <summary>
        ///     The upper-left corner of this component.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                if (Parent == null)
                {
                    return _position;
                }
                var vec = new Vector2
                {
                    X = Parent.Position.X + Parent.MenuWidth,
                    Y =
                        Parent.Position.Y +
                        (Array.IndexOf(Parent.Children.FindAll(c => c.Visible).ToArray(), this) *
                         MenuSettings.DefaultHeight)
                };
                return vec;
            }
            set { _position = value; }
        }

        /// <summary>
        ///     True if this component is visible, false otherwise.
        /// </summary>
        public abstract bool Visible { get; set; }

        /// <summary>
        ///     The Width that this AMenuComponent needs.
        /// </summary>
        public abstract int Width { get; }

        /// <summary>
        ///     An indexer used to find a child
        /// </summary>
        /// <param name="name">The name of the child</param>
        /// <returns></returns>
        public abstract AMenuComponent this[string name] { get; }

        /// <summary>
        ///     Gets received when a mouse interacts with the screen.
        /// </summary>
        public abstract void OnWndProc(InputEventArgs args);

        /// <summary>
        ///     Call this to recalculate the MenuWidth of this component and all other components with the same Parent.
        /// </summary>
        internal void ResetMenuWidth()
        {
            if (Parent == null)
            {
                return;
            }

            foreach (var child in Parent.Children)
            {
                child.MenuWidth = -1;
            }
        }

        /// <summary>
        ///     Get the value of a child with a certain name.
        /// </summary>
        /// <typeparam name="T">The type of MenuValue of this child.</typeparam>
        /// <param name="name">The name of the child.</param>
        /// <returns>The value that is attached to this Child.</returns>
        public abstract T GetValue<T>(string name) where T : AMenuValue;

        /// <summary>
        ///     Menu Drawing
        /// </summary>
        internal abstract void OnDraw();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            try
            {
                var comp = this[binder.Name];
                var item = comp as MenuItem;
                result = item != null ? item.ValueAsObject : comp;
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }
    }
}