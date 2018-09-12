using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Drawing;

namespace AppAutomatedIndustrialWeighingSystem.src.classes {

   class CameraIP {
      private string sIP;
      private string sUsername;
      private string sPassword;
      private string sUrlSnapshot;

      public CameraIP(string sIP) {
         this.sIP = sIP;
      }

      public CameraIP(string sIP, string sUsername, string sPassword) : this(sIP) {
         this.sUsername = sUsername;
         this.sPassword = sPassword;
      }

      public CameraIP(string sIP, string sUsername, string sPassword, string sUrlSnapshot) : this(sIP, sUsername, sPassword) {
         this.sUrlSnapshot = sUrlSnapshot;
      }

      public bool actionSnapshot(string sUrlOutput) {
         if (this.sUrlSnapshot != null) {
            try {
               HttpWebRequest oRequest = (HttpWebRequest) WebRequest.Create(this.sUrlSnapshot);
               oRequest.PreAuthenticate = true;
               oRequest.Credentials = new NetworkCredential(sUsername, sPassword);

               HttpWebResponse oResponse = (HttpWebResponse) oRequest.GetResponse();

               Stream oStream = oResponse.GetResponseStream();

               if (File.Exists(sUrlOutput)) File.Delete(sUrlOutput);
               Image oImage = new Bitmap(oStream);
               oStream.Close();

               oImage.Save(sUrlOutput);

               if (File.Exists(sUrlOutput)) return true;
            } catch (Exception) {
               return false;
            }
         }
         
         return false;
      }

      public string getIP() { return this.sIP; }
      public string getUsername() { return this.sUsername; }
      public string getPassword() { return this.sPassword; }
      public string getUrlSnapshot() { return this.sUrlSnapshot; }
   }
}
