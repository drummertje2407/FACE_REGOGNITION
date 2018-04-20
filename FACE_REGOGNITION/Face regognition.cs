using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using RestSharp;





namespace FACE_REGOGNITION
{
    class Face_regognition
    {
        private string appID = "e70fd410";
        private string appKEY = "48ac815082bb94799792fe32c4ecf241";


        public Face_regognition()
        {

        }

        //detect method
        public string add_User(Image<Bgr, byte> image, string username, string galleryname)
        {
            System.Drawing.Image im = image.ToBitmap();
            string base64 = ImageToBase64(im, System.Drawing.Imaging.ImageFormat.Jpeg);//convert image to bitmap

            var client = new RestClient("https://api.kairos.com/");
            var request = new RestRequest("enroll", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { image = base64, subject_id = username, gallery_name = galleryname });//adds the body

            request.AddHeader("app_id", appID);
            request.AddHeader("app_key", appKEY);
            var response = client.Execute(request);//execute request
            return response.Content.ToString();
        }








        public string Recognizeface(Image<Bgr, byte> image)
        {
            System.Drawing.Image im = image.ToBitmap();
            string base64 = ImageToBase64(im, System.Drawing.Imaging.ImageFormat.Jpeg);//convert image to bitmap

            var client = new RestClient("https://api.kairos.com/");
            var request = new RestRequest("recognize", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { image = base64, gallery_name = "Thuis" });//adds the body

            request.AddHeader("app_id", appID);
            request.AddHeader("app_key", appKEY);
            var response = client.Execute(request);//execute request




            return (response.Content.ToString());
        }


        private string ImageToBase64(Image img, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                
                img.Save(ms, format);
                
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public string galleryList()
        {
            var client = new RestClient("https://api.kairos.com/");
            var request = new RestRequest("/gallery/list_all", Method.POST);

            request.AddHeader("app_id", appID);
            request.AddHeader("app_key", appKEY);
            var response = client.Execute(request);

            return (response.Content.ToString());
        }

        public string galleryView(string gallery)
        {
            var client = new RestClient("https://api.kairos.com/");
            var request = new RestRequest("/gallery/view", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { gallery_name = gallery});

            request.AddHeader("app_id", appID);
            request.AddHeader("app_key", appKEY);
            var response = client.Execute(request);

            return (response.Content.ToString());
        }

        public void galleryRemove(string gallery)
        {
            var client = new RestClient("https://api.kairos.com/");
            var request = new RestRequest("/gallery/remove", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { gallery_name = gallery });

            request.AddHeader("app_id", appID);
            request.AddHeader("app_key", appKEY);
            var response = client.Execute(request);

        }
        public void subjectRemove(string gallery, string subject_Id)
        {
            var client = new RestClient("https://api.kairos.com/");
            var request = new RestRequest("/gallery/remove_subject", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { gallery_name = gallery, subject_id = subject_Id });

            request.AddHeader("app_id", appID);
            request.AddHeader("app_key", appKEY);
            var response = client.Execute(request);
        }


    }
}
