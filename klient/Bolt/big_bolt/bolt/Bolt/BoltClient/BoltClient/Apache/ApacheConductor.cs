using System;
using System.Net;
using System.IO;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
//using System.Net.Security;

namespace BoltClient
{
    class ApacheConductor
    {
        WebResponse response = null;
        //private const string serverAddr = "https://blackbox.next/";
        private const string serverAddr = "https://10.42.43.1/";
        //private const string username = "newmen";
        //private const string password = "1234567";
        private const string username = "usersukabolt";
        private const string password = "tralala";

        public ApacheConductor()
        {
            ServicePointManager.CertificatePolicy = new AcceptAllCertificatePolicy();
        }

        private void Connect(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(serverAddr + url);
                request.Timeout = 1000; // 5 sec

                NetworkCredential nwCredential = new NetworkCredential(username, password);
                request.Credentials = nwCredential;
                request.PreAuthenticate = true;

                response = request.GetResponse();
            }
            catch (Exception e)
            {
                throw new DisconnectException(e);
            }
            
        }

        public int CheckState()
        {
            Connect("status.php?howareyou=sava");

            string rd = "";
            try {
                Stream stream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(stream, Encoding.UTF8);

                rd = readStream.ReadToEnd();
            }
            catch (Exception e)
            {
                throw new DisconnectException(e);
            }

            if (rd == "ja!") return 1;
            else if (rd == "goend") return 2;
            return 0;
        }

        public void SendBattaryState()
        {
            PowerStatus ps = SystemInformation.PowerStatus;
            int percent = (int)(ps.BatteryLifePercent * 100);

            Connect("battery.php?lifepercent=" + percent);
        }

        public void SendShutDownNow()
        {
            Connect("shutdown.php?shutdown=now");
        }

        public void SendShutUp()
        {
            Connect("shutdown.php?shutdown=up");
        }

        internal class AcceptAllCertificatePolicy : ICertificatePolicy
        {
            public bool CheckValidationResult(ServicePoint sPoint, X509Certificate cert,
            WebRequest wRequest, int certProb)
            {
                //if (cert.Subject == "CN=blackbox.next, OU=Secure Center, O=Secure" &&
                //    cert.GetExpirationDateString() == "18.10.2019 12:09:36" &&
                //    cert.GetEffectiveDateString() == "20.10.2009 12:09:36" &&
                //    cert.GetSerialNumberString() == "00EB6F31C5B453B8AE") return true;
                if (cert.Subject == "CN=supermega, OU=Secure Center, O=Secure" &&
                    cert.GetExpirationDateString() == "21.10.2019 16:50:51" &&
                    cert.GetEffectiveDateString() == "23.10.2009 16:50:51" &&
                    cert.GetSerialNumberString() == "00D170CA2DE163D26E") return true;

                return false;
            }
        }
    }
}
