using System.Globalization;

namespace Ship.Model.Extension
{
    public class PersianCalander
    {
        public static string ToShamsi(System.DateTime MiladiDate)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            string year = persianCalendar.GetYear(MiladiDate).ToString("0000");
            string month = persianCalendar.GetMonth(MiladiDate).ToString("00");
            string day = persianCalendar.GetDayOfMonth(MiladiDate).ToString("00");

            return year + "/" + month + "/" + day;
        }
        public static string ToShamsiTime(System.DateTime MiladiDate)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            string year = persianCalendar.GetYear(MiladiDate).ToString("0000");
            string month = persianCalendar.GetMonth(MiladiDate).ToString("00");
            string day = persianCalendar.GetDayOfMonth(MiladiDate).ToString("00");
            string Hour = persianCalendar.GetHour(MiladiDate).ToString("00");
            string Second = persianCalendar.GetSecond(MiladiDate).ToString("00");
            string Minute = persianCalendar.GetMinute(MiladiDate).ToString("00");
            string Millisecond = persianCalendar.GetMilliseconds(MiladiDate).ToString("00");

            return year + "/" + month + "/" + day + " " + Hour + ":" + Minute + ":" + Second;
        }
        public static System.DateTime ToMiladi(string ShamsiDate)
        {
            System.Globalization.PersianCalendar pc = new PersianCalendar();
            int year = 0, month = 0, day = 0, Second = 0, Minute = 0, Millisecond = 0, Hour = 0;
            try
            {
                year = int.Parse(ShamsiDate.Substring(0, 4));
            }
            catch { year = 0000; }
            try
            {
                month = int.Parse(ShamsiDate.Substring(5, 2));
            }
            catch { month = 00; }
            try
            {
                day = int.Parse(ShamsiDate.Substring(8, 2));
            }
            catch { day = 00; }
            try
            {
                Second = int.Parse(ShamsiDate.Substring(17, 2));
            }
            catch { Second = 00; }
            try
            {
                Minute = int.Parse(ShamsiDate.Substring(14, 2));
            }
            catch { Minute = 00; }
            try
            {
                Millisecond = int.Parse(ShamsiDate.Substring(20, 2));
            }
            catch { Millisecond = 00; }
            try
            {
                Hour = int.Parse(ShamsiDate.Substring(11, 2));
            }
            catch { Hour = 00; }

            try
            {
                return pc.ToDateTime(year, month, day, Hour, Minute, Second, Millisecond);
            }
            catch { return pc.ToDateTime(1, 1, 1, 1, 1, 1, 1); }
        }
        public static int DaysLeft(System.DateTime Start, System.DateTime Finish)
        {
            System.TimeSpan ts = Finish - Start;

            int days = System.Math.Abs(ts.Days);

            return days;
        }
    }
}