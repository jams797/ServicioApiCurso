using Microsoft.AspNetCore.Mvc;
using ServicioApiCurso.Models;
using ServicioApiCurso.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServicioApiCurso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestServiceExternalController : ControllerBase
    {
        // GET: api/<TestServiceExternalController>
        [HttpGet]
        public async Task<dynamic> Get()
        {
            try
            {
                /*HttpClient HttpC = new HttpClient();
                HttpC.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpC.Timeout = TimeSpan.FromSeconds(10);
                //HttpC.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/posts");

                var BodySend = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("user", "abc"),
                    new KeyValuePair<string, string>("pass", "1234"),
                };
                var contenSend = new FormUrlEncodedContent(BodySend);

                var resp = await HttpC.PostAsync("https://jsonplaceholder.typicode.com/posts", contenSend);

                var body = await resp.Content.ReadAsStringAsync();

                var jsonBody = PostModelReqService.FromJson(body);

                return jsonBody;*/

                return await (new PostExternalService()).GetPost();

                //return await (new CollectionExternalService()).GetCollection();

            } catch (TaskCanceledException ex)
            {
                return "Tiempo excedido";
            } catch (Exception ex)
            {
                return "Error general";
            }
        }

        // GET api/<TestServiceExternalController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TestServiceExternalController>
        [HttpPost]
        public async Task<dynamic> Post([FromBody] PublishPostModelReqService ModelReq)
        {
            return await (new PostExternalService()).PublishPost(ModelReq);
        }

        // PUT api/<TestServiceExternalController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestServiceExternalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
