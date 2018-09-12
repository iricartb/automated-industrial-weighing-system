using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace AppAutomatedIndustrialWeighingSystem.src.classes.signature {

   class PanelSignature : Panel {
      private Point oStartPoint;
      private Point oEndPoint;
      private Pen oPen;
      private Glyph oGlyph;
      private Signature oSignature;
      private String sFileName;
      private Boolean bIsCapturing;
      private Boolean bPanelSigned;
      private Control oControlDependency;

      public PanelSignature(Color oPenColor, Color oBackgroundColor, Control oControlDependency = null) {
         bIsCapturing = false;
         bPanelSigned = false;
         oPen = new Pen(oPenColor);
         oGlyph = null;
         oSignature = new Signature();
         sFileName = @"signature.xml";

         if (oControlDependency != null) {
            this.oControlDependency = oControlDependency;
            this.oControlDependency.BackColor = Color.LightGray;
         }
         
         this.BackColor = oBackgroundColor;

         this.MouseMove += PanelSignature_MouseMove;
         this.MouseUp += PanelSignature_MouseUp;
         this.MouseDown += PanelSignature_MouseDown;
      }

      private void drawLine(Line oLine, Graphics oCustomGraphics = null) {
         if (oCustomGraphics == null) {
            using (Graphics oGraphics = this.CreateGraphics()) {
               oGraphics.DrawLine(oPen, oLine.StartPoint, oLine.EndPoint);
            }
         }
         else {
            Graphics oGraphics = oCustomGraphics;
            oGraphics.DrawLine(oPen, oLine.StartPoint, oLine.EndPoint);
         }
      }

      private void clearSignaturePanel() {
         using (Graphics oGraphics = this.CreateGraphics()) {
            SolidBrush oSolidBrush = new SolidBrush(this.BackColor);

            oGraphics.FillRectangle(oSolidBrush, 0, 0, this.Width, this.Height);

            if (oControlDependency != null) {
               oControlDependency.BackColor = Color.LightGray;
            }

            bPanelSigned = false;
         }
      }

      private void drawSignature(Graphics oCustomGraphics = null) {
         foreach (Glyph oGlyph in oSignature.Glyphs) {
            foreach (Line oLine in oGlyph.Lines) {
               drawLine(oLine, oCustomGraphics);
            }
         }
      }

      private void serializeSignature() {
         XmlSerializer oSerializer = new XmlSerializer(typeof(Signature));

         if (File.Exists(sFileName)) {
            File.Delete(sFileName);
         }

         using (TextWriter oTextWriter = new StreamWriter(sFileName)) {
            oSerializer.Serialize(oTextWriter, oSignature);
            oTextWriter.Close();
         }
      }

      private void deserializeSignature() {
         XmlSerializer oDeserializer = new XmlSerializer(typeof(Signature));
         using (TextReader oTextReader = new StreamReader(sFileName)) {
            oSignature = (Signature) oDeserializer.Deserialize(oTextReader);
            oTextReader.Close();
         }
      }

      public MemoryStream getSignatureImage() {
         MemoryStream oOutputStream = new MemoryStream();
         
         using (Bitmap oBitmap = new Bitmap(this.Size.Width, this.Size.Height)) {
            using (Graphics oGraphics = Graphics.FromImage(oBitmap)) {
               drawSignature(oGraphics);

               oBitmap.Save(oOutputStream, System.Drawing.Imaging.ImageFormat.Png);
            }
         }

         return oOutputStream;
      }

      private void PanelSignature_MouseMove(object sender, MouseEventArgs e) {
         if (bIsCapturing) {
            if ((oStartPoint.IsEmpty) && (oEndPoint.IsEmpty)) {
               oEndPoint = e.Location;
            }
            else {
               oStartPoint = oEndPoint;
               oEndPoint = e.Location;
               Line oLine = new Line(oStartPoint, oEndPoint);
               oGlyph.Lines.Add(oLine);

               bPanelSigned = true;

               if (oControlDependency != null) {
                  oControlDependency.BackColor = Color.LightSeaGreen;
               }

               drawLine(oLine);
            }
         }
      }

      private void PanelSignature_MouseUp(object sender, MouseEventArgs e) {
         bIsCapturing = false;
         oSignature.Glyphs.Add(oGlyph);
         oStartPoint = new Point();
         oEndPoint = new Point();
      }

      private void PanelSignature_MouseDown(object sender, MouseEventArgs e) {
         bIsCapturing = true;
         oGlyph = new Glyph();
      }

      public Boolean isPanelSigned() { return bPanelSigned; }
   }
}
