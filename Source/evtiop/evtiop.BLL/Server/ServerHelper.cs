using System.IO;
using System.Net;
using System.Windows.Media.Imaging;

namespace evtiop.BLL.Server
{
    public class ServerHelper
    {
        public BitmapImage GetImageFromServer(string path)
        {
            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential("admin", "admin");
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
                client.UploadFile($"ftp://192.168.0.117/ {imageName}", WebRequestMethods.Ftp.UploadFile, path);
            }
        }
        public bool CheckFtpConnection()
        {
            FtpWebRequest webRequest = (FtpWebRequest)FtpWebRequest.Create("ftp://192.168.0.117");
            webRequest.Credentials = new NetworkCredential("admin", "admin");

            try
            {
                WebResponse webResponse = webRequest.GetResponse();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
