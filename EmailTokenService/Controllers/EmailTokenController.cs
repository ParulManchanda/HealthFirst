using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace EmailTokenService.Controllers
{
    public class EmailTokenController : ApiController
    {
        private const int saltKeySize = 10;

        // POST api/<controller>
        public string Post([FromBody]String email)
        {
            // ideally this should be in async task but since this is not time consuming code so keeping it simple for now
            return getTokenForEmail(email);
        }

        private static string getTokenForEmail(string email)
        {
            // Bad email address return null- can also throw 400 bad request. Ideally this should be validated on client side
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            // 1. validate value is proper email
            bool isEmailvalid = validateEmail(email);
            if (!isEmailvalid)
            {
                return null;
            }

            // 2. Generate unique token - salt, time, emailid 
            var salt = generateSalt();
            if (salt == null || salt.Length == 0)
            {
                return null;
            }

            // 3. Generate unique token - salt, time, emailid 
            var tokenGenerated = generateToken(salt, email);
            if (string.IsNullOrWhiteSpace(tokenGenerated))
            {
                return null;
            }

            // 3. Return email & token
            return tokenGenerated;
        }

        private static bool validateEmail(string emailToValidate)
        {
 	        try
            {
                var addr = new MailAddress(emailToValidate);
                return String.Equals(addr.Address, emailToValidate, StringComparison.InvariantCultureIgnoreCase);
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        private static Byte[] generateSalt()
        {
            try 
            {
                var rng = RandomNumberGenerator.Create();
                Byte[] result = new Byte[saltKeySize];
                rng.GetBytes(result);
                return result;
            }
            catch 
            {
                return null;
            }          
        }

        private static string getSHA1Hash(string strToHash)
        {
            var sha1Obj = new SHA1CryptoServiceProvider();

            Byte[] bytesToHash = System.Text.Encoding.ASCII.GetBytes(strToHash);

            bytesToHash = sha1Obj.ComputeHash(bytesToHash);

            string strResult = "";

            foreach(Byte b in bytesToHash)
            {
                strResult += b.ToString("x2");
            }

            return strResult;
        }    

        private static string generateToken(Byte[] salt, string email)
        {
            try 
            {
                var currentTime = DateTime.Now;
                string strToken = String.Concat(email, salt.ToString(), currentTime);
                var token = getSHA1Hash(strToken);               
                return token;               
            }
            catch           
            {
                return null;
            }        
        }
    }
}