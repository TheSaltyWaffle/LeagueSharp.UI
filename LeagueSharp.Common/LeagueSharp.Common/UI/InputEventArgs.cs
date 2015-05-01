using System.Windows.Forms;
using SharpDX;

namespace LeagueSharp.Common.UI
{
    public class InputEventArgs
    {
        private readonly WndEventArgs _args;

        public InputEventArgs(WndEventArgs args)
        {
            _args = args;
            Cursor = Utils.GetCursorPos();
        }

        public bool Process
        {
            get { return _args.Process; }
            set { _args.Process = value; }
        }

        public Vector2 Cursor { get; private set; }

        public Keys Key
        {
            get
            {
                Keys keyData;
                if ((Keys) ((int) _args.WParam) != Control.ModifierKeys)
                {
                    keyData = (Keys) ((int) _args.WParam) | Control.ModifierKeys;
                }
                else
                {
                    keyData = (Keys) ((int) _args.WParam);
                }
                return keyData;
            }
        }

        public Keys SingleKey
        {
            get { return (Keys) ((int) _args.WParam); }
        }

        public WindowsMessages Msg
        {
            get { return (WindowsMessages) _args.Msg; }
        }
    }
}