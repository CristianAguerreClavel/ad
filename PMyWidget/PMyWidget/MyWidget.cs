using System;
using Gtk;

namespace PSerpis.Ad
{
	[System.ComponentModel.ToolboxItem(true)]
	public abstract partial class MyWidget : Gtk.Bin, IEntityListView
	{
		public MyWidget ()
		{
			this.Build ();
			Visible = true;
			
			
			
		}
		
		public TreeView treeView{
			get {return treeView;}
		}
		
		#region IEntityListView implementation
		public abstract void New ();
		
		public abstract void Edit ();
		
		public abstract void Refresh ();
		
		public abstract void Delete ();
		
		public bool HasSelected {
			get {
//				TreeIter treeIter;
//				return treeView.Selection.GetSelected(out treeIter);
				return treeView.Selection.CountSelectedRows() > 0;
			}
		}
		
		#endregion

	
	}
}

