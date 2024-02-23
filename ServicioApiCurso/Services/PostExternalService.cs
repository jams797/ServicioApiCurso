using ServicioApiCurso.Helpers;
using ServicioApiCurso.Models;

namespace ServicioApiCurso.Services
{
    public class PostExternalService
    {
        MethodsHelper MethodsHep = new MethodsHelper();
        public async Task<List<PostModelReqService>> GetPost() {
            string RespService = await MethodsHep.GetServiceExternal(EndPointHelper.Post);
            return PostModelReqService.FromJson(RespService);
        }

        public async Task<string> PublishPost(PublishPostModelReqService ReqModel)
        {
            string RespService = await MethodsHep.PostServiceExternal(EndPointHelper.Post, null, ReqModel.ToJson());
            return RespService;
        }
    }
}
