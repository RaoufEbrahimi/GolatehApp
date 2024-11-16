using System.Collections.Generic;
using System.Text;

namespace Ship.Model.Extension
{
    public class DSKWORPropsVM
    {
        public string DSW_ID { get; set; } // کد کارگاه
        public int? DSW_YY { get; set; } // سال عملکرد
        public int? DSW_MM { get; set; } // ماه عملکرد
        public string DSW_LISTNO { get; set; } // شماره لیست
        public string DSW_ID1 { get; set; } // شماره بیمه
        public string DSW_FNAME { get; set; } // نام
        public string DSW_LNAME { get; set; } // نام خانوادگی
        public string DSW_DNAME { get; set; } // نام پدر
        public string DSW_IDNO { get; set; } // شماره شناسنامه
        public string DSW_IDPLC { get; set; } // محل صدور
        public string DSW_IDATE { get; set; } // تاریخ صدور
        public string DSW_BDATE { get; set; } // تاریخ تولد
        public string DSW_SEX { get; set; } // جنسیت
        public string DSW_NAT { get; set; } // ملیت
        public string DSW_OCP { get; set; } // شرح شغل
        public string DSW_SDATE { get; set; } // تاریخ شروع به کار
        public string DSW_EDATE { get; set; } // تاریخ ترک کار
        public int? DSW_DD { get; set; } // تعداد روزهای کارکرد
        public decimal? DSW_ROOZ { get; set; } // دستمزد روزانه
        public decimal? DSW_MAH { get; set; } // دستمزد ماهانه
        public decimal? DSW_MAZ { get; set; } // مزایای ماهانه
        public decimal? DSW_MASH { get; set; } // جمع دستمزد و مزایای ماهانه مشمول
        public decimal? DSW_TOTL { get; set; } // جمع کل دستمزد و مزایای ماهانه
        public decimal? DSW_BIME { get; set; } // حق بیمه سهم بیمه شده
        public int? DSW_PRATE { get; set; } // نرخ پورسانتاژ
        public string DSW_JOB { get; set; } // کد شغل
        public string PER_NATCOD { get; set; } // کد ملی
    }
    public class DSKKARPropsVM
    {
        public string DSK_ID { get; set; } // کد کارگاه
        public string DSK_NAME { get; set; } // نام کارگاه
        public string DSK_FARM { get; set; } // نام کارفرما
        public string DSK_ADRS { get; set; } // آدرس کارگاه
        public int? DSK_KIND { get; set; } // نوع لیست همیشه مقدار 0 دارد
        public int? DSK_YY { get; set; } // سال عملکرد
        public int? DSK_MM { get; set; } // ماه عملکرد
        public string DSK_LISTNO { get; set; } // شماره لیست
        public string DSK_DISC { get; set; } // شرح لیست
        public int? DSK_NUM { get; set; } // تعداد کارکنان
        public int? DSK_TDD { get; set; } // مجموع روزهای کارکرد
        public decimal? DSK_TROOZ { get; set; } // مجموع دستمزد روزانه
        public decimal? DSK_TMAH { get; set; } // مجموع دستمزد ماهانه
        public decimal? DSK_TMAZ { get; set; } // مجموع مزایای ماهانه مشمول
        public decimal? DSK_TMASH { get; set; } // مجموع دستمزد و مزایای ماهانه مشمول
        public decimal? DSK_TTOTL { get; set; } // مجموع کل دستمزد و مزایای ماهانه (مشمول و غیر مشمول)
        public decimal? DSK_TBIME { get; set; } // مجموع حق بیمه سهم بیمه شده
        public decimal? DSK_TKOSO { get; set; } // مجموع حق بیمه سهم کارفرما
        public decimal? DSK_BIC { get; set; } // مجموع حق بیمه بیکاری
        public int? DSK_RATE { get; set; } // نرخ حق بیمه
        public int? DSK_PRATE { get; set; } // نرخ پورسانتاژ
        public decimal? DSK_BIMH { get; set; } // نرخ مشاغل و سخت و زیان آور
        public string MON_PYM { get; set; } // ردیف پیمان
    }



