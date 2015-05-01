using System;
using System.Diagnostics.CodeAnalysis;
using SharpDX;
using SharpDX.Direct3D9;

namespace LeagueSharp.Common.D3DX
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class Primitive
    {
        #region Shape

        /// <summary>
        ///     Line -> Draw Shape
        /// </summary>
        /// <param name="ppLine"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="fDegrees"></param>
        /// <param name="Sides"></param>
        /// <param name="Size"></param>
        /// <param name="LineWidth"></param>
        /// <param name="Color"></param>
        public static void DrawShape(this Line ppLine,
            float x,
            float y,
            float fDegrees,
            int Sides,
            int Size,
            int LineWidth,
            ColorBGRA Color)
        {
            if (Sides < 1)
            {
                return; // => No point of calculation if it's null.
            }

            // => Point Array
            var pt = new Point[Sides + 1];

            // => Degrees into Radians
            fDegrees = (float) ((fDegrees * Math.PI) / 180f);

            var extraDegrees = float.Epsilon;

            for (var i = 0; i < Sides; ++i, extraDegrees += (float) ((Math.PI * Math.PI) / Sides))
            {
                // => Degree
                var degree = fDegrees + extraDegrees;

                while (degree > (Math.PI * Math.PI))
                {
                    // => Don't go over the limit
                    degree -= (float) (Math.PI * Math.PI);
                }

                var _x = (Math.Cos(degree) * Size) + x;
                var _y = (Math.Sin(degree) * Size) + y;
                pt[i] = new Point((int) _x, (int) _y);
            }

            // => Set last point as new first point
            pt[Sides] = new Point(pt[0].X, pt[0].Y);

            for (var i = 0; i < Sides; ++i)
            {
                // => Draw the shape
                ppLine.DrawLine(pt[i].X, pt[i].Y, pt[i + 1].X, pt[i + 1].Y, LineWidth, Color);
            }
        }

        #endregion

        #region Line

        /// <summary>
        ///     Global pointer line.
        /// </summary>
        internal static readonly Line g_pLine = new Line(D3Dx.m_pD3Ddev);

        /// <summary>
        ///     Trigonometric Function
        /// </summary>
        internal struct TrigonometricFunction
        {
            /// <summary>
            ///     Cosine (cos)
            /// </summary>
            public float Cosine;

            /// <summary>
            ///     Geometry: Hypotenuse
            /// </summary>
            public float Hypotenuse;

            /// <summary>
            ///     Geometry: Radian (angular measure)
            /// </summary>
            public float Radian;

            /// <summary>
            ///     Sine (sin)
            /// </summary>
            public float Sine;

            /// <summary>
            ///     Trigonometric functions: Tanget
            /// </summary>
            public float Tangent;
        }

        /// <summary>
        ///     Line -> Draw Line
        /// </summary>
        /// <param name="ppLine">Line Pointer</param>
        /// <param name="sX">X:Start axis</param>
        /// <param name="sY">Y:Start axis</param>
        /// <param name="eX">X:End axis</param>
        /// <param name="eY">Y:End axis</param>
        /// <param name="dwWidth">Line Width</param>
        /// <param name="Color">Color</param>
        public static void DrawLine(this Line ppLine,
            float sX,
            float sY,
            float eX,
            float eY,
            int dwWidth,
            ColorBGRA Color)
        {
            var vLine = new Vector2[2]; // => Two Points
            ppLine.Antialias = false; // => To smooth edges

            ppLine.Width = dwWidth; // => Width of the line
            ppLine.Begin(); // => Begin Drawing

            vLine[0] = new Vector2(sX, sY); // => Array 0
            vLine[1] = new Vector2(eX, eY); // => Array 1

            ppLine.Draw(vLine, Color); // => Draw line
            ppLine.End(); // => End Drawing
        }

        /// <summary>
        ///     Line -> Draw Arrow
        /// </summary>
        /// <param name="ppLine">Line Pointer</param>
        /// <param name="X">X axis</param>
        /// <param name="Y">Y axis</param>
        /// <param name="fDegrees">Degrees</param>
        /// <param name="dwLength">Length</param>
        /// <param name="dwAngleDivisor">Angle Divisor</param>
        /// <param name="dwLineWidth">Line Width</param>
        /// <param name="Color">Color</param>
        public static void DrawArrow(this Line ppLine,
            float X,
            float Y,
            double fDegrees,
            int dwLength,
            int dwAngleDivisor,
            int dwLineWidth,
            ColorBGRA Color)
        {
            while (fDegrees > 360f)
            {
                fDegrees -= 360f; // >= 0f && <= 360f
            }

            var dwAngleLength = dwLength / ((dwAngleDivisor == 0) ? 1 : dwAngleDivisor); // => Angle Length
            var fRightAngle = 135f + fDegrees; // => Right Angle
            var fLeftAngle = 225f + fDegrees; // => Left Angle

            while (fRightAngle > 360f)
            {
                fRightAngle -= 360f; // >= 0f && <= 360f
            }
            while (fLeftAngle > 360f)
            {
                fLeftAngle -= 360f; // >= 0f && <= 360f
            }

            fRightAngle = ((fRightAngle * Math.PI) / 180f); // Degree -> Radian
            fLeftAngle = ((fLeftAngle * Math.PI) / 180f); // Degree -> Radian
            fDegrees = ((fDegrees * Math.PI) / 180f); // Degree -> Radian

            // => Point
            var pt = new Point((int) (Math.Cos(fDegrees) * dwLength + X), (int) (Math.Sin(fDegrees) * dwLength + Y));

            // => Draw Line
            ppLine.DrawLine(X, Y, pt.X, pt.Y, dwLineWidth, Color);

            // => Draw Right Angle
            ppLine.DrawLine(
                pt.X, pt.Y, (float) (Math.Cos(fRightAngle) * dwAngleLength + pt.X),
                (float) (Math.Sin(fRightAngle) * dwAngleLength + pt.Y), dwLineWidth, Color);

            // => Draw Left Angle
            ppLine.DrawLine(
                pt.X, pt.Y, (float) (Math.Cos(fLeftAngle) * dwAngleLength + pt.X),
                (float) (Math.Sin(fLeftAngle) * dwAngleLength + pt.Y), dwLineWidth, Color);
        }

        /// <summary>
        ///     Line -> Draw Gradient Line
        /// </summary>
        /// <param name="ppLine">Line Pointer</param>
        /// <param name="startVector2">Start Vector2</param>
        /// <param name="endVector2">End Vector2</param>
        /// <param name="startColorBgra">Start color</param>
        /// <param name="endColorBgra">End color</param>
        /// <param name="dwWidth">Line width</param>
        public static void DrawGradientLine(this Line ppLine,
            Vector2 startVector2,
            Vector2 endVector2,
            ColorBGRA startColorBgra,
            ColorBGRA endColorBgra,
            int dwWidth)
        {
            // => Copy endColorBgra into pColor
            var pColor = endColorBgra;

            // => New instance of a Trigonometric Function
            var vTrig = new TrigonometricFunction();

            // => Get total Distance and Quadrent
            var vDistance = new Vector2(
                startVector2.X - endVector2.X + float.Epsilon, startVector2.Y - endVector2.Y + float.Epsilon);

            // => Get Trigonometric Details
            vTrig.Hypotenuse =
                (float) (Math.Sqrt(vDistance.X * vDistance.X + vDistance.Y * vDistance.Y) + float.Epsilon);
            vTrig.Radian = (float) (Math.Cos((vDistance.Y) / (vTrig.Hypotenuse)));

            // => Get Offset based on Quadrent
            if (vDistance.X < 0 && vDistance.Y < 0 || vDistance.X < 0 && vDistance.Y > 0)
            {
                vTrig.Radian = (float) (Math.PI - vTrig.Radian);
            }
            else
            {
                vTrig.Radian = (float) (Math.PI + vTrig.Radian);
            }

            // => Calculate Ratio to Offset
            vTrig.Cosine = (float) (Math.Cos(vTrig.Radian));
            vTrig.Sine = (float) (Math.Sin(vTrig.Radian));

            // => Alpha Increasement Rate
            var fRateY = 255f / Math.Abs(vTrig.Hypotenuse);
            var fAlpha = 0f;

            // => Reset alpha
            pColor.A = 0;

            // => Line copy from start position
            var extraVector2 = new Vector2(startVector2.X, startVector2.Y);

            // => Draw Underline
            ppLine.DrawLine(startVector2.X, startVector2.Y, endVector2.X, endVector2.Y, dwWidth, startColorBgra);

            // => Draw Overlapping line with an increasing alpha channel
            for (var i = 0f; i < vTrig.Hypotenuse; ++i)
            {
                // => Draw line from extra position to end position
                ppLine.DrawLine(
                    extraVector2.X, extraVector2.Y, extraVector2.X + vTrig.Sine, extraVector2.Y + vTrig.Cosine, dwWidth,
                    pColor);

                // => Increase offset by ratio
                extraVector2.X += vTrig.Sine;
                extraVector2.Y += vTrig.Cosine;

                // => Increase alpha
                if (fAlpha + fRateY < 255f)
                {
                    fAlpha += fRateY;
                }

                // => Set new Increased Alpha
                pColor.A = (byte) fAlpha;
            }
        }

        #endregion

        #region Text

        /// <summary>
        ///     CenteredText Drawing Flags
        /// </summary>
        [Flags]
        public enum CenteredTextFlags
        {
            // => 00000000
            None = 0,

            // => 00000001
            HorizontalLeft = 1 << 0,

            // => 00000010
            HorizontalCenter = 1 << 1,

            // => 00000100
            HorizontalRight = 1 << 2,

            // => 00001000
            VerticalUp = 1 << 3,

            // => 00010000
            VerticalCenter = 1 << 4,

            // => 00100000
            VerticalDown = 1 << 5
        }

        /// <summary>
        ///     Font -> Draw Text
        /// </summary>
        /// <param name="m_pFont">Font Pointer</param>
        /// <param name="lpSprite">Sprite Pointer</param>
        /// <param name="lpchText">Text</param>
        /// <param name="lpRectangle">Rectangle</param>
        /// <param name="uFormat">Font Draw Flags Format</param>
        /// <param name="D3DCOLOR">Color</param>
        /// <returns></returns>
        public static int DrawTextA(this Font m_pFont,
            Sprite lpSprite,
            string lpchText,
            Rectangle lpRectangle,
            FontDrawFlags uFormat,
            ColorBGRA D3DCOLOR)
        {
            return lpSprite == null ? 0 : m_pFont.DrawText(lpSprite, lpchText, lpRectangle, uFormat, D3DCOLOR);
        }

        /// <summary>
        ///     Font -> Draw Text
        /// </summary>
        /// <param name="m_pFont">Font Pointer</param>
        /// <param name="lpSprite">Sprite Pointer</param>
        /// <param name="lpchText">Text</param>
        /// <param name="x">X axis</param>
        /// <param name="y">Y axis</param>
        /// <param name="D3DCOLOR">Color</param>
        /// <returns></returns>
        public static int DrawTextA(this Font m_pFont,
            Sprite lpSprite,
            string lpchText,
            int x,
            int y,
            ColorBGRA D3DCOLOR)
        {
            return lpSprite == null ? 0 : m_pFont.DrawText(lpSprite, lpchText, x, y, D3DCOLOR);
        }

        /// <summary>
        ///     Font -> Draw Text
        /// </summary>
        /// <param name="m_pFont">Font Pointer</param>
        /// <param name="lpSprite">Sprite Pointer</param>
        /// <param name="lpchText">Text</param>
        /// <param name="nCount">Text Length</param>
        /// <param name="lpRect">Rectangle</param>
        /// <param name="uFormat">Format</param>
        /// <param name="D3DCOLOR">Color</param>
        /// <returns></returns>
        public static int DrawTextA(this Font m_pFont,
            Sprite lpSprite,
            string lpchText,
            int nCount,
            IntPtr lpRect,
            int uFormat,
            ColorBGRA D3DCOLOR)
        {
            return lpSprite == null ? 0 : m_pFont.DrawText(lpSprite, lpchText, nCount, lpRect, uFormat, D3DCOLOR);
        }

        /// <summary>
        ///     Font -> Draw Angled Text
        /// </summary>
        /// <param name="m_pFont">Font Pointer</param>
        /// <param name="lpSprite">Sprite Pointer</param>
        /// <param name="vPos">Position</param>
        /// <param name="vCenter">Center Position</param>
        /// <param name="Color">Color</param>
        /// <param name="fDegree">Degree</param>
        /// <param name="szpString">String</param>
        /// <returns></returns>
        public static int DrawAngledText(this Font m_pFont,
            Sprite lpSprite,
            Vector2 vPos,
            Vector2 vCenter,
            ColorBGRA Color,
            float fDegree,
            string szpString)
        {
            if (lpSprite == null)
            {
                return 0; // => Return -> "If the function fails, the return value is zero."
            }

            const int W = 2; // => Width
            const int H = 2; // => Height

            var RECT = new Rectangle((int) vPos.X, (int) vPos.Y, (int) vPos.X + W, (int) vPos.Y + H); // => Rectangle
            var vScales = new Vector2(1f, 1f); // => Scale

            var vMatrix = Matrix.Transformation2D(Vector2.Zero, 0f, vScales, vCenter, fDegree, vPos);
            // => Modified Matrix
            var vMatrixOriginal = lpSprite.Transform; // => Original Matrix

            lpSprite.Begin(SpriteFlags.AlphaBlend); // => Begin sprite draw with AlphaBlend (to enable transparency)
            lpSprite.Transform = vMatrix; // => Sprite Matrix changed to Modified Matrix

            // => Draw our Text
            var status = m_pFont.DrawText(lpSprite, szpString, RECT, FontDrawFlags.NoClip | FontDrawFlags.Left, Color);

            lpSprite.Transform = vMatrixOriginal; // => Sprite Matrix changed to Orignal Matrix
            lpSprite.End(); // => End sprite draw

            return status; // => Return
        }

        public static int DrawCenteredText(this Font m_pFont,
            Sprite lpSprite,
            string lpchText,
            Rectangle rectangle,
            CenteredTextFlags flags,
            ColorBGRA D3DCOLOR)
        {
            if (lpSprite == null)
            {
                return 0; // => Return -> "If the function fails, the return value is zero."
            }

            var x = 0;
            var y = 0;

            var textDimension = m_pFont.MeasureText(lpSprite, lpchText, 0);

            // => Horizontal
            if (flags.HasFlag(CenteredTextFlags.HorizontalLeft))
            {
                x = rectangle.TopLeft.X;
            }
            else if (flags.HasFlag(CenteredTextFlags.HorizontalCenter))
            {
                x = rectangle.TopLeft.X + (rectangle.Width - textDimension.Width) / 2;
            }
            else if (flags.HasFlag(CenteredTextFlags.HorizontalRight))
            {
                x = rectangle.TopRight.X - textDimension.Width;
            }

            // => Vertical
            if (flags.HasFlag(CenteredTextFlags.VerticalUp))
            {
                y = rectangle.TopLeft.Y;
            }
            else if (flags.HasFlag(CenteredTextFlags.VerticalCenter))
            {
                y = rectangle.TopLeft.Y + (rectangle.Height - textDimension.Height) / 2;
            }
            else if (flags.HasFlag(CenteredTextFlags.VerticalDown))
            {
                y = rectangle.BottomLeft.Y - textDimension.Height;
            }

            return m_pFont.DrawTextA(lpSprite, lpchText, x, y, D3DCOLOR);
        }

        #endregion

        #region Rectangle

        /// <summary>
        ///     Line -> Draw Rectangle
        /// </summary>
        /// <param name="ppLine">Line Pointer</param>
        /// <param name="X">X axis</param>
        /// <param name="Y">Y axis</param>
        /// <param name="Width">Width</param>
        /// <param name="Height">Height</param>
        /// <param name="Color">Color</param>
        public static void DrawRectangle(this Line ppLine, float X, float Y, int Width, int Height, ColorBGRA Color)
        {
            var vLine = new Vector2[2]; // => Vector

            ppLine.Width = Width; // => Set Width
            ppLine.Antialias = false; // => Set Antialias
            ppLine.GLLines = true; // => Set GLLines

            ppLine.Begin(); // => Begin line

            vLine[0] = new Vector2(X + Width / 2f, Y); // => Set Vector 0
            vLine[1] = new Vector2(X + Width / 2f, Y + Height); // => Set Vector 1

            ppLine.Draw(vLine, Color); // => Draw Vector
            ppLine.End(); // => End line
        }

        /// <summary>
        ///     Line -> Draw Rectangle Outline
        /// </summary>
        /// <param name="ppLine">Line Pointer</param>
        /// <param name="X">X axis</param>
        /// <param name="Y">Y axis</param>
        /// <param name="Width">Width</param>
        /// <param name="Height">Height</param>
        /// <param name="Color">Color</param>
        public static void DrawRectangleOutline(this Line ppLine,
            float X,
            float Y,
            int Width,
            int Height,
            ColorBGRA Color)
        {
            ppLine.DrawRectangle(X, Y, Width, 1, Color); // => Direct Draw 0
            ppLine.DrawRectangle(X, Y, 1, Height, Color); // => Direct Draw 1

            ppLine.DrawRectangle(X + Width, Y, 1, Height, Color); // => Direct Draw 2
            ppLine.DrawRectangle(X, Y + Height, Width, 1, Color); // => Direct Draw 3
        }

        /// <summary>
        ///     Line -> Draw Rounded Rectangle
        /// </summary>
        /// <param name="ppLine">Line Pointer</param>
        /// <param name="X">X axis</param>
        /// <param name="Y">Y axis</param>
        /// <param name="Width">Width</param>
        /// <param name="Height">Height</param>
        /// <param name="Smooth">Smoothness</param>
        /// <param name="Color">Color</param>
        public static void DrawRoundedRectangle(this Line ppLine,
            float X,
            float Y,
            int Width,
            int Height,
            int Smooth,
            ColorBGRA Color)
        {
            var pt = new Point[4];

            // => Corners
            pt[0] = new Point((int) (X + (Width - Smooth)), (int) (Y + (Height - Smooth)));
            pt[1] = new Point((int) (X + Smooth), (int) (Y + (Height - Smooth)));
            pt[2] = new Point((int) (X + Smooth), (int) (Y + Smooth));
            pt[3] = new Point((int) (X + Width - Smooth), (int) (Y + Smooth));

            // => Draw Cross
            ppLine.DrawRectangle(X, Y + Smooth, Width, Height - Smooth * 2, Color);
            ppLine.DrawRectangle(X + Smooth, Y, Width - Smooth * 2, Height, Color);

            // => Current Degree
            var fDegree = 0d;

            // => For each corner
            for (var i = 0; i < 4; ++i)
            {
                for (var k = fDegree; k < fDegree + (Math.PI * Math.PI) / 4; k += ((1) * (Math.PI / 180f)))
                {
                    var cos = (float) (Math.Cos(k) * Smooth);
                    var sin = (float) (Math.Sin(k) * Smooth);

                    ppLine.DrawLine(pt[i].X, pt[i].Y, pt[i].X + cos, pt[i].Y + sin, 3, Color);
                }

                fDegree += (Math.PI * Math.PI) / 4;
            }
        }

        /// <summary>
        ///     Line -> Draw Gradient Rectangle
        /// </summary>
        /// <param name="ppLine">Line Pointer</param>
        /// <param name="positionVector2">Position</param>
        /// <param name="Width">Width</param>
        /// <param name="Height">Height</param>
        /// <param name="startColor">Start Color</param>
        /// <param name="endColor">End Color</param>
        public static void DrawGradientRectangle(this Line ppLine,
            Vector2 positionVector2,
            int Width,
            int Height,
            ColorBGRA startColor,
            ColorBGRA endColor)
        {
            // => Draw default Rectangle
            ppLine.DrawRectangle(positionVector2.X, positionVector2.Y, Width, Height, startColor);

            // => Copy endColor into pColor
            var pColor = endColor;

            // => Reset ALPHA channel in pColor
            pColor.A = 0;

            // => Alpha Increasement Rate
            var fRateY = 255f / Height;

            // => Draw overlapping rectangles
            for (var i = 0; i < Height; ++i)
            {
                // => Draw new Rectangle with new ALPHA channel
                ppLine.DrawRectangle(positionVector2.X, positionVector2.Y + i, Width, 1, pColor);

                // => Increase ALPHA channel
                pColor.A = (byte) Math.Min(255, pColor.A + fRateY);
            }
        }

        #endregion
    }
}