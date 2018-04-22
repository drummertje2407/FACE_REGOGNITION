using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Newtonsoft.Json; // json convertion

namespace FACE_REGOGNITION
{
    public class Faceregognition
    {
        Apirequest apirequest = new Apirequest();
        List<string> persons = new List<string>();
        System.Data.DataTable sql;
        Arduino arduino = new Arduino();

        public Faceregognition(subjectIDsDataSet.subjectDataTable Sql)
        {
            sql = Sql;
            
        }

        public void RegognitionHandler(Image<Bgr, byte> image)
        {
            var response = apirequest.Recognizeface(image); //actual apirequest
            var rootob = JsonConvert.DeserializeObject<RootObject>(response); //handles the json data ----> puts it inside (C#) classes
            if (rootob.images != null && rootob.images[0].candidates != null)
            {

                for (int i = 0; i < rootob.images[0].candidates.Count; i++)
                {
                    if (i < 1 || rootob.images[0].candidates[i].subject_id != rootob.images[0].candidates[i - 1].subject_id)
                    {
                        

                        persons.Add(rootob.images[0].candidates[i].subject_id);
                    }
                }

                Faceregogniced(persons);
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
            foreach (string s in subjects)
            {
                if (Authenticate(s) == false)
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
                arduino.Opendoor();
            }
        }

    }
}
