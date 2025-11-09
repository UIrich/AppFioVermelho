using AppFioVermelho.ViewModels;

namespace AppFioVermelho.Views;

public partial class DonatorSelectView : ContentPage
{
	public DonatorSelectView()
	{
		InitializeComponent();

        var userVM = new UserViewModel();

        BindingContext = userVM;
    }
}