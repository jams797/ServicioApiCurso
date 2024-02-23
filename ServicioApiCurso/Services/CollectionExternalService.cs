using ServicioApiCurso.Helpers;

namespace ServicioApiCurso.Services
{
    public class CollectionExternalService
    {
        MethodsHelper MethodsHep = new MethodsHelper();
        public async Task<string> GetCollection()
        {
            string RespService = await MethodsHep.GetServiceExternal(EndPointHelper.Colletion);
            return RespService;
        }
    }
}
