using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleLPR2;

namespace AppAutomatedIndustrialWeighingSystem.src.classes {

   class LicensePlateRecognition {
      public static List<Candidate> actionAnalyzeImage(string sUrlImage) {
         List<Candidate> oListCandidates = null;
         try {
            ISimpleLPR oSimpleLPR = SimpleLPR.Setup();
         
            // Product Key
            // oSimpleLPR.set_productKey("key.xml");

            oSimpleLPR.set_countryWeight("Spain", 1.0F);
            oSimpleLPR.realizeCountryWeights();

            IProcessor oProcesor = oSimpleLPR.createProcessor();
            oListCandidates = oProcesor.analyze(sUrlImage, 200);
         } catch(Exception) {
            oListCandidates = null;
         }

         return oListCandidates;
      }
   }
}
