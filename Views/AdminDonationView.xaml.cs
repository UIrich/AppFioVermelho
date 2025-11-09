using AppFioVermelho.ViewModels;

namespace AppFioVermelho.Views;

public partial class AdminDonationView : ContentPage
{
	public AdminDonationView()
	{
		InitializeComponent();

        this.BindingContext = new DonationViewModel();
    }
}