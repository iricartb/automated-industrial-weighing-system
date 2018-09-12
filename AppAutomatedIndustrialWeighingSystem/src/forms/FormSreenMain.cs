using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Data.SqlClient;
using System.Drawing.Printing;
using SimpleLPR2;
using Telerik.Reporting;
using Telerik.Reporting.Processing;
using AppAutomatedIndustrialWeighingSystem.src.classes;
using AppAutomatedIndustrialWeighingSystem.src.classes.signature;
using AppAutomatedIndustrialWeighingSystem.src.reports;

namespace AppAutomatedIndustrialWeighingSystem {

   public partial class FormScreenMain : Form {
      private const int SCREEN_START_ANIMATE_FOREGROUND_COLOR_FADEOUT_SECONDS = 3;

      private double nScreenBackgroundColorRed;
      private double nScreenBackgroundColorGreen;
      private double nScreenBackgroundColorBlue;

      private double nScreenForegroundColorRed;
      private double nScreenForegroundColorGreen;
      private double nScreenForegroundColorBlue;

      private double nScreenAnimateForegroundColorRed;
      private double nScreenAnimateForegroundColorGreen;
      private double nScreenAnimateForegroundColorBlue;

      public FormScreenMain() {
         InitializeComponent();

         Global.oWeighingSerialPort.WriteTimeout = Global.APP_WEIGHING_SERIAL_PORT_COM_IO_TIMEOUT_SECONDS * 1000;
         Global.oWeighingSerialPort.ReadTimeout = Global.APP_WEIGHING_SERIAL_PORT_COM_IO_TIMEOUT_SECONDS * 1000;
      }

      private void clearScreen(string sTypeScreen = Global.APP_TYPE_SCREEN_BLANK) {
         nScreenForegroundColorRed = 0;
         nScreenForegroundColorGreen = 0;
         nScreenForegroundColorBlue = 0;

         this.Click -= FormScreenMain_Click;

         Global.oWeighingSerialPort.DataReceived -= oWeighingSerialPort_DataReceived;
         Global.nWeighingStatus = Global.APP_WEIGHING_STATUS_NONE;

         oTimerForeground.Enabled = false;
         oTimerInactivity.Interval = Global.APP_TIMER_INACTIVITY_INTERVAL_DEFAULT_SECONDS * 1000;
         oTimerInactivity.Enabled = false;
         oTimerLPR.Enabled = false;
         oTimerWeighing.Enabled = false;
         oTimerWeighingChangeScreen.Enabled = false;
         oTimerPrint.Enabled = false;

         Global.nAppCurrentLPRAttempts = 0;

         oImagePushButton.Visible = false;
         oImageBackButton.Visible = false;
         oImageSafety.Visible = false;
         oImageVehicle.Visible = false;
         oImageSignature.Visible = false;
         oImagePrinter.Visible = false;
         oPanelRejectButton.Visible = false;
         oPanelAcceptButton.Visible = false;
         oPanelLPRSnapshot.Visible = false;

         oLabelHeader.Visible = false;
         oImageBusiness.Visible = false;
         oLabelAppName.Visible = false;

         oLabelHeader.ForeColor = Color.White;
         oLabelAppName.ForeColor = Color.White;

         oLabelDescription.Visible = false;
         oLabelDescription.Font = new Font("Verdana", 18);
         oLabelDescription.ForeColor = Color.White;
         oLabelDescription.TextAlign = ContentAlignment.TopLeft;

         oLabelTryAgain.Visible = false;

         foreach(Control oControlElement in Global.oListDinamicControls) {
            oControlElement.Dispose();
         }

         Global.oListDinamicControls.Clear();

         if (sTypeScreen == Global.APP_TYPE_SCREEN_BLANK) {
            nScreenBackgroundColorRed = Global.APP_BLANK_SCREEN_BACKGROUND_COLOR_RED;
            nScreenBackgroundColorGreen = Global.APP_BLANK_SCREEN_BACKGROUND_COLOR_GREEN;
            nScreenBackgroundColorBlue = Global.APP_BLANK_SCREEN_BACKGROUND_COLOR_BLUE;
         }
         else if (sTypeScreen == Global.APP_TYPE_SCREEN_SUCCESS) {
            nScreenBackgroundColorRed = Global.APP_SUCCESS_SCREEN_BACKGROUND_COLOR_RED;
            nScreenBackgroundColorGreen = Global.APP_SUCCESS_SCREEN_BACKGROUND_COLOR_GREEN;
            nScreenBackgroundColorBlue = Global.APP_SUCCESS_SCREEN_BACKGROUND_COLOR_BLUE;
         }
         else if (sTypeScreen == Global.APP_TYPE_SCREEN_WARNING) {
            nScreenBackgroundColorRed = Global.APP_WARNING_SCREEN_BACKGROUND_COLOR_RED;
            nScreenBackgroundColorGreen = Global.APP_WARNING_SCREEN_BACKGROUND_COLOR_GREEN;
            nScreenBackgroundColorBlue = Global.APP_WARNING_SCREEN_BACKGROUND_COLOR_BLUE;
         }
         else if (sTypeScreen == Global.APP_TYPE_SCREEN_ERROR) {
            nScreenBackgroundColorRed = Global.APP_ERROR_SCREEN_BACKGROUND_COLOR_RED;
            nScreenBackgroundColorGreen = Global.APP_ERROR_SCREEN_BACKGROUND_COLOR_GREEN;
            nScreenBackgroundColorBlue = Global.APP_ERROR_SCREEN_BACKGROUND_COLOR_BLUE;
         }

         this.BackColor = Color.FromArgb((int) nScreenBackgroundColorRed, (int) nScreenBackgroundColorGreen, (int) nScreenBackgroundColorBlue);
      }

      private void actionNavigationSuccess(string sScreenDestiny) {
         lock(Global.oMonitorScreen) {
            Global.sAppBeforeScreen = Global.sAppCurrentScreen;

            if (sScreenDestiny == Global.APP_SCREEN_START) {
               Global.sAppCurrentScreen = Global.APP_SCREEN_START;

               clearScreen(Global.APP_TYPE_SCREEN_BLANK);

               actionNavigationScreenStart();
            }
            else if (sScreenDestiny == Global.APP_SCREEN_LPR) {
               Global.sAppCurrentScreen = Global.APP_SCREEN_LPR;

               clearScreen(Global.APP_TYPE_SCREEN_SUCCESS);

               actionNavigationScreenLPR();
            }
            else if (sScreenDestiny == Global.APP_SCREEN_ORIGINS) {
               Global.sAppCurrentScreen = Global.APP_SCREEN_ORIGINS;

               clearScreen(Global.APP_TYPE_SCREEN_SUCCESS);

               actionNavigationScreenOrigins();
            }
            else if (sScreenDestiny == Global.APP_SCREEN_PRODUCTS) {
               Global.sAppCurrentScreen = Global.APP_SCREEN_PRODUCTS;

               clearScreen(Global.APP_TYPE_SCREEN_SUCCESS);

               actionNavigationScreenProducts();
            }
            else if (sScreenDestiny == Global.APP_SCREEN_WEIGHING) {
               Global.sAppCurrentScreen = Global.APP_SCREEN_WEIGHING;

               clearScreen(Global.APP_TYPE_SCREEN_SUCCESS);

               actionNavigationScreenWeighing();
            }
            else if (sScreenDestiny == Global.APP_SCREEN_TICKET) {
               Global.sAppCurrentScreen = Global.APP_SCREEN_TICKET;

               clearScreen(Global.APP_TYPE_SCREEN_SUCCESS);

               actionNavigationScreenTicket();
            }
            else if (sScreenDestiny == Global.APP_SCREEN_PRINT) {
               Global.sAppCurrentScreen = Global.APP_SCREEN_PRINT;

               clearScreen(Global.APP_TYPE_SCREEN_SUCCESS);

               actionNavigationScreenPrint();
            }
         }
      }

