using AppFioVermelho.Models;
using AppFioVermelho.ViewModels;

namespace AppFioVermelho.Views;

public partial class DonationView : ContentPage
{
	public DonationView(Donation doacao = null, UserViewModel userVM = null)
	{
		InitializeComponent();

        var vm = new DonationViewModel(userVM);

        if (doacao != null)
        {
            vm.DoacaoExistente = doacao;
            vm.Instituicao = doacao.Instituicao;
            vm.TipoSangue = doacao.TipoSangue;
        }

        BindingContext = vm;
    }
}