    //public class ConverFileToZip
    //{

    //    public void CompressDirectoryWithPassword(string OutputFilePath, string fileName, string extensionType)
    //    {
    //        try
    //        {

    //            List<string> files = new List<string>();
    //            string SavePath;

    //            using (ZipFile zip = new ZipFile())
    //            {
    //                zip.AlternateEncoding = Encoding.UTF8;
    //                files.Add(OutputFilePath + "\\" + fileName + extensionType);
    //                zip.AlternateEncodingUsage = Ionic.Zip.ZipOption.Always;
    //                zip.AddFiles(files, "FileBime");//Zip file inside filename

    //                SavePath = OutputFilePath;

    //                if (!Directory.Exists(SavePath))
    //                {
    //                    Directory.CreateDirectory(SavePath);
    //                }

    //                SavePath += "\\" + fileName + ".zip";

    //                zip.Save(SavePath);//location and name for creating zip file

    //            }
    //        }
    //        catch (Exception ex)
    //        {

    //        }
    //    }

    //}

    public class ConvertWindowsPersianToDos
    {

        #region
        public Dictionary<byte, byte> CharachtersMapper_Group1 = new Dictionary<byte, byte>
{

{48 , 128}, // 0
{49 , 129}, // 1
{50 , 130}, // 2
{51 , 131}, // 3
{52 , 132}, // 4
{53 , 133}, // 5
{54 , 134}, // 6
{55 , 135}, // 7
{56 , 136}, // 8
{57 , 137}, // 9
{161, 138}, // ،
{191, 140}, // ؟
{193, 143}, //ء 
{194, 141}, // آ
{195, 144}, // أ
{196, 248}, //ؤ 
{197, 144}, //إ
{200, 146}, //ب 
{201, 249}, //ة
{202, 150}, //ت
{203, 152}, //ث 
{204, 154}, //ﺝ
{205, 158}, //ﺡ
{206, 160}, //ﺥ
{207, 162}, //د
{208, 163}, //ذ
{209, 164}, //ر
{210, 165},//ز
{211, 167},//س
{212, 169},//ش
{213, 171}, //ص
{214, 173}, //ض
{216, 175}, //ط
{217, 224}, //ظ
{218, 225}, //ع
{219, 229}, //غ
{220, 139}, //-
{221, 233},//ف
{222, 235},//ق
{223, 237},//ك
{225, 241},//ل
{227, 244},//م
{228, 246},//ن
{229, 249},//ه
{230, 248},//و
{236, 253},//ى
{237, 253},//ی
{129, 148},//پ
{141, 156},//چ
{142, 166},//ژ
{152, 237},//ک
{144, 239},//گ
 
 
 
 
};
        public Dictionary<byte, byte> CharachtersMapper_Group2 = new Dictionary<byte, byte>
{
{48,128},//
{49,129},//
{50,130},
{51,131},//
{52,132},//
{53,133},
{54,134},//
{55,135},//
{56,136},
{57,137},//
{161,138},//،
{191,140},//?
{193,143},//ء
{194,141},//آ
{195,144},//أ
{196,248},//ؤ
{197,144},//إ
{198,254},//ئ
{199,144},//ا
{200,147},//ب
{201,251},//ة
{202,151},//ت
{203,153},//ث
{204,155},//ج
{205,159},//ح
{206,161},//خ
{207,162},//د
{208,163},//ذ
{209,164},//ر
{210,165},//ز
{211,168},//س
{212,170},//ش
{213,172},//ص
{214,174},//ض
{216,175},//ط
{217,224},//ظ
{218,228},//ع
{219,232},//غ
{220,139},//-
{221,234},//ف
{222,236},//ق
{223,238},//ك
{225,243},//ل
{227,245},//م
{228,247},//ن
{229,251},//ه
{230,248},//و
{236,254},//ی
{237,254},//ی
{129,149},//پ
{141 ,157},//چ
{142,166},//ژ
{152,238},//ک
{144,240},//گ
 
 
};


