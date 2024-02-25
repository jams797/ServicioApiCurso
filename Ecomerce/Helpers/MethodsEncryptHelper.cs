using LibraryMethod.Helpers;

namespace Ecomerce.Helpers
{
    public class MethodsEncryptHelper
    {
        public string EncryptPassword(string value)
        {
            var SectionKey = (new ConfigurationBuilder()).AddJsonFile("appsettings.json").Build().GetSection("passwordEncrypt");
            var EncryptH = new EncryptHelper
            {
                EncKey = SectionKey.GetValue<string>("key"),
                EncMacKey = SectionKey.GetValue<string>("macKey"),
            };

            return EncryptH.EncryptValue(value);
        }

        public string DencryptPassword(string value)
        {
            var SectionKey = (new ConfigurationBuilder()).AddJsonFile("appsettings.json").Build().GetSection("passwordEncrypt");
            var EncryptH = new EncryptHelper
            {
                EncKey = SectionKey.GetValue<string>("key"),
                EncMacKey = SectionKey.GetValue<string>("macKey"),
            };

            return EncryptH.DencryptValue(value);
        }

        public string EncryptToken(string value)
        {
            var SectionKey = (new ConfigurationBuilder()).AddJsonFile("appsettings.json").Build().GetSection("tokenSession");
            var EncryptH = new EncryptHelper
            {
                EncKey = SectionKey.GetValue<string>("key"),
                EncMacKey = SectionKey.GetValue<string>("macKey"),
            };

            return EncryptH.EncryptValue(value);
        }

        public string DencryptToken(string value)
        {
            var SectionKey = (new ConfigurationBuilder()).AddJsonFile("appsettings.json").Build().GetSection("tokenSession");
            var EncryptH = new EncryptHelper
            {
                EncKey = SectionKey.GetValue<string>("key"),
                EncMacKey = SectionKey.GetValue<string>("macKey"),
            };

            return EncryptH.DencryptValue(value);
        }
    }
}
