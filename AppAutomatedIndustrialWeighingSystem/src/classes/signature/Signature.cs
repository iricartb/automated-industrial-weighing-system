using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAutomatedIndustrialWeighingSystem.src.classes.signature {

   [Serializable]
   class Signature {
      public Signature() {
         this.Glyphs = new List<Glyph>();
      }

      public List<Glyph> Glyphs { get; set; }
   }
}
