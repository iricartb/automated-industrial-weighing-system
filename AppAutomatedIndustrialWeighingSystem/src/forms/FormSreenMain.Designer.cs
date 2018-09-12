namespace AppAutomatedIndustrialWeighingSystem
{
   partial class FormScreenMain
   {
      /// <summary>
      /// Variable del diseñador requerida.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Limpiar los recursos que se estén utilizando.
      /// </summary>
      /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Código generado por el Diseñador de Windows Forms

      /// <summary>
      /// Método necesario para admitir el Diseñador. No se puede modificar
      /// el contenido del método con el editor de código.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         this.oTimerForeground = new System.Windows.Forms.Timer(this.components);
         this.oLabelAppName = new System.Windows.Forms.Label();
         this.oLabelDescription = new System.Windows.Forms.Label();
         this.oTimerInactivity = new System.Windows.Forms.Timer(this.components);
         this.oTimerLPR = new System.Windows.Forms.Timer(this.components);
         this.oLabelTryAgain = new System.Windows.Forms.Label();
         this.oTimerWeighing = new System.Windows.Forms.Timer(this.components);
         this.oLabelHeader = new System.Windows.Forms.Label();
         this.oPanelRejectButton = new System.Windows.Forms.Panel();
         this.oLabelRejectButton = new System.Windows.Forms.Label();
         this.oPanelAcceptButton = new System.Windows.Forms.Panel();
         this.oLabelAcceptButton = new System.Windows.Forms.Label();
         this.oTimerPrint = new System.Windows.Forms.Timer(this.components);
         this.oPanelLPRSnapshot = new System.Windows.Forms.Panel();
         this.oImageLPRSnapshot = new System.Windows.Forms.PictureBox();
         this.oImagePrinter = new System.Windows.Forms.PictureBox();
         this.oImageSignature = new System.Windows.Forms.PictureBox();
         this.oImageVehicle = new System.Windows.Forms.PictureBox();
         this.oImagePushButton = new System.Windows.Forms.PictureBox();
         this.oImageSafety = new System.Windows.Forms.PictureBox();
         this.oImageBusiness = new System.Windows.Forms.PictureBox();
         this.oImageBackButton = new System.Windows.Forms.PictureBox();
         this.oTimerWeighingChangeScreen = new System.Windows.Forms.Timer(this.components);
         this.oPanelRejectButton.SuspendLayout();
         this.oPanelAcceptButton.SuspendLayout();
         this.oPanelLPRSnapshot.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.oImageLPRSnapshot)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.oImagePrinter)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.oImageSignature)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.oImageVehicle)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.oImagePushButton)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.oImageSafety)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.oImageBusiness)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.oImageBackButton)).BeginInit();
         this.SuspendLayout();
         // 
         // oTimerForeground
         // 
         this.oTimerForeground.Tick += new System.EventHandler(this.oTimerForeground_Tick);
         // 
         // oLabelAppName
         // 
         this.oLabelAppName.AutoSize = true;
         this.oLabelAppName.Cursor = System.Windows.Forms.Cursors.Hand;
         this.oLabelAppName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.oLabelAppName.ForeColor = System.Drawing.Color.White;
         this.oLabelAppName.Location = new System.Drawing.Point(901, 743);
         this.oLabelAppName.Name = "oLabelAppName";
         this.oLabelAppName.Size = new System.Drawing.Size(111, 16);
         this.oLabelAppName.TabIndex = 5;
         this.oLabelAppName.Text = "oLabelAppName";
         this.oLabelAppName.Click += new System.EventHandler(this.oLabelAppName_Click);
         // 
         // oLabelDescription
         // 
         this.oLabelDescription.AutoSize = true;
         this.oLabelDescription.Cursor = System.Windows.Forms.Cursors.Hand;
         this.oLabelDescription.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.oLabelDescription.ForeColor = System.Drawing.Color.White;
         this.oLabelDescription.Location = new System.Drawing.Point(0, 43);
         this.oLabelDescription.Name = "oLabelDescription";
         this.oLabelDescription.Size = new System.Drawing.Size(225, 29);
         this.oLabelDescription.TabIndex = 6;
         this.oLabelDescription.Text = "oLabelDescription";
         this.oLabelDescription.Click += new System.EventHandler(this.oLabelDescription_Click);
         // 
         // oTimerInactivity
         // 
         this.oTimerInactivity.Interval = 60000;
         this.oTimerInactivity.Tick += new System.EventHandler(this.oTimerInactivity_Tick);
         // 
         // oTimerLPR
         // 
         this.oTimerLPR.Interval = 1000;
         this.oTimerLPR.Tick += new System.EventHandler(this.oTimerLPR_Tick);
         // 
         // oLabelTryAgain
         // 
         this.oLabelTryAgain.AutoSize = true;
         this.oLabelTryAgain.Cursor = System.Windows.Forms.Cursors.Hand;
         this.oLabelTryAgain.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.oLabelTryAgain.ForeColor = System.Drawing.Color.White;
         this.oLabelTryAgain.Location = new System.Drawing.Point(2, 77);
         this.oLabelTryAgain.Name = "oLabelTryAgain";
         this.oLabelTryAgain.Size = new System.Drawing.Size(193, 29);
         this.oLabelTryAgain.TabIndex = 8;
         this.oLabelTryAgain.Text = "oLabelTryAgain";
         this.oLabelTryAgain.Click += new System.EventHandler(this.oLabelTryAgain_Click);
         // 
         // oTimerWeighing
         // 
         this.oTimerWeighing.Interval = 3000;
         this.oTimerWeighing.Tick += new System.EventHandler(this.oTimerWeighing_Tick);
         // 
         // oLabelHeader
         // 
         this.oLabelHeader.AutoSize = true;
         this.oLabelHeader.Cursor = System.Windows.Forms.Cursors.Hand;
         this.oLabelHeader.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.oLabelHeader.ForeColor = System.Drawing.Color.White;
         this.oLabelHeader.Location = new System.Drawing.Point(0, 0);
         this.oLabelHeader.Name = "oLabelHeader";
         this.oLabelHeader.Size = new System.Drawing.Size(262, 38);
         this.oLabelHeader.TabIndex = 14;
         this.oLabelHeader.Text = "oLabelHeader";
         this.oLabelHeader.Click += new System.EventHandler(this.oLabelHeader_Click);
         // 
         // oPanelRejectButton
         // 
         this.oPanelRejectButton.BackColor = System.Drawing.Color.Brown;
         this.oPanelRejectButton.Controls.Add(this.oLabelRejectButton);
         this.oPanelRejectButton.Cursor = System.Windows.Forms.Cursors.Hand;
         this.oPanelRejectButton.Location = new System.Drawing.Point(424, 369);
         this.oPanelRejectButton.Name = "oPanelRejectButton";
         this.oPanelRejectButton.Size = new System.Drawing.Size(300, 81);
         this.oPanelRejectButton.TabIndex = 19;
         this.oPanelRejectButton.Click += new System.EventHandler(this.oPanelRejectButton_Click);
         // 
         // oLabelRejectButton
         // 
         this.oLabelRejectButton.AutoSize = true;
         this.oLabelRejectButton.BackColor = System.Drawing.Color.Transparent;
         this.oLabelRejectButton.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.oLabelRejectButton.ForeColor = System.Drawing.Color.White;
         this.oLabelRejectButton.Location = new System.Drawing.Point(53, 30);
         this.oLabelRejectButton.Name = "oLabelRejectButton";
         this.oLabelRejectButton.Size = new System.Drawing.Size(175, 25);
         this.oLabelRejectButton.TabIndex = 19;
         this.oLabelRejectButton.Text = "NO CONFORME";
         this.oLabelRejectButton.Click += new System.EventHandler(this.oLabelRejectButton_Click);
         // 
         // oPanelAcceptButton
         // 
         this.oPanelAcceptButton.BackColor = System.Drawing.Color.LightSeaGreen;
         this.oPanelAcceptButton.Controls.Add(this.oLabelAcceptButton);
         this.oPanelAcceptButton.Cursor = System.Windows.Forms.Cursors.Hand;
         this.oPanelAcceptButton.Location = new System.Drawing.Point(724, 369);
         this.oPanelAcceptButton.Name = "oPanelAcceptButton";
         this.oPanelAcceptButton.Size = new System.Drawing.Size(300, 81);
         this.oPanelAcceptButton.TabIndex = 20;
         this.oPanelAcceptButton.Click += new System.EventHandler(this.oPanelAcceptButton_Click);
         // 
         // oLabelAcceptButton
         // 
         this.oLabelAcceptButton.AutoSize = true;
         this.oLabelAcceptButton.BackColor = System.Drawing.Color.Transparent;
         this.oLabelAcceptButton.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.oLabelAcceptButton.ForeColor = System.Drawing.Color.White;
         this.oLabelAcceptButton.Location = new System.Drawing.Point(80, 30);
         this.oLabelAcceptButton.Name = "oLabelAcceptButton";
         this.oLabelAcceptButton.Size = new System.Drawing.Size(135, 25);
         this.oLabelAcceptButton.TabIndex = 19;
         this.oLabelAcceptButton.Text = "CONFORME";
         this.oLabelAcceptButton.Click += new System.EventHandler(this.oLabelAcceptButton_Click);
         // 
         // oTimerPrint
         // 
         this.oTimerPrint.Interval = 1000;
         this.oTimerPrint.Tick += new System.EventHandler(this.oTimerPrint_Tick);
         // 
         // oPanelLPRSnapshot
         // 
         this.oPanelLPRSnapshot.BackColor = System.Drawing.Color.Black;
         this.oPanelLPRSnapshot.Controls.Add(this.oImageLPRSnapshot);
         this.oPanelLPRSnapshot.Location = new System.Drawing.Point(238, 217);
         this.oPanelLPRSnapshot.Name = "oPanelLPRSnapshot";
         this.oPanelLPRSnapshot.Size = new System.Drawing.Size(282, 107);
         this.oPanelLPRSnapshot.TabIndex = 23;
         // 
         // oImageLPRSnapshot
         // 
         this.oImageLPRSnapshot.Cursor = System.Windows.Forms.Cursors.Hand;
         this.oImageLPRSnapshot.ErrorImage = null;
         this.oImageLPRSnapshot.Location = new System.Drawing.Point(72, 1);
         this.oImageLPRSnapshot.Name = "oImageLPRSnapshot";
         this.oImageLPRSnapshot.Size = new System.Drawing.Size(138, 106);
         this.oImageLPRSnapshot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         this.oImageLPRSnapshot.TabIndex = 23;
         this.oImageLPRSnapshot.TabStop = false;
         // 
         // oImagePrinter
         // 
         this.oImagePrinter.Cursor = System.Windows.Forms.Cursors.Hand;
         this.oImagePrinter.ErrorImage = null;
         this.oImagePrinter.Image = global::AppAutomatedIndustrialWeighingSystem.Properties.Resources.ImagePrinter;
         this.oImagePrinter.Location = new System.Drawing.Point(457, 7);
         this.oImagePrinter.Name = "oImagePrinter";
         this.oImagePrinter.Size = new System.Drawing.Size(192, 184);
         this.oImagePrinter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         this.oImagePrinter.TabIndex = 21;
         this.oImagePrinter.TabStop = false;
         // 
         // oImageSignature
         // 
         this.oImageSignature.Cursor = System.Windows.Forms.Cursors.Hand;
         this.oImageSignature.ErrorImage = null;
         this.oImageSignature.Image = global::AppAutomatedIndustrialWeighingSystem.Properties.Resources.ImageSignature;
         this.oImageSignature.Location = new System.Drawing.Point(520, 217);
         this.oImageSignature.Name = "oImageSignature";
         this.oImageSignature.Size = new System.Drawing.Size(195, 151);
         this.oImageSignature.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         this.oImageSignature.TabIndex = 17;
         this.oImageSignature.TabStop = false;
         // 
         // oImageVehicle
         // 
         this.oImageVehicle.Cursor = System.Windows.Forms.Cursors.Hand;
         this.oImageVehicle.ErrorImage = null;
         this.oImageVehicle.Image = global::AppAutomatedIndustrialWeighingSystem.Properties.Resources.ImageVehicle;
         this.oImageVehicle.Location = new System.Drawing.Point(715, 192);
         this.oImageVehicle.Name = "oImageVehicle";
         this.oImageVehicle.Size = new System.Drawing.Size(310, 176);
         this.oImageVehicle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         this.oImageVehicle.TabIndex = 16;
         this.oImageVehicle.TabStop = false;
         // 
         // oImagePushButton
         // 
         this.oImagePushButton.Cursor = System.Windows.Forms.Cursors.Hand;
         this.oImagePushButton.ErrorImage = null;
         this.oImagePushButton.Image = global::AppAutomatedIndustrialWeighingSystem.Properties.Resources.ImagePushButton;
         this.oImagePushButton.Location = new System.Drawing.Point(904, 33);
         this.oImagePushButton.Name = "oImagePushButton";
         this.oImagePushButton.Size = new System.Drawing.Size(121, 158);
         this.oImagePushButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         this.oImagePushButton.TabIndex = 15;
         this.oImagePushButton.TabStop = false;
         this.oImagePushButton.Click += new System.EventHandler(this.oImagePushButton_Click);
         // 
         // oImageSafety
         // 
         this.oImageSafety.Cursor = System.Windows.Forms.Cursors.Hand;
         this.oImageSafety.ErrorImage = null;
         this.oImageSafety.Image = global::AppAutomatedIndustrialWeighingSystem.Properties.Resources.ImageSafety;
         this.oImageSafety.Location = new System.Drawing.Point(650, 113);
         this.oImageSafety.Name = "oImageSafety";
         this.oImageSafety.Size = new System.Drawing.Size(254, 78);
         this.oImageSafety.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         this.oImageSafety.TabIndex = 13;
         this.oImageSafety.TabStop = false;
         this.oImageSafety.Click += new System.EventHandler(this.oImageSafety_Click);
         // 
         // oImageBusiness
         // 
         this.oImageBusiness.Cursor = System.Windows.Forms.Cursors.Hand;
         this.oImageBusiness.ErrorImage = null;
         this.oImageBusiness.Image = global::AppAutomatedIndustrialWeighingSystem.Properties.Resources.ImageBusiness;
         this.oImageBusiness.Location = new System.Drawing.Point(474, 330);
         this.oImageBusiness.Name = "oImageBusiness";
         this.oImageBusiness.Size = new System.Drawing.Size(46, 38);
         this.oImageBusiness.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         this.oImageBusiness.TabIndex = 11;
         this.oImageBusiness.TabStop = false;
         this.oImageBusiness.Click += new System.EventHandler(this.oImageBusiness_Click);
         // 
         // oImageBackButton
         // 
         this.oImageBackButton.Cursor = System.Windows.Forms.Cursors.Hand;
         this.oImageBackButton.ErrorImage = null;
         this.oImageBackButton.Image = global::AppAutomatedIndustrialWeighingSystem.Properties.Resources.ImageBackButton;
         this.oImageBackButton.Location = new System.Drawing.Point(789, 7);
         this.oImageBackButton.Name = "oImageBackButton";
         this.oImageBackButton.Size = new System.Drawing.Size(115, 106);
         this.oImageBackButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         this.oImageBackButton.TabIndex = 10;
         this.oImageBackButton.TabStop = false;
         this.oImageBackButton.Click += new System.EventHandler(this.oImageBackButton_Click);
         // 
         // oTimerWeighingChangeScreen
         // 
         this.oTimerWeighingChangeScreen.Interval = 1500;
         this.oTimerWeighingChangeScreen.Tick += new System.EventHandler(this.oTimerWeighingChangeScreen_Tick);
         // 
         // FormScreenMain
         // 
         this.BackColor = System.Drawing.Color.Black;
         this.ClientSize = new System.Drawing.Size(1024, 768);
         this.ControlBox = false;
         this.Controls.Add(this.oPanelLPRSnapshot);
         this.Controls.Add(this.oImagePrinter);
         this.Controls.Add(this.oPanelAcceptButton);
         this.Controls.Add(this.oPanelRejectButton);
         this.Controls.Add(this.oImageSignature);
         this.Controls.Add(this.oImageVehicle);
         this.Controls.Add(this.oImagePushButton);
         this.Controls.Add(this.oLabelHeader);
         this.Controls.Add(this.oImageSafety);
         this.Controls.Add(this.oImageBusiness);
         this.Controls.Add(this.oImageBackButton);
         this.Controls.Add(this.oLabelTryAgain);
         this.Controls.Add(this.oLabelDescription);
         this.Controls.Add(this.oLabelAppName);
         this.DoubleBuffered = true;
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
         this.Name = "FormScreenMain";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "FormScreenMain";
         this.TopMost = true;
         this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
         this.Load += new System.EventHandler(this.FormScreenMain_Load);
         this.Click += new System.EventHandler(this.FormScreenMain_Click);
         this.oPanelRejectButton.ResumeLayout(false);
         this.oPanelRejectButton.PerformLayout();
         this.oPanelAcceptButton.ResumeLayout(false);
         this.oPanelAcceptButton.PerformLayout();
         this.oPanelLPRSnapshot.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.oImageLPRSnapshot)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.oImagePrinter)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.oImageSignature)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.oImageVehicle)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.oImagePushButton)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.oImageSafety)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.oImageBusiness)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.oImageBackButton)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Timer oTimerForeground;
      private System.Windows.Forms.Label oLabelAppName;
      private System.Windows.Forms.Label oLabelDescription;
      private System.Windows.Forms.Timer oTimerInactivity;
      private System.Windows.Forms.Timer oTimerLPR;
      private System.Windows.Forms.Label oLabelTryAgain;
      private System.Windows.Forms.Timer oTimerWeighing;
      private System.Windows.Forms.PictureBox oImageBackButton;
      private System.Windows.Forms.PictureBox oImageBusiness;
      private System.Windows.Forms.PictureBox oImageSafety;
      private System.Windows.Forms.Label oLabelHeader;
      private System.Windows.Forms.PictureBox oImagePushButton;
      private System.Windows.Forms.PictureBox oImageVehicle;
      private System.Windows.Forms.PictureBox oImageSignature;
      private System.Windows.Forms.Panel oPanelRejectButton;
      private System.Windows.Forms.Label oLabelRejectButton;
      private System.Windows.Forms.Panel oPanelAcceptButton;
      private System.Windows.Forms.Label oLabelAcceptButton;
      private System.Windows.Forms.PictureBox oImagePrinter;
      private System.Windows.Forms.Timer oTimerPrint;
      private System.Windows.Forms.Panel oPanelLPRSnapshot;
      private System.Windows.Forms.PictureBox oImageLPRSnapshot;
      private System.Windows.Forms.Timer oTimerWeighingChangeScreen;

   }
}
