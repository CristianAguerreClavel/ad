using System;
using Gtk;
using MySql.Data.MySqlClient;


public partial class MainWindow: Gtk.Window
{	
	private MySqlConnection mySqlConnection;
	
	public MySqlDataReader consulta(){
		//SELECT * FROM articulo
		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
		mySqlCommand.CommandText = "select * from articulo";
		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
		return mySqlDataReader;
	}
	
	public void eliminarElemento(string elementoEliminar){
		//DELETE FROM articulo
		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
		mySqlCommand.CommandText = "Delete from articulo where id = "+elementoEliminar;
		mySqlCommand.ExecuteNonQuery();
	}
	
	public void editarElemento(string elementoEditar){
		//TODO metodo editarElemento
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
	
	public void crearTreeView(){
		//TODO para realizar la recarga de los elementos
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
		//cortar
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
		
		string[] valoresDeConsulta = new string[fieldCount];
		
		while(mySqlDataReader.Read()){
			for (int i = 0; i < fieldCount; i ++){
				valoresDeConsulta[i] = mySqlDataReader.GetValue(i).ToString();
			}
			listStore.AppendValues(valoresDeConsulta);
		}
		mySqlDataReader.Close();
		
		treeView.Model = listStore;
		
		/*//Codigo propio de fila seleccionada mostrada por consola
		treeView.Selection.Changed += delegate {
			TreeIter iter;
			bool isSelected = treeView.Selection.GetSelected(out iter);
			if (isSelected){
				Console.WriteLine(listStore.GetValue(iter,1));
			}
		};*/
		
			
		//Por defecto los botones estan desabilitados
		editAction.Sensitive = false;
		deleteAction.Sensitive = false;
		
		//Cada vez que se selecciona o deselecciona una fila se actualizan los botones
		treeView.Selection.Changed += delegate {
			editAction.Sensitive = treeView.Selection.CountSelectedRows() > 0;
			deleteAction.Sensitive = treeView.Selection.CountSelectedRows()> 0;
		};
		
		//Boton edit habre una ventaa con el nombre del elemento seleccionado
		editAction.Activated += delegate {
			
			if(treeView.Selection.CountSelectedRows() == 0){//Este if sirve para proteger que solo se 
														//pueda ejecutar la accion cuando el boton esta 
					return;								//habilitado por si en un futuro cambiara la condicion
														//de habilitacion del boton
			}
			
			TreeIter iter;
			bool isSelected = treeView.Selection.GetSelected(out iter);
			if (isSelected){
				string elementoSelected = (string) listStore.GetValue(iter,1);
				MessageDialog md = new MessageDialog (this, 
                                      				DialogFlags.DestroyWithParent,
	                              					MessageType.Error, 
                                     				ButtonsType.Close, elementoSelected);
				md.Run();
				md.Destroy();
			}
		};
		
		//boton eliminar
		deleteAction.Activated += delegate {
			TreeIter iter;
			bool isSelected = treeView.Selection.GetSelected(out iter);
			if(isSelected){
				string mensage = "¿Seguro que desea eliminar "+(string)listStore.GetValue(iter,1)
											+"? Esta accion sera irreversible";
				
				string elementoEliminar = (string)listStore.GetValue(iter,0);
				
				MessageDialog md = new MessageDialog (this, 
                                      				DialogFlags.DestroyWithParent,
	                              					MessageType.Question, 
                                     				ButtonsType.YesNo, mensage);
				
				ResponseType result = (ResponseType)md.Run ();//Comprobacion de la respuesta del dialogo

				if (result == ResponseType.Yes){
					eliminarElemento(elementoEliminar);//llamada al metodo eliminar pasando por parametro el id del objeto selecionado
					md.Destroy();
				}else
					md.Destroy();
			}
		};
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
		
		mySqlConnection.Close();
	}
}