      private void actionNavigationWarningError(string sWarningErrorCode, string sWarningErrorHeader, string sWarningErrorDetail, string sWarningErrorTryAgain = null) {
         int nItemsHeightAvailableSpace = (this.Size.Height / 2);
         
         oLabelHeader.Text = sWarningErrorHeader;
         oLabelHeader.Location = new Point(((this.Size.Width - oLabelHeader.Size.Width) / 2), ((nItemsHeightAvailableSpace - oLabelHeader.Size.Height) / 2));
         oLabelHeader.Visible = true;

         oLabelDescription.Text = sWarningErrorCode + " - " + sWarningErrorDetail;
         oLabelDescription.Location = new Point(((this.Size.Width - oLabelDescription.Size.Width) / 2), ((this.Size.Height - oLabelDescription.Size.Height) / 2));
         oLabelDescription.TextAlign = ContentAlignment.MiddleCenter;
         oLabelDescription.Visible = true;

         if (sWarningErrorTryAgain != null) {
            this.Click += FormScreenMain_Click;

            oLabelTryAgain.Text = sWarningErrorTryAgain;
            oLabelTryAgain.Location = new Point(((this.Size.Width - oLabelTryAgain.Size.Width) / 2), ((nItemsHeightAvailableSpace) + ((nItemsHeightAvailableSpace - oLabelTryAgain.Size.Height) / 2)));
            oLabelTryAgain.Visible = true;
         }

         oTimerInactivity.Interval = Global.APP_TIMER_INACTIVITY_SCREEN_WARNING_ERROR_INTERVAL_SECONDS * 1000;
         oTimerInactivity.Enabled = true;
      }

      private void actionNavigationError(string sErrorCode, string sErrorHeader, string sErrorDetail) {
         lock(Global.oMonitorScreen) {
            Global.sAppBeforeScreen = Global.sAppCurrentScreen;
            Global.sAppCurrentScreen = Global.APP_SCREEN_ERROR;

            clearScreen(Global.APP_TYPE_SCREEN_ERROR);

            actionNavigationWarningError(sErrorCode, sErrorHeader, sErrorDetail);
         }
      }

      private void actionNavigationWarning(string sWarningCode, string sWarningHeader, string sWarningDetail, string sWarningTryAgain = null) {
         lock(Global.oMonitorScreen) {
            Global.sAppBeforeScreen = Global.sAppCurrentScreen;
            Global.sAppCurrentScreen = Global.APP_SCREEN_WARNING;

            clearScreen(Global.APP_TYPE_SCREEN_WARNING);

            actionNavigationWarningError(sWarningCode, sWarningHeader, sWarningDetail, sWarningTryAgain);
         }
      }

      private void actionNavigationScreenStart() {
         int nItemsHeightAvailableSpace = (this.Size.Height / 4);

         this.Click += FormScreenMain_Click;

         oLabelHeader.Text = Global.APP_SCREEN_START_LABEL_HEADER_TEXT;
         oLabelHeader.ForeColor = Color.Black;
         oLabelHeader.Location = new Point(((this.Size.Width - oLabelHeader.Size.Width) / 2), ((nItemsHeightAvailableSpace * 3) - (oLabelHeader.Size.Height / 2)));
         oLabelHeader.Visible = true;

         oLabelDescription.Text = Global.APP_SCREEN_START_LABEL_DESCRIPTION_TEXT;
         oLabelDescription.Font = new Font("Verdana", 9);
         oLabelDescription.ForeColor = Color.Black;
         oLabelDescription.Location = new Point(((this.Size.Width - oLabelDescription.Size.Width) / 2), ((nItemsHeightAvailableSpace * 3) - (oLabelDescription.Size.Height / 2) + (Global.APP_SCREEN_LABEL_MARGINS)));
         oLabelDescription.Visible = true;

         oLabelAppName.Text = Global.APP_NAME + " - " + Global.APP_VERSION;
         oLabelAppName.ForeColor = Color.Black;
         oLabelAppName.Location = new Point((this.Size.Width - oLabelAppName.Size.Width - Global.APP_SCREEN_LABEL_MARGINS), (this.Size.Height - (Global.APP_SCREEN_LABEL_MARGINS / 2)));
         oLabelAppName.Visible = true;

         oImageSafety.Size = new Size((int) (this.Size.Width / 1.5), ((int) (this.Size.Height / 2.4)));
         oImageSafety.Location = new Point(((this.Size.Width - oImageSafety.Size.Width) / 2), Global.APP_SCREEN_LABEL_MARGINS);
         oImageSafety.Visible = true;

         oImagePushButton.Location = new Point(((this.Size.Width - oImagePushButton.Size.Width) / 2), ((nItemsHeightAvailableSpace * 3) - (oLabelHeader.Size.Height) - (oImagePushButton.Size.Height) - (Global.APP_SCREEN_LABEL_MARGINS / 2)));
         oImagePushButton.Visible = true;

         oImageBusiness.Location = new Point(oLabelAppName.Location.X - oImageBusiness.Size.Width - 10, oLabelAppName.Location.Y - (oImageBusiness.Size.Height / 2));
         oImageBusiness.Visible = true;

         nScreenAnimateForegroundColorRed = (255 / (SCREEN_START_ANIMATE_FOREGROUND_COLOR_FADEOUT_SECONDS * (1000 / oTimerForeground.Interval)));
         nScreenAnimateForegroundColorGreen = (255 / (SCREEN_START_ANIMATE_FOREGROUND_COLOR_FADEOUT_SECONDS * (1000 / oTimerForeground.Interval)));
         nScreenAnimateForegroundColorBlue = (255 / (SCREEN_START_ANIMATE_FOREGROUND_COLOR_FADEOUT_SECONDS * (1000 / oTimerForeground.Interval)));

         oTimerForeground.Enabled = true;
      }

      private void actionNavigationScreenLPR() {
         int nItemsHeightAvailableSpace = (this.Size.Height / 4);

         oLabelHeader.Text = Global.APP_SCREEN_LPR_LABEL_HEADER_TEXT;
         oLabelHeader.Location = new Point(((this.Size.Width - oLabelHeader.Size.Width) / 2), ((nItemsHeightAvailableSpace - oLabelHeader.Size.Height) / 2));
         oLabelHeader.Visible = true;

         oPanelLPRSnapshot.Size = new Size(this.Size.Width, (nItemsHeightAvailableSpace * 2));
         oPanelLPRSnapshot.Location = new Point(((this.Size.Width - oPanelLPRSnapshot.Size.Width) / 2), ((this.Size.Height - oPanelLPRSnapshot.Size.Height) / 2));
         oPanelLPRSnapshot.Visible = true;

         oLabelDescription.Text = String.Empty;
         oLabelDescription.Font = new Font("Let's go Digital", 48);
         oLabelDescription.ForeColor = Color.Cyan;
         oLabelDescription.Location = new Point(((this.Size.Width - oLabelDescription.Size.Width) / 2), ((nItemsHeightAvailableSpace * 3) + (nItemsHeightAvailableSpace / 2) - (oLabelDescription.Size.Height / 2)));
         oLabelDescription.Visible = true;

         oTimerLPR.Enabled = true;
         oTimerInactivity.Enabled = true;

         this.Refresh();
      }

      private void actionNavigationScreenOrigins() {
         int nItemsHeightAvailableSpace = (this.Size.Height / 4);

         try {
            oLabelHeader.Text = Global.APP_SCREEN_ORIGINS_LABEL_HEADER_TEXT.Replace("{1}", Format.getFormatLicensePlate(Global.oVehicle.getPlateNumber()));
            oLabelHeader.Location = new Point(((this.Size.Width - oLabelHeader.Size.Width) / 2), ((nItemsHeightAvailableSpace - oLabelHeader.Size.Height) / 2));
            oLabelHeader.Visible = true;

            oLabelDescription.Text = Global.APP_SCREEN_ORIGINS_LABEL_DESCRIPTION_TEXT.Replace("{1}", Global.oVehicle.getCarrier());
            oLabelDescription.Location = new Point(((this.Size.Width - oLabelDescription.Size.Width) / 2), (oLabelHeader.Location.Y + oLabelHeader.Size.Height + Global.APP_SCREEN_LABEL_MARGINS));
            oLabelDescription.Visible = true;

            int nOriginItemsHeightAvailableSpace = (this.Size.Height - (oLabelDescription.Location.Y + oLabelDescription.Size.Height) - (Global.APP_SCREEN_LABEL_MARGINS * 3));
            int nOriginItemWidth = (this.Size.Width - (Global.APP_SCREEN_LABEL_MARGINS * 2));
            int nOriginItemHeight = ((nOriginItemsHeightAvailableSpace - ((Global.APP_SCREEN_LABEL_MARGINS / 2) * 4)) / 5);
         
            for(int i = 0; i < Global.oListAvailableOrigins.Count; i++) {
               Label oLabelOrigin = new Label();

               oLabelOrigin.Size = new Size(nOriginItemWidth, nOriginItemHeight);

               if (i == 0) oLabelOrigin.Location = new Point(Global.APP_SCREEN_LABEL_MARGINS, (oLabelDescription.Location.Y + oLabelDescription.Size.Height + (Global.APP_SCREEN_LABEL_MARGINS * 2)));
               else oLabelOrigin.Location = new Point(Global.APP_SCREEN_LABEL_MARGINS, (oLabelDescription.Location.Y + oLabelDescription.Size.Height + (Global.APP_SCREEN_LABEL_MARGINS * 2)) + ((nOriginItemHeight + (Global.APP_SCREEN_LABEL_MARGINS / 2)) * i)); 

               oLabelOrigin.Text = Global.oListAvailableOrigins[i][1];
               oLabelOrigin.TextAlign = ContentAlignment.MiddleCenter;
               oLabelOrigin.ForeColor = Color.White;

               oLabelOrigin.Font = new Font(Global.APP_SCREEN_DINAMIC_LABEL_FONT_NAME, Global.APP_SCREEN_DINAMIC_LABEL_FONT_SIZE);

               oLabelOrigin.BackColor = Color.FromArgb(Global.APP_SCREEN_DINAMIC_LABEL_BACKGROUND_COLOR_RED, Global.APP_SCREEN_DINAMIC_LABEL_BACKGROUND_COLOR_GREEN, Global.APP_SCREEN_DINAMIC_LABEL_BACKGROUND_COLOR_BLUE);

               oLabelOrigin.Cursor = Cursors.Hand;

               int nCodeOrigin = Int32.Parse(Global.oListAvailableOrigins[i][0]);
               string sOrigin = Global.oListAvailableOrigins[i][1];

               oLabelOrigin.Click += (sender, e) => oLabelOrigin_Click(sender, e, nCodeOrigin, sOrigin); 

               oLabelOrigin.Visible = true;

               this.Controls.Add(oLabelOrigin);

               Global.oListDinamicControls.Add(oLabelOrigin);
            }

            oTimerInactivity.Enabled = true;
         }
         catch(Exception oException) {
            actionNavigationError(Global.APP_SCREEN_ERROR_KERNEL_LABEL_ERROR_CODE_TEXT, Global.APP_SCREEN_ERROR_KERNEL_LABEL_HEADER_TEXT, Global.APP_SCREEN_ERROR_KERNEL_LABEL_DESCRIPTION_TEXT.Replace("{1}", oException.ToString()));
         }
      }

