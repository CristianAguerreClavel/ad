using Gtk;
using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace PSerpis.Ad
{
	public class ComboBoxHelper
	{
		TreeIter treeIter;
		private const string selectFormat = "select {0}, {1} from {2}";
		private ComboBox comboBox;
		private ListStore listStore;
		
		public ComboBoxHelper (ComboBox comboBox, IDbConnection dbConnection, string keyFieldName,
		                       string valueFieldName,string tableName,int initialId)
		{
			this.comboBox = comboBox;
			//this.initalId = initialId;
			
			CellRendererText cellRenderText1 = new CellRendererText();
			comboBox.PackStart(cellRenderText1,false);
			comboBox.AddAttribute(cellRenderText1,"text",0);//el ultimo parametro el 0 sirve para elegir la columna a visualizar
			
			CellRendererText cellRenderText = new CellRendererText();
			comboBox.PackStart(cellRenderText,false);
			comboBox.AddAttribute(cellRenderText,"text",1);//el ultimo parametro el 1 sirve para elegir la columna a visualizar
			
			listStore = new ListStore(typeof(int),typeof(string));
			
			TreeIter initialTreeIter;/* = listStore.AppendValues(0, "Sin asignar");*/
			IDbCommand dbCommand = dbConnection.CreateCommand();
			dbCommand.CommandText = string.Format(selectFormat, keyFieldName,valueFieldName,tableName);
			IDataReader dataReader = dbCommand.ExecuteReader();
			
			//Recorre el dataReader para insertar los valores en el comboBox
			while(dataReader.Read())
			{
				int id =(int) dataReader["id"];
				string nombre = (string)dataReader["nombre"];
				treeIter = listStore.AppendValues(id,nombre);
				if (id == initialId)
					initialTreeIter = treeIter;
			}
			
			
			dataReader.Close();
			comboBox.Model = listStore;
			comboBox.SetActiveIter(initialTreeIter);
					
			comboBox.Changed += delegate {
				TreeIter treeIter;
				comboBox.GetActiveIter(out treeIter);
				int id = (int) listStore.GetValue(treeIter,0);
				
				Console.WriteLine("ID = "+ id);
			};
		}
		
		public int id {
				get{
					comboBox.GetActiveIter(out treeIter);
					int id = (int)listStore.GetValue(treeIter,0);
					return id;
				}
			}
	}
}