        public Dictionary<byte, byte> CharachtersMapper_Group3 = new Dictionary<byte, byte>
{

{48 , 128}, // 0
{49 , 129}, // 1
{50 , 130}, // 2
{51 , 131}, // 3
{52 , 132}, // 4
{53 , 133}, // 5
{54 , 134}, // 6
{55 , 135}, // 7
{56 , 136}, // 8
{57 , 137}, // 9
{161, 138}, // ،
{191, 140}, // ؟
{193, 143}, //
{194, 141}, //
{195, 145}, //
{196, 248}, //
{197, 145}, // 
{198, 252}, //
{199, 145}, // 
{200, 146}, // 
{201, 249}, //
{202, 150}, //
{203, 152}, // 
{204, 154}, //
{205, 158}, // 
{206, 160}, //
{207, 162}, //
{208, 163}, // 
{209, 164}, //
{210, 165}, //
{211, 167}, // 
{212, 169}, // 
{213, 171}, //
{214, 173}, // 
{216, 175}, // 
{217, 224}, //
{218, 226}, // 
{219, 230}, // 
{220, 139}, //
{221, 233}, // 
{222, 235}, //
{223, 237}, //
{225, 241}, // 
{227, 244}, //
{228, 246}, //
{229, 249}, // 
{230, 248}, // 
{236, 252}, //
{237, 252}, // 
{129, 148}, // 
{141, 156}, //
{142, 166}, // 
{152, 237}, // 
{144, 239}//
};
        public Dictionary<byte, byte> CharachtersMapper_Group4 = new Dictionary<byte, byte>
{
{48 , 128}, // 0
{49 , 129}, // 1
{50 , 130}, // 2
{51 , 131}, // 3
{52 , 132}, // 4
{53 , 133}, // 5
{54 , 134}, // 6
{55 , 135}, // 7
{56 , 136}, // 8
{57 , 137}, // 9
{161, 138}, // ،
{191, 140}, // ؟
{193,143}, //
{194,141}, //
{195,145}, //
{196,248}, // 
{197,145}, // 
{198,254}, //
{199,145}, // 
{200,147}, // 
{201,250}, //
{202,151}, //
{203,153}, //
{204,155}, //
{205,159}, //
{206,161}, //
{207,162}, //
{208,163}, //
{209,164}, //
{210,165}, //
{211,168}, // 
{212,170}, //
{213,172}, //
{214,174}, //
{216,175}, // 
{217,224}, //
{218,227}, //
{219,231}, //
{220,139}, //
{221,234}, //
{222,236}, //
{223,238}, //
{225,243}, //
{227,245}, // 
{228,247}, //
{229,250}, //
{230,248}, //
{236,254}, //
{237,254}, // 
{129,149}, //
{141,157}, //
{142,166}, // 
{152,238}, // 
{144,240}, //
};
        #endregion
        public bool is_Lattin_Letter(byte c)
        {
            if (c < 128 && c > 31)
            {
                return true;
            }
            return false;


        }


        public byte get_Lattin_Letter(byte c)
        {
            if ("0123456789".IndexOf((char)c) >= 0)
                return (byte)(c + 80);
            return get_FarsiExceptions(c);
        }


