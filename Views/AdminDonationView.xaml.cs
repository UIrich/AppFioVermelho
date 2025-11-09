using AppFioVermelho.ViewModels;

namespace AppFioVermelho.Views;

public partial class AdminDonationView : ContentPage
{
	public AdminDonationView()
	{
		InitializeComponent();

        var userVM = new UserViewModel();
        var donationVM = new DonationViewModel(userVM);

        BindingContext = donationVM;
    }
}