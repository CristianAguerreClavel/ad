using System;

namespace PSerpis.Ad
{
	public class ArticuloListView : MyWidget
	{
		public ArticuloListView ()
		{
		}

		#region implemented abstract members of PSerpis.Ad.MyWidget
		public override void New ()
		{
			Console.WriteLine("Nuevo desde ArticuloListView New");
		}

		public override void Edit ()
		{
			Console.WriteLine("Nuevo desde ArticuloListView Edit");
		}

		public override void Refresh ()
		{
			
		}

		public override void Delete ()
		{
			
		}
		#endregion
	}
}

