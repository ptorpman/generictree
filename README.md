================================================================================

Generic Tree

           (c) 2013 Peter R. Torpman (peter at torpman dot se)

================================================================================

GenericTree is a collection of C# classes to draw a generic, hierarchical tree.

        Graphics mGraphics = this.CreateGraphics();

        GenericTree mTree = new GenericTree(mGraphics, 0, this.Width, this.Height);

		String[] parent1    = new String[3] { "Father", "1900, New York", "1990, London"};
		String[] parent2    = new String[3] { "Mother", "1905, London", "1994, London"};
		String[] child1     = new String[3] { "The son", "1930, London", "2001, Newark"};
		String[] child2     = new String[3] { "The daughter", "1934, London", "2010, Stockholm"};
		String[] grandchild = new String[3] { "The grandchild", "1970, Atlanta", ""};
		
		mTree.AddNode(0, parent1, null, false, true);
        mTree.AddNode(0, parent2, null, false, true);
        mTree.AddNode(1, child1, null, true, true);
        mTree.AddNode(1, child2, null, true, false);
        mTree.AddNode(2, grandchild1, null, true, false);
         
        mTree.Draw();


At the moment the development is in its early stages, and lots of improvements can be made.		