using BIVN_PACKING.Entitis;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BIVN_PACKING.DAL
{
    public class Database
    {
        public Users CheckLogin(string username, string password)
        {
            BIVNEntities _db = new BIVNEntities();
            var Login = _db.Users.Where(x => x.USER_NAME == username && x.PASSWORD == password).FirstOrDefault();
            return Login;
        }
        public Users CheckLogin(Users user)
        {
            BIVNEntities _db = new BIVNEntities();
            var Login = _db.Users.Where(x => x.USER_NAME == user.USER_NAME && x.PASSWORD == user.PASSWORD).FirstOrDefault();
            return Login;
        }
        public Produce SerialCheck (Produce produce)
        {
            BIVNEntities _db = new BIVNEntities();
            var pro = _db.Produce.Where(x => x.BOXID == produce.BOXID && x.MODEL == produce.MODEL && x.WO == produce.WO).FirstOrDefault();
            return pro;
        }
        public static string GetRunningVersion()
        {
            try
            {
                return ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

    }
}
