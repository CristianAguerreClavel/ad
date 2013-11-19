using System;
using Gtk;

namespace Serpis.Ad
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			
			Application.Init ();
			
			App.Instance.DbConnection = null;//TODO asignar objeto conexion
			
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
		}
	}
}
