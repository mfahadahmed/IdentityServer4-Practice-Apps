using IdentityModel;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        private static async Task Main()
        {
            //Console.WriteLine("Calling Discovery Document");
            ////discover endpoints from metadata

            
            //var disco = await new HttpClient().GetDiscoveryDocumentAsync("https://localhost:5000");
            //if (disco.IsError)
            //{
            //    Console.WriteLine(disco.Error);
            //    Console.ReadLine();
            //    return;
            //}

            //Console.WriteLine(disco.Json);
            //Console.WriteLine("\n\n");

            Console.WriteLine("Requesting Client Credentials Token");
            
            // request token
            var tokenResponse = await new HttpClient().RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "http://localhost:5000/connect/token",
                ClientId = "test.transmission.api.new.secret",
                ClientSecret = "4drwpqZnWkNSABp8PUNA"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                Console.ReadLine();
                return;
            }

            Console.WriteLine(tokenResponse.AccessToken);

            //Console.WriteLine(tokenResponse.AccessToken);
            //Console.WriteLine("\n\nCalling APIs with Client 1");

            await CallApi("http://localhost:5000/api/v1/test/get1", tokenResponse.AccessToken);
            //await CallApi("http://localhost:5000/api/v1/test/get2", tokenResponse.AccessToken);

            //tokenResponse = await new HttpClient().RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            //{
            //    Address = "http://localhost:5000/connect/token",
            //    ClientId = "sparkpost.api.cc.client",
            //    ClientSecret = "secret"
            //});

            //if (tokenResponse.IsError)
            //{
            //    Console.WriteLine(tokenResponse.Error);
            //    return;
            //}

            //Console.WriteLine(tokenResponse.AccessToken);
            //Console.WriteLine("\n\nCalling APIs with Client 2");

            //await CallApi("http://localhost:5000/api/v1/test/get1", tokenResponse.AccessToken);
            //await CallApi("http://localhost:5000/api/v1/test/get2", tokenResponse.AccessToken);

            Console.ReadLine();
        }

        public static async Task CallApi(string url, string accessToken)
        {
            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(accessToken);

            Console.WriteLine("\nCalling API: " + url);

            var response = await apiClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
        }
    }
}
