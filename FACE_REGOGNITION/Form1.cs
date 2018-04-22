using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Configuration;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Newtonsoft.Json; // json convertion



namespace FACE_REGOGNITION
{
    public partial class Form1 : Form
    {
        public VideoCapture capture = new VideoCapture();
        Image<Bgr, byte> Frame;
        Image<Bgr, byte> ImageFrame;
        private CascadeClassifier haar;
        Rectangle[] rectfaces;
        Faceregognition faceregognition;

        

        public delegate void FacedetectedEventHandler(object source, EventArgs e);
        public event FacedetectedEventHandler Facedetected;
        static System.Timers.Timer Dalaytimer;
        public bool TimerhasElapsed = true;

        

        public Form1()
        {
            InitializeComponent();
            // (try to) load xml file in to haarcascade
            try
            {
                haar = new CascadeClassifier("haarcascade_frontalface_default.xml");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

            Dalaytimer = new System.Timers.Timer(3000);
            Dalaytimer.AutoReset = false;
            Dalaytimer.Elapsed += Toggletimerelapsed;   //timer to avoid to many api requests

            Application.Idle += InitVideofeed;
            DetectfaceTimer.Tick += EmguDetectface;
            Facedetected += KairosRegogniseface;
        }

        private void InitVideofeed(object sender, EventArgs arg)
        {
            Frame = capture.QueryFrame().ToImage<Bgr, byte>();
            ImageFrame = Frame;
            if (rectfaces != null)
            {
                if (rectfaces.Count() != 0)
                {
                    foreach (Rectangle rect in rectfaces)
                    {
                        ImageFrame.Draw(rect, new Bgr(Color.MediumBlue), 3); //put facerectangles in the frame or update them
                    }
                    OnFacedetected();
                }
            }

            videofeedBox.Image = ImageFrame;
        }

        private void EmguDetectface(object sender, EventArgs arg)
        {
            if (Frame != null)
            {
                Image<Gray, byte> grayframe = Frame.Convert<Gray, byte>();     //convert to gray

                grayframe._EqualizeHist();      // normalize brightness and increase contrast

                rectfaces = haar.DetectMultiScale(grayframe, 1.2, 7, new Size(100, 100), new Size(250, 250)); //actual face detection
            }
            
        }

        private void Toggletimerelapsed(object sender, EventArgs e)
        {
            TimerhasElapsed = true;
        }
        
        protected virtual void OnFacedetected()
        {
            if (TimerhasElapsed)
            {
                if (Facedetected != null)
                {
                    Facedetected(this, EventArgs.Empty);
                    faceregognition.RegognitionHandler(Frame);
                }
                Dalaytimer.Start();
                TimerhasElapsed = false;
            }
        }
        

        private void KairosRegogniseface(object sender, EventArgs arg)
        {
            
           
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'subjectIDsDataSet.subject' table. You can move, or remove it, as needed.
            this.subjectTableAdapter.Fill(this.subjectIDsDataSet.subject);
            faceregognition = new Faceregognition(subjectTableAdapter.GetData());
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
        }
    }
   

}

