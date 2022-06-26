
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAEA_LAB14_HttpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            /* var getPer = await GetPersonaAsync();
             foreach (var item in getPer)
             {
                 Console.WriteLine(item.FullName);
             }
            */

            Task.Run(() => GetPersonaAsync());

            Console.WriteLine("------------");
            Task.Run(()=> PostPersonaAsync());


            Console.Read();
        }

        private static async Task<List<GetPersonaResponse>> GetPersonaAsync()
        {
            HttpClient client = new HttpClient();
            var getPersonas = new List<GetPersonaResponse>();
            var urlBase = "https://localhost:44382/";
            client.BaseAddress = new Uri(urlBase);
            var url = string.Concat(urlBase, "api/People/Get");

            var response = client.GetAsync(url).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                getPersonas = JsonConvert.DeserializeObject<List<GetPersonaResponse>>(result);   
            }

            foreach (var item in getPersonas)
            {
                Console.WriteLine(item.FullName);
            }
            return getPersonas;
        }

        private static async Task PostPersonaAsync()
        {

            HttpClient client = new HttpClient();
            var urlBase = "https://localhost:44382/";
            client.BaseAddress = new Uri(urlBase);
            var url = string.Concat(urlBase, "api/People/Insert");

            var model = new PostPersonaRequest
            {
                FirstName = "Freddy",
                LastName = "Espinoza"
            };

            //Te va a enviar
            var request = JsonConvert.SerializeObject(model);
            //Como
            var content = new StringContent(request, Encoding.UTF8, "application/json");

            var response = client.PostAsync(url, content).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Creado correctamente");

                //Si tuviera una respuesta en postman 
                /*
                 * var result = await response.Content.ReadAsStringAsync();
                 * var appoitment = JsonConvert.DeserializeObject<AppoitmentResponse>(result);
                 * Console.WriteLine(appoitment.message);
                 * Console.WriteLine(appoitment.messageEN);
                 */
            }
            else
            {
                Console.WriteLine("Error");
            }

        }

        //No asincrona
        private static void PostPersona()
        {

            HttpClient client = new HttpClient();
            var urlBase = "https://localhost:44382/";
            client.BaseAddress = new Uri(urlBase);
            var url = string.Concat(urlBase, "api/People/Insert");

            var model = new PostPersonaRequest
            {
                FirstName = "Geral",
                LastName = "Balbin"
            };

            //Te va a enviar
            var request = JsonConvert.SerializeObject(model);
            //Como
            var content = new StringContent(request, Encoding.UTF8, "application/json");

            var response = client.PostAsync(url, content).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Creado correctamente");
            }
            else
            {
                Console.WriteLine("Error");
            }

        }

    }
}
