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
    /// This class is used to represent a node in the tree
    /// </summary>
    public class TreeNode : IDrawable
    {
        /// <summary>
        /// Text to display (Three lines of text)
        /// </summary>
        private String[] _text = new String[3];
        /// <summary>
        /// Size of texts
        /// </summary>
        private SizeF[] _textSizes = new SizeF[3];
        /// <summary>
        /// User data
        /// </summary>
        private object _userData = null;
        /// <summary>
        /// Flag if the node has a parent (and a line should be drawn to it)
        /// </summary>
        private bool _hasParent = false;
        /// <summary>
        /// Flag if the node has a child (and a line should be drawn to it)
        /// </summary>
        private bool _hasChild = false;
        /// <summary>
        /// Width of node
        /// </summary>
        private float _width = 0;
        /// <summary>
         /// Height of node
        /// </summary>
        private float _height = 0;
        /// <summary>
        /// X coordinate of node (upper left corner)
        /// </summary>
        private float _x = 0;
        /// <summary>
        /// Y coordinate of node (upper left corner)
        /// </summary>
        private float _y = 0;
        /// <summary>
        /// Common settings
        /// </summary>
        private TreeCommon _treeCommon = null;
        /// <summary>
        /// Connector to upper row
        /// </summary>
        private RowConnector _connectorUp = null;
        /// <summary>
        /// Connector to lower row
        /// </summary>
        private RowConnector _connectorDown = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="txt">The three lines of text that should be drawn</param>
        /// <param name="parent">Flag if node has a parent</param>
        /// <param name="child">Flag if node has a child</param>
        /// <param name="data">User data that can be retrieved later</param>
        /// <param name="cmn">Common settings</param>
        public TreeNode(String[] txt, bool parent, bool child, object data, TreeCommon cmn)
        {
            for (int i = 0; i < 3; i++)
            {
                _text[i] = txt[i];
            }

            _userData = data;
            _hasParent = parent;
            _hasChild = child;
            _treeCommon = cmn;

            GetSize();
        }

        /// <summary>
        /// Returns X coordinate
        /// </summary>
        /// <returns></returns>
        public float GetX()
        {
            return _x;
        }

        /// <summary>
        /// Sets X coordinate
        /// </summary>
        /// <param name="x"></param>
        public void SetX(float x)
        {
            _x = x;
        }


        /// <summary>
        /// Returns Y coordinate
        /// </summary>
        /// <returns></returns>
        public float GetY()
        {
            return _y;
        }

        /// <summary>
        /// Sets Y coordinate
        /// </summary>
        /// <param name="y"></param>
        public void SetY(float y)
        {
            _y = y;
        }

        /// <summary>
        /// Returns the connector at the top
        /// </summary>
        /// <returns></returns>
        public RowConnector GetConnectorUp()
        {
            return _connectorUp;
        }

        /// <summary>
        /// Sets the connector at the top
        /// </summary>
        /// <returns></returns>
        public void SetConnectorUp(RowConnector conn)
        {
            _connectorUp = conn;
        }

        /// <summary>
        /// Set the connector at top.
        /// </summary>
        /// <param name="rowConnector"></param>
        /// <param name="pointTop"></param>
        /// <param name="pointBottom"></param>
        public void SetConnectorUp(RowConnector rowConnector, PointF pointTop, PointF pointBottom)
        {
            _connectorUp = rowConnector;
            _connectorUp.SetTopPoint(pointTop);
            _connectorUp.SetBottomPoint(pointBottom);
        }


        /// <summary>
        /// Returns the connector at the bottom
        /// </summary>
        /// <returns></returns>
        public RowConnector GetConnectorDown()
        {
            return _connectorDown;
        }

        /// <summary>
        /// Sets the connector at the bottom
        /// </summary>
        /// <returns></returns>
        public void SetConnectorDown(RowConnector conn)
        {
            _connectorDown = conn;
        }

        /// <summary>
        /// Sets the connector at the bottom
        /// </summary>
        /// <returns></returns>
        public void SetConnectorDown(RowConnector rowConnector, PointF pointTop, PointF pointBottom)
        {
            _connectorDown = rowConnector;
            _connectorDown.SetTopPoint(pointTop);
            _connectorDown.SetBottomPoint(pointBottom);
        }


        /// <summary>
        /// Returns if a node has a parent
        /// </summary>
        /// <returns></returns>
        public bool HasParent()
        {
            return _hasParent;
        }

        /// <summary>
        /// Returns if a node has a child
        /// </summary>
        /// <returns></returns>
        public bool HasChild()
        {
            return _hasChild;
        }

        /// <summary>
        /// Returns width of node
        /// </summary>
        /// <returns></returns>
        public float GetWidth()
        {
            return _width;
        }

        /// <summary>
        /// Returns height of node
        /// </summary>
        /// <returns></returns>
        public float GetHeight()
        {
            return _height;
        }


        /// <summary>
        /// Returns the size of the node. (Part of IDrawable interface.)
        /// </summary>
        /// <returns>Size (width and height) of node</returns>
        public SizeF GetSize()
        {
            _height = 0;

            if (_text[0] != "")
            {
                _textSizes[0] = _treeCommon.GetGraphics().MeasureString(_text[0], _treeCommon.GetBigFont()) + new SizeF(3, 3);
            }
            else
            {
                // Use a dummy value to get the row a size
                _textSizes[0] = _treeCommon.GetGraphics().MeasureString("A Dummy Value", _treeCommon.GetBigFont()) + new SizeF(3, 3);
            }

            _height += _textSizes[0].Height;

            if (_text[1] != "")
            {
                _textSizes[1] = _treeCommon.GetGraphics().MeasureString(_text[1], _treeCommon.GetSmallFont()) + new SizeF(3, 3);
            }
            else
            {
                // Use a dummy value to get the row a size
                _textSizes[1] = _treeCommon.GetGraphics().MeasureString("Test", _treeCommon.GetSmallFont()) + new SizeF(3, 3);
            }
            
            _height += _textSizes[1].Height;

            if (_text[2] != "")
            {
                _textSizes[2] = _treeCommon.GetGraphics().MeasureString(_text[2], _treeCommon.GetSmallFont()) + new SizeF(3, 3);
            }
            else
            {
                // Use a dummy value to get the row a size
                _textSizes[2] = _treeCommon.GetGraphics().MeasureString("Test", _treeCommon.GetSmallFont()) + new SizeF(3, 3);
            }
          
            _height += _textSizes[2].Height;

            _width = _textSizes[0].Width;

            if (_width < _textSizes[1].Width)
            {
                _width = _textSizes[1].Width;
            }
            if (_width < _textSizes[2].Width)
            {
                _width = _textSizes[2].Width;
            }


            return new SizeF(_width, _height);
        }
        
        /// <summary>
        /// Draw the object at (x, y).
        /// </summary>
        public void Draw()
        {
            SizeF size = GetSize();

            RectangleF rect = new RectangleF(_x, _y, _width, _height);

            float y = _y;


            using (StringFormat string_format = new StringFormat())
            {
                string_format.Alignment = StringAlignment.Center;
                string_format.LineAlignment = StringAlignment.Center;

                if (_text[0] != "")
                {
                    _treeCommon.GetGraphics().DrawString(_text[0], _treeCommon.GetBigFont(), Brushes.Black, new PointF(_x, y));
                }
                y += _textSizes[0].Height;

                if (_text[1] != "")
                {
                    _treeCommon.GetGraphics().DrawString(_text[1], _treeCommon.GetSmallFont(), Brushes.Black, new PointF(_x, y));
                }
                y += _textSizes[1].Height;

                if (_text[2] != "")
                {
                    _treeCommon.GetGraphics().DrawString(_text[2], _treeCommon.GetSmallFont(), Brushes.Black, new PointF(_x, y));
                }

                _treeCommon.GetGraphics().DrawRectangle(Pens.Black, Rectangle.Round(rect));
            }
        }

        /// <summary>
        /// Returns true if a node is at the specified location
        /// </summary>
        /// <param name="pt">Point where the mouse is</param>
        /// <returns>True if the node is at the specified point.</returns>
        public bool IsAtPoint(PointF pt)
        {
            if (pt.X >= _x && (pt.X <= _x + _width))
            {
                // Within the right X coordinates
                if (pt.Y >= _y && pt.Y <= (_y + _height))
                {
                    // Within the right Y coordinates
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns the user data
        /// </summary>
        /// <returns>An object or null</returns>
        public object GetUserData()
        {
            return _userData;
        }


    }
}
