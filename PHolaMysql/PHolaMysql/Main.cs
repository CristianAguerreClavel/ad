using System;
using MySql.Data.MySqlClient;

namespace Serpis.Ad
{
	class MainClass
	{
		
		private static string[] getColumNames(MySqlDataReader mySqlDataReader){
		
			string[] names = new string[mySqlDataReader.FieldCount];
			int numColum = mySqlDataReader.FieldCount;
			
			//Obtencion de las cabeceras
			for (int i = 0; i<numColum; i++){
				names[i]=mySqlDataReader.GetName(i);
			}
			return names;
		}
		
		public static string getLine(MySqlDataReader mySqlDataReader){
			int colum = mySqlDataReader.FieldCount;
			string line = "";
			
			for(int i =0;i<colum;i++){
				if(mySqlDataReader.GetValue(i) is DBNull){
					line += "null"+"	 ";	
				}else{
					line += mySqlDataReader.GetValue(i)+"	 ";	
				}
			}
			return line;
		}
		
		
		public static void Main (string[] args)
		{
			string connectionString ="Server=localhost;Database=dbprueba;user id=root;password=sistemas";
			MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
			mySqlConnection.Open();
			
			//select * from categoria
			MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
			mySqlCommand.CommandText = "select * from articulo";
			MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
			
			
			//Visualizacion de las cabeceras
			
			string[] names = getColumNames(mySqlDataReader);
			
			for (int i = 0; i < names.Length; i++){
				Console.Write(names[i]);
				Console.Write("     ");
			}
			Console.WriteLine();
			Console.WriteLine("-----------------------------------------");
			
			//Recorremos el datareader para visualizar los datos
			while(mySqlDataReader.Read()) {
				string line ="";
				line = getLine(mySqlDataReader);
				Console.WriteLine(line);
			}
			
			mySqlDataReader.Close();
			mySqlConnection.Close();
			//Console.WriteLine ("OK");
			
			//Modificar si es un null
			
						
		}
	}
}
