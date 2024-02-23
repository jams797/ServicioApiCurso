using System.Text;
using static System.Net.WebRequestMethods;

namespace ServicioApiCurso.Helpers
{
    public class MethodsHelper
    {

        public async Task<string> GetServiceExternal(string EndPoint, Dictionary<string, string>? HeadersAdd = null)
        {
            try
            {
                string Url = (new ConfigurationBuilder()).AddJsonFile("appsettings.json").Build().GetSection("serviceUrl").Value;
                Url += EndPoint;

                return await GeneralServiceExternal("GET", Url, HeadersAdd);
            } catch (Exception ex) {
                //
            }
            return null;

        }

        public async Task<string> PostServiceExternal(string EndPoint, Dictionary<string, string>? HeadersAdd = null, string JsonData = "{}")
        {
            try
            {
                string Url = (new ConfigurationBuilder()).AddJsonFile("appsettings.json").Build().GetSection("serviceUrl").Value;
                Url += EndPoint;

                return await GeneralServiceExternal("POST", Url, HeadersAdd, JsonData);
            }
            catch (Exception ex)
            {
                //
            }
            return null;

        }

        public async Task<string> GeneralServiceExternal(string TypeMethod, string Url, Dictionary<string, string>? HeadersAdd = null, string JsonData = "{}")
        {
            HttpClient HttpClient = new HttpClient();

            // Tiempo recomendable: 3 - 6
            // Tiempo advertencia: 6 - 9
            // Tiempo critico: mayor a 10
            HttpClient.Timeout = TimeSpan.FromSeconds(10);
            try
            {
                HttpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
                if (HeadersAdd != null)
                {
                    foreach (var item in HeadersAdd)
                    {
                        HttpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }

                var ContentBody = new StringContent(JsonData, Encoding.UTF8);

                HttpResponseMessage? Response = null;
                switch (TypeMethod)
                {
                    case "GET":
                        Response = await HttpClient.GetAsync(Url);
                        break;
                    case "POST":
                        Response = await HttpClient.PostAsync(Url, ContentBody);
                        break;
                }

                /*if(Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //
                }*/
                if (Response != null)
                {
                    if (Response.IsSuccessStatusCode)
                    {
                        return await Response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                //
            }
            return null;
        }
    }
}
