using Gtk;
using System;
using System.Data;

namespace Serpis.Ad
{
	public class ArticuloListView : MyWidget
	{
		public ArticuloListView (IDbConnection dbConnection) : base (dbConnection)
		{
			TreeView.AppendColumn("id", new CellRendererText(),"text",0);
			TreeView.AppendColumn("nombre", new CellRendererText(),"text",1);
			TreeView.AppendColumn("categoria", new CellRendererText(),"text",2);
			TreeView.AppendColumn("precio", new CellRendererText(),"text",3);
			ListStore listStore = new ListStore(typeof(int),typeof(string),typeof(string),typeof(string));
			TreeView.Model = listStore;
			
			listStore.AppendValues(1,"Articulo1","Cat. 1","1");
			listStore.AppendValues(2,"Articulo2","Cat. 2","2");
		}
		
		
		public override void New ()
		{
			Console.WriteLine("ArticuloListView.New()");
		}
		
	}
}