      private void actionNavigationScreenProducts() {
         int nItemsHeightAvailableSpace = (this.Size.Height / 4);

         try {
            oLabelHeader.Text = Global.APP_SCREEN_PRODUCTS_LABEL_HEADER_TEXT.Replace("{1}", Format.getFormatLicensePlate(Global.oVehicle.getPlateNumber()));
            oLabelHeader.Location = new Point(((this.Size.Width - oLabelHeader.Size.Width) / 2), ((nItemsHeightAvailableSpace - oLabelHeader.Size.Height) / 2));
            oLabelHeader.Visible = true;

            oLabelDescription.Text = Global.APP_SCREEN_PRODUCTS_LABEL_DESCRIPTION_TEXT.Replace("{1}", Global.oVehicle.getCarrier()).Replace("{2}", Global.oVehicle.getOrigin());
            oLabelDescription.Location = new Point(((this.Size.Width - oLabelDescription.Size.Width) / 2), (oLabelHeader.Location.Y + oLabelHeader.Size.Height + Global.APP_SCREEN_LABEL_MARGINS));
            oLabelDescription.Visible = true;

            oImageBackButton.Location = new Point(Global.APP_SCREEN_LABEL_MARGINS, oLabelHeader.Location.Y); 
            oImageBackButton.Visible = true;

            int nProductItemsHeightAvailableSpace = (this.Size.Height - (oLabelDescription.Location.Y + oLabelDescription.Size.Height) - (Global.APP_SCREEN_LABEL_MARGINS * 3));
            int nProductItemWidth = (this.Size.Width - (Global.APP_SCREEN_LABEL_MARGINS * 2));
            int nProductItemHeight;

            if (Global.oListAvailableProducts.Count <= 5) nProductItemHeight = ((nProductItemsHeightAvailableSpace - ((Global.APP_SCREEN_LABEL_MARGINS / 2) * 4)) / 5);
            else nProductItemHeight = ((nProductItemsHeightAvailableSpace - ((Global.APP_SCREEN_LABEL_MARGINS / 2) * (Global.oListAvailableProducts.Count - 1))) / Global.oListAvailableProducts.Count);
            
            for(int i = 0; i < Global.oListAvailableProducts.Count; i++) {
               Label oLabelProduct = new Label();

               oLabelProduct.Size = new Size(nProductItemWidth, nProductItemHeight);

               if (i == 0) oLabelProduct.Location = new Point(Global.APP_SCREEN_LABEL_MARGINS, (oLabelDescription.Location.Y + oLabelDescription.Size.Height + (Global.APP_SCREEN_LABEL_MARGINS * 2)));
               else oLabelProduct.Location = new Point(Global.APP_SCREEN_LABEL_MARGINS, (oLabelDescription.Location.Y + oLabelDescription.Size.Height + (Global.APP_SCREEN_LABEL_MARGINS * 2)) + ((nProductItemHeight + (Global.APP_SCREEN_LABEL_MARGINS / 2)) * i));

               oLabelProduct.Text = Global.oListAvailableProducts[i][1];
               oLabelProduct.TextAlign = ContentAlignment.MiddleCenter;
               oLabelProduct.ForeColor = Color.White;

               oLabelProduct.Font = new Font(Global.APP_SCREEN_DINAMIC_LABEL_FONT_NAME, Global.APP_SCREEN_DINAMIC_LABEL_FONT_SIZE);

               oLabelProduct.BackColor = Color.FromArgb(Global.APP_SCREEN_DINAMIC_LABEL_BACKGROUND_COLOR_RED, Global.APP_SCREEN_DINAMIC_LABEL_BACKGROUND_COLOR_GREEN, Global.APP_SCREEN_DINAMIC_LABEL_BACKGROUND_COLOR_BLUE);

               oLabelProduct.Cursor = Cursors.Hand;

               int nCodeProduct = Int32.Parse(Global.oListAvailableProducts[i][0]);
               string sProduct = Global.oListAvailableProducts[i][1];

               oLabelProduct.Click += (sender, e) => oLabelProduct_Click(sender, e, nCodeProduct, sProduct);

               oLabelProduct.Visible = true;

               this.Controls.Add(oLabelProduct);

               Global.oListDinamicControls.Add(oLabelProduct);
            }

            oTimerInactivity.Enabled = true;
         }
         catch (Exception oException) {
            actionNavigationError(Global.APP_SCREEN_ERROR_KERNEL_LABEL_ERROR_CODE_TEXT, Global.APP_SCREEN_ERROR_KERNEL_LABEL_HEADER_TEXT, Global.APP_SCREEN_ERROR_KERNEL_LABEL_DESCRIPTION_TEXT.Replace("{1}", oException.ToString()));
         }
      }

      private void actionNavigationScreenWeighing() {
         int nItemsHeightAvailableSpace = (this.Size.Height / 4);

         Global.oWeighingSerialPort.DataReceived += oWeighingSerialPort_DataReceived;

         oLabelHeader.Text = Global.APP_SCREEN_WEIGHING_LABEL_HEADER_TEXT.Replace("{1}", Format.getFormatLicensePlate(Global.oVehicle.getPlateNumber()));
         oLabelHeader.Location = new Point(((this.Size.Width - oLabelHeader.Size.Width) / 2), ((nItemsHeightAvailableSpace - oLabelHeader.Size.Height) / 2));
         oLabelHeader.Visible = true;

         oLabelDescription.Text = "0".PadLeft(6, '0');
         oLabelDescription.Font = new Font("Let's go Digital", 90);
         oLabelDescription.ForeColor = Color.Cyan;
         oLabelDescription.Location = new Point(((this.Size.Width - oLabelDescription.Size.Width) / 2), ((this.Size.Height - oLabelDescription.Size.Height) / 2));
         oLabelDescription.Visible = true;

         Global.oVehicle.setGrossWeight(0);
         Global.oVehicle.setNetWeight(0);

         oTimerWeighing.Enabled = true;
         oTimerInactivity.Enabled = true;

         this.Refresh();

         oTimerWeighing_Tick(this, null);
      }

