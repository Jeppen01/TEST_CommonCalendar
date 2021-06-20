using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TEST_CommonCalendar.Models
{
    public class mErrorHandling
    {
        [Key]
        public Guid k_ErrorID { get; set; }

        [Display(Name = "ClassName")]
        public string s_ClassName { get; set; }

        [Display(Name = "Function Name")]
        public string s_FunctionName { get; set; }

        [Display(Name = "Exception")]
        public string s_ExceptionText { get; set; }

        [Display(Name = "Date Time")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime d_OccurTime { get; set; }
    }
}
