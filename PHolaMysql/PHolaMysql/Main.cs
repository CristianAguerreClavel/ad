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
		
		public static void Main (string[] args)
		{
			string connectionString ="Server=localhost;Database=dbprueba;user id=root;password=sistemas";
			MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
			mySqlConnection.Open();
			
			//select * from categoria
			MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
			mySqlCommand.CommandText = "select * from articulo";
			MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
			
			int numColum = mySqlDataReader.FieldCount;
			
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
				for(int i = 0; i < numColum;i++){
				Console.Write (mySqlDataReader.GetValue(i));
				Console.Write("     ");
				}
				Console.WriteLine();
			}
			
			mySqlDataReader.Close();
			mySqlConnection.Close();
			//Console.WriteLine ("OK");
			
			
			
		}
	}
}
