using AppFioVermelho.ViewModels;

namespace AppFioVermelho.Views;

public partial class AdminView : ContentPage
{
    private UserViewModel userVM;
    private DonationViewModel donationVM;
    public AdminView()
	{
		InitializeComponent();

        userVM = new UserViewModel();
        donationVM = new DonationViewModel(userVM);

        BindingContext = donationVM;
    }
}