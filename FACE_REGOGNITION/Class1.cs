using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FACE_REGOGNITION
{
    public class Candidate
    {
        public double confidence { get; set; }
        public string enrollment_timestamp { get; set; }
        public string face_id { get; set; }
        public string subject_id { get; set; }
    }
    public class Transaction
    {
        public double confidence { get; set; }
        public string enrollment_timestamp { get; set; }
        public int eyeDistance { get; set; }
        public string face_id { get; set; }
        public string gallery_name { get; set; }
        public int height { get; set; }
        public int pitch { get; set; }
        public double quality { get; set; }
        public int roll { get; set; }
        public string status { get; set; }
        public string subject_id { get; set; }
        public int topLeftX { get; set; }
        public int topLeftY { get; set; }
        public int width { get; set; }
        public int yaw { get; set; }
    }
    public class Img
    {
        public List<Candidate> candidates { get; set; }
        public Transaction transaction { get; set; }
    }

    public class RootObject
    {
        public List<Img> images { get; set; }
    }
}
