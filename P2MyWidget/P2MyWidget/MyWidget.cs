using Gtk;
using System;
using System.Data;

namespace Serpis.Ad
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class MyWidget : Gtk.Bin, IEntityListVIew
	{
		protected IDbConnection dbConnection;
		
		public MyWidget (IDbConnection dbConnection)
		{
			this.dbConnection = dbConnection;
			this.Build ();
			
			Visible = true;
			treeView.Selection.Changed += delegate {//Susbcripcion a nuestro evento
				if (SelectedChanged != null)
					SelectedChanged(this, EventArgs.Empty);//los dos parametros son siempre obligatorios el segundo quiere decir que no le apsa nada que le pasa vacio
			};
		}
		
		
		public TreeView TreeView {
			get {return treeView;}
		}

		#region IEntityListVIew implementation
		public virtual void New ()
		{
			Console.WriteLine("MyWidget.new()");
		}
		
		public bool HasSelected {
			get {
				return treeView.Selection.CountSelectedRows() > 0;
			}
		}
		public event EventHandler SelectedChanged;//implementacion del metodo
		#endregion
		
		
	}
}

