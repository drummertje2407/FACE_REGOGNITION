using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;

namespace FACE_REGOGNITION
{
    class Arduino 
    {

        System.Data.DataTable sql; 
        public SerialPort currentPort;
        bool portFound;

        public Arduino(System.Data.DataTable Sql)
        {
            sql = Sql;
            try
            {
                string[] ports = SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    currentPort = new SerialPort(port, 9600);
                    if (DetectArduino())
                    {
                        portFound = true;
                        break;
                    }
                    else
                    {
                        portFound = false;
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        private bool DetectArduino()
        {
            try
            {
                //The below setting are for the Hello handshake
                byte[] buffer = new byte[5];
                buffer[0] = Convert.ToByte(16);
                buffer[1] = Convert.ToByte(128);
                buffer[2] = Convert.ToByte(0);
                buffer[3] = Convert.ToByte(0);
                buffer[4] = Convert.ToByte(4);
                int intReturnASCII = 0;
                char charReturnValue = (Char)intReturnASCII;
                currentPort.Open();
                currentPort.Write(buffer, 0, 5);
                Thread.Sleep(1000);
                int count = currentPort.BytesToRead;
                string returnMessage = "";
                while (count > 0)
                {
                    intReturnASCII = currentPort.ReadByte();
                    returnMessage = returnMessage + Convert.ToChar(intReturnASCII);
                    count--;
                }

                currentPort.Close();
                if (returnMessage.Contains("HELLO FROM ARDUINO"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private bool Authenticate(string subject_ID)
        {
            
            string code = sql.Rows.Find(subject_ID)["Code"].ToString();
            switch (code)
            {
                case "Green":
                    return true;
                case "red":
                    return false;
                default:
                    return false;
            }
        }
        public void Faceregogniced(List<string> subjects)
        {
            bool Clearence = false;
            foreach(string s in subjects)
            {
                if(Authenticate(s) == false)
                {
                    Clearence = false;
                }
                else
                {
                    Clearence = true;
                }
            }
            if (Clearence)
            {
                Opendoor();
            }
        }
        public void Opendoor()
        {
            try
            {
                currentPort.Open();
                currentPort.Write("opendoor");
                Thread.Sleep(1500);
                currentPort.Close();
            }
            catch { }
          
            
        }

        public void CloseDoor()
        {
            currentPort.Open();
            currentPort.Write("opendoor");
            Thread.Sleep(1000);
        }

    }

    
}
/*---------ARDUINO CODE----------
            
            byte inputByte_0;

            byte inputByte_1;

            byte inputByte_2;

            byte inputByte_3;

            byte inputByte_4;

            //Setup

            void setup() {

            pinMode(ledPin_3, OUTPUT);
            Serial.begin(9600);
            digitalWrite(ledPin_3, HIGH);//
            delay(250);//
            digitalWrite(ledPin_3, LOW);//
            delay(250);//
            }

            //Main Loop

            void loop() {

            //Read Buffer
            if (Serial.available() == 5) 
            {
            //Read buffer
            inputByte_0 = Serial.read();
            delay(100);    
            inputByte_1 = Serial.read();
            delay(100);      
            inputByte_2 = Serial.read();
            delay(100);      
            inputByte_3 = Serial.read();
            delay(100);
            inputByte_4 = Serial.read();   
            }
            //Check for start of Message
            if(inputByte_0 == 16)
            {       
            //Detect Command type
            switch (inputByte_1) 
            {
                case 127:
                    //Set PIN and value
                    switch (inputByte_2)
                {
                    case 4:
                    if(inputByte_3 == 255)
                    {
                        digitalWrite(ledPin_3, HIGH); 
                        break;
                    }
                    else
                    {
                        digitalWrite(ledPin_3, LOW); 
                        break;
                    }
                    break;
                } 
                break;
                case 128:
                //Say hello
                Serial.print("HELLO FROM ARDUINO");
                break;
            } 
            //Clear Message bytes
            inputByte_0 = 0;
            inputByte_1 = 0;
            inputByte_2 = 0;
            inputByte_3 = 0;
            inputByte_4 = 0;
            //Let the PC know we are ready for more data
            Serial.print("-READY TO RECEIVE");
            }
            }
*/
