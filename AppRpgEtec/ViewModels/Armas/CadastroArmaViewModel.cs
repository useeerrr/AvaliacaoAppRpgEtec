using AppRpgEtec.Models;
using AppRpgEtec.Services.Armas;
using AppRpgEtec.Services.Personagens;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppRpgEtec.ViewModels.Armas
{
    public class CadastroArmaViewModel : BaseViewModel
    {
        private ArmaService aService;
        private PersonagemService pService;

        public CadastroArmaViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);

            aService = new ArmaService(token);
            pService = new PersonagemService(token);

            _ = ObterPersonagens();

            SalvarCommand = new Command(SalvarArma);
        }

        public ICommand SalvarCommand { get; set; }


        private int id;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }


        private string nome;

        public string Nome
        {
            get => nome;
            set
            {
                nome = value;
                OnPropertyChanged(nameof(Nome));
            }
        }


        private int dano;

        public int Dano
        {
            get => dano;
            set
            {
                dano = value;
                OnPropertyChanged(nameof(Dano));
            }
        }


        private int personagemId;

        public int PersonagemId
        {
            get => personagemId;
            set
            {
                personagemId = value;
                OnPropertyChanged(nameof(PersonagemId));
            }
        }


        private Personagem personagemSelecionado;

        public Personagem PersonagemSelecionado
        {
            get => personagemSelecionado;

            set
            {
                if (value != null)
                {
                    personagemSelecionado = value;

                    OnPropertyChanged(nameof(PersonagemSelecionado));
                }
            }
        }


        public ObservableCollection<Personagem> Personagens
        {
            get;
            set;
        }


        public async Task ObterPersonagens()
        {
            try
            {
                Personagens = await pService.GetPersonagensAsync();

                OnPropertyChanged(nameof(Personagens));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Ops",
                    ex.Message,
                    "Ok");
            }
        }


        public async Task CarregarArma(int id)
        {
            try
            {
                Arma arma = await aService.GetArmaAsync(id);

                Id = arma.Id;

                Nome = arma.Nome;

                Dano = arma.Dano;

                PersonagemId = arma.PersonagemId;


                if (Personagens == null)
                {
                    await ObterPersonagens();
                }


                foreach (var personagem in Personagens)
                {
                    if (personagem.Id == arma.PersonagemId)
                    {
                        PersonagemSelecionado = personagem;

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Ops",
                    ex.Message,
                    "Ok");
            }
        }


        public async void SalvarArma()
        {
            try
            {
                Arma model = new Arma()
                {
                    Id = Id,
                    Nome = Nome,
                    Dano = Dano,
                    PersonagemId = PersonagemSelecionado.Id
                };


                if (model.Id == 0)
                {
                    await aService.PostArmaAsync(model);
                }
                else
                {
                    await aService.PutArmaAsync(model);
                }


                await Application.Current.MainPage.DisplayAlert(
                    "Mensagem",
                    "Dados salvo com sucesso",
                    "Ok");


                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Ops!",
                    ex.Message,
                    "Ok");
            }
        }
    }
}
