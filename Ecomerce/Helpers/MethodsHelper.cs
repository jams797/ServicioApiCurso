using Ecomerce.Models.General;
using ServicioApiCurso.Helpers;

namespace Ecomerce.Helpers
{
    public class MethodsHelper
    {
        public string ConvertDateTimeToString(DateTime dateTime)
        {
            var dateTimeFormat = (new ConfigurationBuilder()).AddJsonFile("appsettings.json").Build().GetSection("formatDateTime").Value;
            return dateTime.ToString(dateTimeFormat);
        }

        public DateTime ConvertStringToDateTime(string dateTime)
        {
            var dateTimeFormat = (new ConfigurationBuilder()).AddJsonFile("appsettings.json").Build().GetSection("formatDateTime").Value;
            return DateTime.ParseExact(dateTime, dateTimeFormat, System.Globalization.CultureInfo.InvariantCulture);
        }

        public string CreateTokenSesion(int userId)
        {
            var secondsValidate = (new ConfigurationBuilder()).AddJsonFile("appsettings.json").Build().GetSection("tokenSession").GetValue<int>("secondsValidate");
            var ModelTokenSesion = new TokenSessionModel
            {
                UserId = userId,
                DateExpired = ConvertDateTimeToString(DateTime.Now.AddSeconds(secondsValidate)),
            };

            var EncryptMet = new MethodsEncryptHelper();
            return EncryptMet.EncryptToken(ModelTokenSesion.ToJson());
        }

        public string? ValidateTokenSesion(string token)
        {
            try
            {
                var ModelSesion = GetModelSesionByToken(token);

                if (DateTime.Now.CompareTo(ConvertStringToDateTime(ModelSesion.DateExpired)) < 0)
                {
                    return null;
                } else
                {
                    return MessageHelper.TokenSesionErrorExpired;
                }
            } catch (Exception ex)
            {
                return MessageHelper.TokenSesionErrorValidate;
            }
        }

        public TokenSessionModel? GetModelSesionByToken(string token)
        {
            var EncryptMet = new MethodsEncryptHelper();
            var ModelSesionString = EncryptMet.DencryptToken(token);

            var ModelSesion = TokenSessionModel.FromJson(ModelSesionString);

            return ModelSesion;
        }
    }
}
