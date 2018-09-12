using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAutomatedIndustrialWeighingSystem.src.classes {

   class Format {
      public static string getFormatLicensePlate(string sLicensePlate) {
         return sLicensePlate.Replace(" ", "-");
      }
   }
}
