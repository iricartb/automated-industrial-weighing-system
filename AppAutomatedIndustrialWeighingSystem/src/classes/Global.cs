using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Data.SqlClient;
using System.Windows.Forms;
using AppAutomatedIndustrialWeighingSystem.src.classes;

namespace AppAutomatedIndustrialWeighingSystem {
   
   class Global {
      public const string APP_NAME = "AIWS - AUTOMATED INDUSTRIAL WEIGHING SYSTEM";
      public const string APP_VERSION = "2.0";

      public const int APP_LPR_PROCESS_NUM_ATTEMPTS = 10;
      public const string APP_LPR_CAMERA_IP_ADDRESS = "xxx.xxx.xxx.xxx";
      public const string APP_LPR_CAMERA_USERNAME = "-";
      public const string APP_LPR_CAMERA_PASSWORD = "-";
      public const string APP_LPR_CAMERA_IMAGE_NAME = "LPR_" + Global.APP_LPR_CAMERA_IP_ADDRESS + ".jpg";
      public const string APP_LPR_CAMERA_URLSNAPSHOT = "http://" + Global.APP_LPR_CAMERA_IP_ADDRESS + "/cgi-bin/encoder?USER=" + Global.APP_LPR_CAMERA_USERNAME + "&PWD=" + Global.APP_LPR_CAMERA_PASSWORD + "&SNAPSHOT";
      public const string APP_LPR_CAMERA_URLRTSP = "rtsp://" + Global.APP_LPR_CAMERA_USERNAME + ":" + Global.APP_LPR_CAMERA_PASSWORD + "@" + Global.APP_LPR_CAMERA_IP_ADDRESS + ":7070";

      public static string APP_WEIGHING_SERIAL_PORT_COM = "COM1";
      public static int APP_WEIGHING_SERIAL_PORT_COM_BAUDRATE = 1200;
      public static Parity APP_WEIGHING_SERIAL_PORT_COM_PARITY = Parity.Even;
      public static int APP_WEIGHING_SERIAL_PORT_COM_DATABITS = 7;
      public static StopBits APP_WEIGHING_SERIAL_PORT_COM_STOPBITS = StopBits.One;
      public static int APP_WEIGHING_SERIAL_PORT_COM_IO_TIMEOUT_SECONDS = 2;
      public static int APP_WEIGHING_SERIAL_PORT_COM_SUDDEN_DEATH_TIMEOUT_SECONDS = 3;
      public static string APP_WEIGHING_SERIAL_PORT_COM_PROTOCOL_SEND_DATA = "P";

      public const int APP_WEIGHING_STATUS_NONE = 0;
      public const int APP_WEIGHING_STATUS_SUCCESS = 1;
      public const int APP_WEIGHING_STATUS_ERROR = 2;

      public const string APP_SCREEN_START = "APP_SCREEN_START";
      public const string APP_SCREEN_LPR = "APP_SCREEN_LPR";
      public const string APP_SCREEN_ORIGINS = "APP_SCREEN_ORIGINS";
      public const string APP_SCREEN_PRODUCTS = "APP_SCREEN_PRODUCTS";
      public const string APP_SCREEN_WEIGHING = "APP_SCREEN_WEIGHING";
      public const string APP_SCREEN_TICKET = "APP_SCREEN_TICKET";
      public const string APP_SCREEN_PRINT = "APP_SCREEN_PRINT";
      public const string APP_SCREEN_WARNING = "APP_SCREEN_WARNING";
      public const string APP_SCREEN_ERROR = "APP_SCREEN_ERROR";

