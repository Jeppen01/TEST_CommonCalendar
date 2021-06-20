using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TEST_CommonCalendar.CODE.VARIABLE_TYPE;

namespace TEST_CommonCalendar.Models
{
    public class mEVENT
    {
        [Key]
        public Guid k_EVENT_ID { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string s_Title { get; set; }

        [Display(Name = "Wou?")]
        public string s_Location { get; set; }

        [Required]
        [Display(Name = "Ufank")]
        public DateTime d_DateTimeFrom { get; set; }

        [Required]
        [Display(Name = "Schluss")]
        public DateTime d_DateTimeTo { get; set; }

        [NotMapped]
        [Display(Name = "Auerzeit")]
        public DateTime d_HourFrom { get; set; }

        [NotMapped]
        [Display(Name = "Auerzeit")]
        public DateTime d_HourTo { get; set; }

        [NotMapped]
        public string s_DisplayDateTimeFrom
        {
            get
            { return d_DateTimeFrom.ToString("yyyy-MM-dd") + "T" + d_DateTimeFrom.ToString("HH:mm:ss") + ".000"; }
        }

        [NotMapped]
        public string s_DisplayDateTimeTo
        {
            get
            { return d_DateTimeTo.ToString("yyyy-MM-dd") + "T" + d_DateTimeTo.ToString("HH:mm:ss") + ".000"; }
        }

        [NotMapped]
        public List<x_SelectList> l_HourFrom { get; set; }

        [NotMapped]
        public List<x_SelectList> l_HourTo { get; set; }

        [NotMapped]
        public string s_DailyCalHour { get; set; }
    }
}
