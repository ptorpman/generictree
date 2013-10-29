//=============================================================================================================================================
// Copyright (c) 2013-  Peter R. Torpman (peter at torpman dot se)
//
// This file is part of GenericTree (http://generictree.sourceforge.net)
//
// GenericTree is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 3 of the License, or (at your option) any later version.
//
// GenericTree is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.
//=============================================================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GenericTree
{
    /// <summary>
    /// This class contains the common settings used by GenericTree
    /// </summary>
    public class TreeCommon : IDisposable
    {
        /// <summary>
        /// Spacing between nodes
        /// </summary>
        private int _nodeSpacing = 20;
        /// <summary>
        /// Horizontal height of connector lines
        /// </summary>
        private int _connectorLineHeight = 20;
        /// <summary>
        /// Big Font used
        /// </summary>
        private Font _bigFont = new Font(new FontFamily("Segoe UI"), 16, FontStyle.Regular, GraphicsUnit.Pixel);
        /// <summary>
        /// Small Font used
        /// </summary>
        private Font _smallFont = new Font(new FontFamily("Segoe UI"), 11, FontStyle.Regular, GraphicsUnit.Pixel);
        /// <summary>
        /// Pen used to draw
        /// </summary>
        private Pen _pen = Pens.Black;
        /// <summary>
        /// Font brush
        /// </summary>
        private Brush _fontBrush = Brushes.Black;
        /// <summary>
        /// Background brush
        /// </summary>
        private Brush _backgroundBrush = Brushes.White;
        /// <summary>
        /// Width of owning control
        /// </summary>
        private int _ownerWidth = 0;
        /// <summary>
        /// Height of owning control
        /// </summary>
        private int _ownerHeight = 0;
        /// <summary>
        /// Center point on X axis
        /// </summary>
        private int _centerX = 0;
        /// <summary>
        /// Center point on Y axis
        /// </summary>
        private int _centerY = 0;
        /// <summary>
        /// Upper X coordinate of drawing area
        /// </summary>
        private int _x = 0;
        /// <summary>
        /// Upper Y coordinate of drawing area
        /// </summary>
        private int _y = 0;
        /// <summary>
        /// Background color of owning form
        /// </summary>
        private Color _backgroundColor;
        /// <summary>
        /// Graphics instance
        /// </summary>
        private Graphics _graphics = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public TreeCommon()
        {
        }

        /// <summary>
        /// Returns spacing between nodes
        /// </summary>
        /// <returns></returns>
        public int GetNodeSpacing()
        {
            return _nodeSpacing;
        }

        public int GetConnectorLineHeight()
        {
            return _connectorLineHeight;
        }

        public Font GetBigFont()
        {
            return _bigFont;
        }

        public Font GetSmallFont()
        {
            return _smallFont;
        }

        public Pen GetPen()
        {
            return _pen;
        }

        public Brush GetFontBrush()
        {
            return _fontBrush;
        }

        public Brush GetBackgroundBrush()
        {
            return _backgroundBrush;
        }

        public int GetOwnerWidth()
        {
            return _ownerWidth;
        }

        public int GetOwnerHeight()
        {
            return _ownerHeight;
        }

        public int GetCenterX()
        {
            return _centerX;
        }

        public int GetCenterY()
        {
            return _centerY;
        }

        public int GetX()
        {
            return _x;
        }

        public int GetY()
        {
            return _y;
        }

        public Color GetBackgroundColor()
        {
            return _backgroundColor;
        }

        public Graphics GetGraphics()
        {
            return _graphics;
        }

        /// <summary>
        /// Set the fonts used in GenericTree - one big, one small
        /// </summary>
        /// <param name="fam">Font family</param>
        /// <param name="style">Font style</param>
        /// <param name="bigSize">Size of big font</param>
        /// <param name="smallSize">Size of small font</param>
        public void SetFont(FontFamily fam, FontStyle style, int bigSize, int smallSize)
        {
            _bigFont.Dispose();
            _smallFont.Dispose();
            _bigFont = new Font(fam, bigSize, style, GraphicsUnit.Pixel);
            _smallFont = new Font(fam, smallSize, style, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// Initialize the common settings.
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="textRenderingHint"></param>
        /// <param name="bgColor"></param>
        public void Initialize(Graphics gr, int width, int height, int x, int y, int centerX, int centerY, System.Drawing.Text.TextRenderingHint textRenderingHint, Color bgColor)
        {
            _graphics = gr;
            _ownerWidth = width;
            _ownerHeight = height;
            _x = x;
            _y = y;
            _centerX = centerX;
            _centerY = centerY;
            _graphics.TextRenderingHint = textRenderingHint;
            _backgroundColor = bgColor;
        }

        public void Dispose()
        {
            _graphics.Dispose();
        }
    }

}