        private byte get_FarsiExceptions(byte c)
        {
            switch (c)
            {
                case (byte)'(': return (byte)')';
                case (byte)'{': return (byte)'}';
                case (byte)'[': return (byte)']';
                case (byte)')': return (byte)'(';
                case (byte)'}': return (byte)'{';
                case (byte)']': return (byte)'[';
                default: return (byte)c;


            }


        }
        public bool is_Final_Letter(byte c)
        {
            string s = "ءآأؤإادذرزژو";


            if (s.ToString().IndexOf((char)c) >= 0)
            {
                return true;


            }
            return false;
        }
        public bool IS_White_Letter(byte c)
        {
            if (c == 8 || c == 09 || c == 10 || c == 13 || c == 27 || c == 32 || c == 0)
            {
                return true;
            }
            return false;
        }
        public bool Char_Cond(byte c)
        {
            return IS_White_Letter(c)
            || is_Lattin_Letter(c)
            || c == 191;
        }
        public List<byte> get_Unicode_To_IranSystem(string Unicode_Text)
        {


            // " رشته ای که فارسی است را دو کاراکتر فاصله به ابتدا و انتهای آن اضافه می کنیم
            string unicodeString = " " + Unicode_Text + " ";
            //ایجاد دو انکدینگ متفاوت
            Encoding ascii = //Encoding.ASCII;
            Encoding.GetEncoding("windows-1256");


            Encoding unicode = Encoding.Unicode;


            // تبدیل رشته به بایت
            byte[] unicodeBytes = unicode.GetBytes(unicodeString);


            // تبدیل بایتها از یک انکدینگ به دیگری
            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);


            // Convert the new byte[] into a char[] and then into a string.
            char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            string asciiString = new string(asciiChars);
            byte[] b22 = Encoding.GetEncoding("windows-1256").GetBytes(asciiChars);




            int limit = b22.Length;


            byte pre = 0, cur = 0;




            List<byte> IS_Result = new List<byte>();
            for (int i = 0; i < limit; i++)
            {


                if (is_Lattin_Letter(b22[i]))
                {
                    cur = get_Lattin_Letter(b22[i]);


                    IS_Result.Add(cur);




                    pre = cur;
                }
                else if (i != 0 && i != b22.Length - 1)
                {
                    cur = get_Unicode_To_IranSystem_Char(b22[i - 1], b22[i], b22[i + 1]);


                    if (cur == 145) // برای بررسی استثنای لا
                    {
                        if (pre == 243)
                        {
                            IS_Result.RemoveAt(IS_Result.Count - 1);
                            IS_Result.Add(242);
                        }
                        else
                        {
                            IS_Result.Add(cur);
                        }
                    }
                    else
                    {
                        IS_Result.Add(cur);
                    }






                    pre = cur;
                }


            }


            return IS_Result;
        }
        public byte get_Unicode_To_IranSystem_Char(byte PreviousChar, byte CurrentChar, byte NextChar)
        {


            bool PFlag = Char_Cond(PreviousChar) || is_Final_Letter(PreviousChar);
            bool NFlag = Char_Cond(NextChar);
            if (PFlag && NFlag) return UCTOIS_Group_1(CurrentChar);
            else if (PFlag) return UCTOIS_Group_2(CurrentChar);
            else if (NFlag) return UCTOIS_Group_3(CurrentChar);


            return UCTOIS_Group_4(CurrentChar);


        }


        private byte UCTOIS_Group_4(byte CurrentChar)
        {


            if (CharachtersMapper_Group4.ContainsKey(CurrentChar))
            {
                return (byte)CharachtersMapper_Group4[CurrentChar];
            }
            return (byte)CurrentChar;
        }


        private byte UCTOIS_Group_3(byte CurrentChar)
        {
            if (CharachtersMapper_Group3.ContainsKey(CurrentChar))
            {
                return (byte)CharachtersMapper_Group3[CurrentChar];
            }
            return (byte)CurrentChar;
        }


        private byte UCTOIS_Group_2(byte CurrentChar)
        {
            if (CharachtersMapper_Group2.ContainsKey(CurrentChar))
            {
                return (byte)CharachtersMapper_Group2[CurrentChar];
            }
            return (byte)CurrentChar;
        }
        private byte UCTOIS_Group_1(byte CurrentChar)
        {
            if (CharachtersMapper_Group1.ContainsKey(CurrentChar))
            {
                return (byte)CharachtersMapper_Group1[CurrentChar];
            }
            return (byte)CurrentChar;
        }
    }
}