      private void actionNavigationScreenTicket() {
         try {
            int nItemsHeightAvailableSpace = (this.Size.Height / 4);

            oLabelHeader.Text = Global.APP_SCREEN_TICKET_LABEL_HEADER_TEXT.Replace("{1}", Format.getFormatLicensePlate(Global.oVehicle.getPlateNumber()));
            oLabelHeader.Location = new Point(((this.Size.Width - oLabelHeader.Size.Width) / 2), ((nItemsHeightAvailableSpace - oLabelHeader.Size.Height) / 2));
            oLabelHeader.Visible = true;

            oLabelDescription.Text = Global.APP_SCREEN_TICKET_LABEL_DESCRIPTION_TEXT.Replace("{1}", Format.getFormatLicensePlate(Global.oVehicle.getPlateNumber())).Replace("{2}", Global.oVehicle.getCarrier()).Replace("{3}", Global.oVehicle.getOrigin()).Replace("{4}", Global.oVehicle.getProduct()).Replace("{5}", Global.oVehicle.getGrossWeight().ToString()).Replace("{6}", Global.oVehicle.getTare().ToString()).Replace("{7}", Global.oVehicle.getNetWeight().ToString());
            oLabelDescription.Font = new Font("Verdana", 18);
            oLabelDescription.Location = new Point(((this.Size.Width / 3) + (Global.APP_SCREEN_LABEL_MARGINS)), (oLabelHeader.Location.Y + oLabelHeader.Size.Height + Global.APP_SCREEN_LABEL_MARGINS));
            oLabelDescription.Visible = true;

            oImageVehicle.Location = new Point((oLabelDescription.Location.X - oImageVehicle.Size.Width - (Global.APP_SCREEN_LABEL_MARGINS / 2)), (oLabelDescription.Location.Y + (oLabelDescription.Size.Height / 2) - (oImageVehicle.Size.Height / 2)));
            oImageVehicle.Visible = true;

            PanelSignature oPanelSignature = new PanelSignature(Color.Blue, Color.LightGray, oPanelAcceptButton);
            oPanelSignature.Size = new Size(((this.Size.Width / 3) - (Global.APP_SCREEN_LABEL_MARGINS * 3)), nItemsHeightAvailableSpace - (Global.APP_SCREEN_LABEL_MARGINS / 2));
            oPanelSignature.Location = new Point((oLabelDescription.Location.X) + 10, (nItemsHeightAvailableSpace * 2));
            oPanelSignature.Visible = true;

            oImageSignature.Location = new Point((oPanelSignature.Location.X - (oImageVehicle.Size.Width / 2) - (oImageSignature.Size.Width / 2) - (Global.APP_SCREEN_LABEL_MARGINS / 2)), (oPanelSignature.Location.Y + (oPanelSignature.Size.Height / 2) - (oImageSignature.Size.Height / 2)));
            oImageSignature.Visible = true;

            Label oLabelSignature = new Label();
            oLabelSignature.AutoSize = true;
            oLabelSignature.Text = Global.APP_SCREEN_TICKET_LABEL_DESCRIPTION_SIGNATURE_TEXT;
            oLabelSignature.ForeColor = Color.White;
            oLabelSignature.Font = new Font("Verdana", 13);
            oLabelSignature.Location = new Point((oPanelSignature.Location.X - 5), ((oPanelSignature.Location.Y) - (Global.APP_SCREEN_LABEL_MARGINS)));
            oLabelSignature.Visible = true;

            oPanelRejectButton.Size = new Size((this.Size.Width / 6), (nItemsHeightAvailableSpace / 3));
            oPanelRejectButton.Location = new Point(((this.Size.Width / 2) - (oPanelRejectButton.Size.Width) - (Global.APP_SCREEN_LABEL_MARGINS * 2)), ((nItemsHeightAvailableSpace * 3) + (nItemsHeightAvailableSpace / 2) - (oPanelRejectButton.Size.Height / 2)));
            oPanelRejectButton.Visible = true;

            oLabelRejectButton.Location = new Point(((oPanelRejectButton.Size.Width / 2) - (oLabelRejectButton.Size.Width / 2)), ((oPanelRejectButton.Size.Height / 2) - (oLabelRejectButton.Size.Height / 2)));

            oPanelAcceptButton.Size = new Size((this.Size.Width / 6), (nItemsHeightAvailableSpace / 3));
            oPanelAcceptButton.Location = new Point(((this.Size.Width / 2) + (Global.APP_SCREEN_LABEL_MARGINS * 2)), ((nItemsHeightAvailableSpace * 3) + (nItemsHeightAvailableSpace / 2) - (oPanelAcceptButton.Size.Height / 2)));
            oPanelAcceptButton.Visible = true;

            oLabelAcceptButton.Location = new Point(((oPanelAcceptButton.Size.Width / 2) - (oLabelAcceptButton.Size.Width / 2)), ((oPanelAcceptButton.Size.Height / 2) - (oLabelAcceptButton.Size.Height / 2)));

            this.Controls.Add(oPanelSignature);
            this.Controls.Add(oLabelSignature);

            Global.oListDinamicControls.Add(oPanelSignature);
            Global.oListDinamicControls.Add(oLabelSignature);

            oTimerInactivity.Interval = Global.APP_TIMER_INACTIVITY_SCREEN_TICKET_INTERVAL_SECONDS * 1000;
            oTimerInactivity.Enabled = true;
         }
         catch (Exception oException) {
            MessageBox.Show("ERROR 2: " + oException.ToString());
         }
      }

      private void actionNavigationScreenPrint() {
         int nItemsHeightAvailableSpace = (this.Size.Height / 4);

         oLabelHeader.Text = Global.APP_SCREEN_PRINT_LABEL_HEADER_TEXT.Replace("{1}", Format.getFormatLicensePlate(Global.oVehicle.getPlateNumber()));
         oLabelHeader.Location = new Point(((this.Size.Width - oLabelHeader.Size.Width) / 2), ((nItemsHeightAvailableSpace - oLabelHeader.Size.Height) / 2));
         oLabelHeader.Visible = true;

         oLabelDescription.Text = Global.APP_SCREEN_PRINT_LABEL_DESCRIPTION_TEXT;
         oLabelDescription.TextAlign = ContentAlignment.MiddleCenter;
         
         oImagePrinter.Location = new Point(((this.Size.Width - oImagePrinter.Size.Width) / 2), ((this.Size.Height - oImagePrinter.Size.Height - oLabelDescription.Size.Height) / 2));
         oImagePrinter.Visible = true;

         oLabelDescription.Location = new Point(((this.Size.Width - oLabelDescription.Size.Width) / 2), (oImagePrinter.Location.Y + oImagePrinter.Size.Height + Global.APP_SCREEN_LABEL_MARGINS));
         oLabelDescription.Visible = true;

         oTimerPrint.Enabled = true;

         oTimerInactivity.Interval = Global.APP_TIMER_INACTIVITY_SCREEN_PRINT_INTERVAL_SECONDS * 1000;
         oTimerInactivity.Enabled = true;
      }

      private Boolean actionCreateTicket() {
         Boolean bActionCreateTicket = false;
         SqlConnection oSQLConnection = new SqlConnection(Properties.Settings.Default.APP_DATABASE_CONNECTION_STRING);
         SqlTransaction oSQLTransaction = null;
         SqlCommand oSQLCommand;
         String sTicketNumber;
         String sTimestamp;
         String sSQLInsert;
         
         try {
            oSQLConnection.Open();

            oSQLTransaction = oSQLConnection.BeginTransaction();

            sTicketNumber = Global.getTicketNumber(oSQLConnection, oSQLTransaction);

            if (sTicketNumber.Length > 0) {
               sTimestamp = Global.getTimestamp(oSQLConnection, oSQLTransaction);

               if (sTimestamp.Length > 0) {
                  PanelSignature oPanelSignature = (PanelSignature) Global.oListDinamicControls[0];
                  MemoryStream oSignatureStream = oPanelSignature.getSignatureImage();
                  String sSignature = System.Convert.ToBase64String(oSignatureStream.GetBuffer());

                  sSQLInsert = "INSERT INTO moviments (numero_ticket, matricula, codigo_transportista, codigo_procedencia, codigo_producto, situacion, entrada_salida, tara, bruto, neto, fecha, autor, estado, full_de_seguiment, bascula, incidencia, comentarios, signature) " +
                               "VALUES ('" + sTicketNumber + "','" + Global.oVehicle.getPlateNumber() + "'," + Global.oVehicle.getCodeCarrier() + "," + Global.oVehicle.getCodeOrigin() + "," + Global.oVehicle.getCodeProduct() + ",'" + Global.oVehicle.getState() + "',0," + Global.oVehicle.getTare() + "," + Global.oVehicle.getGrossWeight() + "," + Global.oVehicle.getNetWeight() + ",'" + sTimestamp + "','AIWS','V', 'N', 1, 'No', '', '" + sSignature + "')";

                  oSQLCommand = new SqlCommand(sSQLInsert, oSQLConnection);
                  
                  oSQLCommand.Connection = oSQLConnection;
                  oSQLCommand.Transaction = oSQLTransaction;

                  int nSQLCommand = oSQLCommand.ExecuteNonQuery();
                  
                  if (nSQLCommand > 0) {
                     oSQLTransaction.Commit();

                     Global.oVehicle.setSignature(sSignature);
                     Global.oVehicle.setTicketNumber(sTicketNumber);

                     bActionCreateTicket = true;
                  }
                  else oSQLTransaction.Rollback();
               }
               else oSQLTransaction.Rollback();
            }
            else oSQLTransaction.Rollback();

            oSQLConnection.Close();
         }
         catch(Exception) {
            if (oSQLTransaction != null) oSQLTransaction.Rollback();

            if ((oSQLConnection != null) && (oSQLConnection.State == ConnectionState.Open)) oSQLConnection.Close();
         }

         return bActionCreateTicket;
      }

