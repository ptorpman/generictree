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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace GenericTree
{
    public partial class Form1 : Form
    {      
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void drawTree()
        {
            //mGraphics = this.CreateGraphics();

            //GenericTree mTree = new GenericTree(mGraphics, 0, this.Width, this.Height);

            //mTree.AddNode(0, "1Hello1", null, false, true);
            //mTree.AddNode(0, "2Hello2\n2Hello2", null, false, true);
            //mTree.AddNode(0, "3Hello3", null, false, true);
            //mTree.AddNode(1, "4Hello4", null, true, true);
            //mTree.AddNode(1, "5Hello5\n5Hello5", null, false, true);
            //mTree.AddNode(1, "4Hello4\nIt\nrocks", null, true, true);
            //mTree.AddNode(1, "4Hello4\nIt\nsucks", null, true, false);
            //mTree.AddNode(2, "6Hello6", null, true, false);
            //mTree.AddNode(2, "7Hello7\n7Hello7", null, true, false);
            //mTree.AddNode(2, "8Hello8", null, true, true);
            //mTree.AddNode(3, "Holy Crap", null, true, false);
            
            //mTree.Draw();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            drawTree();
        }
    }
}
