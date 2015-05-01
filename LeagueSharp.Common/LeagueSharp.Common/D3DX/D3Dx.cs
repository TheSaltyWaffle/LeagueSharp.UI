using System.Diagnostics.CodeAnalysis;
using SharpDX.Direct3D9;

namespace LeagueSharp.Common.D3DX
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class D3Dx
    {
        internal static Font m_font;
        internal static Device m_pD3Ddev;

        static D3Dx()
        {
            m_pD3Ddev = Drawing.Direct3DDevice;
            m_pD3Ddev.SetRenderState(RenderState.AlphaBlendEnable, true);
            m_font = D3DXCreateFont();
        }

        /// <summary>
        ///     D3DX -> Create Font
        /// </summary>
        /// <param name="Height">Height</param>
        /// <param name="Width">Width</param>
        /// <param name="Weight">Font Weight</param>
        /// <param name="MipLevels">Mipmapping Levels</param>
        /// <param name="Italic">Font Italic</param>
        /// <param name="CharSet">Font CharacterSet</param>
        /// <param name="OutputPrecision">Font Percision</param>
        /// <param name="Quality">Font Quality</param>
        /// <param name="PitchAndFamily">Font Pitch and Family</param>
        /// <param name="pFacename">Font Facename</param>
        /// <returns>Font Pointer</returns>
        public static Font D3DXCreateFont(int Height = 0xe,
            int Width = 0x0,
            FontWeight Weight = FontWeight.DoNotCare,
            int MipLevels = 0x0,
            bool Italic = false,
            FontCharacterSet CharSet = FontCharacterSet.Default,
            FontPrecision OutputPrecision = FontPrecision.Default,
            FontQuality Quality = FontQuality.Antialiased,
            FontPitchAndFamily PitchAndFamily = FontPitchAndFamily.Decorative | FontPitchAndFamily.DontCare,
            string pFacename = "Tahoma")
        {
            return new Font(
                m_pD3Ddev, Height, Width, Weight, MipLevels, Italic, CharSet, OutputPrecision, Quality, PitchAndFamily,
                pFacename);
        }

        /// <summary>
        ///     D3DX -> Create Sprite
        /// </summary>
        /// <returns>Sprite Instance</returns>
        public static Sprite CreateSprite()
        {
            return new Sprite(m_pD3Ddev);
        }

        /// <summary>
        ///     D3DX -> Create Line
        /// </summary>
        /// <returns>Line Instance</returns>
        public static Line CreateLine()
        {
            return new Line(m_pD3Ddev);
        }

        /// <summary>
        ///     D3DX -> Device
        /// </summary>
        /// <returns>Device Instance</returns>
        public static Device GetDevice()
        {
            return m_pD3Ddev;
        }
    }
}