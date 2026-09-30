using AppRpgEtec.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AppRpgEtec.Services.Armas
{
    public class ArmaService : Request
    {
        private readonly Request _request;
        private readonly string _token;

        private const string ApiUrlBase =
            "https://rpgapi3ds2026-2-fsf6e4d0b2hjh7bc.mexicocentral-01.azurewebsites.net/Armas";


        public ArmaService(string token)
        {
            _token = token;

            _request = new Request();
        }


        public async Task<ObservableCollection<Arma>> GetArmasAsync()
        {
            string urlComplementar = "/GetAll";

            ObservableCollection<Arma> listaArmas =
                await _request.GetAsync<ObservableCollection<Arma>>(
                    ApiUrlBase + urlComplementar,
                    _token);

            return listaArmas;
        }


        public async Task<Arma> GetArmaAsync(int armaId)
        {
            string urlComplementar = $"/{armaId}";

            Arma arma =
                await _request.GetAsync<Arma>(
                    ApiUrlBase + urlComplementar,
                    _token);

            return arma;
        }


        public async Task<int> PostArmaAsync(Arma a)
        {
            return await _request.PostReturnIntAsync(
                ApiUrlBase,
                a,
                _token);
        }


        public async Task<int> PutArmaAsync(Arma a)
        {
            return await _request.PutAsync(
                ApiUrlBase,
                a,
                _token);
        }


        public async Task<int> DeleteArmaAsync(int armaId)
        {
            string urlComplementar = $"/{armaId}";

            return await _request.DeleteAsync(
                ApiUrlBase + urlComplementar,
                _token);
        }
    }
}
