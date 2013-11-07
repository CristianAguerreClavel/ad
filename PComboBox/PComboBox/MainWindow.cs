using System;
using Gtk;
using MySql.Data.MySqlClient;
using System.Data;
using PSerpis.Ad;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		IDbConnection dbConnection = new MySqlConnection("Server=localhost;" +
							"Database=dbprueba;User id=root;Password=sistemas");
		dbConnection.Open();
		
	
		string tabla="categoria";
		
		
		ComboBoxHelper comboBoxHelper = new ComboBoxHelper(comboBox,dbConnection,"id","nombre",tabla,3);
		
//		CellRendererText cellRenderText1 = new CellRendererText();
//		comboBox.PackStart(cellRenderText1,false);
//		comboBox.AddAttribute(cellRenderText1,"text",0);//el ultimo parametro el 1 sirve para elegir la columna a visualizar
//		int initialid =2;
//		
//		
//		CellRendererText cellRenderText = new CellRendererText();
//		comboBox.PackStart(cellRenderText,false);
//		comboBox.AddAttribute(cellRenderText,"text",1);//el ultimo parametro el 1 sirve para elegir la columna a visualizar
//		
//		ListStore listStore = new ListStore(typeof(int),typeof(string));
//		
//		TreeIter initialTreeIter = listStore.AppendValues(0, "Sin asignar");;
//		IDbCommand dbCommand = dbConnection.CreateCommand();
//		dbCommand.CommandText = "select id, nombre from categoria";
//		IDataReader dataReader = dbCommand.ExecuteReader();
//		
//		while(dataReader.Read())
//		{
//			int id =(int) dataReader["id"];
//			string nombre = (string)dataReader["nombre"];
//			TreeIter treeIter = listStore.AppendValues(id,nombre);
//			if (id == initialid)
//				initialTreeIter = treeIter;
//			
//		}
//		dataReader.Close();
//		comboBox.Model = listStore;
//		comboBox.SetActiveIter(initialTreeIter);
//				
//		comboBox.Changed += delegate {
//			TreeIter treeIter;
//			comboBox.GetActiveIter(out treeIter);
//			int id = (int) listStore.GetValue(treeIter,0);
//			
//			Console.WriteLine("ID = "+ id);
//		};
		
		
		
		
		
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
