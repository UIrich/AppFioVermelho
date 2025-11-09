using AppFioVermelho.ViewModels;

namespace AppFioVermelho.Views;

public partial class DonatorSelectView : ContentPage
{
	public DonatorSelectView()
	{
		InitializeComponent();

        var userVM = new UserViewModel();
        var donationVM = new DonationViewModel(userVM);

        BindingContext = donationVM;
    }
}