      public const string APP_SCREEN_START_LABEL_HEADER_TEXT = "- PULSE AQUÍ PARA EMPEZAR -";
      public const string APP_SCREEN_START_LABEL_DESCRIPTION_TEXT = "- AL PULSAR EL BOTÓN USTED DECLARA ACEPTAR LAS NORMAS DE SEGURIDAD -";
      public const string APP_SCREEN_LPR_LABEL_HEADER_TEXT = "DETECTANDO MATRÍCULA, POR FAVOR ESPERE ...";
      public const string APP_SCREEN_ORIGINS_LABEL_HEADER_TEXT = "SELECCIONE LA PROCEDENCIA DEL VEHÍCULO {1}";
      public const string APP_SCREEN_ORIGINS_LABEL_DESCRIPTION_TEXT = "TRANSPORTISTA: {1}";
      public const string APP_SCREEN_PRODUCTS_LABEL_HEADER_TEXT = "SELECCIONE EL PRODUCTO DEL VEHÍCULO {1}";
      public const string APP_SCREEN_PRODUCTS_LABEL_DESCRIPTION_TEXT = "TRANSPORTISTA: {1}, PROCEDENCIA: {2}";
      public const string APP_SCREEN_WEIGHING_LABEL_HEADER_TEXT = "PESANDO VEHÍCULO {1}, POR FAVOR ESPERE ...";
      public const string APP_SCREEN_TICKET_LABEL_HEADER_TEXT = "CONFIRMACIÓN DE LA INFORMACIÓN DEL VEHÍCULO {1}";
      public static string APP_SCREEN_TICKET_LABEL_DESCRIPTION_TEXT = "MATRÍCULA: {1}" + Environment.NewLine + "TRANSPORTISTA: {2}" + Environment.NewLine + "PROCEDENCIA: {3}" + Environment.NewLine + "PRODUCTO: {4}" + Environment.NewLine + "PESO BRUTO: {5} KG" + Environment.NewLine + "TARA: {6} KG" + Environment.NewLine + "PESO NETO: {7} KG";
      public const string APP_SCREEN_TICKET_LABEL_DESCRIPTION_SIGNATURE_TEXT = "Si esta conforme firme en el siguiente recuadro:";
      public const string APP_SCREEN_PRINT_LABEL_HEADER_TEXT = "FINALIZACIÓN Y GENERACIÓN DEL TICKET DEL VEHÍCULO {1}";
      public static string APP_SCREEN_PRINT_LABEL_DESCRIPTION_TEXT = "POR FAVOR, NO SE OLVIDE DE RECOGER EL TICKET DE LA IMPRESORA" + Environment.NewLine + Environment.NewLine + Environment.NewLine + "GRACIAS POR SU VISITA";

      public const string APP_SCREEN_WARNING_LPR_PROCESS_NOT_VALID_CANDIDATES_LABEL_WARNING_CODE_TEXT = "W-01";
      public const string APP_SCREEN_WARNING_LPR_PROCESS_NOT_VALID_CANDIDATES_LABEL_HEADER_TEXT = "ALERTA - MATRÍCULA NO DETECTADA O NO AUTORIZADA - ASISTENCIA";
      public static string APP_SCREEN_WARNING_LPR_PROCESS_NOT_VALID_CANDIDATES_LABEL_DESCRIPTION_TEXT = "El sistema no ha podido detectar la matrícula {1} como un vehículo autorizado." + Environment.NewLine + Environment.NewLine + "Por favor inténtelo de nuevo o bien pulse el timbre para solicitar asistencia.";
      public static string APP_SCREEN_WARNING_LPR_PROCESS_NOT_VALID_CANDIDATES_LABEL_TRY_AGAIN_TEXT = "- PULSE AQUÍ PARA VOLVER A INTENTAR -";

      public const string APP_SCREEN_WARNING_LPR_PROCESS_CARRIER_NOT_FOUND_LABEL_WARNING_CODE_TEXT = "W-02";
      public const string APP_SCREEN_WARNING_LPR_PROCESS_CARRIER_NOT_FOUND_LABEL_HEADER_TEXT = "ALERTA - VEHÍCULO SIN TRANSPORTISTA ASIGNADO - ASISTENCIA";
      public static string APP_SCREEN_WARNING_LPR_PROCESS_CARRIER_NOT_FOUND_LABEL_DESCRIPTION_TEXT = "El sistema no ha podido detectar el transportista del vehículo con matrícula {1}." + Environment.NewLine + Environment.NewLine + "Por favor pulse el timbre para solicitar asistencia.";

      public const string APP_SCREEN_WARNING_LPR_PROCESS_PRODUCTS_NOT_FOUND_LABEL_WARNING_CODE_TEXT = "W-03";
      public const string APP_SCREEN_WARNING_LPR_PROCESS_PRODUCTS_NOT_FOUND_LABEL_HEADER_TEXT = "ALERTA - VEHÍCULO SIN PRODUCTOS ASIGNADOS - ASISTENCIA";
      public static string APP_SCREEN_WARNING_LPR_PROCESS_PRODUCTS_NOT_FOUND_LABEL_DESCRIPTION_TEXT = "El sistema no ha podido detectar productos sobre las procedencias del vehículo con matrícula {1}." + Environment.NewLine + Environment.NewLine + "Por favor pulse el timbre para solicitar asistencia.";

