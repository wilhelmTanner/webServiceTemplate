using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using ServicioDemo;
using CountryService;
using static CountryService.CountryInfoServiceSoapTypeClient;

namespace Tanner.Template.Base.DataAccess.Oracle
{
    public class TemplatePaisesRepository
    {

        public async Task<string> GetName(string name)
        {
            var client = new SOAPDemoSoapClient();
            var response = await client.GetByNameAsync(name);
            return string.Empty;
        }

        public async Task<string> GetCountryByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException("El código del país no puede estar vacío.", nameof(code));
            }

            try
            {
                CountryInfoServiceSoapTypeClient client = new CountryInfoServiceSoapTypeClient(new EndpointConfiguration());

                var request = await client.CountryNameAsync(code);

                if (request?.Body == null || string.IsNullOrEmpty(request.Body.CountryNameResult))
                {
                    return await Task.FromResult("Nombre de país no encontrado o inválido.");
                }

                string countryName = request.Body.CountryNameResult;
                return await Task.FromResult(countryName);
            }
            catch (Exception ex)
            {
                return await Task.FromResult("Se produjo un error al obtener el nombre del país: " + ex.Message);
            }
        }

        public async Task<tContinent[]> GetContinents()
        {

            try
            {
                CountryInfoServiceSoapTypeClient client = new CountryInfoServiceSoapTypeClient(new EndpointConfiguration());

                var request = await client.ListOfContinentsByCodeAsync();
          
                if (request?.Body == null ||  request.Body.ListOfContinentsByCodeResult.Count() == 0)
                {
                    return Array.Empty<tContinent>();
                }

                tContinent[] listOfContinents = request.Body.ListOfContinentsByCodeResult;
                return await Task.FromResult(listOfContinents);
            }
            catch (Exception ex)
            {
                return Array.Empty<tContinent>();
            }
        }

    }
}
