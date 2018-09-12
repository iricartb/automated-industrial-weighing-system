using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AppAutomatedIndustrialWeighingSystem.src.classes.signature {

   [Serializable]
   class Line {
      public Line(Point oStartPoint, Point oEndPoint) {
         this.StartPoint = oStartPoint;
         this.EndPoint = oEndPoint;
      }

      public Point StartPoint { get; set; }
      public Point EndPoint { get; set; }
   }
}
