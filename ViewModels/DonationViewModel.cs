using AppFioVermelho.Models;
using AppFioVermelho.Services;
using AppFioVermelho.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppFioVermelho.ViewModels
{
    public class DonationViewModel : BaseViewModel
    {
        private readonly DonationService donationService;
        private readonly DonationLogService donationLogService;
        private readonly CurrentUserService currentUserService;

        public DonationViewModel()
        {
            donationService = new DonationService(new DatabaseService());
            donationLogService = new DonationLogService(new DatabaseService());
            currentUserService = CurrentUserService.Instance;

            SalvarDoacaoCommand = new Command(SalvarDoacao);
            EditarDoacaoCommand = new Command<Donation>(EditarDoacao);
            ExcluirDoacaoCommand = new Command<Donation>(ExcluirDoacao);
            SelecionarDoacaoCommand = new Command<Donation>(SelecionarDoacao);
            AbrirEditarDoacaoCommand = new Command<Donation>(AbrirEditarDoacao);
            VerHistoricoCommand = new Command(VerHistorico);
            ListarDoacoesCommand = new Command(ListarDoacoes);


            Doacoes = new ObservableCollection<Donation>();
            Historico = new ObservableCollection<DonationLog>();

            CarregarDoacoes();
        }

        private string _instituicao;
        public string Instituicao
        {
            get { return _instituicao; }
            set { _instituicao = value; OnPropertyChanged(); }
        }

        private string _tipoSangue;
        public string TipoSangue
        {
            get { return _tipoSangue; }
            set { _tipoSangue = value; OnPropertyChanged(); }
        }

        private Donation _doacaoSelecionada;
        public Donation DoacaoSelecionada
        {
            get { return _doacaoSelecionada; }
            set { _doacaoSelecionada = value; OnPropertyChanged(); }
        }
        
        public Donation DoacaoExistente { get; set; }

        private bool _mostrarHistorico;
        public bool MostrarHistorico
        {
            get => _mostrarHistorico;
            set { _mostrarHistorico = value; OnPropertyChanged(); }
        }

        public ICommand AlternarHistoricoCommand { get; set; }

        public ObservableCollection<Donation> Doacoes { get; set; }
        public ObservableCollection<DonationLog> Historico { get; set; }

        public ICommand SalvarDoacaoCommand { get; set; }
        public ICommand AbrirEditarDoacaoCommand { get; set; }
        public ICommand EditarDoacaoCommand { get; set; }
        public ICommand ExcluirDoacaoCommand { get; set; }
        public ICommand SelecionarDoacaoCommand { get; set; }
        public ICommand VerHistoricoCommand { get; set; }
        public ICommand ListarDoacoesCommand { get; set;  }

        private void CarregarDoacoes()
        {
            Doacoes.Clear();
            foreach (var d in donationService.GetDoacoes())
                Doacoes.Add(d);
        }

        private void ListarDoacoes()
        {
            CarregarDoacoes();
        }

        private void SalvarDoacao()
        {
            if (string.IsNullOrEmpty(Instituicao) || string.IsNullOrEmpty(TipoSangue))
            {
                ShowError("Preencha todos os campos!");
                return;
            }

            if (DoacaoExistente != null)
            {
                DoacaoExistente.Instituicao = Instituicao;
                DoacaoExistente.TipoSangue = TipoSangue;
                donationService.Atualizar(DoacaoExistente);
                ShowInfo("Doação atualizada com sucesso!");
            }
            else
            {
                Donation donation = new Donation
                {
                    Instituicao = Instituicao,
                    TipoSangue = TipoSangue,
                    Data = DateTime.Now
                };
                donationService.Inserir(donation);
                ShowInfo("Doação cadastrada com sucesso!");
            }

            LimparCampos();
            OpenView(new AdminView());
        }

        private void AbrirEditarDoacao(Donation doacao)
        {
            if (doacao == null) return;

            OpenView(new DonationView(doacao));
        }

        private void EditarDoacao(Donation doacao)
        {
            if (doacao == null) return;

            doacao.Instituicao = Instituicao;
            doacao.TipoSangue = TipoSangue;

            donationService.Atualizar(doacao);
            CarregarDoacoes();

            ShowInfo("Doação atualizada com sucesso!");
            LimparCampos();
            OpenView(new AdminView());
        }

        private void ExcluirDoacao(Donation doacao)
        {
            if (doacao == null)
            {
                ShowError("Doação inválida!");
                return;
            }

            donationService.Deletar(doacao);
            CarregarDoacoes();
            ShowInfo("Doação excluída com sucesso!");
        }

        private async void SelecionarDoacao(Donation doacao)
        {
            if (doacao == null)
            {
                ShowError("Doação inválida!");
                return;
            }

            if (!currentUserService.IsLoggedIn)
            {
                ShowError("Nenhum usuário logado!");
                return;
            }

            bool confirmado = await ShowConfirm(
                $"Deseja selecionar a doação de {doacao.TipoSangue} na {doacao.Instituicao}?"
            );
            if (!confirmado) return;

            var user = currentUserService.CurrentUser;

            DonationLog log = new DonationLog
            {
                UserId = user.Id,
                DonationId = doacao.Id,
                DateTime = DateTime.Now
            };

            donationLogService.Inserir(log);

            ShowInfo($"Doação selecionada!\nData/Hora: {DateTime.Now}");
        }

        private void LimparCampos()
        {
            Instituicao = string.Empty;
            TipoSangue = string.Empty;
        }

        private void VerHistorico()
        {
            if (!currentUserService.IsLoggedIn)
            {
                ShowError("Nenhum usuário logado!");
                return;
            }

            var user = currentUserService.CurrentUser;

            if (user.IsAdmin)
            {
                foreach (var log in donationLogService.GetLogs())
                    Historico.Add(log);
            }
            else
            {
                foreach (var log in donationLogService.GetLogsPorUsuario(user.Id))
                    Historico.Add(log);
            }

            if (Historico.Count == 0)
            {
                ShowInfo("Nenhum histórico encontrado!");
                return;
            }

            MostrarHistorico = !MostrarHistorico;
        }
    }
}
