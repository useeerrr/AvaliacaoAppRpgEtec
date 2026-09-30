using AppRpgEtec.ViewModels.Armas;

namespace AppRpgEtec.Views.Armas;

[QueryProperty(nameof(ArmaId), "pId")]
public partial class CadastroArmaView : ContentPage
{
    private CadastroArmaViewModel cadViewModel;

    public string ArmaId
    {
        set
        {
            if (int.TryParse(value, out int id))
            {
                Title = "Editar Arma";

                _ = cadViewModel.CarregarArma(id);
            }
        }
    }

    public CadastroArmaView()
    {
        InitializeComponent();

        cadViewModel = new CadastroArmaViewModel();

        BindingContext = cadViewModel;

        Title = "Nova Arma";
    }
}