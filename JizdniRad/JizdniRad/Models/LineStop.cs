using System.ComponentModel.DataAnnotations.Schema;

namespace JizdniRad.Models
{
    [Table("LineStop")]
    public class LineStop
    {
        public int ID { get; set; }
        public int StopID { get; set; }
        public int LineID { get; set; }
        public int? TimeToNextStop { get; set; }
        public int? NextLineStopID { get; set; }
      

        public virtual Stop Stop{ get; set; }


        public virtual Line Line { get; set; }

        [ForeignKey("ID")]
        public virtual LineStop NextStop { get; set; }

    }
}
