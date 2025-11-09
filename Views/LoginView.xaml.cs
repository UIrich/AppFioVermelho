using AppFioVermelho.ViewModels;

namespace AppFioVermelho.Views;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();
        BindingContext = new UserViewModel();
    }
}