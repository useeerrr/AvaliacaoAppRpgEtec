using AppRpgEtec.Models;
using AppRpgEtec.Services.Armas;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppRpgEtec.ViewModels.Armas
{
    public class ListagemArmaViewModel : BaseViewModel
    {
        private ArmaService aService;

        public ObservableCollection<Arma> Armas { get; set; }


        public ListagemArmaViewModel()
        {
            string token = Preferences.Get(
                "UsuarioToken",
                string.Empty);

            aService = new ArmaService(token);

            Armas = new ObservableCollection<Arma>();


            _ = ObterArmas();


            RemoverArmaCommand = new Command<Arma>(
                async (Arma a) =>
                {
                    await RemoverArma(a);
                });


            NovoArmaCommand = new Command(
                async () =>
                {
                    await Shell.Current.GoToAsync(
                        "cadArmaView");
                });


            EditarArmaCommand = new Command<Arma>(
                async (Arma a) =>
                {
                    await Shell.Current.GoToAsync(
                        $"cadArmaView?pId={a.Id}");
                });
        }


        public ICommand RemoverArmaCommand { get; }

        public ICommand NovoArmaCommand { get; }

        public ICommand EditarArmaCommand { get; }


        public async Task ObterArmas()
        {
            try
            {
                Armas = await aService.GetArmasAsync();

                OnPropertyChanged(nameof(Armas));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Ops",
                    ex.Message + " Detalhes: " + ex.InnerException,
                    "Ok");
            }
        }


        public async Task RemoverArma(Arma a)
        {
            try
            {
                if (await Application.Current.MainPage.DisplayAlert(
                    "Confirmação",
                    $"Confirma a remoção de {a.Nome}?",
                    "Sim",
                    "Não"))
                {
                    await aService.DeleteArmaAsync(a.Id);


                    await Application.Current.MainPage.DisplayAlert(
                        "Mensagem",
                        "Arma removida com sucesso!",
                        "Ok");


                    await ObterArmas();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Ops",
                    ex.Message + " Detalhes: " + ex.InnerException,
                    "Ok");
            }
        }
    }
}

