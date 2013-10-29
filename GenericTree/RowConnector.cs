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
    /// This class contains the points needed to draw a connector line between two rows
    /// </summary>
    public class RowConnector
    {
        /// <summary>
        /// The top point to which a connector can be drawn.
        /// </summary>
        private PointF _topPoint = PointF.Empty;
        /// <summary>
        /// The bottom point to which a connector can be drawn.
        /// </summary>
        private PointF _bottomPoint = PointF.Empty;
        /// <summary>
        /// Common settings
        /// </summary>
        private TreeCommon _treeCommon = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cmn">Common settings</param>
        public RowConnector(TreeCommon cmn)
        {
            _treeCommon = cmn;
        }

        /// <summary>
        /// Draws the row connector
        /// </summary>
        public void Draw()
        {
            _treeCommon.GetGraphics().DrawLine(_treeCommon.GetPen(), _topPoint, _bottomPoint);
        }

        public PointF GetTopPoint()
        {
            return _topPoint;
        }

        public void SetTopPoint(PointF pointTop)
        {
            _topPoint = pointTop;
        }

        public PointF GetBottomPoint()
        {
            return _bottomPoint;
        }

        public void SetBottomPoint(PointF pointBottom)
        {
            _bottomPoint = pointBottom;
        }


        /// <summary>
        /// Returns the connector that is farthest to the left
        /// </summary>
        /// <param name="rc1">One row connector</param>
        /// <param name="rc2">Another row connector</param>
        /// <returns>The leftmost row connector</returns>
        static public RowConnector GetLeftMost(RowConnector rc1, RowConnector rc2)
        {
            if (rc1 != null && rc2 == null)
            {
                return rc1;
            }

            if (rc1 == null && rc2 != null)
            {
                return rc2;
            }

            if (rc1 == null && rc2 == null)
            {
                return null;
            }

            // Both are set... Which one is farthest to the left?
            if (rc1._topPoint.X < rc2._topPoint.X)
            {
                return rc1;
            }
            else
            {
                return rc2;
            }
        }

        /// <summary>
        /// Returns the connector that is farthest to the right
        /// </summary>
        /// <param name="rc1">One row connector</param>
        /// <param name="rc2">Another row connector</param>
        /// <returns>The rightmost row connector</returns>
        static public RowConnector GetRightMost(RowConnector rc1, RowConnector rc2)
        {
            if (rc1 != null && rc2 == null)
            {
                return rc1;
            }

            if (rc1 == null && rc2 != null)
            {
                return rc2;
            }

            if (rc1 == null && rc2 == null)
            {
                return null;
            }

            // Both are set... Which one is farthest to the left?
            if (rc1._topPoint.X > rc2._topPoint.X)
            {
                return rc1;
            }
            else
            {
                return rc2;
            }
        }


    }
}
