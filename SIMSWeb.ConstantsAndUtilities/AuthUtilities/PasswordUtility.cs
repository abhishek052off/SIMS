using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.ConstantsAndUtilities.AuthUtilities
{
    public static class PasswordUtility
    {
        public static string HashPassword(string password, int workFactor = 10)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor);
        }

        public static bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            //var hashedOfEnteredPwd = BCrypt.Net.BCrypt.HashPassword(enteredPassword, 10);

            //return hashedOfEnteredPwd == hashedPassword;

            return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
        }
    }
}