      /* =============================================================== [ EVENTS ] =============================================================== */

      private void FormScreenMain_Load(object sender, EventArgs e) {
         actionNavigationSuccess(Global.APP_SCREEN_START);
      }

      private void FormScreenMain_Click(object sender, EventArgs e) {
         if (Global.sAppCurrentScreen == Global.APP_SCREEN_START) {
            actionNavigationSuccess(Global.APP_SCREEN_LPR);
         }
         else if ((Global.sAppCurrentScreen == Global.APP_SCREEN_WARNING) && (oLabelTryAgain.Visible == true) && (Global.sAppBeforeScreen != null)) {
            actionNavigationSuccess(Global.sAppBeforeScreen);
         }
      }

      private void oLabelHeader_Click(object sender, EventArgs e) {
         if (Global.sAppCurrentScreen == Global.APP_SCREEN_START) {
            actionNavigationSuccess(Global.APP_SCREEN_LPR);
         }
      }

      private void oLabelDescription_Click(object sender, EventArgs e) {
         if (Global.sAppCurrentScreen == Global.APP_SCREEN_START) {
            actionNavigationSuccess(Global.APP_SCREEN_LPR);
         }
      }

      private void oImagePushButton_Click(object sender, EventArgs e) {
         actionNavigationSuccess(Global.APP_SCREEN_LPR);
      }

      private void oImageSafety_Click(object sender, EventArgs e) {
         actionNavigationSuccess(Global.APP_SCREEN_LPR);
      }

      private void oLabelAppName_Click(object sender, EventArgs e) {
         if (Global.sAppCurrentScreen == Global.APP_SCREEN_START) {
            actionNavigationSuccess(Global.APP_SCREEN_LPR);
         }
      }

      private void oLabelSafetyAccept_Click(object sender, EventArgs e) {
         actionNavigationSuccess(Global.APP_SCREEN_LPR);
      }

      private void oImageBusiness_Click(object sender, EventArgs e) {
         actionNavigationSuccess(Global.APP_SCREEN_LPR);
      }

      private void oLabelTryAgain_Click(object sender, EventArgs e) {
         if (Global.sAppBeforeScreen != null) {
            actionNavigationSuccess(Global.sAppBeforeScreen); 
         }
      }

      private void oImageBackButton_Click(object sender, EventArgs e) {
         if (Global.sAppBeforeScreen != null) {
            actionNavigationSuccess(Global.sAppBeforeScreen); 
         }
      }

      private void oPanelRejectButton_Click(object sender, EventArgs e) {
         actionNavigationSuccess(Global.APP_SCREEN_START);
      }

      private void oLabelRejectButton_Click(object sender, EventArgs e) {
         actionNavigationSuccess(Global.APP_SCREEN_START);
      }

      private void oPanelAcceptButton_Click(object sender, EventArgs e) {
         if (oPanelAcceptButton.BackColor == Color.LightSeaGreen) {
            if (actionCreateTicket()) {
               actionNavigationSuccess(Global.APP_SCREEN_PRINT);
            }
            else {
               actionNavigationError(Global.APP_SCREEN_ERROR_CREATE_TICKET_LABEL_ERROR_CODE_TEXT, Global.APP_SCREEN_ERROR_CREATE_TICKET_LABEL_HEADER_TEXT, Global.APP_SCREEN_ERROR_CREATE_TICKET_LABEL_DESCRIPTION_TEXT);
            }
         }
         else {
            Global.oListDinamicControls[1].ForeColor = Color.Red;
         }
      }

      private void oLabelAcceptButton_Click(object sender, EventArgs e) {
         if (oPanelAcceptButton.BackColor == Color.LightSeaGreen) {
            if (actionCreateTicket()) {
               actionNavigationSuccess(Global.APP_SCREEN_PRINT);
            }
            else {
               actionNavigationError(Global.APP_SCREEN_ERROR_CREATE_TICKET_LABEL_ERROR_CODE_TEXT, Global.APP_SCREEN_ERROR_CREATE_TICKET_LABEL_HEADER_TEXT, Global.APP_SCREEN_ERROR_CREATE_TICKET_LABEL_DESCRIPTION_TEXT);
            }
         }
      }

      private void oLabelOrigin_Click(object sender, EventArgs e, int nCodeOrigin, string sOrigin) {
         SqlConnection oSQLConnection = null;
         SqlCommand oSQLCommand;
         SqlDataReader oSQLDataReader;
         string sSQLQuery = string.Empty;

         try {
            Global.oVehicle.setCodeOrigin(nCodeOrigin);
            Global.oVehicle.setOrigin(sOrigin);

            oSQLConnection = new SqlConnection(Properties.Settings.Default.APP_DATABASE_CONNECTION_STRING);

            oSQLConnection.Open();

            sSQLQuery = "SELECT codi_prod, producte FROM productes_proc WHERE codi_proc = " + nCodeOrigin;

            oSQLCommand = new SqlCommand(sSQLQuery, oSQLConnection);

            oSQLDataReader = oSQLCommand.ExecuteReader();

            if (oSQLDataReader.HasRows) {
               // Add available products
               Global.oListAvailableProducts.Clear();
               for(;oSQLDataReader.Read();) {
                  List<String> oAvailableProduct = new List<String>();
                  oAvailableProduct.Add(oSQLDataReader[0].ToString().Trim());
                  oAvailableProduct.Add(oSQLDataReader[1].ToString());

                  Global.oListAvailableProducts.Add(oAvailableProduct);
               }

               Global.oListAvailableProducts.Sort(delegate(List<String> oItem1, List<String> oItem2) {
                  return oItem1[1].CompareTo(oItem2[1]);
               });

               oSQLDataReader.Close();

               actionNavigationSuccess(Global.APP_SCREEN_PRODUCTS);
            }
            else {
               oSQLDataReader.Close();

               actionNavigationWarning(Global.APP_SCREEN_WARNING_ORIGIN_PRODUCTS_NOT_FOUND_LABEL_WARNING_CODE_TEXT, Global.APP_SCREEN_WARNING_ORIGIN_PRODUCTS_NOT_FOUND_LABEL_HEADER_TEXT, Global.APP_SCREEN_WARNING_ORIGIN_PRODUCTS_NOT_FOUND_LABEL_DESCRIPTION_TEXT.Replace("{1}", sOrigin).Replace("{2}", Format.getFormatLicensePlate(Global.oVehicle.getPlateNumber())));
            }

            oSQLConnection.Close();
         }
         catch(SqlException) {
            if ((oSQLConnection != null) && (oSQLConnection.State == ConnectionState.Open)) oSQLConnection.Close();

            actionNavigationError(Global.APP_SCREEN_ERROR_DATABASE_CONNECTION_LABEL_ERROR_CODE_TEXT, Global.APP_SCREEN_ERROR_DATABASE_CONNECTION_LABEL_HEADER_TEXT, Global.APP_SCREEN_ERROR_DATABASE_CONNECTION_LABEL_DESCRIPTION_TEXT.Replace("{1}", (Properties.Settings.Default.APP_DATABASE_CONNECTION_STRING.Split(';')[0]).Split('=')[1]));
         }
         catch(Exception oException) {
            if ((oSQLConnection != null) && (oSQLConnection.State == ConnectionState.Open)) oSQLConnection.Close();

            actionNavigationError(Global.APP_SCREEN_ERROR_KERNEL_LABEL_ERROR_CODE_TEXT, Global.APP_SCREEN_ERROR_KERNEL_LABEL_HEADER_TEXT, Global.APP_SCREEN_ERROR_KERNEL_LABEL_DESCRIPTION_TEXT.Replace("{1}", oException.ToString()));
         }
      }

      private void oLabelProduct_Click(object sender, EventArgs e, int nCodeProduct, string sProduct) {
         Global.oVehicle.setCodeProduct(nCodeProduct);
         Global.oVehicle.setProduct(sProduct);

         actionNavigationSuccess(Global.APP_SCREEN_WEIGHING);
      }

