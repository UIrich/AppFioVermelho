using AppFioVermelho.Models;
using AppFioVermelho.Services;
using AppFioVermelho.Views;
using System.Windows.Input;

namespace AppFioVermelho.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private readonly UserService userService;
        private readonly CurrentUserService currentUserService;
        public DonationViewModel DonationVM { get; set; }


        public UserViewModel()
        {
            DonationVM = new DonationViewModel(this);

            userService = new UserService(new DatabaseService());
            currentUserService = CurrentUserService.Instance;

            AbrirCadastroUsuarioCommand = new Command(AbrirCadastroUsuario);
            CadastrarCommand = new Command(Cadastrar);
            LoginCommand = new Command(Login);
            SairCommand = new Command(Sair);
            AbrirListaDoacoesCommand = new Command(AbrirListaDoacoes);
            AbrirCadastroDoacaoCommand = new Command(AbrirCadastroDoacao);

            AtualizarNomeUsuario();
        }

        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; OnPropertyChanged(); }
        }

        private string _cpf;
        public string Cpf
        {
            get { return _cpf; }
            set { _cpf = value; OnPropertyChanged(); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }

        private string _senha;
        public string Senha
        {
            get { return _senha; }
            set { _senha = value; OnPropertyChanged(); }
        }

        private bool _isAdmin;
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set { _isAdmin = value; OnPropertyChanged(); }
        }

        private string _nomeUsuarioLogado;
        public string NomeUsuarioLogado
        {
            get { return _nomeUsuarioLogado; }
            set { _nomeUsuarioLogado = value; OnPropertyChanged(); }
        }

        public ICommand AbrirCadastroUsuarioCommand { get; set; }
        public ICommand CadastrarCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand SairCommand { get; set; }
        public ICommand AbrirCadastroDoacaoCommand { get; set; }
        public ICommand AbrirListaDoacoesCommand { get; set; }


        public void AbrirCadastroUsuario()
        {
            LimparCampos();
            OpenView(new SigninView());
        }

        public void Cadastrar()
        {
            if (string.IsNullOrWhiteSpace(Nome) ||
                string.IsNullOrWhiteSpace(Cpf) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Senha))
            {
                ShowError("Preencha todos os campos!");
                return;
            }

            User user = new User
            {
                Nome = Nome,
                Cpf = Cpf,
                Email = Email,
                Senha = Senha,
                IsAdmin = IsAdmin
            };

            userService.Inserir(user);

            ShowInfo("Cadastro realizado com sucesso!");
            LimparCampos();
            Back(); 
        }
        public void AtualizarNomeUsuario()
        {
            if (currentUserService.CurrentUser != null)
            {
                NomeUsuarioLogado = currentUserService.CurrentUser.Nome;
            }
            else
            {
                NomeUsuarioLogado = "Usuário não logado";
            }
        }
        public void Login()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Senha))
            {
                ShowError("Preencha todos os campos!");
                return;
            }

            User user = userService.GetUsuarioPorEmail(Email);

            if (user != null && user.Senha == Senha)
            {
                currentUserService.SetUser(user);

                ShowInfo("Login realizado com sucesso!");

                if (user.IsAdmin)
                {
                    OpenView(new AdminView());
                }
                else
                {
                    OpenView(new DonatorSelectView());
                }
            }
            else
            {
                ShowError("E-mail ou senha incorretos!");
                LimparCampos();
            }
        }

        private void Sair()
        {
            currentUserService.ClearUser();
            AtualizarNomeUsuario();
            LimparCampos();
            OpenView(new LoginView());
        }

        public void AbrirCadastroDoacao()
        {
            OpenView(new DonationView());
        }

        public void LimparCampos()
        {
            Nome = string.Empty;
            Cpf = string.Empty;
            Email = string.Empty;
            Senha = string.Empty;
            IsAdmin = false;
        }

        public void AbrirListaDoacoes()
        {
            OpenView(new AdminDonationView());
        }
    }
}
