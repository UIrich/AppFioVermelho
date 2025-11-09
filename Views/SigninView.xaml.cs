using AppFioVermelho.ViewModels;

namespace AppFioVermelho.Views;

public partial class SigninView : ContentPage
{
	public SigninView()
	{
		InitializeComponent();
        BindingContext = new UserViewModel();
	}
}