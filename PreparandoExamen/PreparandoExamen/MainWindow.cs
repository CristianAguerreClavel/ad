using System;
using Gtk;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

public partial class MainWindow: Gtk.Window
{	
	//	IMPORTANTE: Añadir en referencias la libreria de MySql.Data y añadir en el proyecto el using, añadir el uso de collections
	
	MySqlConnection mySqlConnection; //Declaramos aqui para que sea accesible
	
	public void CrearConexion() //metodo para abrir una conexion
	{
		string conectionString = "Server=localhost;Database=dbprueba;user id=root;password=sistemas"; //string que contiene la cadena de conecxion
		mySqlConnection = new MySqlConnection(conectionString);
		mySqlConnection.Open();
	}
	
	
	//----------------------------------------METODOS CONSTRUCION DE INTERFAZ
	
	
	public MySqlDataReader consulta()
	{
		string sentenciaSql = "Select * from articulo";
		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();//creamos un comando "asociandolo" a la conexcion
		mySqlCommand.CommandText = sentenciaSql;//establecemos el tipo de comando 
		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();//Guardamos en el objeto mySqlDataReader la consulta
		return mySqlDataReader;
	}
	
	
	private string[] getColumnNames(MySqlDataReader mySqlDataReader) //metodo que devuelve un array con el nombre de las columnas de la tabla que contiene el mySqlDataReader
	{
		List<string> columnNames = new List<string>();//Lista de valores que almacenara el nombre de las columnas de la tabla en la base de datos
		for (int index = 0; index < mySqlDataReader.FieldCount; index++)//se obtienen del mySqlDataReader el nombre de las columnas
			columnNames.Add (mySqlDataReader.GetName (index));//se añade el nombre a la lista
		return columnNames.ToArray ();
	}
	
	
	private void appendColumns(string[] columnNames) {//meotodo para establecer las columnas en el treeView a partir de un array de cabeceras
		int index = 0;
		foreach (string columnName in columnNames)//se recorre el array que recibe por parametro
		{ 
			treeView.AppendColumn (columnName, new CellRendererText(), "text", index++);//se añade la columna en el treeView
		}
	}
	
	private ListStore createListStore(int fieldCount) {//metodo para crear una listStore la cual se genera con un array de typos
		Type[] types = new Type[fieldCount];//array de tipos del tamaño de fieldCount que se obtiene del mySqlDataReader
		for (int index = 0; index < fieldCount; index++)//ciclo para rellenar el array
			types[index] = typeof(string);
		return new ListStore(types);
	}
	
	private ListStore listStoreGeneration(MySqlDataReader mySqlDataReader)
	{
		string[] columNames = getColumnNames(mySqlDataReader);//obtencion de los nombres de las columnas a partir del dataReader y almacenado en un array de string
		appendColumns(columNames);//insercion metiante el metodo appendColumns las cabeceras en el treeview "el treeview esta insertado desde la vista diseño"
		
		ListStore listStore = createListStore(mySqlDataReader.FieldCount);//Creacion del listStore mediante la llamada al metodo y pasandole el tamaño por parametro
		
		while (mySqlDataReader.Read ()) {//bucle para rellenar los datos en el listStore
			List<string> values = new List<string>();
			for (int index = 0; index < mySqlDataReader.FieldCount; index++)
				values.Add ( mySqlDataReader.GetValue (index).ToString() );
			listStore.AppendValues(values.ToArray());
		}
		
		mySqlDataReader.Close();
		return listStore;
	}
	
	private void setModelToTreeView(ListStore listStore)//metodo que inserta el listStore, nota: encapsulado en un metodo para simplificar el refresco automatico
	{
		treeView.Model = listStore;
	}
	
	
	//---------------------------------------------------METODOS DE ACCIONES DE LA INTERFAZ
	
	
	
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		CrearConexion();//metodo para establecer la conexion
		
//		Contruccion de la ventana desde la interfaz diseñador creando un VBox, un TreeView(treeView) y una Toolbar
		
		MySqlDataReader mySqlDataReader = consulta();//ejecucion de la consulta y voldacada en el dataReader
		ListStore listStore = listStoreGeneration(mySqlDataReader);//obtencion de cabereras, insercion de columnas, y rellenado de tados, GENERACION del listStore
		setModelToTreeView(listStore);//asignacion/insercion del listStore en el treeView
		
//		Añadidos desde la pestaña Acciones los botones de editar y eliminar
		
		
		
	}
	
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
