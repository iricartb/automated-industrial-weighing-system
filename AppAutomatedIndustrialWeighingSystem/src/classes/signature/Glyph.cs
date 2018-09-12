using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAutomatedIndustrialWeighingSystem.src.classes.signature {

   [Serializable]
   class Glyph {
      public Glyph() {
         this.Lines = new List<Line>();
      }

      public List<Line> Lines { get; set; }
   }
}