      private void oTimerForeground_Tick(object sender, EventArgs e) {
         if (nScreenForegroundColorRed < 255) nScreenForegroundColorRed += nScreenAnimateForegroundColorRed;
         if (nScreenForegroundColorGreen < 255) nScreenForegroundColorGreen += nScreenAnimateForegroundColorGreen;
         if (nScreenForegroundColorBlue < 255) nScreenForegroundColorBlue += nScreenAnimateForegroundColorBlue;

         if ((nScreenForegroundColorRed >= 255) || (nScreenForegroundColorGreen >= 255) || (nScreenForegroundColorBlue >= 255)) {
            if (nScreenForegroundColorRed >= 255) nScreenForegroundColorRed = 255;
            if (nScreenForegroundColorGreen >= 255) nScreenForegroundColorGreen = 255;
            if (nScreenForegroundColorBlue >= 255) nScreenForegroundColorBlue = 255;

            if ((nScreenForegroundColorRed == 255) && (nScreenForegroundColorGreen == 255) && (nScreenForegroundColorBlue == 255)) {
               nScreenForegroundColorRed = 0;
               nScreenForegroundColorGreen = 0;
               nScreenForegroundColorBlue = 0;
            }
         }

         oLabelHeader.ForeColor = Color.FromArgb((int) nScreenForegroundColorRed, (int) nScreenForegroundColorGreen, (int) nScreenForegroundColorBlue);
         oLabelDescription.ForeColor = Color.FromArgb((int) nScreenForegroundColorRed, (int) nScreenForegroundColorGreen, (int) nScreenForegroundColorBlue);
      }

