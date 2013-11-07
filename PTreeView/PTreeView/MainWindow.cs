using Gtk;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

using Serpis.Ad;

public partial class MainWindow: Gtk.Window
{	
	private MySqlConnection mySqlConnection;
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		mySqlConnection = new MySqlConnection("Server=localhost;Database=dbprueba;User Id=root;Password=sistemas");
		mySqlConnection.Open ();
		
		string selectSql = 
			"select a.id, a.nombre, c.nombre as categoria, a.precio " +
			"from articulo a left join categoria c " +
			"on a.categoria = c.id ";
		TreeViewHelper treeViewHelper = new TreeViewHelper(treeView, mySqlConnection, selectSql);
		
		ListStore listStore = treeViewHelper.ListStore;
		
		editAction.Sensitive = false;
		deleteAction.Sensitive = false;
		
		editAction.Activated += delegate {
			if (treeView.Selection.CountSelectedRows() == 0)
				return;
			TreeIter treeIter;
			treeView.Selection.GetSelected(out treeIter);
			object id = listStore.GetValue (treeIter, 0);
			object nombre = listStore.GetValue (treeIter, 1);
			
			MessageDialog messageDialog = new MessageDialog(this,
                DialogFlags.DestroyWithParent,
                MessageType.Info,
                ButtonsType.Ok,
                "Seleccionado Id={0} Nombre={1}", id, nombre);
			messageDialog.Title = "Este es el título del mensaje";
			messageDialog.Run ();
			messageDialog.Destroy ();
		};
		
		deleteAction.Activated += delegate {
			if (treeView.Selection.CountSelectedRows() == 0)
				return;
			TreeIter treeIter;
			treeView.Selection.GetSelected(out treeIter);
			object id = listStore.GetValue (treeIter, 0);
			
			MessageDialog messageDialog = new MessageDialog(this,
                DialogFlags.DestroyWithParent,
                MessageType.Question,
                ButtonsType.YesNo,
                "¿Quieres eliminar el elemento seleccionado?");
			messageDialog.Title = "Eliminar elemento";
			ResponseType response = (ResponseType)messageDialog.Run ();
			messageDialog.Destroy ();
			if (response == ResponseType.Yes ) {
				MySqlCommand deleteMySqlCommand = mySqlConnection.CreateCommand();
				deleteMySqlCommand.CommandText = "delete from articulo where id=" + id;
				deleteMySqlCommand.ExecuteNonQuery();
			}
		};
		
		treeView.Selection.Changed += delegate {
			bool hasSelectedRows = treeView.Selection.CountSelectedRows() > 0;
			editAction.Sensitive = hasSelectedRows;
			deleteAction.Sensitive = hasSelectedRows;
		};
		
		//treeView.Selection.CountSelectedRows()
//		treeView.Selection.Changed += delegate {
//			TreeIter treeIter;
//			Console.WriteLine ("============");
//			if (treeView.Selection.GetSelected (out treeIter)) {
//				Console.WriteLine ("listStore.GetPath(treeIter)=" + listStore.GetPath(treeIter) );
//				Console.WriteLine ("listStore.GetValue(treeIter, 0)=" + listStore.GetValue(treeIter, 0));
//				Console.WriteLine ("listStore.GetValue(treeIter, 1)=" + listStore.GetValue(treeIter, 1));
//			} else
//				Console.WriteLine ("Ninguno seleccionado");
//		};
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
		
		mySqlConnection.Close ();
	}
}
