using System;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ship.Model.Extension
{
    public static class WithStrings
    {
        private static readonly char[] StandardNumbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private static readonly CultureInfo CultureUS = new CultureInfo("en-US");//fa-Ir


        public static string ToFaStr(this string T)
        {
            return T.Trim().Replace("ي", "ی").Replace("ك", "ک").Replace("ؤ", "و").Replace("ة", "ه").Replace("إ", "ا");
        }
        public static string ToFaNum(this string T)
        {
            if (T.Trim() == "") return "";

            T = T.Replace("0", "۰")
                .Replace("1", "۱")
                .Replace("2", "۲")
                .Replace("3", "۳")
                .Replace("4", "۴")
                .Replace("5", "۵")
                .Replace("6", "۶")
                .Replace("7", "۷")
                .Replace("8", "۸")
                .Replace("9", "۹");

            T.Replace('0', '\u06f0')
                    .Replace('1', '\u06f1')
                    .Replace('2', '\u06f2')
                    .Replace('3', '\u06f3')
                    .Replace('4', '\u06f4')
                    .Replace('5', '\u06f5')
                    .Replace('6', '\u06f6')
                    .Replace('7', '\u06f7')
                    .Replace('8', '\u06f8')
                    .Replace('9', '\u06f9');


            return T;
        }
        public static string ToEnNum(this string T)
        {
            try
            {
                if (string.IsNullOrEmpty(T)) return "";
                return T.Trim().Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9");
            }
            catch (Exception e)
            {
                return T;
            }
        }



        public static string ToRial(this decimal T)
        {
            return T.ToString("#,0 ریال");
        }
        public static string ToRial(this string T)
        {
            decimal pricedec = decimal.Parse(T);
            return pricedec.ToString("#,0 ریال");
        }
        public static string AddCommaToInt(this int T)
        {
            return T.ToString("N0", CultureUS);
        }
        public static string AddCommaToDecimal(this decimal T)
        {
            return T.ToString("N0", CultureUS);
        }

        public static string ParseCommaToDecimal(this decimal T)
        {
            try
            {
                string gg = T.ToString();
                gg = gg.Replace("/000", "").Replace(".000", "");

                return gg;
            }
            catch (Exception e)
            {
                return T.ToString();
            }
        }

        public static string AddCommaToDouble(this double T)
        {
            return T.ToString("N0", CultureUS);
        }
        public static string AddCommaToFloat(this float T)
        {
            return T.ToString("N0", CultureUS);
        }
        public static string AddCommaToString(this string T)
        {
            return decimal.Parse(T).AddCommaToDecimal();
        }



        public static int ToInt(this string T)
        {
            return Convert.ToInt32(T, CultureUS);
        }
        public static int ToInt(this bool T)
        {
            return Convert.ToInt32(T, CultureUS);
        }
        public static int ToInt(this float T)
        {
            return Convert.ToInt32(T, CultureUS);
        }
        public static int ToInt(this decimal T)
        {
            return Convert.ToInt32(T, CultureUS);
        }
        public static int ToInt(this double T)
        {
            return Convert.ToInt32(T, CultureUS);
        }



        public static decimal ToDecimal(this string T)
        {
            return Convert.ToDecimal(T, CultureUS);
        }
        public static decimal ToDecimal(this bool T)
        {
            return Convert.ToDecimal(T, CultureUS);
        }
        public static decimal ToDecimal(this int T)
        {
            return Convert.ToDecimal(T, CultureUS);
        }
        public static decimal ToDecimal(this decimal T)
        {
            return Convert.ToDecimal(T, CultureUS);
        }
        public static decimal ToDecimal(this double T)
        {
            return Convert.ToInt32(T, CultureUS);
        }

        public static double ToDouble(this string T)
        {
            return Convert.ToDouble(T, CultureUS);
        }
        public static double ToDouble(this bool T)
        {
            return Convert.ToDouble(T, CultureUS);
        }
        public static double ToDouble(this int T)
        {
            return Convert.ToDouble(T, CultureUS);
        }
        public static double ToDouble(this decimal T)
        {
            return Convert.ToDouble(T, CultureUS);
        }



        public static float ToFloat(this string T)
        {
            return float.Parse(T, CultureUS);
        }
        public static float ToFloat(this bool? T)
        {
            return T == null ? 0 : T == true ? 1 : 0;
        }
        public static float ToFloat(this int T)
        {
            return T;
        }
        public static float ToFloat(this decimal T)
        {
            return (float)T;
        }
        public static float ToFloat(this double T)
        {
            return (float)T;
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        #region تبدیل رقم به حروف
        private static string ConvertRaghamToHorof(this string Numbers)
        {
            string horof = "";

            try
            {
                string[] AddJoda = Numbers.Split(',');
                for (int i = 0; i < AddJoda.Length; i++)
                {
                    string strh = int.Parse(AddJoda[i]).pishConvertRaghamToHorof3(AddJoda.Length - i);
                    if (horof == "")
                    {
                        horof = horof + strh;
                    }
                    else
                    {
                        if (strh != "") horof = horof + " و " + strh;
                    }

                }
                return horof;
            }
            catch
            {
                return null;
            }

        }
        private static string ConvertRaghamToHorof0to19(this int Numbers)
        {
            try
            {
                if (Numbers > 19) return "";
                switch (Numbers)
                {
                    case 0:
                        return "صفر";
                    case 1:
                        return "یک ";
                    case 2:
                        return "دو";
                    case 3:
                        return "سه ";
                    case 4:
                        return "چهار";
                    case 5:
                        return "پنج ";
                    case 6:
                        return "شش ";
                    case 7:
                        return "هفت ";
                    case 8:
                        return "هشت ";
                    case 9:
                        return "نه ";
                    case 10:
                        return "ده ";
                    case 11:
                        return "یازده ";
                    case 12:
                        return "دوازده ";
                    case 13:
                        return "سیزده ";
                    case 14:
                        return "چهارده ";
                    case 15:
                        return "پانزده ";
                    case 16:
                        return "شانزده ";
                    case 17:
                        return "هیفده ";
                    case 18:
                        return "هیجده ";
                    case 19:
                        return "نوزده ";
                        //case 20:
                        //    return "بیست"; 

                }


                return "";
            }
            catch
            {
                return "-1";
            }
            finally
            {
            }

        }
        private static string ConvertRaghamToHorof20to99(this int Numbers)
        {
            try
            {
                //اعداد سفر  تا بیست
                if (!(Numbers > 19 && Numbers < 100)) return "";
                // برای تبدیل به حروف  رقم دوم
                string str = int.Parse(Numbers.ToString().Substring(1)).ConvertRaghamToHorof0to19();
                if (Numbers > 19 && Numbers < 30)
                {
                    if (Numbers == 20)
                    {
                        return " بیست ";
                    }
                    else
                    {
                        return " بیست و " + str;
                    }

                }
                else if (Numbers > 29 && Numbers < 40)
                {
                    if (Numbers == 30)
                    {
                        return " سی ";
                    }
                    else
                    {
                        //string str = ConvertRaghamToHorof0to20(Ragham.ToString().Substring(1));
                        return " سی و " + str;
                    }
                }
                else if (Numbers > 39 && Numbers < 50)
                {
                    if (Numbers == 40)
                    {
                        return " چهل ";
                    }
                    else
                    {
                        //string str = ConvertRaghamToHorof0to20(Ragham.ToString().Substring(1));
                        return "چهل و" + str;
                    }
                }
                else if (Numbers > 49 && Numbers < 60)
                {
                    if (Numbers == 50)
                    {
                        return " پنجاه ";
                    }
                    else
                    {
                        //string str = ConvertRaghamToHorof0to20(Ragham.ToString().Substring(1));
                        return " پنجاه و " + str;
                    }
                }
                else if (Numbers > 59 && Numbers < 70)
                {
                    if (Numbers == 60)
                    {
                        return " شصت ";
                    }
                    else
                    {
                        //string str = ConvertRaghamToHorof0to20(Ragham.ToString().Substring(1));
                        return " شصت و " + str;
                    }
                }
                else if (Numbers > 69 && Numbers < 80)
                {
                    if (Numbers == 70)
                    {
                        return " هفتاد ";
                    }
                    else
                    {
                        //string str = ConvertRaghamToHorof0to20(Ragham.ToString().Substring(1));
                        return " هفتاد و " + str;
                    }
                }
                else if (Numbers > 79 && Numbers < 90)
                {
                    if (Numbers == 80)
                    {
                        return " هشتاد ";
                    }
                    else
                    {
                        //string str = ConvertRaghamToHorof0to20(Ragham.ToString().Substring(1));
                        return " هشتاد و " + str;
                    }
                }
                else if (Numbers > 89 && Numbers < 100)
                {
                    if (Numbers == 90)
                    {
                        return " نود ";
                    }
                    else
                    {
                        //string str = ConvertRaghamToHorof0to20(Ragham.ToString().Substring(1));
                        return " نود و " + str;
                    }
                }
                return "";
            }
            catch
            {
                return "-1";
            }
            finally
            {
            }

        }
        private static string ConvertRaghamToHorof0to99(this int Numbers)
        {
            try
            {
                if (Numbers > 99) return "";
                if (Numbers < 20)
                {
                    return Numbers.ConvertRaghamToHorof0to19();
                }
                else if (Numbers < 100)
                {
                    return Numbers.ConvertRaghamToHorof20to99();
                }
                return "";
            }
            catch
            {
                return "";
            }
            finally
            { }
        }
        private static string ConvertRaghamToHorof100to999(this int Numbers)
        {
            try
            {
                //اعداد 100  تا 999
                if (!(Numbers > 99 && Numbers < 1000)) return "";
                // برای تبدیل به حروف  رقم دوم

                string str = int.Parse(Numbers.ToString().Substring(1)).ConvertRaghamToHorof0to99();
                if (Numbers > 99 && Numbers < 200)
                {
                    if (Numbers == 100)
                    {
                        return " صد ";
                    }
                    else
                    {
                        return " صد و " + str;
                    }

                }
                else if (Numbers > 199 && Numbers < 300)
                {
                    if (Numbers == 200)
                    {
                        return " دویست ";
                    }
                    else
                    {
                        //string str = ConvertRaghamToHorof0to20(Ragham.ToString().Substring(1));
                        return " دویست و " + str;
                    }
                }
                else if (Numbers > 299 && Numbers < 400)
                {
                    if (Numbers == 300)
                    {
                        return " سیصد ";
                    }
                    else
                    {
                        //string str = ConvertRaghamToHorof0to20(Ragham.ToString().Substring(1));
                        return " سیصد و " + str;
                    }
                }
                else if (Numbers > 399 && Numbers < 500)
                {
                    if (Numbers == 400)
                    {
                        return " چهارصد ";
                    }
                    else
                    {
                        //string str = ConvertRaghamToHorof0to20(Ragham.ToString().Substring(1));
                        return "چهارصد و" + str;
                    }
                }
                else if (Numbers > 499 && Numbers < 600)
                {
                    if (Numbers == 500)
                    {
                        return " پانصد ";
                    }
                    else
                    {
                        //string str = ConvertRaghamToHorof0to20(Ragham.ToString().Substring(1));
                        return " پانصد و " + str;
                    }
                }
                else if (Numbers > 599 && Numbers < 700)
                {
                    if (Numbers == 600)
                    {
                        return " ششصد ";
                    }
                    else
                    {
                        //string str = ConvertRaghamToHorof0to20(Ragham.ToString().Substring(1));
                        return " ششصد و " + str;
                    }
                }
                else if (Numbers > 699 && Numbers < 800)
                {
                    if (Numbers == 700)
                    {
                        return " هفتصد ";
                    }
                    else
                    {
                        //string str = ConvertRaghamToHorof0to20(Ragham.ToString().Substring(1));
                        return " هفتصد و " + str;
                    }
                }
                else if (Numbers > 799 && Numbers < 900)
                {
                    if (Numbers == 800)
                    {
                        return " هشتصد ";
                    }
                    else
                    {
                        //string str = ConvertRaghamToHorof0to20(Ragham.ToString().Substring(1));
                        return " هشتصدو " + str;
                    }
                }
                else if (Numbers > 899 && Numbers < 1000)
                {
                    if (Numbers == 900)
                    {
                        return " نهصد ";
                    }
                    else
                    {
                        //string str = ConvertRaghamToHorof0to20(Ragham.ToString().Substring(1));
                        return " نهصد و " + str;
                    }
                }
                return "";
            }
            catch
            {
                return "-1";
            }
            finally
            {
            }

        }
        private static string pishConvertRaghamToHorof3(this int Numbers, int LocationNumber)
        {
            try
            {   //اگر عدد سفر نیازبه پیش ندارد
                if (Numbers == 0) return "";
                if (Numbers > 1000) return "";

                //string str = ConvertRaghamToHorof0to999(Ragham);
                if (Numbers == 0) return "";
                switch (LocationNumber)
                {
                    case 1:
                        return Numbers.String_ToChar0to999();
                    case 2:
                        return Numbers.String_ToChar0to999() + " هزار ";
                    case 3:
                        return Numbers.String_ToChar0to999() + " میلیون ";
                    case 4:
                        return Numbers.String_ToChar0to999() + " میلیارد ";
                    case 5:
                        return Numbers.String_ToChar0to999() + " تریلیارد ";
                    case 6:

                        return Numbers.String_ToChar0to999() + " تعریف نشده1 ";
                    case 7:

                        return Numbers.String_ToChar0to999() + " تعریف نشده2 ";
                    case 8:

                        return Numbers.String_ToChar0to999() + " تعریف نشده3 ";
                    case 9:

                        return Numbers.String_ToChar0to999() + " تعریف نشده4 ";
                }

                return "";
            }
            catch
            {
                return "error";
            }
            finally
            {
            }

        }
        private static string String_ToChar0to999(this int Numbers)
        {
            try
            {
                if (!(Numbers < 1000)) return "";

                if (Numbers < 20)
                {
                    return Numbers.ConvertRaghamToHorof0to19();
                }
                else if (Numbers > 19 && Numbers < 100)
                {
                    return Numbers.ConvertRaghamToHorof20to99();
                }
                else if (Numbers > 99 && Numbers < 1000)
                {
                    return Numbers.ConvertRaghamToHorof100to999();
                }
                else
                {
                    return "خطا برنامه نویسی";
                }
            }
            catch
            {
                return "-1";
            }
            finally
            {
            }

        }

        public static string ConvertNumbersSplit(this decimal Numbers, bool DefaultEn = true)
        {
            if (DefaultEn)
            {
                return Numbers.ToString("0,0", CultureUS);
            }
            else
            {
                return Numbers.ToString("0,0", CultureUS).ToEnNum().ToFaNum();
            }
        }
        public static string ToNumToChar(this decimal Numbers)
        {
            return Numbers.ConvertNumbersSplit().ConvertRaghamToHorof();
        }
        public static string ToNumToChar(this int Numbers)
        {
            return ConvertNumbersSplit(Numbers).ConvertRaghamToHorof();
        }
        public static string ToNumToChar(this long Numbers)
        {
            return ConvertNumbersSplit(Numbers).ConvertRaghamToHorof();
        }
        public static string ToNumToChar(this string Numbers)
        {
            return decimal.Parse(Numbers).ConvertNumbersSplit().ConvertRaghamToHorof();
        }

        #endregion ConvertRaghamToHorof
        public static string RenderPartialView(this string controllerName, string partialView, object model)
        {
            var context = new HttpContextWrapper(HttpContext.Current) as HttpContextBase;
            var routes = new RouteData();
            routes.Values.Add("controller", controllerName);
            var requestContext = new RequestContext(context, routes);
            string requiredString = requestContext.RouteData.GetRequiredString("controller");
            var controllerFactory = ControllerBuilder.Current.GetControllerFactory();
            var controller = controllerFactory.CreateController(requestContext, requiredString) as ControllerBase;
            controller.ControllerContext = new ControllerContext(context, routes, controller);
            var ViewData = new ViewDataDictionary();
            var TempData = new TempDataDictionary();
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, partialView);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}