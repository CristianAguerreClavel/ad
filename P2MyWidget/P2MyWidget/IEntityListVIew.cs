using System;

namespace Serpis.Ad
{
	public interface IEntityListVIew
	{
		void New();
		bool HasSelected {get;}
		event EventHandler SelectedChanged;
	}
}

