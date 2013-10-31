using Gtk;
using System;
using System.Collections.Generic;
using System.Data;

namespace Serpis.Ad
{
	public class TreeViewHelper
	{
		private TreeView treeView;
		private ListStore listStore;
		public TreeViewHelper (TreeView treeView, IDbConnection dbConnection, string selectSql)
		{
			this.treeView = treeView;
			IDbCommand dbCommand = dbConnection.CreateCommand ();
			dbCommand.CommandText = selectSql;
			IDataReader dataReader = dbCommand.ExecuteReader();
			string[] columnNames = getColumnNames(dataReader);
			appendColumns(columnNames);
			listStore = createListStore(dataReader.FieldCount);
			while (dataReader.Read ()) {
				List<string> values = new List<string>();
				for (int index = 0; index < dataReader.FieldCount; index++)
					values.Add ( dataReader.GetValue (index).ToString() );
				listStore.AppendValues(values.ToArray());
			}
			dataReader.Close ();
			treeView.Model = listStore;
		}
		
		public ListStore ListStore {
			get {return listStore;}
		}
		
		private string[] getColumnNames(IDataReader dataReader) {
			List<string> columnNames = new List<string>();
			for (int index = 0; index < dataReader.FieldCount; index++)
				columnNames.Add (dataReader.GetName (index));
			return columnNames.ToArray ();
		}
	
		private void appendColumns(string[] columnNames) {
			int index = 0;
			foreach (string columnName in columnNames) 
				treeView.AppendColumn (columnName, new CellRendererText(), "text", index++);
		}
		
		private ListStore createListStore(int fieldCount) {
			Type[] types = new Type[fieldCount];
			for (int index = 0; index < fieldCount; index++)
				types[index] = typeof(string);
			return new ListStore(types);
		}
	}
}

