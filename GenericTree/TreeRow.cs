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
    /// This class represents a row in the tree
    /// </summary>
    class TreeRow
    {
        /// <summary>
        /// The total width of the row
        /// </summary>
        private float _rowWidth = 0;
        /// <summary>
        /// The maximum height of the row
        /// </summary>
        private float _rowHeight = 0;
        /// <summary>
        /// List of nodes in the row
        /// </summary>
        private List<TreeNode> _treeNodes;
        /// <summary>
        /// Common settings
        /// </summary>
        private TreeCommon _treeCommon = null;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cmn">Common settings</param>
        public TreeRow(TreeCommon cmn)
        {
            _treeCommon = cmn;
        }

        /// <summary>
        /// Adds a node to the row
        /// </summary>
        /// <param name="text">Text to display in node (Three rows)</param>
        /// <param name="hasParent">Flag if node has parent</param>
        /// <param name="hasChild">Flag if node has a child</param>
        /// <param name="userData">User supplied data which can be retrieved later</param>
        public void AddNode(String[] text, bool hasParent, bool hasChild, object userData)
        {
            if (_treeNodes == null)
            {
                _treeNodes = new List<TreeNode>();
            }

            _treeNodes.Add(new TreeNode(text, hasParent, hasChild, userData, _treeCommon));
        }

        /// <summary>
        /// Draws a row of TreeNodes
        /// </summary>
        /// <param name="currY">Current Y coordinate value</param>
        /// <returns></returns>
        public float Draw(float currY)
        {
            Inspect(currY);

            bool hadChild = false;

            // Draw nodes
            for (int i = 0; i < _treeNodes.Count; i++)
            {
                _treeNodes[i].Draw();
            }

            // Draw vertical connectors 
            for (int i = 0; i < _treeNodes.Count; i++)
            {
                if (_treeNodes[i].HasParent() && _treeNodes[i].GetConnectorUp() != null)
                {
                    _treeNodes[i].GetConnectorUp().Draw();
                }
            }

            for (int i = 0; i < _treeNodes.Count; i++)
            {
                if (_treeNodes[i].HasChild() && _treeNodes[i].GetConnectorDown() != null)
                {
                    _treeNodes[i].GetConnectorDown().Draw();
                    hadChild = true;
                }
            }

            // Draw vertical connectors 
            float y;

            if (hadChild == false)
            {
                y = currY + _rowHeight;
            }
            else
            {
                y = currY + _rowHeight + (2 * _treeCommon.GetConnectorLineHeight());
            }

            return y;
        }

        /// <summary>
        /// Inspects a row of TreeNodes and performs calculations
        /// </summary>
        /// <param name="currY">Current Y coordinate value</param>
        private void Inspect(float currY)
        {
            // Loop through the nodes to get row width and height
            for (int i = 0; i < _treeNodes.Count; i++)
            {
                _rowWidth += _treeNodes[i].GetWidth();

                if (i + 1 != _treeNodes.Count)
                {
                    // Add spacing
                    _rowWidth += _treeCommon.GetNodeSpacing();
                }

                if (_rowHeight < _treeNodes[i].GetHeight())
                {
                    _rowHeight = _treeNodes[i].GetHeight();
                }
            }


            // Loop through the nodes and set their X and Y values
            float currX = _treeCommon.GetCenterX() - (_rowWidth / 2);

            for (int i = 0; i < _treeNodes.Count; i++)
            {
                TreeNode n = _treeNodes[i];

                n.SetX(currX);
                n.SetY(currY);

                float x = currX + (n.GetWidth() / 2);

                if (n.HasParent())
                {
                    // Create two points for the connector towards the upper row
                    n.SetConnectorUp(new RowConnector(_treeCommon), new PointF(x, n.GetY() - _treeCommon.GetConnectorLineHeight()), new PointF(x, n.GetY()));
                }

                if (n.HasChild())
                {
                    // Create two points for the connector towards the lower row
                    n.SetConnectorDown(new RowConnector(_treeCommon),
                        new PointF(x, n.GetY() + n.GetHeight()), 
                        new PointF(x, n.GetY() + _rowHeight + _treeCommon.GetConnectorLineHeight()));
                }

                if (i + 1 != _treeNodes.Count)
                {
                    // If not last node add width and spacing
                    currX += n.GetWidth() + _treeCommon.GetNodeSpacing();
                }
                else
                {
                    // Last node,
                    currX += n.GetWidth();
                }
            }
        }

        /// <summary>
        /// Returns the upwards connector that is farthest to the left
        /// </summary>
        /// <returns>RowConnector or null</returns>
        public RowConnector GetConnectorUpLeft()
        {
            for (int i = 0; i < _treeNodes.Count; i++)
            {
                if (_treeNodes[i].GetConnectorUp() != null)
                {
                    return _treeNodes[i].GetConnectorUp();
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the upwards connector that is farthest to the right
        /// </summary>
        /// <returns>RowConnector or null</returns>
        public RowConnector GetConnectorUpRight()
        {
            RowConnector rc = null;

            for (int i = 0; i < _treeNodes.Count; i++)
            {
                if (_treeNodes[i].GetConnectorUp() != null)
                {
                    rc = _treeNodes[i].GetConnectorUp();
                }
            }

            return rc;
        }

        /// <summary>
        /// Returns the downwards connector that is farthest to the left
        /// </summary>
        /// <returns>RowConnector or null</returns>
        public RowConnector GetConnectorDownLeft()
        {
            for (int i = 0; i < _treeNodes.Count; i++)
            {
                if (_treeNodes[i].GetConnectorDown() != null)
                {
                    return _treeNodes[i].GetConnectorDown();
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the downwards connector that is farthest to the right
        /// </summary>
        /// <returns>RowConnector or null</returns>
        public RowConnector GetConnectorDownRight()
        {
            RowConnector rc = null;

            for (int i = 0; i < _treeNodes.Count; i++)
            {
                if (_treeNodes[i].GetConnectorDown() != null)
                {
                    rc = _treeNodes[i].GetConnectorDown();
                }
            }

            return rc;
        }

        /// <summary>
        /// Returns a node that is at the specified point, or null.
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public TreeNode NodeAtPoint(PointF pt)
        {
            for (int i = 0; i < _treeNodes.Count; i++)
            {
                if (_treeNodes[i].IsAtPoint(pt))
                {
                    return _treeNodes[i];
                }
            }

            return null;
        }


        public TreeNode GetNode(object userData)
        {
            for (int i = 0; i < _treeNodes.Count; i++)
            {
                if (_treeNodes[i].GetUserData() == userData)
                {
                    return _treeNodes[i];
                }
            }

            return null;
        }
    }
}