      public const string APP_SCREEN_WARNING_ORIGIN_PRODUCTS_NOT_FOUND_LABEL_WARNING_CODE_TEXT = "W-04";
      public const string APP_SCREEN_WARNING_ORIGIN_PRODUCTS_NOT_FOUND_LABEL_HEADER_TEXT = "ALERTA - VEHÍCULO SIN PRODUCTOS ASIGNADOS - ASISTENCIA";
      public static string APP_SCREEN_WARNING_ORIGIN_PRODUCTS_NOT_FOUND_LABEL_DESCRIPTION_TEXT = "El sistema no ha podido detectar productos sobre las procedencia {1} del vehículo con matrícula {2}." + Environment.NewLine + Environment.NewLine + "Por favor pulse el timbre para solicitar asistencia.";

      public const string APP_SCREEN_WARNING_LPR_PROCESS_TARE_NOT_FOUND_LABEL_WARNING_CODE_TEXT = "W-05";
      public const string APP_SCREEN_WARNING_LPR_PROCESS_TARE_NOT_FOUND_LABEL_HEADER_TEXT = "ALERTA - VEHÍCULO SIN TARA O MÚLTIPLES TARAS - ASISTENCIA";
      public static string APP_SCREEN_WARNING_LPR_PROCESS_TARE_NOT_FOUND_LABEL_DESCRIPTION_TEXT = "El sistema no ha podido detectar la tara del vehículo con matrícula {1} o bien contiene múltiples taras." + Environment.NewLine + Environment.NewLine + "Por favor pulse el timbre para solicitar asistencia.";

      public const string APP_SCREEN_WARNING_WEIGHING_SERIAL_PORT_TARE_MAJOR_THAN_GROSS_WEIGHT_LABEL_WARNING_CODE_TEXT = "W-06";
      public const string APP_SCREEN_WARNING_WEIGHING_SERIAL_PORT_TARE_MAJOR_THAN_GROSS_WEIGHT_LABEL_HEADER_TEXT = "ALERTA - VEHÍCULO CON TARA ERRÓNEA - ASISTENCIA";
      public static string APP_SCREEN_WARNING_WEIGHING_SERIAL_PORT_TARE_MAJOR_THAN_GROSS_WEIGHT_LABEL_DESCRIPTION_TEXT = "Peso neto negativo, la tara del vehículo con matrícula {1} es mayor que su actual pesaje (tara + producto)." + Environment.NewLine + Environment.NewLine + "Por favor pulse el timbre para solicitar asistencia.";

      public const string APP_SCREEN_ERROR_KERNEL_LABEL_ERROR_CODE_TEXT = "E-01";
      public const string APP_SCREEN_ERROR_KERNEL_LABEL_HEADER_TEXT = "ERROR - KERNEL";
      public const string APP_SCREEN_ERROR_KERNEL_LABEL_DESCRIPTION_TEXT = "El sistema ha detectado un error en tiempo de ejecución: {1}.";

      public const string APP_SCREEN_ERROR_DATABASE_CONNECTION_LABEL_ERROR_CODE_TEXT = "E-02";
      public const string APP_SCREEN_ERROR_DATABASE_CONNECTION_LABEL_HEADER_TEXT = "ERROR - CONEXIÓN BASE DE DATOS";
      public const string APP_SCREEN_ERROR_DATABASE_CONNECTION_LABEL_DESCRIPTION_TEXT = "El sistema no ha podido establecer la conexión con la base de datos {1}.";

      public const string APP_SCREEN_ERROR_LPR_CAMERA_CONNECTION_LABEL_ERROR_CODE_TEXT = "E-03";
      public const string APP_SCREEN_ERROR_LPR_CAMERA_CONNECTION_LABEL_HEADER_TEXT = "ERROR - CONEXIÓN CÁMARA IP";
      public const string APP_SCREEN_ERROR_LPR_CAMERA_CONNECTION_LABEL_DESCRIPTION_TEXT = "El sistema no ha podido establecer la conexión con la cámara IP {1}.";

