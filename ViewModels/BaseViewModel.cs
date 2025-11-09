using System.Windows.Input;

namespace AppFioVermelho.ViewModels
{
    public class BaseViewModel : BaseNotifyViewModel
    {
        public ICommand BackCommand { get; set; }
        public BaseViewModel()
        {
            BackCommand = new Command(Back);
        }

        public async void Back()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async void OpenView(ContentPage page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public async void ShowInfo(string message)
        {
            await Application.Current.MainPage.DisplayAlert("Informação", message, "OK");
        }

        public async void ShowError(string message)
        {
            await Application.Current.MainPage.DisplayAlert("Erro", message, "OK");
        }

        public async Task<bool> ShowConfirm(string message)
        {
            return await Application.Current.MainPage.DisplayAlert("Confirmação", message, "Sim", "Não");
        }

    }
}
