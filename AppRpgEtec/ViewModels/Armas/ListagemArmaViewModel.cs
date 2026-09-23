using AppRpgEtec.Models;
using AppRpgEtec.Services.Armas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace AppRpgEtec.ViewModels.Armas
{
 
        public class ListagemArmaViewModel : BaseViewModel
        {
            private ArmaService aService;

            public ObservableCollection<Arma> Armas { get; set; }

            public ListagemArmaViewModel()
            {
                string token = Preferences.Get("UsuarioToken", string.Empty);

                aService = new ArmaService(token);
                Armas = new ObservableCollection<Arma>();

                _ = ObterArmas();

                RemoverArmaCommand = new Command<Arma>(async (Arma a) =>
                {
                    await RemoverArma(a);
                });
            }

            public ICommand RemoverArmaCommand { get; }

            public async Task ObterArmas()
            {
                try
                {
                    Armas = await aService.GetArmasAsync();
                    OnPropertyChanged(nameof(Armas));
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage
                        .DisplayAlertAsync("Ops",
                        ex.Message + " Detalhes: " + ex.InnerException, "Ok");
                }
            }

            public async Task RemoverArma(Arma a)
            {
                try
                {
                    if (await Application.Current.MainPage
                        .DisplayAlertAsync("Confirmação",
                        $"Confirma a remoção de {a.Nome}?",
                        "Sim", "Não"))
                    {
                        await aService.DeleteArmaAsync(a.Id);

                        await Application.Current.MainPage
                            .DisplayAlertAsync("Mensagem",
                            "Arma removida com sucesso!", "Ok");

                        _ = ObterArmas();
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage
                        .DisplayAlertAsync("Ops",
                        ex.Message + " Detalhes: " + ex.InnerException, "Ok");
                }
            }
        private Arma armaSelecionada;

        public Arma ArmaSelecionada
        {
            get { return armaSelecionada; }
            set
            {
                if (value != null)
                {
                    armaSelecionada = value;

                    Shell.Current.GoToAsync($"cadArmaView?pId={armaSelecionada.Id}");
                }
            }
        }
    }
    }


