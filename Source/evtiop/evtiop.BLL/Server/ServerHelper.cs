using System.IO;
using System.Net;
using System.Windows.Media.Imaging;

namespace evtiop.BLL.Server
{
    public class ServerHelper
    {
        private string address1 = "ftp://192.168.0.117";
        private string address2 = "ftp://192.168.43.79";

        public BitmapImage GetImageFromServer(string path)
        {
            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential("admin", "admin");
                string conn = address2 + "/" + path;
                using (MemoryStream stream = new MemoryStream(client.DownloadData($"ftp://192.168.0.117/ {path}")))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = stream;
                    image.EndInit();

                    return image;
                }
            }
        }
        public void UploadImageToServer(string path, string imageName)
        {
            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential("admin", "admin");
                string conn = address2 + "/" + imageName;
                client.UploadFile($"ftp://192.168.0.117/ {imageName}", WebRequestMethods.Ftp.UploadFile, path);
            }
        }
        public bool CheckFtpConnection()
        {
            FtpWebRequest webRequest = (FtpWebRequest)FtpWebRequest.Create(address1);
            webRequest.Credentials = new NetworkCredential("admin", "admin");
            webRequest.Method = WebRequestMethods.Ftp.PrintWorkingDirectory;

            try
            {
                webRequest.GetResponse();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
