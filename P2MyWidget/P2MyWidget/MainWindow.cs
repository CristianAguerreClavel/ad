using System;
using Gtk;

using Serpis.Ad;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		//App.Instance.DbConnection; TODO uso de dbConnection
		
		CategoriaListView categoriaListView = new CategoriaListView(App.Instance.DbConnection);
		ArticuloListView articuloListView = new ArticuloListView(App.Instance.DbConnection);
		notebook.AppendPage(articuloListView, new Label("Articulos"));
		notebook.AppendPage(categoriaListView, new Label("Categorias"));
		
		articuloListView.SelectedChanged += delegate {
			refesAction();
		};
		
		
		categoriaListView.SelectedChanged += delegate {
			refesAction();
		};
		
		/*newAction.Activated += delegate {
			if (!(notebook.CurrentPageWidget is IEntityListVIew)){
				return;
			}
			IEntityListVIew entittyListView = (IEntityListVIew) notebook.CurrentPageWidget;
			Console.WriteLine("entitytListView.getType(0)"+ entittyListView.GetType());
			entittyListView.New();
			
		};*/
		
		newAction.Activated += delegate {
			IEntityListVIew entittyListView = notebook.CurrentPageWidget as IEntityListVIew;//as comprueba si es posible la conversion y si es posible convierte sino devuelve null
			if (entittyListView == null){
				return;
			}
			Console.WriteLine("entitytListView.getType(0)"+ entittyListView.GetType());
			entittyListView.New();
			
		};
		
		notebook.SwitchPage += delegate {
			refesAction();
		};
		
		
	}
	
	public void refesAction(){
		IEntityListVIew entityListView = notebook.CurrentPageWidget as IEntityListVIew;
		newAction.Sensitive = entityListView != null;
		editAction.Sensitive = entityListView != null && entityListView.HasSelected;
	}
		
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
