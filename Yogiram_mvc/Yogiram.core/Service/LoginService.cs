using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Yogiram.core.Models;
using System.Web.Security;
using Yogiram.core.DataSource;
using Yogiram.core.Context;
using Yogiram.core.Mvc;
using Yogiram.core.Service;
using Yogiram.core.Handler;
using System.Security.Cryptography;
using System.Text;


namespace Yogiram.core.Service
{
    public class LoginService
    {
        public ContextContainer Context { get; set; }

        private CoreEntities db;

        public LoginService(ContextContainer Context)
        {
            this.Context = Context;
            db = Context.CoreEntities;
        }
        public UserDetails Authenticate(int? CompanyId, String Password, String UserName)
        {
            UserDetails User = null;

            User = AuthenticateOtherUser(CompanyId ?? 0, UserName, Password);

            if (User != null)
                return User;
            else
            {
                User = (from a in db.EmployeeMaster
                        where a.CompanyId == CompanyId && a.EmployeeCode.Equals(UserName)
                        select new UserDetails
                        {
                            CompanyId = a.CompanyId,
                            UserId = a.EmployeeId,
                            Password = a.Password,
                            UserName = a.EmployeeName,
                            PasswordExpiryDate = a.PassExpDate
                        }).FirstOrDefault();
            }

            if (User != null)
            {
                if (VerifyHash(Password, User.Password))
                {
                    return new UserDetails()
                    {
                        CompanyId = User.CompanyId,
                        UserId = User.UserId,
                        UserName = User.UserName,
                        PasswordExpired = User.PasswordExpiryDate != null && User.PasswordExpiryDate < DateTime.Now,
                    };
                }
                else
                {
                    //SaveLoginHistory(user.User.UserId, From, false);
                    User = null;
                    //db.SaveChanges();
                }
            }

            return User;
        }

        protected UserDetails AuthenticateOtherUser(int CompanyId, String UserName, String Password)
        {
            var User = (from a in db.AT_User
                        where a.UserName == UserName && a.Enabled &&
                        (a.CompanyId == 0 || (a.CompanyId == CompanyId && a.UserId < 0))
                        select new UserDetails()
                        {
                            UserName = a.UserName,
                            CompanyId = a.CompanyId ?? 0,
                            Password = a.Password,
                            //ExpiresOn = User.PasswordExpiryDate,
                            //PasswordExpired = user.PasswordExpiryDate != null && user.User.PasswordExpiryDate.Value < DateTime.Now,
                            UserId = a.UserId,
                        }).FirstOrDefault();

            if (User != null)
            {
                if (VerifyHash(Password, User.Password))
                {
                    return new UserDetails()
                    {
                        CompanyId = User.CompanyId,
                        UserId = User.UserId,
                        UserName = User.UserName
                    };
                }
                else
                {
                    //SaveLoginHistory(user.User.UserId, From, false);
                    User = null;
                    //db.SaveChanges();
                }
            }

            return User;
        }

        /* http://www.obviex.com/samples/hash.aspx */

        public static string ComputeHash(string password, byte[] saltBytes)
        {
            if (saltBytes == null)
            {
                saltBytes = new byte[6];
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                rng.GetNonZeroBytes(saltBytes);
            }

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(password);
            byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + saltBytes.Length];

            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            HashAlgorithm hash = HashAlgorithm.Create("SHA1");

            hash = new SHA1Managed();

            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);
            byte[] hashWithSaltBytes = new byte[hashBytes.Length + saltBytes.Length];

            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            return hashValue;
        }

        public static bool VerifyHash(string password, string hashValue)
        {
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            int hashSizeInBits = 160, hashSizeInBytes;

            hashSizeInBytes = hashSizeInBits / 8;

            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            byte[] saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];

            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            string expectedHashString = ComputeHash(password, saltBytes);

            return (hashValue == expectedHashString);
        }

    }
}
