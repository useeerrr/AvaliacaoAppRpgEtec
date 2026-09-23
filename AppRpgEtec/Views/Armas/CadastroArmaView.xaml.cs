using AppRpgEtec.ViewModels.Armas;

namespace AppRpgEtec.Views.Armas;

public partial class CadastroArmaView : ContentPage
{
	private CadastroArmaView cadViewModel;
	public CadastroArmaView()
	{
		InitializeComponent();

		cadViewModel = new CadastroArmaView();
		BindingContext = cadViewModel;
		Title = "Nova Arma";

	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        string id = Shell.Current.CurrentState.Location.OriginalString
            .Split("pId=")
            .LastOrDefault();

        if (int.TryParse(id, out int armaId))
        {
            await cadViewModel.CarregarArma(armaId);
            Title = "Editar Arma";
        }
    }

    private async Task CarregarArma(int armaId)
    {
        throw new NotImplementedException();
    }
}