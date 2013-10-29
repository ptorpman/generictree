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
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace GenericTree
{
    // The GenTree is responsible for drawing a generic tree containing a number of rows of 
    // rectangles (filled with text). E.g first row could contain parents, second, the actual person
    // and his spouse, and the third row the children of the couple.
    public class GenericTree
    {
        /// <summary>
        /// Common settings
        /// </summary>
        public TreeCommon mCommon = new TreeCommon();

        /// <summary>
        /// Dictionary containing row information
        /// </summary>
        private Dictionary<int, TreeRow> mTree = new Dictionary<int, TreeRow>();

        /// <summary>
        /// Currently selected node.
        /// </summary>
        private TreeNode mSelectedNode;

        /// <summary><c>Constructor</c> of the GenericTree</summary>
        /// <param name="gr">The row of the GenericTree to which the node should be added.</param>
        /// <param name="x">The leftmost X coordinate.</param>
        /// <param name="y">The upper Y coordinate.</param>
        /// <param name="width">The width of the area on which the GenericTree should be drawn.</param>
        /// <param name="height">The height of the area on which the GenericTree should be drawn.</param>
        public GenericTree(Graphics gr, int x, int y, int width, int height, Color bgColor)
        {
            mCommon.Initialize(gr, width, height, x, y, x + width / 2, height / 2, TextRenderingHint.ClearTypeGridFit, bgColor);           
        }


        /// <summary><c>AddNode</c> adds a node to a row in the GenericTree</summary>
        /// <param name="row">The row of the GenericTree to which the node should be added.</param>
        /// <param name="text">The text that should be inside the node.</param>
        /// <param name="userData">User data that can be retrieved later.</param>
        /// <param name="hasParent">Flag if the node has a parent or not.</param>
        /// <param name="hasChild">Flag if the node has a child or not.</param>
        ///
        public void AddNode(int row, String[] text, object userData = null, bool hasParent = false, bool hasChild = false)
        {
            if (mTree.Keys.Contains(row) == false)
            {
                // Add new row to tree
                mTree.Add(row, new TreeRow(mCommon));
            }

            // Add node to row
            mTree[row].AddNode(text, hasParent, hasChild, userData);
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
            mCommon.SetFont(fam, style, bigSize, smallSize);
        }


        /// <summary>
        /// Draws the tree
        /// </summary>
        public void Draw()
        {
            // Make sure the area is cleared.
            mCommon.GetGraphics().Clear(mCommon.GetBackgroundColor());

            float currY = mCommon.GetY();

            // Sort rows
            var list = mTree.Keys.ToList();
            list.Sort();


            // Loop through the rows and perform calculations
            int currKey = 0;
            foreach (var key in list)
            {
                // Draw the row
                currY = mTree[key].Draw(currY);

                currKey++;
            }

            // Loop through the nodes and draw horizontal connector lines
            currKey = 0;

            RowConnector leftUp = null;
            RowConnector rightUp = null;
            RowConnector leftDown = null;
            RowConnector rightDown = null;
            RowConnector rcLeft = null;
            RowConnector rcRight = null;

            foreach (var key in list)
            {
                leftUp = null; rightUp = null; leftDown = null; rightDown = null;

                float x1; float y; float x2;
                int row = key;

                // First row               
                if (row == mTree.Keys.First())
                {
                    // First row can only have downwards connectors
                    leftDown = mTree[row].GetConnectorDownLeft();
                    rightDown = mTree[row].GetConnectorDownRight();

                    // See if next row has any upwards connectors
                    if (mTree.Keys.Contains(row + 1))
                    {
                        leftUp = mTree[row + 1].GetConnectorUpLeft();
                        rightUp = mTree[row + 1].GetConnectorUpRight();
                    }

                    rcLeft = RowConnector.GetLeftMost(leftDown, leftUp);
                    rcRight = RowConnector.GetRightMost(rightDown, rightUp);

                    if (rcLeft == null || rcRight == null)
                    {
                        row++;
                        continue;
                    }

                    if (rcLeft == leftDown)
                    {
                        x1 = rcLeft.GetBottomPoint().X;
                        y = rcLeft.GetBottomPoint().Y;
                        x2 = rcRight.GetBottomPoint().X;
                    }
                    else
                    {
                        x1 = rcLeft.GetTopPoint().X;
                        y = rcLeft.GetTopPoint().Y;
                        x2 = rcRight.GetTopPoint().X;
                    }

                    // Draw the line
                    mCommon.GetGraphics().DrawLine(mCommon.GetPen(), new PointF(x1, y), new PointF(x2, y));
                    row++;

                    continue;
                }

                // Last row
                if (row == list.Last() && list.Contains(row - 1))
                {
                    // Last row can only have upwards connectors
                    leftUp = mTree[row].GetConnectorUpLeft();
                    rightUp = mTree[row].GetConnectorUpRight();

                    leftDown = mTree[row - 1].GetConnectorDownLeft();
                    rightDown = mTree[row - 1].GetConnectorDownRight();

                    rcLeft = RowConnector.GetLeftMost(leftDown, leftUp);
                    rcRight = RowConnector.GetRightMost(rightDown, rightUp);

                    if (rcLeft == null || rcRight == null)
                    {
                        row++;
                        continue;
                    }

                    if (rcLeft == leftDown)
                    {
                        x1 = rcLeft.GetBottomPoint().X;
                        y = rcLeft.GetBottomPoint().Y;
                        x2 = rcRight.GetBottomPoint().X;
                    }
                    else
                    {
                        x1 = rcLeft.GetTopPoint().X;
                        y = rcLeft.GetTopPoint().Y;
                        x2 = rcRight.GetTopPoint().X;
                    }

                    // Draw the line
                    mCommon.GetGraphics().DrawLine(mCommon.GetPen(), new PointF(x1, y), new PointF(x2, y));
                    row++;

                    continue;
                }

                // Row in the middle can have 
                // First row can only have downwards connectors
                leftDown = mTree[row].GetConnectorDownLeft();
                rightDown = mTree[row].GetConnectorDownRight();

                // See if next row has any upwards connectors
                if (list.Contains(row + 1))
                {
                    leftUp = mTree[row + 1].GetConnectorUpLeft();
                    rightUp = mTree[row + 1].GetConnectorUpRight();
                }

                rcLeft = RowConnector.GetLeftMost(leftDown, leftUp);
                rcRight = RowConnector.GetRightMost(rightDown, rightUp);

                if (rcLeft == null || rcRight == null)
                {
                    row++;
                    continue;
                }

                if (rcLeft == leftDown)
                {
                    x1 = rcLeft.GetBottomPoint().X;
                    y = rcLeft.GetBottomPoint().Y;
                    x2 = rcRight.GetBottomPoint().X;
                }
                else
                {
                    x1 = rcLeft.GetTopPoint().X;
                    y = rcLeft.GetTopPoint().Y;
                    x2 = rcRight.GetTopPoint().X;
                }

                // Draw the line
                mCommon.GetGraphics().DrawLine(mCommon.GetPen(), new PointF(x1, y), new PointF(x2, y));
                row++;
            }
        }

        /// <summary>
        /// Returns the node currently under the mouse pointer
        /// </summary>
        /// <param name="pt">Point on the form</param>
        /// <returns>TreeNode or null</returns>
        public TreeNode NodeAtPoint(PointF pt)
        {
            var list = mTree.Keys.ToList();
            list.Sort();

            // Loop through the rows and perform calculations
            foreach (var key in list)
            {
                // Draw the row
                TreeNode n = mTree[key].NodeAtPoint(pt);

                if (n != null)
                {
                    // Found a node under the mouse!
                    return n;
                }
            }

            return null;
        }

        /// <summary>
        /// Find a tree node with specific user data
        /// </summary>
        /// <param name="userData">Pointer to previously added user data</param>
        /// <returns></returns>
        public TreeNode FindNode(object userData)
        {
            var list = mTree.Keys.ToList();
            list.Sort();

            // Loop through the rows and perform calculations
            TreeNode node = null;


            foreach (var key in list)
            {
                // Draw the row
                TreeRow row = mTree[key];

                node = row.GetNode(userData);

                if (node != null) 
                {
                    return node;
                }
            }

            return node;
        }

        /// <summary>
        /// Select a node with specific user data
        /// </summary>
        /// <param name="userData">User data</param>
        public TreeNode SelectNode(object userData)
        {
            mSelectedNode = FindNode(userData);
            return mSelectedNode;
        }

        public TreeNode SelectNode(PointF point)
        {
            mSelectedNode = NodeAtPoint(point);
            return mSelectedNode;
        }

        /// <summary>
        /// Returns currently selected node.
        /// </summary>
        /// <returns></returns>
        public TreeNode GetSelectedNode()
        {
            return mSelectedNode;
        }
    }
}
