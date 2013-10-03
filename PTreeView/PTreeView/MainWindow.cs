using System;
using Gtk;
using MySql.Data.MySqlClient;


public partial class MainWindow: Gtk.Window
{	
	private MySqlConnection mySqlConnection;
	
	public MySqlDataReader consulta(){
		//SELECT * FROM CATEGORIA
		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
		mySqlCommand.CommandText = "select * from categoria";
		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
		return mySqlDataReader;
	}
	
	public string[] cabecera(MySqlDataReader mySqlDataReader){
		string[] names = new string[mySqlDataReader.FieldCount];
			int numColum = mySqlDataReader.FieldCount;
			
			//Obtencion de las cabeceras
			for (int i = 0; i<numColum; i++){
				names[i]=mySqlDataReader.GetName(i);
			}
			return names;
	}
	
	public string[] valores(MySqlDataReader mySqlDataReader){
		
			int colum = mySqlDataReader.FieldCount;
			string[] values = new string[colum];
			
			for(int i =0;i<colum;i++){
				if(mySqlDataReader.GetValue(i) is DBNull){
					values[i] = "null";	
				}else{
					values[i] =mySqlDataReader.GetValue(i).ToString();	
				}
			
			}
		return values;
	}
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		string connectionString ="Server=localhost;Database=dbprueba;user id=root;password=sistemas";
		mySqlConnection = new MySqlConnection(connectionString);
		mySqlConnection.Open();
		
		
		MySqlDataReader mySqlDataReader = consulta ();//realiza la consulta y la almacena en la variable
		//string[] names = cabecera(mySqlDataReader);//obtiene las cabeceras de la consulta
		//string[] valores = valores(mySqlDataReader);
		
		
		
		treeView.AppendColumn("id", new CellRendererText(), "text",0);
		treeView.AppendColumn("nombre", new CellRendererText(), "text",1);
		treeView.AppendColumn("categoria", new CellRendererText(), "text",2);
		treeView.AppendColumn("precio", new CellRendererText(), "text",3);
		
		int fieldCount = mySqlDataReader.FieldCount;
		Type[] types = new Type[fieldCount];
		for (int i = 0; i < fieldCount; i++){
			types[i]=typeof(string);	
		}
		//ListStore listStore = new ListStore(typeof(string), typeof(string),typeof(string),typeof(string));
		ListStore listStore = new ListStore(types);
		
		string[] valores1 = new string[fieldCount];
		
		while(mySqlDataReader.Read()){
			for (int i = 0; i < fieldCount; i ++){
				valores1[i] = mySqlDataReader.GetValue(i).ToString();
			}
			listStore.AppendValues(valores1);
		}
		
		treeView.Model = listStore;
		
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
		
		mySqlConnection.Close();
	}
}
