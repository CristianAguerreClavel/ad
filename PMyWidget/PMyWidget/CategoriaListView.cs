using System;

namespace PSerpis.Ad
{
	public class CategoriaListView: MyWidget
	{
		public CategoriaListView ()
		{
		}

		#region implemented abstract members of PSerpis.Ad.MyWidget
		public override void New ()
		{
			Console.WriteLine("Nuevo desde CategoriaLIstView New");
		}

		public override void Edit ()
		{
			Console.WriteLine("Nuevo desde CategoriaLIstView Edit");
		}

		public override void Refresh ()
		{
			throw new NotImplementedException ();
		}

		public override void Delete ()
		{
			throw new NotImplementedException ();
		}
		#endregion
	}
}

