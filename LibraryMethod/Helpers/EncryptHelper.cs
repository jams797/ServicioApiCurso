using ApiPolizasElectronicas.Models.Complementos;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMethod.Helpers
{
    public class EncryptHelper
    {
        public string EncKey {  get; set; }
        public string EncMacKey { get; set; }


        public string EncryptValue(string Value)
        {
            var Aes = new Encryptor<AesEngine, Sha256Digest>(Encoding.UTF8, Encoding.UTF8.GetBytes(EncKey), Encoding.UTF8.GetBytes(EncMacKey));
            return Aes.Encrypt(Value);
        }

        public string DencryptValue(string Value)
        {
            var Aes = new Encryptor<AesEngine, Sha256Digest>(Encoding.UTF8, Encoding.UTF8.GetBytes(EncKey), Encoding.UTF8.GetBytes(EncMacKey));
            return Aes.Decrypt(Value);
        }

    }
}
