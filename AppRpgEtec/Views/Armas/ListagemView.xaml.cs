using AppRpgEtec.ViewModels.Armas;
using AppRpgEtec.ViewModels.Personagens;

namespace AppRpgEtec.Views.Armas;

public partial class ListagemView : ContentPage
{
    private ListagemArmaViewModel viewModel;
    public ListagemView()
	{
		InitializeComponent();
		viewModel = new ListagemArmaViewModel();
		BindingContext = viewModel;

		Title = "Lista de Armas";


	}

    protected override void OnAppearing()
    {
		base.OnAppearing();

		_ = viewModel.ObterArmas();
    }
}