      public const string APP_SCREEN_ERROR_WEIGHING_SERIAL_PORT_CONNECTION_LABEL_ERROR_CODE_TEXT = "E-04";
      public const string APP_SCREEN_ERROR_WEIGHING_SERIAL_PORT_CONNECTION_LABEL_HEADER_TEXT = "ERROR - CONEXIÓN PUERTO SERIE RS-232";
      public const string APP_SCREEN_ERROR_WEIGHING_SERIAL_PORT_CONNECTION_LABEL_DESCRIPTION_TEXT = "El sistema no ha podido establecer la conexión con la báscula de pesaje.";

      public const string APP_SCREEN_ERROR_CREATE_TICKET_LABEL_ERROR_CODE_TEXT = "E-05";
      public const string APP_SCREEN_ERROR_CREATE_TICKET_LABEL_HEADER_TEXT = "ERROR - TICKET";
      public const string APP_SCREEN_ERROR_CREATE_TICKET_LABEL_DESCRIPTION_TEXT = "El sistema ha detectado un error durante la generación del ticket.";

      public const string APP_TYPE_SCREEN_BLANK = "APP_TYPE_SCREEN_BLANK";
      public const string APP_TYPE_SCREEN_SUCCESS = "APP_TYPE_SCREEN_SUCCESS";
      public const string APP_TYPE_SCREEN_WARNING = "APP_TYPE_SCREEN_WARNING";
      public const string APP_TYPE_SCREEN_ERROR = "APP_TYPE_SCREEN_ERROR";

      public const int APP_BLANK_SCREEN_BACKGROUND_COLOR_RED = 255;
      public const int APP_BLANK_SCREEN_BACKGROUND_COLOR_GREEN = 255;
      public const int APP_BLANK_SCREEN_BACKGROUND_COLOR_BLUE = 255;

      public const int APP_SUCCESS_SCREEN_BACKGROUND_COLOR_RED = 0;
      public const int APP_SUCCESS_SCREEN_BACKGROUND_COLOR_GREEN = 50;
      public const int APP_SUCCESS_SCREEN_BACKGROUND_COLOR_BLUE = 70;

      public const int APP_WARNING_SCREEN_BACKGROUND_COLOR_RED = 200;
      public const int APP_WARNING_SCREEN_BACKGROUND_COLOR_GREEN = 150;
      public const int APP_WARNING_SCREEN_BACKGROUND_COLOR_BLUE = 0;

      public const int APP_ERROR_SCREEN_BACKGROUND_COLOR_RED = 70;
      public const int APP_ERROR_SCREEN_BACKGROUND_COLOR_GREEN = 0;
      public const int APP_ERROR_SCREEN_BACKGROUND_COLOR_BLUE = 0;

      public const int APP_SCREEN_DINAMIC_LABEL_BACKGROUND_COLOR_RED = 0;
      public const int APP_SCREEN_DINAMIC_LABEL_BACKGROUND_COLOR_GREEN = 75;
      public const int APP_SCREEN_DINAMIC_LABEL_BACKGROUND_COLOR_BLUE = 125;

      public const string APP_SCREEN_DINAMIC_LABEL_FONT_NAME = "Verdana";
      public const int APP_SCREEN_DINAMIC_LABEL_FONT_SIZE = 18;

      public const int APP_SCREEN_LABEL_MARGINS = 40;

      public const int APP_TIMER_INACTIVITY_INTERVAL_DEFAULT_SECONDS = 60;
      public const int APP_TIMER_INACTIVITY_SCREEN_TICKET_INTERVAL_SECONDS = 120;
      public const int APP_TIMER_INACTIVITY_SCREEN_WARNING_ERROR_INTERVAL_SECONDS = 30;
      public const int APP_TIMER_INACTIVITY_SCREEN_PRINT_INTERVAL_SECONDS = 30;

      public static CameraIP oLPRCamera = new CameraIP(Global.APP_LPR_CAMERA_IP_ADDRESS, Global.APP_LPR_CAMERA_USERNAME, Global.APP_LPR_CAMERA_PASSWORD, Global.APP_LPR_CAMERA_URLSNAPSHOT);
      public static SerialPort oWeighingSerialPort = new SerialPort(Global.APP_WEIGHING_SERIAL_PORT_COM, Global.APP_WEIGHING_SERIAL_PORT_COM_BAUDRATE, Global.APP_WEIGHING_SERIAL_PORT_COM_PARITY, Global.APP_WEIGHING_SERIAL_PORT_COM_DATABITS, Global.APP_WEIGHING_SERIAL_PORT_COM_STOPBITS);

