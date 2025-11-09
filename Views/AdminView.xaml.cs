using AppFioVermelho.ViewModels;

namespace AppFioVermelho.Views;

public partial class AdminView : ContentPage
{
	public AdminView()
	{
		InitializeComponent();

        this.BindingContext = new UserViewModel();
    }
}