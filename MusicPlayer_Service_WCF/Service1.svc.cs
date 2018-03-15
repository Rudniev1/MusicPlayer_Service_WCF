using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace MusicPlayer_Service_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
     ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class Service1 : ItransferService
    {
        FileStream file;
        string path = @"F:\muzyka\pietrek in the mix\";

        public void setsFolderPath (string paths)
        {
            path = @paths;
        }

        public Stream GetLargeObject(string filename)
        {

            string filePath = Path.Combine(path + filename);

            try
            {
               file = File.OpenRead(filePath);
                return file;
                
            }
            catch (IOException ex)
            {
                Console.WriteLine(String.Format("An exception was thrown while trying to open file {0}", filePath));
                Console.WriteLine("Exception is: ");
                Console.WriteLine(ex.ToString());
                throw;
                //return null;
            }
        }

        public string[] GetPlaylist()
        {
            string[] fileArray = Directory.GetFiles(path, "*.mp3", SearchOption.AllDirectories);
            return fileArray;
        }
    }
}
