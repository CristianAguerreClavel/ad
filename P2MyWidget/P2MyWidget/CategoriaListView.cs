using Gtk;
using System;

namespace Serpis.Ad
{
	public class CategoriaListView : MyWidget
	{
		public CategoriaListView (IDbConnection dbConnection) : base (dbConnection)
		{
			TreeView.AppendColumn("id", new CellRendererText(),"text",0);
			TreeView.AppendColumn("nombre", new CellRendererText(),"text",1);
			ListStore listStore =  new ListStore(typeof(int),typeof(string));
			TreeView.Model = listStore;
			
			listStore.AppendValues(1,"Cat. 1");
			listStore.AppendValues(2,"Cat. 2");
			listStore.AppendValues(3,"Cat. 3");
		}
		
		public override void New ()
		{
			Console.WriteLine("Categoria.New()");
		}
	}
}

