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
    /// Interface used for drawing a node
    /// </summary>
    interface IDrawable
    {
        /// <summary>
        ///  Return the object's needed size.
        /// </summary>
        /// <returns></returns>
        SizeF GetSize();

        /// <summary>
        ///  Draw the object centered at (x, y).
        /// </summary>
        void Draw();

        /// <summary>
        /// Returns true if a node is at the specified location
        /// </summary>
        /// <param name="point">The point at which the mouse is</param>
        /// <returns></returns>
         bool IsAtPoint(PointF point);
    }
}