      private void oTimerLPR_Tick(object sender, EventArgs e) {
         SqlConnection oSQLConnection = null;
         SqlCommand oSQLCommand;
         SqlDataReader oSQLDataReader;
         string sLPRCandidates = string.Empty;
         string sSQLQuery = string.Empty;
         bool bLPRCandidate = false;
         bool bLPRError = false;
         int nItemsHeightAvailableSpace = (this.Size.Height / 4);

         try {
            if (Global.nAppCurrentLPRAttempts < Global.APP_LPR_PROCESS_NUM_ATTEMPTS) {
               if (Global.oLPRCamera.actionSnapshot(Global.APP_LPR_CAMERA_IMAGE_NAME)) {
                  lock(Global.oMonitorScreen) {
                     if (Global.sAppCurrentScreen == Global.APP_SCREEN_LPR) {
                        MemoryStream oImageLPRSnapshotStream = new MemoryStream();

                        Image oImageSnapshot = Image.FromFile(Global.APP_LPR_CAMERA_IMAGE_NAME);
                        oImageSnapshot.Save(oImageLPRSnapshotStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        oImageSnapshot.Dispose();

                        oImageLPRSnapshot.Image = Image.FromStream(oImageLPRSnapshotStream);
                        oImageLPRSnapshot.Size = new Size(oImageLPRSnapshot.Image.Size.Width, (oPanelLPRSnapshot.Size.Height));
                        oImageLPRSnapshot.Location = new Point(((oPanelLPRSnapshot.Size.Width - oImageLPRSnapshot.Size.Width) / 2), 0);

                        oImageLPRSnapshot.Refresh();
                     }
                  }

                  List<Candidate> oLPRCandidates = LicensePlateRecognition.actionAnalyzeImage(Global.APP_LPR_CAMERA_IMAGE_NAME);
                  if ((oLPRCandidates != null) && (oLPRCandidates.Count > 0)) {
                     for(int j = 0; ((j < oLPRCandidates.Count) && (!bLPRError) && (!bLPRCandidate)); j++) {
                        string sLPRCandidate = oLPRCandidates[j].text.Trim();

                        if (sLPRCandidates.Length > 0) sLPRCandidates += ", " + Format.getFormatLicensePlate(sLPRCandidate);
                        else sLPRCandidates += Format.getFormatLicensePlate(sLPRCandidate);

                        oSQLConnection = new SqlConnection(Properties.Settings.Default.APP_DATABASE_CONNECTION_STRING);

                        oSQLConnection.Open();

                        sSQLQuery = "SELECT Vehiculos.matricula, Vehiculos.situacion, Vehiculos.entrada_autorizada FROM Vehiculos " +
                                    "WHERE ((Vehiculos.matricula = '" + sLPRCandidate.Replace(" ", "") + "') OR (Vehiculos.matricula = '" + Format.getFormatLicensePlate(sLPRCandidate) + "'))";

                        oSQLCommand = new SqlCommand(sSQLQuery, oSQLConnection);

                        oSQLDataReader = oSQLCommand.ExecuteReader();

                        if (oSQLDataReader.HasRows) {
                           oSQLDataReader.Read();

                           if (oSQLDataReader[2].ToString().Trim() == "1") {
                              Global.oVehicle.setPlateNumber(oSQLDataReader[0].ToString().Trim());
                              Global.oVehicle.setState(oSQLDataReader[1].ToString().Trim());

                              oSQLDataReader.Close();

                              sSQLQuery = "SELECT Vehiculos.matricula, Procedencia_1.codigo AS codeOrigin1, Procedencia_1.procedencia AS origin1, Procedencia_2.codigo AS codeOrigin2, Procedencia_2.procedencia AS origin2, Procedencia_3.codigo AS codeOrigin3, Procedencia_3.procedencia AS origin3, Procedencia_4.codigo AS codeOrigin4, Procedencia_4.procedencia AS origin4, Procedencia.codigo AS codeOrigin5, Procedencia.procedencia AS origin5 FROM Vehiculos " +
                                          "LEFT OUTER JOIN Procedencia AS Procedencia_1 ON Vehiculos.procedencia = Procedencia_1.procedencia " +
                                          "LEFT OUTER JOIN Procedencia ON Vehiculos.procedencia5 = Procedencia.procedencia " +
                                          "LEFT OUTER JOIN Procedencia AS Procedencia_4 ON Vehiculos.procedencia4 = Procedencia_4.procedencia " +
                                          "LEFT OUTER JOIN Procedencia AS Procedencia_3 ON Vehiculos.procedencia3 = Procedencia_3.procedencia " +
                                          "LEFT OUTER JOIN Procedencia AS Procedencia_2 ON Vehiculos.procedencia2 = Procedencia_2.procedencia " +
                                          "WHERE ((Vehiculos.entrada_autorizada = '1') AND ((Vehiculos.matricula = '" + sLPRCandidate.Replace(" ", "") + "') OR (Vehiculos.matricula = '" + Format.getFormatLicensePlate(sLPRCandidate) + "')) AND " +
                                          "((Procedencia_1.autorizado = '1') OR (Procedencia_2.autorizado = '1') OR (Procedencia_3.autorizado = '1') OR (Procedencia_4.autorizado = '1') OR (Procedencia.autorizado = '1')))";

                              oSQLCommand = new SqlCommand(sSQLQuery, oSQLConnection);

                              oSQLDataReader = oSQLCommand.ExecuteReader();

                              if (oSQLDataReader.HasRows) {
                                 oSQLDataReader.Read();

                                 // Add available origins
                                 Global.oListAvailableOrigins.Clear();
                                 if ((oSQLDataReader[1].ToString().Trim().Length > 0) && (oSQLDataReader[2].ToString().Trim().Length > 0)) {
                                    List<String> oAvailableOrigin = new List<String>();
                                    oAvailableOrigin.Add(oSQLDataReader[1].ToString().Trim());
                                    oAvailableOrigin.Add(oSQLDataReader[2].ToString());

                                    Global.oListAvailableOrigins.Add(oAvailableOrigin);
                                 }

                                 if ((oSQLDataReader[3].ToString().Trim().Length > 0) && (oSQLDataReader[4].ToString().Trim().Length > 0)) {
                                    List<String> oAvailableOrigin = new List<String>();
                                    oAvailableOrigin.Add(oSQLDataReader[3].ToString().Trim());
                                    oAvailableOrigin.Add(oSQLDataReader[4].ToString());

                                    Global.oListAvailableOrigins.Add(oAvailableOrigin);
                                 }

                                 if ((oSQLDataReader[5].ToString().Trim().Length > 0) && (oSQLDataReader[6].ToString().Trim().Length > 0)) {
                                    List<String> oAvailableOrigin = new List<String>();
                                    oAvailableOrigin.Add(oSQLDataReader[5].ToString().Trim());
                                    oAvailableOrigin.Add(oSQLDataReader[6].ToString());

                                    Global.oListAvailableOrigins.Add(oAvailableOrigin);
                                 }

                                 if ((oSQLDataReader[7].ToString().Trim().Length > 0) && (oSQLDataReader[8].ToString().Trim().Length > 0)) {
                                    List<String> oAvailableOrigin = new List<String>();
                                    oAvailableOrigin.Add(oSQLDataReader[7].ToString().Trim());
                                    oAvailableOrigin.Add(oSQLDataReader[8].ToString());

                                    Global.oListAvailableOrigins.Add(oAvailableOrigin);
                                 }

                                 if ((oSQLDataReader[9].ToString().Trim().Length > 0) && (oSQLDataReader[10].ToString().Trim().Length > 0)) {
                                    List<String> oAvailableOrigin = new List<String>();
                                    oAvailableOrigin.Add(oSQLDataReader[9].ToString().Trim());
                                    oAvailableOrigin.Add(oSQLDataReader[10].ToString());

                                    Global.oListAvailableOrigins.Add(oAvailableOrigin);
                                 }


                                 Global.oListAvailableOrigins.Sort(delegate(List<String> oItem1, List<String> oItem2) {
                                    return oItem1[1].CompareTo(oItem2[1]);
                                 });

                                 oSQLDataReader.Close();

                                 // Check if the carrier exists
                                 sSQLQuery = "SELECT Vehiculos.matricula, Transportista.codigo, Transportista.transportista FROM Vehiculos " +
                                             "INNER JOIN Transportista ON Vehiculos.transportista = Transportista.transportista " +
                                             "WHERE ((Vehiculos.entrada_autorizada = '1') AND ((Vehiculos.matricula = '" + sLPRCandidate.Replace(" ", "") + "') OR (Vehiculos.matricula = '" + Format.getFormatLicensePlate(sLPRCandidate) + "')))";

                                 oSQLCommand = new SqlCommand(sSQLQuery, oSQLConnection);

                                 oSQLDataReader = oSQLCommand.ExecuteReader();

                                 if (oSQLDataReader.HasRows) {
                                    oSQLDataReader.Read();

                                    Global.oVehicle.setCodeCarrier(Int32.Parse(oSQLDataReader[1].ToString().Trim()));
                                    Global.oVehicle.setCarrier(oSQLDataReader[2].ToString().Trim());

                                    oSQLDataReader.Close();

                                    // Check if there are some product assigned to the origins
                                    string sSQLFilter = String.Empty;

                                    sSQLQuery = "SELECT codi_proc, codi_prod FROM productes_proc ";

                                    for(int i = 0; i < Global.oListAvailableOrigins.Count; i++) {
                                       if (sSQLFilter.Length > 0) sSQLFilter += " OR (codi_proc = " + Global.oListAvailableOrigins[i][0] + ")";   
                                       else sSQLFilter = "(codi_proc = " + Global.oListAvailableOrigins[i][0] + ")";   
                                    }

                                    sSQLQuery += "WHERE (" + sSQLFilter + ")";

                                    oSQLCommand = new SqlCommand(sSQLQuery, oSQLConnection);

                                    oSQLDataReader = oSQLCommand.ExecuteReader();

                                    if (oSQLDataReader.HasRows) {
                                       oSQLDataReader.Close();

                                       sSQLQuery = "SELECT Vehiculos.matricula, Vehiculos.tara1 FROM Vehiculos " +
                                                   "WHERE (((Vehiculos.matricula = '" + sLPRCandidate.Replace(" ", "") + "') OR (Vehiculos.matricula = '" + Format.getFormatLicensePlate(sLPRCandidate) + "')) AND ((tara1 <> 0) AND (tara2 = 0) AND (tara3 = 0) AND (tara4 = 0)))";

                                       oSQLCommand = new SqlCommand(sSQLQuery, oSQLConnection);

                                       oSQLDataReader = oSQLCommand.ExecuteReader();

                                       if (oSQLDataReader.HasRows) {
                                          oSQLDataReader.Read();

                                          Global.oVehicle.setTare(Int32.Parse(oSQLDataReader[1].ToString().Trim()));

                                          oSQLDataReader.Close();

                                          actionNavigationSuccess(Global.APP_SCREEN_ORIGINS);

                                          Global.nAppCurrentLPRAttempts = 0;

                                          bLPRCandidate = true;
                                       }
                                       else {
                                          oSQLDataReader.Close();

                                          actionNavigationWarning(Global.APP_SCREEN_WARNING_LPR_PROCESS_TARE_NOT_FOUND_LABEL_WARNING_CODE_TEXT, Global.APP_SCREEN_WARNING_LPR_PROCESS_TARE_NOT_FOUND_LABEL_HEADER_TEXT, Global.APP_SCREEN_WARNING_LPR_PROCESS_TARE_NOT_FOUND_LABEL_DESCRIPTION_TEXT.Replace("{1}", Format.getFormatLicensePlate(Global.oVehicle.getPlateNumber())));

                                          bLPRError = true;
                                       }
                                    }
                                    else {
                                       oSQLDataReader.Close();

                                       actionNavigationWarning(Global.APP_SCREEN_WARNING_LPR_PROCESS_PRODUCTS_NOT_FOUND_LABEL_WARNING_CODE_TEXT, Global.APP_SCREEN_WARNING_LPR_PROCESS_PRODUCTS_NOT_FOUND_LABEL_HEADER_TEXT, Global.APP_SCREEN_WARNING_LPR_PROCESS_PRODUCTS_NOT_FOUND_LABEL_DESCRIPTION_TEXT.Replace("{1}", Format.getFormatLicensePlate(Global.oVehicle.getPlateNumber())));

                                       bLPRError = true;
                                    }
                                 }
                                 else {
                                    oSQLDataReader.Close();

                                    actionNavigationWarning(Global.APP_SCREEN_WARNING_LPR_PROCESS_CARRIER_NOT_FOUND_LABEL_WARNING_CODE_TEXT, Global.APP_SCREEN_WARNING_LPR_PROCESS_CARRIER_NOT_FOUND_LABEL_HEADER_TEXT, Global.APP_SCREEN_WARNING_LPR_PROCESS_CARRIER_NOT_FOUND_LABEL_DESCRIPTION_TEXT.Replace("{1}", Format.getFormatLicensePlate(Global.oVehicle.getPlateNumber())));

                                    bLPRError = true;
                                 }
                              }
                              else {
                                 oSQLDataReader.Close();

                                 actionNavigationWarning(Global.APP_SCREEN_WARNING_LPR_PROCESS_NOT_VALID_CANDIDATES_LABEL_WARNING_CODE_TEXT, Global.APP_SCREEN_WARNING_LPR_PROCESS_NOT_VALID_CANDIDATES_LABEL_HEADER_TEXT, Global.APP_SCREEN_WARNING_LPR_PROCESS_NOT_VALID_CANDIDATES_LABEL_DESCRIPTION_TEXT.Replace("{1}", Format.getFormatLicensePlate(sLPRCandidates)), Global.APP_SCREEN_WARNING_LPR_PROCESS_NOT_VALID_CANDIDATES_LABEL_TRY_AGAIN_TEXT);

                                 bLPRError = true;
                              }
                           }
                           else {
                              oSQLDataReader.Close();

                              actionNavigationWarning(Global.APP_SCREEN_WARNING_LPR_PROCESS_NOT_VALID_CANDIDATES_LABEL_WARNING_CODE_TEXT, Global.APP_SCREEN_WARNING_LPR_PROCESS_NOT_VALID_CANDIDATES_LABEL_HEADER_TEXT, Global.APP_SCREEN_WARNING_LPR_PROCESS_NOT_VALID_CANDIDATES_LABEL_DESCRIPTION_TEXT.Replace("{1}", Format.getFormatLicensePlate(sLPRCandidates)), Global.APP_SCREEN_WARNING_LPR_PROCESS_NOT_VALID_CANDIDATES_LABEL_TRY_AGAIN_TEXT);

                              bLPRError = true;
                           }
                        }
                        else oSQLDataReader.Close();

                        oSQLConnection.Close();
                     }

                     if ((sLPRCandidates.Length > 0) && (!bLPRError) && (!bLPRCandidate)) {
                        lock(Global.oMonitorScreen) {
                           if (Global.sAppCurrentScreen == Global.APP_SCREEN_LPR) {
                              oLabelDescription.Text = sLPRCandidates;
                              oLabelDescription.Location = new Point(((this.Size.Width - oLabelDescription.Size.Width) / 2), ((nItemsHeightAvailableSpace * 3) + (nItemsHeightAvailableSpace / 2) - (oLabelDescription.Size.Height / 2)));

                              this.Refresh();
                           }
                        }
                     }

                     Global.nAppCurrentLPRAttempts++;
                  }
               }
               else {
                  actionNavigationError(Global.APP_SCREEN_ERROR_LPR_CAMERA_CONNECTION_LABEL_ERROR_CODE_TEXT, Global.APP_SCREEN_ERROR_LPR_CAMERA_CONNECTION_LABEL_HEADER_TEXT, Global.APP_SCREEN_ERROR_LPR_CAMERA_CONNECTION_LABEL_DESCRIPTION_TEXT.Replace("{1}", Global.APP_LPR_CAMERA_IP_ADDRESS));
              
                  bLPRError = true;
               }
            }
            else {
               actionNavigationWarning(Global.APP_SCREEN_WARNING_LPR_PROCESS_NOT_VALID_CANDIDATES_LABEL_WARNING_CODE_TEXT, Global.APP_SCREEN_WARNING_LPR_PROCESS_NOT_VALID_CANDIDATES_LABEL_HEADER_TEXT, Global.APP_SCREEN_WARNING_LPR_PROCESS_NOT_VALID_CANDIDATES_LABEL_DESCRIPTION_TEXT.Replace("{1}", Format.getFormatLicensePlate(sLPRCandidates)), Global.APP_SCREEN_WARNING_LPR_PROCESS_NOT_VALID_CANDIDATES_LABEL_TRY_AGAIN_TEXT);

               bLPRError = true;
            }
         }
         catch(SqlException) {
            if ((oSQLConnection != null) && (oSQLConnection.State == ConnectionState.Open)) oSQLConnection.Close();

            actionNavigationError(Global.APP_SCREEN_ERROR_DATABASE_CONNECTION_LABEL_ERROR_CODE_TEXT, Global.APP_SCREEN_ERROR_DATABASE_CONNECTION_LABEL_HEADER_TEXT, Global.APP_SCREEN_ERROR_DATABASE_CONNECTION_LABEL_DESCRIPTION_TEXT.Replace("{1}", (Properties.Settings.Default.APP_DATABASE_CONNECTION_STRING.Split(';')[0]).Split('=')[1]));

            bLPRError = true;
         }
         catch(Exception oException) {
            if ((oSQLConnection != null) && (oSQLConnection.State == ConnectionState.Open)) oSQLConnection.Close();

            actionNavigationError(Global.APP_SCREEN_ERROR_KERNEL_LABEL_ERROR_CODE_TEXT, Global.APP_SCREEN_ERROR_KERNEL_LABEL_HEADER_TEXT, Global.APP_SCREEN_ERROR_KERNEL_LABEL_DESCRIPTION_TEXT.Replace("{1}", oException.ToString()));

            bLPRError = true;
         }
      }

      private void oTimerWeighing_Tick(object sender, EventArgs e) {
         try {
            if (Global.nWeighingStatus == Global.APP_WEIGHING_STATUS_NONE) {
               if (Global.oWeighingSerialPort.IsOpen) Global.oWeighingSerialPort.Close();

               if (!Global.oWeighingSerialPort.IsOpen) {
                  Global.oWeighingSerialPort.Open();
                  Global.oWeighingSerialPort.WriteLine(Global.APP_WEIGHING_SERIAL_PORT_COM_PROTOCOL_SEND_DATA);
               }
            }
            else {
               if (Global.nWeighingStatus == Global.APP_WEIGHING_STATUS_SUCCESS) {
                  oLabelDescription.Text = Global.oVehicle.getGrossWeight().ToString().PadLeft(6, '0') + " KG";
                  oLabelDescription.Location = new Point(((this.Size.Width - oLabelDescription.Size.Width) / 2), ((this.Size.Height - oLabelDescription.Size.Height) / 2));

                  this.Refresh();

                  oTimerWeighing.Enabled = false;
                  oTimerWeighingChangeScreen.Enabled = true;
               }
               else {
                  actionNavigationWarning(Global.APP_SCREEN_WARNING_WEIGHING_SERIAL_PORT_TARE_MAJOR_THAN_GROSS_WEIGHT_LABEL_WARNING_CODE_TEXT, Global.APP_SCREEN_WARNING_WEIGHING_SERIAL_PORT_TARE_MAJOR_THAN_GROSS_WEIGHT_LABEL_HEADER_TEXT, Global.APP_SCREEN_WARNING_WEIGHING_SERIAL_PORT_TARE_MAJOR_THAN_GROSS_WEIGHT_LABEL_DESCRIPTION_TEXT.Replace("{1}", Format.getFormatLicensePlate(Global.oVehicle.getPlateNumber())));
               }
            }
         }
         catch(TimeoutException) {
            if (Global.oWeighingSerialPort.IsOpen) Global.oWeighingSerialPort.Close();
         }
         catch (Exception) {
            if (Global.oWeighingSerialPort.IsOpen) Global.oWeighingSerialPort.Close();

            actionNavigationError(Global.APP_SCREEN_ERROR_WEIGHING_SERIAL_PORT_CONNECTION_LABEL_ERROR_CODE_TEXT, Global.APP_SCREEN_ERROR_WEIGHING_SERIAL_PORT_CONNECTION_LABEL_HEADER_TEXT, Global.APP_SCREEN_ERROR_WEIGHING_SERIAL_PORT_CONNECTION_LABEL_DESCRIPTION_TEXT);
         }
      }

      private void oWeighingSerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e) {
         try {
            string sWeighingSerialPortData = Global.oWeighingSerialPort.ReadExisting();
            sWeighingSerialPortData = sWeighingSerialPortData.Trim();
            int nItemsHeightAvailableSpace = (this.Size.Height / 4);
            
            if (Global.oWeighingSerialPort.IsOpen) Global.oWeighingSerialPort.Close();

            if (sWeighingSerialPortData.Length > 0) {
               int nWeighingSerialPortData = Int32.Parse(sWeighingSerialPortData);

               if (nWeighingSerialPortData > 0) {
                  if (nWeighingSerialPortData >= Global.oVehicle.getTare()) { 
                     Global.oVehicle.setGrossWeight(nWeighingSerialPortData);
                     Global.oVehicle.setNetWeight(Global.oVehicle.getGrossWeight() - Global.oVehicle.getTare());

                     Global.nWeighingStatus = Global.APP_WEIGHING_STATUS_SUCCESS;
                  }
                  else {
                     Global.nWeighingStatus = Global.APP_WEIGHING_STATUS_ERROR;
                  }
               }
            }
         }
         catch (Exception) {
            if (Global.oWeighingSerialPort.IsOpen) Global.oWeighingSerialPort.Close();
         }
      }

