using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMethod.Helpers
{
    public class ValidateRequest
    {
        public bool ValidateIdUser(int Id)
        {
            /*if( Id > 0)
            {
                return true;
            } else
            {
                return false;
            }*/
            return (Id > 0);
        }
    }
}
