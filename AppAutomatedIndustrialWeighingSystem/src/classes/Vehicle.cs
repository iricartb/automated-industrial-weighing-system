using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAutomatedIndustrialWeighingSystem.src.classes {

   class Vehicle {
      private string sPlateNumber;
      private int nCodeCarrier;
      private string sCarrier;
      private int nCodeOrigin;
      private string sOrigin;
      private int nCodeProduct;
      private string sProduct;
      private string sState;
      private int nGrossWeight;
      private int nNetWeight;
      private int nTare;
      private string sTicketNumber;
      private string sSignature;

      public void setPlateNumber(string sPlateNumber) { this.sPlateNumber = sPlateNumber; }
      public void setCodeCarrier(int nCodeCarrier) { this.nCodeCarrier = nCodeCarrier; }
      public void setCarrier(string sCarrier) { this.sCarrier = sCarrier; }
      public void setCodeOrigin(int nCodeOrigin) { this.nCodeOrigin = nCodeOrigin; }
      public void setOrigin(string sOrigin) { this.sOrigin = sOrigin; }
      public void setCodeProduct(int nCodeProduct) { this.nCodeProduct = nCodeProduct; }
      public void setProduct(string sProduct) { this.sProduct = sProduct; }
      public void setState(string sState) { this.sState = sState; }
      public void setGrossWeight(int nGrossWeight) { this.nGrossWeight = nGrossWeight; }
      public void setNetWeight(int nNetWeight) { this.nNetWeight = nNetWeight; }
      public void setTare(int nTare) { this.nTare = nTare; }
      public void setTicketNumber(string sTicketNumber) { this.sTicketNumber = sTicketNumber; }
      public void setSignature(string sSignature) { this.sSignature = sSignature; }

      public string getPlateNumber() { return this.sPlateNumber; }
      public int getCodeCarrier() { return this.nCodeCarrier; }
      public string getCarrier() { return this.sCarrier; }
      public int getCodeOrigin() { return this.nCodeOrigin; }
      public string getOrigin() { return this.sOrigin; }
      public int getCodeProduct() { return this.nCodeProduct; }
      public string getProduct() { return this.sProduct; }
      public string getState() { return this.sState; }
      public int getGrossWeight() { return this.nGrossWeight; }
      public int getNetWeight() { return this.nNetWeight; }
      public int getTare() { return this.nTare; }
      public string getTicketNumber() { return this.sTicketNumber; }
      public string getSignature() { return this.sSignature; }
   }
}