      public static List<Control> oListDinamicControls = new List<Control>();
      public static List<List<String>> oListAvailableOrigins = new List<List<string>>();
      public static List<List<String>> oListAvailableProducts = new List<List<string>>();
      public static string sAppBeforeScreen = Global.APP_SCREEN_START;
      public static string sAppCurrentScreen = Global.APP_SCREEN_START;
      public static int nAppCurrentLPRAttempts = 0;

      public static int nWeighingStatus = Global.APP_WEIGHING_STATUS_NONE;

      public static object oMonitorScreen = new Object();
      public static Vehicle oVehicle = new Vehicle();

      public static string getTimestamp(SqlConnection oSQLConnection, SqlTransaction oSQLTransaction) {
         SqlCommand oSQLCommand;
         SqlDataReader oSQLDataReader;
         String sTimestamp = String.Empty;
         String sSQLQuery = String.Empty;

         try {
            sSQLQuery = "DECLARE @SDATE DATETIME; SET @SDATE = GETDATE(); SELECT CONVERT(VARCHAR(10), @SDATE, 103) + ' ' + CONVERT(VARCHAR(10), @SDATE, 108)";

            oSQLCommand = new SqlCommand(sSQLQuery, oSQLConnection);

            oSQLCommand.Connection = oSQLConnection;
            oSQLCommand.Transaction = oSQLTransaction;

            oSQLDataReader = oSQLCommand.ExecuteReader();

            if (oSQLDataReader.HasRows) {
               oSQLDataReader.Read();

               sTimestamp = oSQLDataReader[0].ToString().Trim();

               oSQLDataReader.Close();
            }
            else oSQLDataReader.Close();
         }
         catch (Exception oException) { Console.WriteLine(oException.ToString()); }

         return sTimestamp;
      }

      public static string getTicketNumber(SqlConnection oSQLConnection, SqlTransaction oSQLTransaction) {
         SqlCommand oSQLCommand;
         SqlDataReader oSQLDataReader;
         String sTicketNumber = String.Empty;
         String sSQLDate = String.Empty;
         String sSQLDateYearMonth = String.Empty;
         String sSQLQuery = String.Empty;

         try {
            sSQLQuery = "SELECT CONVERT(VARCHAR(8), GETDATE(), 112)";

            oSQLCommand = new SqlCommand(sSQLQuery, oSQLConnection);

            oSQLCommand.Connection = oSQLConnection;
            oSQLCommand.Transaction = oSQLTransaction;

            oSQLDataReader = oSQLCommand.ExecuteReader();

            if (oSQLDataReader.HasRows) {
               oSQLDataReader.Read();

               sSQLDate = oSQLDataReader[0].ToString().Trim();
               sSQLDateYearMonth = sSQLDate.Substring(0, 6);

               oSQLDataReader.Close();

               sSQLQuery = "SELECT LAST_ID_TICKET FROM num_ticket WHERE numero_ticket LIKE '" + sSQLDateYearMonth + "%' AND (estado = 'V' OR estado = 'A' OR estado = 'T') ORDER BY LAST_ID_TICKET DESC";

               oSQLCommand = new SqlCommand(sSQLQuery, oSQLConnection);

               oSQLCommand.Connection = oSQLConnection;
               oSQLCommand.Transaction = oSQLTransaction;

               oSQLDataReader = oSQLCommand.ExecuteReader();

               if (oSQLDataReader.HasRows) {
                  oSQLDataReader.Read();

                  sTicketNumber = sSQLDate + (Int32.Parse(oSQLDataReader[0].ToString().Trim()) + 1).ToString().PadLeft(5, '0');

                  oSQLDataReader.Close();
               }
               else {
                  sTicketNumber = sSQLDate + "1".PadLeft(5, '0');

                  oSQLDataReader.Close();
               }
            }
            else oSQLDataReader.Close();
         }
         catch (Exception) { }

         return sTicketNumber;
      }
   }
}