      private void oTimerWeighingChangeScreen_Tick(object sender, EventArgs e) {
         actionNavigationSuccess(Global.APP_SCREEN_TICKET);
      }

      private void oTimerPrint_Tick(object sender, EventArgs e) {
         TicketEnter oTicketEnterReport;
         Filter oTicketEnterReportFilter;
         IReportDocument oReportDocument;
         PrinterSettings oPrinterSettings;

         try {
            oTimerPrint.Enabled = false;

            oTicketEnterReport = new TicketEnter();
            oTicketEnterReportFilter = new Filter();
         
            oTicketEnterReport.param_labelBascula.Visible = true;
            oTicketEnterReport.param_textBascula.Visible = true;
         
            oTicketEnterReportFilter.Expression = "=Fields.numero_ticket";
            oTicketEnterReportFilter.Operator = FilterOperator.Like;
            oTicketEnterReportFilter.Value = Global.oVehicle.getTicketNumber();
         
            oTicketEnterReport.Filters.Add(oTicketEnterReportFilter);

            MemoryStream oSignatureStream = new MemoryStream(System.Convert.FromBase64String(Global.oVehicle.getSignature()), true);

            oTicketEnterReport.oImageSignature1.Value = Image.FromStream(oSignatureStream);
            oTicketEnterReport.oImageSignature2.Value = Image.FromStream(oSignatureStream);

            oTicketEnterReport.oImageSignature1.Visible = true;
            oTicketEnterReport.oImageSignature2.Visible = true;

            oReportDocument = oTicketEnterReport;

            oPrinterSettings = new PrinterSettings();

            ReportProcessor oReportProcessor = new ReportProcessor();
            oReportProcessor.PrintController = new StandardPrintController();

            InstanceReportSource oInstanceReportSource = new InstanceReportSource();
            oInstanceReportSource.ReportDocument = oReportDocument;

            oReportProcessor.PrintReport(oInstanceReportSource, oPrinterSettings);
         }
         catch (Exception oException) {
            actionNavigationError(Global.APP_SCREEN_ERROR_KERNEL_LABEL_ERROR_CODE_TEXT, Global.APP_SCREEN_ERROR_KERNEL_LABEL_HEADER_TEXT, Global.APP_SCREEN_ERROR_KERNEL_LABEL_DESCRIPTION_TEXT.Replace("{1}", oException.ToString()));
         }
      }

      private void oTimerInactivity_Tick(object sender, EventArgs e) {
         actionNavigationSuccess(Global.APP_SCREEN_START);
      }
   }
}