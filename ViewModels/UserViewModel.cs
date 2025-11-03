using AppFioVermelho.Models;
using AppFioVermelho.Services;
using AppFioVermelho.Views;
using System.Windows.Input;

namespace AppFioVermelho.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private readonly UserService userService;
        public UserViewModel()
        {
            userService = new UserService(new DatabaseService());

            CadastrarCommand = new Command(Cadastrar);
            LoginCommand = new Command(Login);
        }

        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; OnPropertyChanged(); }
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

        public ICommand CadastrarCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        public void Cadastrar()
        {

            User user = new User
            {
                Nome = Nome,
                Email = Email,
                Senha = Senha,
                IsAdmin = IsAdmin
            };

            userService.Inserir(user);

            ShowInfo("Cadastro realizado com sucesso!");
            Back(); 
        }
        public void Login()
        {
            User user = userService.GetUsuarioPorEmail(Email);

            if (user != null && user.Senha == Senha)
            {
                ShowInfo("Login realizado com sucesso!");

                if (user.IsAdmin)
                {
                    OpenView(new AdminView());
                }
                else
                {
                    OpenView(new PrincipalView());
                }
            }
            else
            {
                ShowError("E-mail ou senha incorretos!");
            }
        }
    }
}
