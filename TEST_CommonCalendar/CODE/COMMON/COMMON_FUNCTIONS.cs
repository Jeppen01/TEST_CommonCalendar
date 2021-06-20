using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEST_CommonCalendar.CODE.VARIABLE_TYPE;
using TEST_CommonCalendar.Data;
using TEST_CommonCalendar.Models;

namespace TEST_CommonCalendar.CODE.COMMON
{
    public class COMMON_FUNCTIONS
    {
        public static void storeError(ApplicationDbContext _context, string classname, string functionName, string exceptiontext)
        {
            try
            {
                mErrorHandling myError = new mErrorHandling();
                myError.s_ClassName = classname;
                myError.s_FunctionName = functionName;
                myError.s_ExceptionText = exceptiontext;
                myError.d_OccurTime = DateTime.Now;

                _context.Add(myError);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public static List<x_SelectList> getMinutes()
        {
            List<x_SelectList> myreturnlist = new List<x_SelectList>();

            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    x_SelectList val = new x_SelectList();

                    val.list_Value = i.ToString().PadLeft(2, '0') + ":" + (j * 10).ToString().PadLeft(2, '0');
                    val.string_id = val.list_Value;
                    myreturnlist.Add(val);
                }
            }

            return myreturnlist;
        }

        //convert to local datetime
        public static DateTime DateTimeFromUnixTimestampSeconds(long seconds)
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            double ts_offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow).TotalSeconds;
            return UnixEpoch.AddSeconds((seconds / 1000) + ts_offset);
        }
    }
}
