using Ship.Model.Db;
using Ship.Model.Extension;
using Ship.Model.ViewModel;
using Ship.Model.ViewModel.Admin;
using SmsIrRestful;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace Ship.Model.BLL.Admin
{
    public class SiteSettingBLL
    {
        public static List<Dashboard1ViewModel> Dashboard1(long IdPersonnel)
        {
            using (var x = new GolatehEntities())
            {
                return x.Database.SqlQuery<Dashboard1ViewModel>("dbo.PrintDashboard1 " + IdPersonnel + "").ToList();
            }
        }


        public static decimal InsertOrder(long IdPersonnel, int Price, int IdPackage, int IdShip)
        {
            using (var x = new GolatehEntities())
            {
                var shomare = new ShomareOrder() { CreateDate = DateTime.Now };
                x.ShomareOrder.Add(shomare);
                x.SaveChanges();

                var _Add = new Order()
                {
                    CreateDate = DateTime.Now,
                    IdPersonnel = IdPersonnel,
                    Price = Price,
                    IdPackage = IdPackage,
                    IdShip = IdShip,
                    IdShomareOrder = shomare.Id,
                    Ref = null,
                };
                x.Order.Add(_Add);
                x.SaveChanges();

                return _Add.IdShomareOrder;
            }
        }

        public static List<Dashbor99ViewModel> RefreshNote(int IdSoom, long IdAfrad)
        {
            using (var x = new GolatehEntities())
            {
                var som = x.Soom.AsNoTracking().First(v => v.Id == IdSoom);


                return x.Note.AsNoTracking().Where(v => v.IdShip == som.IdShip && v.IdPersonnel == IdAfrad)
                    .Select(v => new Dashbor99ViewModel() { IdYadasht = v.Id, Name = v.Description }).ToList();

            }
        }



        public static object PrintDashboard3(long IdPersonnel, int Type, int IdShip)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    if (Type == 8 || Type == 9 || Type == 20 || Type == 21 || Type == 22 || Type == 23)
                    {
                        var tt = x.Database.SqlQuery<PrintDashboard4ViewModel>("[dbo].PrintDashboard3 @IdPersonnel,@Type,@IdShip",
                            new SqlParameter[] {
                                new SqlParameter("IdPersonnel", IdPersonnel),
                                new SqlParameter("Type", Type),
                                new SqlParameter("IdShip", IdShip) }).ToList();

                        return tt;
                    }
                    else if (Type == 80 || Type == 200 || Type == 210 || Type == 220)
                    {
                        var tt = x.Database.SqlQuery<PrintDashboard4ViewModel>("[dbo].PrintDashboard3 @IdPersonnel,@Type,@IdShip",
                            new SqlParameter[] {
                                new SqlParameter("IdPersonnel", IdPersonnel),
                                new SqlParameter("Type", Type),
                                new SqlParameter("IdShip", IdShip) }).ToList();

                        return tt;
                    }
                    else if (Type == 4)
                    {
                        var tt = x.Database.SqlQuery<PrintDashboard93ViewModel>("[dbo].PrintDashboard3 @IdPersonnel,@Type,@IdShip",
                            new SqlParameter[] {
                                new SqlParameter("IdPersonnel", IdPersonnel),
                                new SqlParameter("Type", Type),
                                new SqlParameter("IdShip", IdShip) }).ToList();

                        return tt;
                    }
                    else
                    {
                        var tt = x.Database.SqlQuery<PrintDashboard3ViewModel>("[dbo].PrintDashboard3 @IdPersonnel,@Type,@IdShip",
                            new SqlParameter[] {
                                new SqlParameter("IdPersonnel", IdPersonnel),
                                new SqlParameter("Type", Type),
                                new SqlParameter("IdShip", IdShip) }).ToList();

                        return tt;
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<object>();
            }
        }



        public class Root
        {
            public object TokenKey { get; set; }
            public bool IsSuccessful { get; set; }
            public string Message { get; set; }
        }




        public static int SendSmsRegister(string Mobile, string Code, string FullName)
        {
            try
            {
                var token = new Token().GetToken("71a11b4af5e9d5d49a2b1b4a", "golateh");

                var ultraFastSend = new UltraFastSend()
                {
                    Mobile = long.Parse(Mobile),
                    TemplateId = 60664,
                    ParameterArray = new List<UltraFastParameters>()
                    {
                        new UltraFastParameters() { Parameter = "FullName", ParameterValue = FullName} ,
                        new UltraFastParameters() { Parameter = "VerificationCode", ParameterValue = Code }
                    }.ToArray()
                };

                UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

                if (ultraFastSendRespone.IsSuccessful)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return 0;
            }
        }
        public static int SendSmsForgetPassword(string Mobile, string Code, string FullName)
        {
            try
            {
                var token = new Token().GetToken("71a11b4af5e9d5d49a2b1b4a", "golateh");

                var ultraFastSend = new UltraFastSend()
                {
                    Mobile = long.Parse(Mobile),
                    TemplateId = 60666,
                    ParameterArray = new List<UltraFastParameters>()
                    {
                        new UltraFastParameters() { Parameter = "FullName", ParameterValue = FullName} ,
                        new UltraFastParameters() { Parameter = "NewPass", ParameterValue = Code }
                    }.ToArray()
                };

                UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

                if (ultraFastSendRespone.IsSuccessful)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return 0;
            }
        }



        public static List<Dashbor2ViewModel> Dashbor3(int IdShip)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var modl = x.Soom.AsNoTracking().Where(v => v.IdShip == IdShip && v.IsActive == false).AsEnumerable()
                        .Select(b => new Dashbor2ViewModel()
                        {
                            Safar = b.SafarCounter,
                            Hazine = b.Safar.Where(v => v.IsActive == false).Sum(v => v.PriceTamirat + v.PriceKharid),
                            Daramad = b.Safar.Where(v => v.IsActive == false).Sum(v => v.JamKolSoodAzSafar),
                            Sood = b.Safar.Where(v => v.IsActive == false).Sum(v => v.JamKolSoodAzSafar) - b.Safar.Where(v => v.IsActive == false).Sum(v => v.PriceTamirat + v.PriceKharid),
                            Soom = b.TiTle,
                            DateFinish = b.ToDate != null ? PersianCalander.ToShamsi(b.ToDate) : "صوم باز",
                            DateStart = b.FromDate != null ? PersianCalander.ToShamsi(b.FromDate) : "مشخص نشده",
                        }).OrderBy(b => b.Soom).ToList();

                    return modl;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<Dashbor2ViewModel>();
            }
        }

        public static List<Dashbor2ViewModel> Dashbor2(int IdShip)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var modl = x.Soom.AsNoTracking().Where(v => v.IdShip == IdShip).AsEnumerable()
                        .Select(b => new Dashbor2ViewModel()
                        {
                            Safar = b.SafarCounter,
                            Hazine = b.Safar.Sum(v => v.PriceMahtaiaj),
                            Sood = b.Safar.Sum(v => v.JamKolSoodAzSafar),
                            Soom = b.TiTle,
                            DateFinish = b.ToDate != null ? PersianCalander.ToShamsi(b.ToDate) : "صوم باز",
                            DateStart = b.FromDate != null ? PersianCalander.ToShamsi(b.FromDate) : "مشخص نشده",
                        }).OrderBy(b => b.Soom).ToList();

                    return modl;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<Dashbor2ViewModel>();
            }
        }

        public static List<SiteSettingViewModel> GetListSiteSetting()
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var lst = x.SiteSetting.AsNoTracking()
                        .Select(b => new SiteSettingViewModel()
                        {
                            Logo = SiteSettings.CDN + "/" + b.Logo,
                            Id = b.Id,
                            Title = b.Title,
                            Author = b.Author,
                            ContactUs = b.ContactUs,
                            Description = b.Description,
                            Keywords = b.Keywords,
                            Email = b.Email,
                            Phone = b.Phone,
                            Dribbble = b.Dribbble,
                            Facebook = b.Facebook,
                            Linkedin = b.Linkedin,
                            Twitter = b.Twitter,
                            Inestagram = b.Inestagram
                        }).ToList();

                    return lst;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<SiteSettingViewModel>();
            }
        }
        public static int SiteSettingSubmit(SiteSettingSaveViewModel model)
        {
            try
            {
                using (var x = new GolatehEntities())
                {

                    var lst = x.SiteSetting.First(v => v.Id == model.Id);

                    lst.Author = model.Author;
                    lst.ContactUs = model.ContactUs;
                    lst.Description = model.Description;
                    lst.Keywords = model.Keywords;
                    lst.Title = model.Title;
                    lst.Phone = model.Phone;
                    lst.Email = model.Email;
                    lst.Twitter = model.Twitter;
                    lst.Facebook = model.Facebook;
                    lst.Dribbble = model.Dribbble;
                    lst.Linkedin = model.Linkedin;
                    lst.Inestagram = model.Inestagram;

                    if (model.Logo2 != null)
                    {
                        string Name = "Logo_" + Path.GetExtension(model.Logo2.FileName);
                        model.Logo2.SaveAs(System.Web.HttpContext.Current.Server.MapPath(@"~/assets/img/Site/" + Name));

                        lst.Logo = "/assets/img/Site/" + Name;
                    }

                    x.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return 0;
            }
        }







        public static string[] GetRolesForUser(int Id)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.Personnel.AsNoTracking().FirstOrDefault(b => b.Id == Id && b.IsActive == true);

                    if (itm == null)
                    {
                        return new string[] { };
                    }
                    else
                    {
                        var IdRols = x.RolePernonnel.AsNoTracking().Where(b => b.IdPersonnel == itm.Id).Select(b => b.IdRole).ToList();
                        return x.Role.AsNoTracking().Where(b => IdRols.Contains(b.Id)).Select(b => b.TitleEn).ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new string[] { };
            }
        }


        public static string[] GetRolesForUserTankhaGardan(int Id)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.TankhaGardan.AsNoTracking().FirstOrDefault(b => b.Id == Id && b.IsActive == true);

                    if (itm == null)
                    {
                        return new string[] { };
                    }
                    else
                    {
                        return new string[] { "TankhaGardan" };
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new string[] { };
            }
        }


        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string Register(string firstname, string lastname, string nationalcode, string password, string mobile)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    firstname = firstname.ToEnNum();
                    lastname = lastname.ToEnNum();
                    nationalcode = nationalcode.ToEnNum();
                    password = password.ToEnNum();
                    mobile = mobile.ToEnNum();

                    if (x.Personnel.AsNoTracking().Any(b => b.NationalCode == nationalcode && b.IsActive == true && b.IdShip == null))
                    {
                        return "این کد ملی قبلا در سیستم موجود می باشد";
                    }
                    else
                    {
                        string sms = RandomString(4);

                        x.Personnel.Add(new Personnel() { SmsActive = sms, FirstName = firstname, LastName = lastname, NationalCode = nationalcode, Phone = mobile, Password = password, IsActive = false });
                        x.SaveChanges();

                        SendSmsRegister(mobile, sms, firstname + " " + lastname);

                        return "1";
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return "0";
            }
        }


        public static string joint(string email)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    email = email.ToEnNum();

                    string sms = RandomString(4);

                    x.JoinIt.Add(new JoinIt() { Email = email });
                    x.SaveChanges();

                    return "1";
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return "0";
            }
        }
        public static string ContactAdd(string name, string email, string subject, string message)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    name = name.ToEnNum();
                    email = email.ToEnNum();
                    subject = subject.ToEnNum();
                    message = message.ToEnNum();

                    string sms = RandomString(4);

                    x.ContactUs.Add(new ContactUs() { Email = email, CreateDate = DateTime.Now, FullName = name, IsSee = false, Message = message, Title = subject });
                    x.SaveChanges();

                    return "1";
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return "0";
            }
        }

        public static int LoginActive(string username, string id)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    id = id.ToEnNum();
                    username = username.ToEnNum();

                    var itm = x.Personnel.FirstOrDefault(b => b.Phone == id && b.SmsActive == username && b.IsActive == false && b.IdShip == null);

                    if (itm == null)
                    {
                        return 0;
                    }

                    itm.IsActive = true;
                    x.SaveChanges();

                    System.Web.Security.FormsAuthentication.SetAuthCookie(itm.Id.ToString(), true);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return 0;
            }
        }

        public static int ResetPassword(string nationalcode, string mobile)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    nationalcode = nationalcode.ToEnNum();
                    mobile = mobile.ToEnNum();

                    var itm = x.Personnel.FirstOrDefault(b => b.Phone == mobile && b.NationalCode == nationalcode && b.IsActive == true && b.IdShip == null);

                    if (itm == null)
                    {
                        return 0;
                    }

                    itm.Password = RandomString(6);
                    x.SaveChanges();

                    SendSmsForgetPassword(mobile, itm.Password, itm.FirstName + " " + itm.LastName);

                    return 1;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return 0;
            }
        }
        public static int Login(string username, string password, int IdType)
        {
            try
            {
                username = username.ToEnNum();
                password = password.ToEnNum();

                if (IdType == 0)
                {
                    using (var x = new GolatehEntities())
                    {
                        var itm = x.Personnel.AsNoTracking().FirstOrDefault(b => b.NationalCode == username && b.Password == password && b.IsActive == true && b.IdShip == null);

                        if (itm == null)
                        {
                            return 0;
                        }
                        System.Web.Security.FormsAuthentication.SetAuthCookie(itm.Id.ToString(), true);
                        return 1;
                    }
                }
                else
                {
                    using (var x = new GolatehEntities())
                    {
                        var itm = x.TankhaGardan.AsNoTracking().FirstOrDefault(b => b.UserName == username && b.PassWord == password && b.IsActive == true);

                        if (itm == null)
                        {
                            return 0;
                        }

                        System.Web.Security.FormsAuthentication.SetAuthCookie(itm.Id.ToString() + "TankhaGardan", true);
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return 0;
            }
        }
        public static bool IsUserAdmin(int IdPersonnel)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    return x.RolePernonnel.AsNoTracking().Any(n => n.IdPersonnel == IdPersonnel && n.IdRole == 1);
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }

        public static PersonnelManagementViewModel GetPersonnelLogin()
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    long id = long.Parse(System.Web.HttpContext.Current.User.Identity.Name);

                    var tt = x.Personnel.AsNoTracking().Where(b => b.Id == id)
                        .Select(b => new PersonnelManagementViewModel()
                        {
                            FirstName = b.FirstName,
                            Id = b.Id,
                            IsActive = b.IsActive,
                            LastName = b.LastName,
                            Password = b.Password,
                            NationalCode = b.NationalCode,
                            Logo = b.Image,
                            IsRolePostArticle = b.RolePernonnel.Any(u => u.IdRole == 2)
                        }).First();

                    return tt;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new PersonnelManagementViewModel();
            }
        }


        public static PersonnelManagementViewModel GetPersonnelLogin2(long id)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tt = x.TankhaGardan.AsNoTracking().Where(b => b.Id == id)
                        .Select(b => new PersonnelManagementViewModel()
                        {
                            FirstName = b.FullName,
                            NationalCode = b.UserName
                        }).First();

                    return tt;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new PersonnelManagementViewModel();
            }
        }

        public static List<PersonnelManagementViewModel> GetListPersonnelManagement(int Id = 0)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    if (Id == 0)
                    {
                        var tt = x.Personnel.AsNoTracking().Select(b => new PersonnelManagementViewModel()
                        {
                            FirstName = b.FirstName,
                            Id = b.Id,
                            IsActive = b.IsActive,
                            LastName = b.LastName,
                            Password = b.Password,
                            NationalCode = b.NationalCode,
                            Logo = b.Image,
                            IsRolePostArticle = b.RolePernonnel.Any(u => u.IdRole == 2),
                            Phone = b.Phone
                        }).OrderBy(b => b.Id).ToList();

                        return tt;
                    }
                    else
                    {
                        var tt = x.Personnel.AsNoTracking().Where(v => v.Id == Id).Select(b => new PersonnelManagementViewModel()
                        {
                            FirstName = b.FirstName,
                            Id = b.Id,
                            IsActive = b.IsActive,
                            LastName = b.LastName,
                            Password = b.Password,
                            NationalCode = b.NationalCode,
                            Logo = b.Image,
                            IsRolePostArticle = b.RolePernonnel.Any(u => u.IdRole == 2),
                            Phone = b.Phone
                        }).OrderBy(b => b.Id).ToList();

                        return tt;
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<PersonnelManagementViewModel>();
            }
        }

        public static int ChangeStatusPersonnelArticle(int Id, bool IsActive)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    if (IsActive)
                    {
                        foreach (var item in x.RolePernonnel.Where(v => v.IdPersonnel == Id && v.IdRole == 2).ToList())
                        {
                            x.RolePernonnel.Remove(item);
                        }

                        x.RolePernonnel.Add(new RolePernonnel() { IdRole = 2, IdPersonnel = Id });
                    }
                    else
                    {
                        foreach (var item in x.RolePernonnel.Where(v => v.IdPersonnel == Id && v.IdRole == 2).ToList())
                        {
                            x.RolePernonnel.Remove(item);
                        }
                    }


                    x.SaveChanges();

                    return 1;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return 0;
            }
        }

        public static int ChangeStatusPersonnel(int Id, bool IsActive)
        {
            try
            {
                using (var x = new GolatehEntities())
                {

                    var vv = x.Personnel.First(b => b.Id == Id);
                    vv.IsActive = IsActive;

                    x.SaveChanges();

                    return 1;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return 0;
            }
        }

        public static int PersonnelManagementDelete(int Id)
        {
            try
            {
                using (var x = new GolatehEntities())
                {

                    var vv = x.Personnel.First(b => b.Id == Id);
                    x.Personnel.Remove(vv);

                    x.SaveChanges();

                    return 1;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return 0;
            }
        }
        public static int PersonnelManagementSubmit(string FirstName, string LastName, string UserName, string Password)
        {
            try
            {
                using (var x = new GolatehEntities())
                {

                    var vv = x.Personnel.Add(new Personnel() { Password = Password, NationalCode = UserName, LastName = LastName, FirstName = FirstName, IsActive = true });

                    x.SaveChanges();

                    return 1;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return 0;
            }
        }

        public static void AddError(Exception ex)
        {
            try
            {
                using (var x = new GolatehEntities())
                {

                    // x.AddError.Add(new AddError() { ErrorText = ex.ToString() });

                    //   x.SaveChanges();
                }
            }
            catch
            {

            }
        }
        public static List<ContactUsViewModel> GetListContactUs()
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var lst = x.ContactUs.AsNoTracking().AsEnumerable()
                        .Select(b => new ContactUsViewModel()
                        {
                            Id = b.Id,
                            Title = b.Title,
                            Message = b.Message,
                            FullName = b.FullName,
                            CreateDate = b.CreateDate,
                            CreateDateString = PersianCalander.ToShamsiTime(b.CreateDate),
                            Email = b.Email
                        }).OrderByDescending(b => b.CreateDate).ToList();

                    return lst;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<ContactUsViewModel>();
            }
        }
        public static List<SubscribeViewModel> GetListSubscribe()
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var lst = x.Subscribe.AsNoTracking().AsEnumerable()
                        .Select(b => new SubscribeViewModel()
                        {
                            Id = b.Id,
                            CreateDate = b.CreateDate,
                            CreateDateString = PersianCalander.ToShamsiTime(b.CreateDate),
                            Email = b.Email
                        }).OrderByDescending(b => b.CreateDate).ToList();

                    return lst;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<SubscribeViewModel>();
            }
        }
        public static List<GetListCategoryViewModel> GetListCategory()
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tt = x.Database.SqlQuery<GetListCategoryViewModel>("dbo.GetListCategory").ToList();

                    return tt;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<GetListCategoryViewModel>();
            }
        }

        public static int AddSaveSahamDar(int IdUser, int Id, int IdPersonnel, int Rate, int IdDong)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.Ship.First(b => b.Id == Id && b.IdPersonnel == IdUser);

                    foreach (var item in itm.ShipToSahamDar.Where(b => b.IdPersonnel == IdPersonnel && b.IsActive).ToList())
                    {
                        item.IsActive = false;
                    }


                    if (IdDong == 1)
                    {
                        itm.ShipToSahamDar.Add(new ShipToSahamDar() { IsActive = true, Rate = (int)Math.Round(double.Parse(Rate.ToString()) * 100 / 6), IdPersonnel = IdPersonnel, Dong = Rate });
                    }
                    else
                    {
                        itm.ShipToSahamDar.Add(new ShipToSahamDar() { IsActive = true, Rate = Rate, IdPersonnel = IdPersonnel, Dong = (int)Math.Round(double.Parse(Rate.ToString()) * 6 / 100) });
                    }

                    if (itm.ShipToSahamDar.Where(b => b.IsActive).Sum(v => v.Rate) > 100)
                    {
                        return 1;
                    }
                    else
                    {
                        x.SaveChanges();
                        return 2;
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return 0;
            }
        }

        public static bool DeleteSahamDar(int IdUser, int IdShip, int Id)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.Ship.First(b => b.Id == IdShip && b.IdPersonnel == IdUser);
                    itm.ShipToSahamDar.First(b => b.Id == Id).IsActive = false;

                    x.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }

        public static bool DeleteAfrad(int IdUser, int IdShip, int Id)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.Personnel.First(b => b.IdShip == IdShip && b.IdParent == IdUser && b.Id == Id);
                    //x.Personnel.Remove(itm);
                    itm.IsActive = false;


                    // 







                    x.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }
        public static bool ToActiveAfrad(int IdUser, int IdShip, int Id)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.Personnel.First(b => b.IdShip == IdShip && b.IdParent == IdUser && b.Id == Id);
                    itm.IsActive = true;
                    x.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }
        public static List<SahamDarViewModel> GetListSahamdar(int IdShip, int IdUser)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.Ship.First(b => b.Id == IdShip && b.IdPersonnel == IdUser);

                    return itm.ShipToSahamDar.Where(v => v.IsActive).Select(b => new SahamDarViewModel()
                    {
                        Id = b.Id,
                        Rate = b.Rate,
                        Title = b.Personnel.FirstName + " " + b.Personnel.LastName,
                        Dong = b.Dong
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<SahamDarViewModel>();
            }
        }


        public static List<SahamDarViewModel> GetListSahamdar2(int IdSafar, int IdUser)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.Safar.AsNoTracking().First(b => b.Id == IdSafar && b.Soom.IdPersonnel == IdUser);


                    return itm.SafarToSahamDar.Select(b => new SahamDarViewModel()
                    {
                        Id = b.Id,
                        Rate = b.Rate,
                        Title = b.Personnel.FirstName + " " + b.Personnel.LastName,
                        TotalPrice = b.TotalPrice
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<SahamDarViewModel>();
            }
        }




        public static object GetPrint(string from, string to, int type, int Ship, int user)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    return x.Database.SqlQuery<Print1ViewModel>("dbo.[Print] @From,@To,@Type,@Ship,@User", new SqlParameter[] {
                        new SqlParameter("From",from),
                        new SqlParameter("To",to),
                        new SqlParameter("Type",type),
                        new SqlParameter("Ship",Ship),
                        new SqlParameter("User",user)
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new object();
            }
        }






        public static List<SahamDarViewModel> GetListSahamdar3(int IdSoom, int IdUser)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.Soom.AsNoTracking().First(b => b.Id == IdSoom && b.IdPersonnel == IdUser);
                    var _IdSafar = itm.Safar.Where(v => v.IsActive == false).Select(v => v.Id).ToArray();


                    var data = x.SafarToSahamDar.AsNoTracking().Where(v => _IdSafar.Contains(v.IdSafar)).ToList();
                    List<SahamDarViewModel> lst = new List<SahamDarViewModel>();
                    foreach (var bb in data.Select(v => v.IdPersonnel).Distinct().ToArray())
                    {
                        var FullName = x.Personnel.AsNoTracking().First(v => v.Id == bb);

                        lst.Add(new SahamDarViewModel()
                        {
                            Rate = data.Where(v => v.IdPersonnel == bb).Sum(v => v.Rate),
                            Title = FullName.FirstName + " " + FullName.LastName,
                            TotalPrice = data.Where(v => v.IdPersonnel == bb).Sum(v => v.TotalPrice)
                        });
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<SahamDarViewModel>();
            }
        }

        public static string AddAfrad(int IdShip, int IdUser, HttpPostedFileBase file, int Id, string FirstName, string LastName, string NationalCode, string Phone, string ShebaNumber, string ShomareCart14, string ShomareHesab, int IdBank)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.Ship.First(b => b.Id == IdShip && b.IdPersonnel == IdUser);

                    var allperson = x.Personnel.AsNoTracking().Where(v => v.IdShip == itm.Id && v.IdParent == IdUser).ToList();

                    string _COde = NationalCode.ToEnNum().Trim();
                    ShebaNumber = ShebaNumber.ToUpper();

                    if (allperson.Count(v => v.NationalCode == _COde) > 1)
                    {
                        return "این کد ملی قبلا ثبت شده است";
                    }

                    if (string.IsNullOrWhiteSpace(ShebaNumber) == false)
                    {
                        if (allperson.Count(v => v.ShebaNumber == ShebaNumber) > 1)
                        {
                            return "این کد شبا قبلا ثبت شده است";
                        }
                    }

                    string ImageUrl = null;
                    if (file != null)
                    {
                        string Name = DateTime.Now.Ticks + Path.GetExtension(file.FileName);
                        file.SaveAs(System.Web.HttpContext.Current.Server.MapPath(@"~/Content/File/ImagePersonnel/" + Name));

                        ImageUrl = "/Content/File/ImagePersonnel/" + Name;
                    }

                    int? _IdBank = null;
                    if (IdBank > 0)
                    {
                        _IdBank = IdBank;
                    }



                    if (Id == 0)
                    {
                        x.Personnel.Add(new Personnel()
                        {
                            CreateDate = DateTime.Now,
                            FirstName = FirstName.ToEnNum().Trim(),
                            IdParent = itm.IdPersonnel,
                            IsActive = true,
                            LastName = LastName.ToEnNum().Trim(),
                            NationalCode = NationalCode.ToEnNum().Trim(),
                            Phone = Phone.Trim(),
                            ShebaNumber = ShebaNumber.ToEnNum().Trim().ToLower(),
                            ShomareCart14 = ShomareCart14.ToEnNum().Trim(),
                            Image = ImageUrl,
                            Password = "-",
                            IdShip = IdShip,
                            ShomareHesab = ShomareHesab,
                            IdBank = _IdBank
                        });
                    }
                    else
                    {
                        var per = x.Personnel.First(b => b.Id == Id && b.IdParent == IdUser && b.IdShip == IdShip);
                        if (ImageUrl == null) { ImageUrl = per.Image; }

                        per.FirstName = FirstName.ToEnNum().Trim();
                        per.LastName = LastName.ToEnNum().Trim();
                        per.NationalCode = NationalCode.ToEnNum().Trim();
                        per.Phone = Phone.Trim();
                        per.ShebaNumber = ShebaNumber.ToEnNum().Trim().ToLower();
                        per.ShomareCart14 = ShomareCart14.ToEnNum().Trim();
                        per.Image = ImageUrl;
                        per.ShomareHesab = ShomareHesab;
                        per.IdBank = _IdBank;
                    }

                    x.SaveChanges();
                }

                return "";
            }
            catch (Exception ex)
            {
                AddError(ex);
                return "error";
            }
        }


        public static List<Afrad2ViewModel> GetListReletionPersenel(int IdShip, int IdUser)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    return x.Personnel.AsNoTracking().Where(b => b.IdParent == IdUser && b.IdShip == IdShip && b.IsActive)
                        .Select(v => new Afrad2ViewModel()
                        {
                            Id = v.Id,
                            Title = v.FirstName + " " + v.LastName
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<Afrad2ViewModel>();
            }
        }

        //9 	ماهی
        //11	قطعات
        public static List<Afrad2ViewModel> GetListKala(long[] IdType)
        {
            using (var x = new GolatehEntities())
            {
                if (IdType.Length == 0)
                {
                    return x.Kala.AsNoTracking().Where(v => v.IsActive)
                        .Select(v => new Afrad2ViewModel()
                        {
                            Id = v.Id,
                            Title = v.Title,
                            Id2 = v.IdUnit,
                            IdTypeKala = v.IdTypeKala,
                            Sort = v.Sort
                        }).OrderBy(b => b.Sort).ThenBy(v => v.IdTypeKala).ThenBy(v => v.Title).ToList();
                }
                else
                {
                    return x.Kala.AsNoTracking().Where(v => IdType.Contains(v.IdTypeKala) && v.IsActive)
                        .Select(v => new Afrad2ViewModel()
                        {
                            Id = v.Id,
                            Title = v.Title,
                            Id2 = v.IdUnit,
                            Sort = v.Sort
                        }).OrderBy(v => v.Sort).ToList();
                }
            }
        }

        public static List<Afrad2ViewModel> GetListKala2(long IdSafar, long[] IdType)
        {
            using (var x = new GolatehEntities())
            {
                var ids = x.SafarToFactor.AsNoTracking().Where(b => b.IdSafar == IdSafar && b.IdTypeFactor == 14).Select(v => v.IdKala).ToList();

                return x.Kala.AsNoTracking().Where(v => IdType.Contains(v.IdTypeKala) && v.IsActive && ids.Contains(v.Id))
                        .Select(v => new Afrad2ViewModel()
                        {
                            Id = v.Id,
                            Title = v.Title,
                            Id2 = v.IdUnit,
                            Sort = v.Sort
                        }).OrderBy(v => v.Sort).ToList();
            }
        }

        public static List<Afrad2ViewModel> GetListUnit()
        {
            using (var x = new GolatehEntities())
            {
                return x.Unit.AsNoTracking()
                    .Select(v => new Afrad2ViewModel()
                    {
                        Id = v.Id,
                        Title = v.Title
                    }).ToList();
            }
        }
        public static List<Afrad2ViewModel> GetListReletionPersenel2(long IdSafar, int IdUser)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var rtt = x.SafarToAfrad.AsNoTracking().Where(v => v.IdSafar == IdSafar).Select(b => b.IdPernonnel).ToArray();

                    var modl = x.Personnel.AsNoTracking().Where(b => b.IdParent == IdUser && rtt.Contains(b.Id))
                        .Select(v => new Afrad2ViewModel()
                        {
                            Id = v.Id,
                            Title = v.FirstName + " " + v.LastName
                        }).ToList();


                    var IdSahebShip = x.Safar.AsNoTracking().First(v => v.Id == IdSafar && v.Soom.IdPersonnel == IdUser).Soom.Ship.IdPersonnel;

                    if (modl.Any(b => b.Id == IdSahebShip) == false)
                    {
                        var _per = x.Personnel.AsNoTracking().First(b => b.Id == IdSahebShip);

                        modl.Add(new Afrad2ViewModel()
                        {
                            Id = _per.Id,
                            Title = _per.FirstName + " " + _per.LastName
                        });
                    }

                    return modl;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<Afrad2ViewModel>();
            }
        }
        public static List<Afrad2ViewModel> GetListReletionPersenel3(long IdSafar, int IdUser)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var IdShip = x.Safar.AsNoTracking().First(b => b.Id == IdSafar).Soom.IdShip;

                    var modl = x.Personnel.AsNoTracking().Where(b => b.IdParent == IdUser && b.IdShip == IdShip && b.IsActive)
                        .Select(v => new Afrad2ViewModel()
                        {
                            Id = v.Id,
                            Title = v.FirstName + " " + v.LastName
                        }).ToList();

                    return modl;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<Afrad2ViewModel>();
            }
        }
        public static List<SematViewModel> GetListSemat()
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    return x.Semat.AsNoTracking().Where(b => b.Id != 10).Select(v => new SematViewModel()
                    {
                        Id = v.Id,
                        Rate = v.Ratio,
                        Title = v.Title
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<SematViewModel>();
            }
        }


        public static List<AfradViewModel> GetListAfrad(int IdShip, int IdUser)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.Ship.First(b => b.Id == IdShip && b.IdPersonnel == IdUser);

                    return x.Personnel.AsNoTracking().Where(v => v.IdParent == itm.IdPersonnel && v.IdShip == itm.Id)
                        .Select(b => new AfradViewModel()
                        {
                            FirstName = b.FirstName,
                            Id = b.Id,
                            NationalCode = b.NationalCode,
                            ShebaNumber = b.ShebaNumber,
                            LastName = b.LastName,
                            ShomareCart14 = b.ShomareCart14,
                            Logo = b.Image,
                            Phone = b.Phone,
                            Namebank = b.Bank.Name,
                            NameBank = b.Bank.Name,
                            ShomareHesab = b.ShomareHesab,
                            IdBank = b.IdBank,
                            IsActive = b.IsActive
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<AfradViewModel>();
            }
        }

        public static bool ShipDelete(int IdUser, int Id)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.Ship.First(b => b.Id == Id && b.IdPersonnel == IdUser);

                    using (var transaction = x.Database.BeginTransaction())
                    {
                        try
                        {
                            x.Database.ExecuteSqlCommand(@"DECLARE @id BIGINT = " + Id + @";

DELETE dbo.PackageToShip WHERE IdShip=@id
DELETE dbo.TankhaGardanToShip WHERE IdShip=@id
DELETE dbo.ShipToSahamDar WHERE IdShip=@id

delete dbo.SafarToAfrad WHERE IdSafar IN (SELECT Id FROM dbo.Safar WHERE IdSoom IN(SELECT Id FROM dbo.Soom WHERE IdShip=@id))
delete dbo.SafarToFactor WHERE IdSafar IN (SELECT Id FROM dbo.Safar WHERE IdSoom IN(SELECT Id FROM dbo.Soom WHERE IdShip=@id))
delete dbo.SafarToHandMoney WHERE IdSafar IN (SELECT Id FROM dbo.Safar WHERE IdSoom IN(SELECT Id FROM dbo.Soom WHERE IdShip=@id))
delete dbo.SafarToSahamDar WHERE IdSafar IN (SELECT Id FROM dbo.Safar WHERE IdSoom IN(SELECT Id FROM dbo.Soom WHERE IdShip=@id))

DELETE dbo.Safar WHERE IdSoom IN(SELECT Id FROM dbo.Soom WHERE IdShip=@id)
DELETE dbo.Soom WHERE IdShip = @id


DELETE dbo.Personnel WHERE IdShip=@id
DELETE dbo.[Order] WHERE IdShip=@id
DELETE dbo.Ship WHERE id=@id

SELECT CAST(1 AS int)");

                            x.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }
        public static bool DeleteAfradhand(long IdPersonnel, int IdShip)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var sip = x.Ship.AsNoTracking().First(b => b.Id == IdShip);
                    x.Personnel.AsNoTracking().First(b => b.Id == IdPersonnel);

                    var Idsom = sip.Soom.Select(c => c.Id).ToList();
                    var IdSafar = x.Safar.AsNoTracking().Where(v => Idsom.Contains(v.IdSoom) && v.IsActive == false).Select(v => v.Id).ToList();

                    foreach (var pp in x.SafarToHandMoney.Where(v => v.IdPersonnel == IdPersonnel && IdSafar.Contains(v.IdSafar) && v.IsPay == false).ToList())
                    {
                        pp.IsPay = true;
                    }

                    x.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }
        public static MessageViewModel ShipAddSave(int IdUser, int Id, string Title, string Number, string YearProduction, int? Tul, int? Arz, string NameMotor, int? HajMotor, string ShomareBime, string Image)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var _now = DateTime.Now;
                    var lst = x.PackageToShip.AsNoTracking().Where(v => v.IdPersonnel == IdUser).ToList();

                    if (lst.Any() && lst.Any(v => v.ExpireDate >= _now && v.IdPackage != null) == false)
                    {
                        return new MessageViewModel() { Status = false, Msg = "نمی توان کشتی جدیدی تعریف کرد. اشتراک تهیه فرماید" };
                    }


                    if (Id == 0)
                    {
                        var _add = new Db.Ship()
                        {
                            NameMotor = NameMotor,
                            Title = Title,
                            Arz = Arz,
                            CreateDate = DateTime.Now,
                            HajMotor = HajMotor,
                            Number = Number,
                            YearProduction = PersianCalander.ToMiladi(YearProduction.ToEnNum()),
                            Tul = Tul,
                            IdPersonnel = IdUser,
                            ShomareBime = ShomareBime,
                            Image = Image
                        };
                        x.Ship.Add(_add);
                        _add.PackageToShip.Add(new PackageToShip()
                        {
                            IdPackage = null,
                            TotalPrice = 0,
                            CreateDate = DateTime.Now,
                            IsPay = true,
                            IdPersonnel = 0,
                            ExpireDate = DateTime.Now.AddMonths(2)
                        });
                    }
                    else
                    {
                        var itm = x.Ship.First(b => b.Id == Id && b.IdPersonnel == IdUser);

                        itm.NameMotor = NameMotor;
                        itm.Title = Title;
                        itm.Arz = Arz;
                        itm.HajMotor = HajMotor;
                        itm.Number = Number;
                        itm.YearProduction = PersianCalander.ToMiladi(YearProduction.ToEnNum());
                        itm.Tul = Tul;
                        itm.ShomareBime = ShomareBime;

                        if (string.IsNullOrWhiteSpace(Image) == false)
                        {
                            itm.Image = Image;
                        }
                    }

                    x.SaveChanges();
                    return new MessageViewModel() { Status = true };
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new MessageViewModel() { Status = true, Msg = "خطا در قبت اطلاعات" };
            }
        }

        public static List<GetListShipViewModel> GetListShip(int IdPersonnel)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tttt = x.Ship.AsNoTracking().Where(b => b.IdPersonnel == IdPersonnel)
                        .AsEnumerable()
                        .Select(b => new GetListShipViewModel()
                        {
                            Arz = b.Arz,
                            HajMotor = b.HajMotor,
                            Id = b.Id,
                            NameMotor = b.NameMotor,
                            Number = b.Number,
                            Title = b.Title,
                            Tul = b.Tul,
                            YearProduction2 = b.YearProduction,
                            TedadSahamDar = b.ShipToSahamDar.Count(v => v.IsActive),
                            TedadAfrad = x.Personnel.AsNoTracking().Count(v => v.IdParent == IdPersonnel && v.IdShip == b.Id && v.IsActive),
                            ShomareBime = b.ShomareBime,
                            Image = b.Image
                        }).OrderBy(v => v.Id).ToList();


                    foreach (var c in tttt)
                    {
                        c.YearProduction = PersianCalander.ToShamsi(c.YearProduction2);
                        c.Yadasht = GetListYadasht(c.Id).Count;

                        var itm = x.PackageToShip.AsNoTracking().FirstOrDefault(v => v.IdShip == c.Id && v.IsPay == true && v.IdPersonnel == IdPersonnel && v.ExpireDate >= DateTime.Now && v.IsPay == true);
                        if (itm != null)
                        {
                            c.ExpireDate = PersianCalander.ToShamsi(itm.ExpireDate);
                            c.IsEnded = (itm.ExpireDate <= DateTime.Now) ? true : false;
                        }
                        else
                        {
                            c.IsEnded = true;
                        }
                    }

                    return tttt;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<GetListShipViewModel>();
            }
        }

        public static List<ListHandMoneyViewModel> GetListHandMoney(int IdShip)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    return x.Database.SqlQuery<ListHandMoneyViewModel>("dbo.ListHandMoney " + IdShip).ToList();
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<ListHandMoneyViewModel>();
            }
        }


        public static List<GetListShipViewModel> GetListShipByIdShip(int IdShip)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tttt = x.Ship.AsNoTracking().Where(b => b.Id == IdShip)
                        .AsEnumerable()
                        .Select(b => new GetListShipViewModel()
                        {
                            Arz = b.Arz,
                            HajMotor = b.HajMotor,
                            Id = b.Id,
                            NameMotor = b.NameMotor,
                            Number = b.Number,
                            Title = b.Title,
                            Tul = b.Tul,
                            YearProduction2 = b.YearProduction,
                            TedadSahamDar = b.ShipToSahamDar.Count(v => v.IsActive),
                            TedadAfrad = 0,
                            ShomareBime = b.ShomareBime
                        }).OrderBy(v => v.Id).ToList();


                    foreach (var c in tttt)
                    {
                        c.YearProduction = PersianCalander.ToShamsi(c.YearProduction2);
                    }

                    return tttt;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<GetListShipViewModel>();
            }
        }



        public static List<GetidValViewModel> GetListOption(int IdOption)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    return x.OptionParameter.AsNoTracking().Where(b => b.IdOption == IdOption)
                        .Select(b => new GetidValViewModel()
                        {
                            Id = b.Id.ToString(),
                            Val = b.Title,
                            Val2 = b.Val
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<GetidValViewModel>();
            }
        }



        public static List<IdValViewModel> GetListBank()
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    return x.Bank.AsNoTracking()
                        .Select(b => new IdValViewModel()
                        {
                            Id = b.Id.ToString(),
                            Title = b.Name
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<IdValViewModel>();
            }
        }


        public static bool SafarIsOpen(int IdUser, int IdSoom)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    return x.Safar.AsNoTracking().Any(b => b.IdPersonnel == IdUser && b.IsActive && b.IdSoom == IdSoom);
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }
        public static bool SoomIsOpen(int IdUser, int IdShip)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    return x.Soom.AsNoTracking().Any(b => b.IdPersonnel == IdUser && b.IsActive && b.IdShip == IdShip);
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }



        public static Int32 PriceHarGolate(Int64 IdSafar)
        {
            using (var x = new GolatehEntities())
            {
                return x.Database.SqlQuery<Int32>("SELECT dbo.PriceHarGolate(" + IdSafar + ")").First();
            }
        }




        public static int SaveBimeh(int IdUser, string[] IdIdPersonnel, string Y, string M, int IdShip)
        {
            try
            {
                using (var x = new GolatehEntities())
                {

                    x.Ship.AsNoTracking().Any(b => b.Id == IdShip && b.IdPersonnel == IdUser);

                    var Lst = x.BimehAfrad.Where(b => b.IdShip == IdShip && b.Year == Y && b.Month == M && !IdIdPersonnel.Contains(b.IdPersonnel.ToString())).ToList();
                    x.BimehAfrad.RemoveRange(Lst);

                    foreach (var item in IdIdPersonnel)
                    {
                        var Any = x.BimehAfrad.FirstOrDefault(b => b.IdShip == IdShip && b.Year == Y && b.Month == M && b.IdPersonnel.ToString() == item);
                        if (Any == null)
                        {
                            x.BimehAfrad.Add(new BimehAfrad() { IdPersonnel = long.Parse(item), IdShip = IdShip, Month = M, Year = Y });
                        }
                    }

                    x.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return 0;
            }
        }

        public static int DeleteBimeh(int IdUser, string Id)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    string[] Splt = Id.Split('|');

                    int IdShip = int.Parse(Splt[2]);
                    string Year = Splt[0];
                    string Month = Splt[1];

                    x.Ship.AsNoTracking().First(b => b.Id == IdShip && b.IdPersonnel == IdUser);

                    x.BimehAfrad.RemoveRange(x.BimehAfrad.Where(b => b.IdShip == IdShip && b.Year == Year && b.Month == Month).ToList());
                    x.SaveChanges();

                    return 1;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return 0;
            }
        }



        public static List<GetListBimehAfradViewModel> GetListBimehAfrad(int IdShip)
        {
            using (var x = new GolatehEntities())
            {
                return x.Database.SqlQuery<GetListBimehAfradViewModel>("dbo.GetListBimehAfrad @IdShip", new SqlParameter[] { new SqlParameter("IdShip", IdShip) }).ToList();
            }
        }



        public static string CloseSafar(int IdUser, long Id, bool Status)
        {
            string Text = "بدهی این سفر";

            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.Safar.First(b => b.Id == Id && b.IdPersonnel == IdUser);
                    if (Status)
                    {
                        itm.IsActive = true;
                        x.Note.RemoveRange(itm.Note.Where(v => v.IdSafar == Id).ToList());
                        x.SafarToFactor.RemoveRange(itm.SafarToFactor.Where(v => v.IdKala == 1151).ToList());

                        foreach (var ss in itm.SafarToHandMoney.Where(v => v.IsPay == true))
                        {
                            ss.IsPay = false;
                        }

                        foreach (var item in itm.SafarToHandMoney.Where(v => v.IdDebtHistory != null).ToList())
                        {
                            x.DebtHistory.First(v => v.Id == item.IdDebtHistory).IsActive = false;
                        }

                        x.SaveChanges();
                        return "";
                    }


                    if (CheckSafae2(PersianCalander.ToShamsi(itm.FromDate), PersianCalander.ToShamsi(itm.ToDate), (int)itm.IdSoom, Id))
                    {
                        return "تاریخ های وارد شده تداخل دارد با تاریخ شروع و پایان صوم";
                    }
                    if (itm.SafarToHandMoney.Any(b => b.IdKala != null && b.Price == null))
                    {
                        return "در لیست مساعده صیدی وجود دارد که در فاکتور فروش نیست";
                    }
                    if (itm.SafarToAfrad.Count(b => b.IdSemat == 1 || b.IdSemat == 8) < 2)
                    {
                        return "صیادان در سفر را تکمیل کنید";
                    }
                    var Rate = x.ShipToSahamDar.AsNoTracking().Where(v => v.IdShip == itm.Soom.IdShip && v.IsActive).Sum(v => v.Rate);
                    if (Rate != 100)
                    {
                        return "اطلاعات سهام داران تکمیل نیست";
                    }


                    itm.IsActive = false;
                    itm.PriceForosh = itm.SafarToFactor.Where(b => b.IdTypeFactor == 14).Sum(v => v.TotalPrice);
                    itm.FactorMosaede = itm.SafarToHandMoney.Where(v => v.TotalPrice != null).Sum(v => v.TotalPrice.Value);
                    itm.PriceTamirat = itm.SafarToFactor.Where(v => v.IdTypeFactor == 16).Sum(v => v.TotalPrice);
                    itm.PriceKharid = itm.SafarToFactor.Where(v => v.IdTypeFactor == 13).Sum(v => v.TotalPrice);
                    itm.PriceMahtaiaj = itm.SafarToFactor.Where(v => v.IdTypeFactor == 18).Sum(v => v.TotalPrice);
                    itm.DaramadSafarFrosh_Machele = Convert.ToInt64(itm.PriceForosh) - Convert.ToInt64(itm.PriceMahtaiaj);
                    foreach (var ss in itm.SafarToHandMoney.Where(v => v.IsPay == true))
                    {
                        ss.IsPay = false;
                    }
                    x.SaveChanges();
                    itm = x.Safar.First(b => b.Id == Id && b.IdPersonnel == IdUser);

                    var List = itm.SafarToFactor.Where(b => b.IdTypeFactor == 14).ToList();

                    foreach (var cc in itm.SafarToHandMoney.Where(b => b.IdKala != null).ToList())
                    {
                        if (List.Any(b => b.IdKala == cc.IdKala))
                        {
                            cc.Price = List.First(b => b.IdKala == cc.IdKala).Price;
                            cc.TotalPrice = List.First(b => b.IdKala == cc.IdKala).Price * Convert.ToInt64(cc.Weight);
                        }
                        else
                        {
                            cc.Price = null;
                            cc.TotalPrice = null;
                        }
                    }

                    string _yer = PersianCalander.ToShamsi(DateTime.Now).Substring(0, 4);
                    foreach (var ii in itm.SafarToAfrad.Where(b => b.IsBimeh).ToList())
                    {
                        ii.PriceBimeh = x.Bimeh.First(v => v.IsActive && v.Year == _yer).Price;
                    }

                    Int64 SoodSafarSegmnt2 = ((Int64)(itm.PriceForosh - itm.PriceMahtaiaj));
                    if (SoodSafarSegmnt2 < 0)
                    {
                        var Div2 = Math.Abs(SoodSafarSegmnt2 / 2);
                        // 1151 کالای هزینه سفر 
                        if (itm.SafarToFactor.Any(v => v.IdKala == 1151 && v.IdTypeFactor == 16))
                        {
                            itm.SafarToFactor.First(v => v.IdKala == 1151 && v.IdTypeFactor == 16).TotalPrice = Div2;
                        }
                        else
                        {
                            itm.SafarToFactor.Add(new SafarToFactor() { IdKala = 1151, IdTypeFactor = 16, Tedad = 1, Price = Div2, CreateDate = DateTime.Now, TotalPrice = Div2, IdUnit = 1 });
                        }




                        var Bedehi = Math.Abs(Div2 / itm.SafarToAfrad.Count(b => b.IdPernonnel != 78));
                        var TextBedehiMacheleh = "بدهی ماچله برای سفر " + itm.Title + " از صوم " + itm.Soom.TiTle + " در تاریخ " + PersianCalander.ToShamsi(itm.ToDate);

                        foreach (var item in itm.SafarToAfrad.Where(b => b.IdPernonnel != 78).ToList())
                        {
                            if (itm.Note.Any(v => v.IdSafar == Id && v.IdPersonnel == item.IdPernonnel))
                            {
                                itm.Note.First(v => v.IdSafar == Id && v.IdPersonnel == item.IdPernonnel).Price = Bedehi;
                            }
                            else
                            {
                                var BedehiSafar = new Note()
                                {
                                    IdSafar = Id,
                                    Price = Bedehi,
                                    CreateDate = DateTime.Now,
                                    IdPersonnel = item.IdPernonnel,
                                    Description = TextBedehiMacheleh,
                                    IdShip = itm.Soom.IdShip,
                                };
                                itm.Note.Add(BedehiSafar);
                            }
                        }




                        //foreach (var item in itm.SafarToAfrad.Where(b => b.IdPernonnel != 78).ToList())
                        //{
                        //    if (itm.SafarToHandMoney.Any(v => v.Tozihat != null && v.Tozihat.Contains(Text) && v.IdKala == null && v.IdPersonnel == item.IdPernonnel))
                        //    {
                        //        itm.SafarToHandMoney.First(v => v.Tozihat != null && v.Tozihat.Contains(Text) && v.IdKala == null && v.IdPersonnel == item.IdPernonnel).Price = Bedehi;
                        //    }
                        //    else
                        //    {
                        //        itm.SafarToHandMoney.Add(new SafarToHandMoney() { Tozihat = Text, Price = Bedehi, CreateDate = DateTime.Now, IdPersonnel = item.IdPernonnel, TotalPrice = Bedehi, CreateDateMoney = DateTime.Now });
                        //    }
                        //}

                        x.SaveChanges();

                        itm.GolateBase = 0;
                        itm.PriceForosh = itm.PriceForosh + itm.SafarToHandMoney.Where(b => b.IdKala != null).Sum(v => Convert.ToInt64(v.TotalPrice));
                        itm.JamKolSoodAzSafar = 0;
                        itm.GolateTedadKol = itm.SafarToAfrad.Sum(b => b.Ratio);

                        foreach (var item in itm.SafarToHandMoney.Where(v => v.IdDebtHistory != null).ToList())
                        {
                            x.DebtHistory.First(v => v.Id == item.IdDebtHistory).IsActive = true;
                        }

                        x.SaveChanges();

                        return "";
                    }

                    x.SafarToHandMoney.RemoveRange(itm.SafarToHandMoney.Where(v => v.Tozihat != null && v.Tozihat.Contains(Text) && v.IdKala == null).ToList());
                    x.SafarToFactor.RemoveRange(itm.SafarToFactor.Where(v => v.IdKala == 1151).ToList());


                    SoodSafarSegmnt2 = SoodSafarSegmnt2 / 2;

                    //---------------- محاسبه سود افراد لنج
                    decimal OneGolate = SoodSafarSegmnt2 / (itm.SafarToAfrad.Where(v => v.IdPernonnel != 78).Sum(v => v.Ratio) + itm.TamiratGolate);
                    foreach (var _shm in itm.SafarToAfrad.ToList())
                    {
                        var _mosaede = itm.SafarToHandMoney.Where(v => v.IdPersonnel == _shm.IdPernonnel).ToList();
                        long Mosaede = _mosaede.Sum(b => Convert.ToInt64(b.TotalPrice.Value));

                        _shm.TotalPrice = (Convert.ToInt64(OneGolate * _shm.Ratio) - (Mosaede + _shm.PriceBimeh));

                        if (_shm.TotalPrice > 0 && Mosaede > 0)
                        {
                            foreach (var bb in _mosaede)
                            {
                                bb.IsPay = true;
                            }
                        }

                        _shm.Nakhales = Convert.ToInt64(OneGolate * _shm.Ratio);
                    }


                    // آشپز
                    if (itm.SafarToFactor.Any(b => b.IdTypeFactor == 14))
                    {
                        if (itm.SafarToAfrad.Any(b => b.IdSemat == 8))
                        {
                            foreach (var _shm in itm.SafarToAfrad.Where(b => b.IdSemat != 8 && b.IdSemat != 10).ToList())
                            {
                                _shm.TotalPrice = _shm.TotalPrice - itm.PriceFood;
                                _shm.KasriTabakhi = itm.PriceFood;
                                itm.SafarToAfrad.First(b => b.IdSemat == 8).TotalPrice += itm.PriceFood;
                            }

                            itm.SafarToAfrad.First(b => b.IdSemat == 8).KasriTabakhi = 0;
                        }
                    }


                    // 78 TamiratGolate
                    if (itm.SafarToAfrad.Any(b => b.IdPernonnel == 78))
                    {
                        if (itm.TamiratGolate == 0)
                        {
                            x.SafarToAfrad.Remove(itm.SafarToAfrad.First(b => b.IdPernonnel == 78));
                        }
                        else
                        {
                            itm.SafarToAfrad.First(b => b.IdPernonnel == 78).TotalPrice = Convert.ToInt64(OneGolate * itm.TamiratGolate);
                        }
                    }
                    else if (itm.TamiratGolate > 0)
                    {
                        itm.SafarToAfrad.Add(new SafarToAfrad() { IdPernonnel = 78, IdSafar = itm.Id, TotalPrice = Convert.ToInt64(OneGolate * itm.TamiratGolate), Ratio = itm.TamiratGolate, IdSemat = 10 });
                    }

                    itm.GolateTedadKol = itm.SafarToAfrad.Sum(b => b.Ratio);

                    //---------------- محاسبه سود صاحب لنج
                    x.SafarToSahamDar.RemoveRange(itm.SafarToSahamDar);
                    Int64 PriceTamirat = (long)(itm.TamiratGolate * OneGolate);

                    foreach (var _shm in itm.Soom.Ship.ShipToSahamDar.Where(v => v.IsActive).ToList())
                    {
                        if (itm.SafarToHandMoney.Any(v => v.IdPersonnel == _shm.IdPersonnel) && itm.SafarToAfrad.Any(v => v.IdPernonnel == _shm.IdPersonnel) == false)
                        {
                            var _Mosaede = itm.SafarToHandMoney.Where(v => v.IdPersonnel == _shm.IdPersonnel).ToList();
                            long Mosaede = _Mosaede.Sum(b => Convert.ToInt64(b.TotalPrice));
                            var _TotapPrice = ((SoodSafarSegmnt2 + PriceTamirat) * _shm.Rate / 100) - Mosaede;
                            itm.SafarToSahamDar.Add(new SafarToSahamDar() { IdPersonnel = _shm.IdPersonnel, IdSafar = itm.Id, Rate = _shm.Rate, TotalPrice = _TotapPrice });

                            if (_TotapPrice > 0 && Mosaede > 0)
                            {
                                foreach (var bb in _Mosaede)
                                {
                                    bb.IsPay = true;
                                }
                            }
                        }
                        else
                        {
                            itm.SafarToSahamDar.Add(new SafarToSahamDar() { IdPersonnel = _shm.IdPersonnel, IdSafar = itm.Id, Rate = _shm.Rate, TotalPrice = ((SoodSafarSegmnt2 + PriceTamirat) * _shm.Rate / 100) });
                        }
                    }

                    x.SaveChanges();

                    itm.GolateBase = (long)OneGolate;
                    itm.PriceForosh = itm.PriceForosh + itm.SafarToHandMoney.Where(b => b.IdKala != null).Sum(v => Convert.ToInt64(v.TotalPrice));

                    itm.JamKolSoodAzSafar = itm.SafarToSahamDar.Sum(v => v.TotalPrice);

                    foreach (var item in itm.SafarToHandMoney.Where(v => v.IdDebtHistory != null).ToList())
                    {
                        x.DebtHistory.First(v => v.Id == item.IdDebtHistory).IsActive = true;
                    }

                    x.SaveChanges();

                    return "";
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return "error";
            }
        }
        public static string CloseSoom(int IdUser, int Id, bool Status)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.Soom.First(b => b.Id == Id && b.IdPersonnel == IdUser);

                    if (Status)
                    {
                        itm.IsActive = true;
                    }
                    else
                    {
                        if (itm.Safar.Count == 0)
                        {
                            return "شما در این صوم سفری انجام نداده اید";
                        }

                        if (itm.Safar.Count(v => v.IsActive) > 0)
                        {
                            return "شما در این صوم سفر باز دارید";
                        }
                        else
                        {
                            itm.IsActive = false;
                        }
                    }

                    x.SaveChanges();
                    return "";
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return "خطا در ثبت اطلاعات";
            }
        }
        public static bool SoomDelete(int IdUser, int Id)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.Soom.First(b => b.Id == Id && b.IdPersonnel == IdUser && b.IsActive);

                    int _idship = itm.IdShip;


                    x.Soom.Remove(itm);
                    x.SaveChanges();

                    int a = 1;
                    foreach (var soom in x.Soom.Where(v => v.IdShip == _idship).ToList())
                    {
                        soom.SommConter = a;
                        a = a + 1;
                    }
                    x.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }
        public static int SoomAddSave(int IdUser, long Id, int IdShip, bool IsActive, string FromDate, string ToDate, string Tozihat, string SoomName)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    if (PersianCalander.ToMiladi(ToDate.ToEnNum()) < PersianCalander.ToMiladi(FromDate.ToEnNum()))
                    {
                        return -1;
                    }

                    if (CheckSoom(FromDate.ToEnNum(), ToDate.ToEnNum(), IdShip, (int)Id, IdUser))
                    {
                        return -5;
                    }

                    if (Id == 0)
                    {
                        var _add = new Db.Soom()
                        {
                            IdPersonnel = IdUser,
                            Tozihat = Tozihat,
                            IdShip = IdShip,
                            IsActive = IsActive,
                            TiTle = SoomName,
                            IsCheckout = false
                        };
                        if (string.IsNullOrWhiteSpace(FromDate) == false)
                        {
                            _add.FromDate = PersianCalander.ToMiladi(FromDate.ToEnNum());
                        }
                        if (string.IsNullOrWhiteSpace(ToDate) == false)
                        {
                            _add.ToDate = PersianCalander.ToMiladi(ToDate.ToEnNum());
                        }

                        var tedad = x.Soom.Count(b => b.IdShip == IdShip);
                        _add.SommConter = tedad + 1;
                        x.Soom.Add(_add);
                    }
                    else
                    {
                        var itm = x.Soom.First(b => b.Id == Id && b.IdPersonnel == IdUser);

                        itm.Tozihat = Tozihat;
                        itm.IdShip = IdShip;
                        itm.IsActive = IsActive;
                        itm.TiTle = SoomName;

                        if (string.IsNullOrWhiteSpace(FromDate) == false)
                        {
                            itm.FromDate = PersianCalander.ToMiladi(FromDate.ToEnNum());
                        }
                        if (string.IsNullOrWhiteSpace(ToDate) == false)
                        {
                            itm.ToDate = PersianCalander.ToMiladi(ToDate.ToEnNum());
                        }
                    }

                    x.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return -2;
            }
        }
        public static List<GetListSoomViewModel> GetListSoom(int IdPersonnel)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tttt = x.Soom.AsNoTracking().Where(b => b.IdPersonnel == IdPersonnel)
                        .AsEnumerable()
                        .Select(b => new GetListSoomViewModel()
                        {
                            Id = b.Id,
                            FromDate2 = b.FromDate,
                            ToDate2 = b.ToDate,
                            Tozihat = b.Tozihat,
                            IdShip = b.IdShip,
                            IsActive = b.IsActive,
                            ShipName = b.Ship.Title,
                            TedadSafar = b.Safar.Count,
                            SoomName = b.TiTle
                        }).OrderByDescending(v => v.FromDate).ToList();

                    foreach (var c in tttt)
                    {
                        if (c.FromDate2 != null)
                        {
                            c.FromDate = PersianCalander.ToShamsi(c.FromDate2.Value);
                        }
                        if (c.ToDate2 != null)
                        {
                            c.ToDate = PersianCalander.ToShamsi(c.ToDate2.Value);
                        }
                    }

                    return tttt;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<GetListSoomViewModel>();
            }
        }

        public static List<GetListSoomViewModel> GetListSoom22(int IdShip)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tttt = x.Soom.AsNoTracking().Where(b => b.IdShip == IdShip)
                        .AsEnumerable()
                        .Select(b => new GetListSoomViewModel()
                        {
                            Id = b.Id,
                            FromDate2 = b.FromDate,
                            ToDate2 = b.ToDate,
                            Tozihat = b.Tozihat,
                            IdShip = b.IdShip,
                            IsActive = b.IsActive,
                            ShipName = b.Ship.Title,
                            TedadSafar = b.Safar.Count,
                            SoomName = b.TiTle
                        }).OrderByDescending(v => v.FromDate).ToList();

                    foreach (var c in tttt)
                    {
                        if (c.FromDate2 != null)
                        {
                            c.FromDate = PersianCalander.ToShamsi(c.FromDate2.Value);
                        }
                        if (c.ToDate2 != null)
                        {
                            c.ToDate = PersianCalander.ToShamsi(c.ToDate2.Value);
                        }
                    }

                    return tttt;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<GetListSoomViewModel>();
            }
        }


        public static bool DeleteSafar(int IdUser, int Id)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.Safar.First(b => b.Id == Id && b.IdPersonnel == IdUser && b.IsActive);

                    long idsom = itm.IdSoom;

                    var idsdebed = itm.SafarToHandMoney.Where(v => v.IdDebtHistory != null).Select(v => v.IdDebtHistory).ToList();
                    x.DebtHistory.RemoveRange(x.DebtHistory.Where(b => idsdebed.Contains(b.Id)).ToList());

                    x.SafarToFactor.RemoveRange(itm.SafarToFactor);
                    x.SafarToAfrad.RemoveRange(itm.SafarToAfrad);
                    x.SafarToHandMoney.RemoveRange(itm.SafarToHandMoney);
                    x.SafarToSahamDar.RemoveRange(itm.SafarToSahamDar);

                    x.Safar.Remove(itm);
                    x.SaveChanges();

                    var itmsom = x.Soom.First(b => b.Id == idsom);

                    int a = 1;
                    foreach (var item in itmsom.Safar)
                    {
                        item.SafarCount = a;
                        a = a + 1;
                    }

                    x.Soom.First(v => v.Id == idsom).SafarCounter = itmsom.Safar.Count;
                    x.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }

        public static int AddAfradToSafar(int IdUser, int IdSoom, int IdSafar, int IdAfrad, int IdSemat, decimal Rate, bool IsBimeh)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var yaer = x.Safar.AsNoTracking().First(v => v.Id == IdSafar).FromDate;

                    var IdShip = x.Soom.AsNoTracking().First(v => v.Id == IdSoom).IdShip;
                    var _now = DateTime.Now;
                    if (x.PackageToShip.AsNoTracking().Any(v => v.IdShip == IdShip && v.ExpireDate >= _now) == false)
                    {
                        return -11;
                    }

                    var _year = PersianCalander.ToShamsi(yaer).Substring(0, 4);
                    var pricebime = 0;
                    if (IsBimeh)
                    {
                        pricebime = x.Bimeh.AsNoTracking().First(v => v.Year == _year).Price;
                    }



                    if (x.SafarToAfrad.AsNoTracking().Count(b => b.IdSafar == IdSafar && b.IdSemat == 1 && b.Safar.IdSoom == IdSoom) > 1)
                    {
                        return -2;
                    }
                    if (x.SafarToAfrad.AsNoTracking().Count(b => b.IdSafar == IdSafar && b.IdSemat == 8 && b.Safar.IdSoom == IdSoom) > 1)
                    {
                        return -3;
                    }


                    if (x.SafarToAfrad.AsNoTracking().Any(b => b.IdPernonnel == IdAfrad && b.IdSafar == IdSafar
                        && b.Safar.IdSoom == IdSoom && b.Safar.IdPersonnel == IdUser) == false)
                    {
                        x.SafarToAfrad.Add(new SafarToAfrad()
                        {
                            IdPernonnel = IdAfrad,
                            IdSafar = IdSafar,
                            IdSemat = IdSemat,
                            Ratio = Rate,
                            IsBimeh = IsBimeh,
                            PriceBimeh = pricebime
                        });
                    }
                    else
                    {
                        return -1;
                    }

                    x.SaveChanges();
                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public static List<GetListSafarViewModel> GetListSafar3(int IdShip)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var IdsSoom = x.Soom.AsNoTracking().Where(b => b.IdShip == IdShip && b.IsActive).Select(b => b.Id).ToList();

                    var tttt = x.Safar.AsNoTracking().Where(v => IdsSoom.Contains(v.IdSoom))
                        .Select(v => new GetListSafarViewModel()
                        {
                            ToDate2 = v.ToDate,
                            Tozihat = v.Tozihat,
                            FromDate2 = v.FromDate,
                            Id = v.Id,
                            IdSoom = v.IdSoom,
                            IsActive = v.IsActive,
                            FactorForosh = v.PriceForosh,
                            FactorKharid = v.PriceKharid,
                            FactorMahtaiaj = v.PriceMahtaiaj,
                            FactorTamirat = v.PriceTamirat,
                            FactorMosaede = v.FactorMosaede,
                            Title = v.Title,
                            IdShip = v.Soom.IdShip,
                            PriceFood = v.PriceFood,
                            PriceTamiratGolate = v.TamiratGolate,
                            IsActiveSoom = v.Soom.IsActive,
                            NamesOOM = v.Soom.TiTle
                        }).OrderByDescending(v => v.Id).ToList();

                    foreach (var c in tttt)
                    {
                        if (c.FromDate2 != null)
                        {
                            c.FromDate = PersianCalander.ToShamsi(c.FromDate2.Value);
                        }
                        if (c.ToDate2 != null)
                        {
                            c.ToDate = PersianCalander.ToShamsi(c.ToDate2.Value);
                        }
                    }

                    return tttt;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<GetListSafarViewModel>();
            }
        }

        public static List<GetListSafarViewModel> GetListSafar2(int IdSoom)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tttt = x.Safar.AsNoTracking().Where(v => v.IdSoom == IdSoom)
                        .Select(v => new GetListSafarViewModel()
                        {
                            ToDate2 = v.ToDate,
                            Tozihat = v.Tozihat,
                            FromDate2 = v.FromDate,
                            Id = v.Id,
                            IdSoom = v.IdSoom,
                            IsActive = v.IsActive,
                            FactorForosh = v.PriceForosh,
                            FactorKharid = v.PriceKharid,
                            FactorMahtaiaj = v.PriceMahtaiaj,
                            FactorTamirat = v.PriceTamirat,
                            FactorMosaede = v.FactorMosaede,
                            Title = v.Title,
                            IdShip = v.Soom.IdShip,
                            PriceFood = v.PriceFood,
                            PriceTamiratGolate = v.TamiratGolate,
                            IsActiveSoom = v.Soom.IsActive
                        }).OrderByDescending(v => v.Id).ToList();

                    foreach (var c in tttt)
                    {
                        if (c.FromDate2 != null)
                        {
                            c.FromDate = PersianCalander.ToShamsi(c.FromDate2.Value);
                        }
                        if (c.ToDate2 != null)
                        {
                            c.ToDate = PersianCalander.ToShamsi(c.ToDate2.Value);
                        }
                    }

                    return tttt;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<GetListSafarViewModel>();
            }
        }
        public static List<GetListSafarViewModel> GetListSafar(int IdUser, int IdSoom)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tttt = x.Safar.AsNoTracking().Where(v => v.IdSoom == IdSoom && v.IdPersonnel == IdUser)
                        .Select(v => new GetListSafarViewModel()
                        {
                            ToDate2 = v.ToDate,
                            Tozihat = v.Tozihat,
                            FromDate2 = v.FromDate,
                            Id = v.Id,
                            IdSoom = v.IdSoom,
                            IsActive = v.IsActive,
                            FactorForosh = v.PriceForosh,
                            FactorKharid = v.PriceKharid,
                            FactorMahtaiaj = v.PriceMahtaiaj,
                            FactorTamirat = v.PriceTamirat,
                            FactorMosaede = v.FactorMosaede,
                            Title = v.Title,
                            IdShip = v.Soom.IdShip,
                            PriceFood = v.PriceFood,
                            PriceTamiratGolate = v.TamiratGolate,
                            IsActiveSoom = v.Soom.IsActive
                        }).OrderByDescending(v => v.Id).ToList();

                    foreach (var c in tttt)
                    {
                        if (c.FromDate2 != null)
                        {
                            c.FromDate = PersianCalander.ToShamsi(c.FromDate2.Value);
                        }
                        if (c.ToDate2 != null)
                        {
                            c.ToDate = PersianCalander.ToShamsi(c.ToDate2.Value);
                        }
                    }

                    return tttt;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<GetListSafarViewModel>();
            }
        }
        public static GetInfohHeaderSafarViewModel GetInfohHeaderSafar(int IdUser, int IdShoom)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itmsoom = x.Soom.AsNoTracking().First(v => v.IdPersonnel == IdUser && v.Id == IdShoom);
                    var itmship = x.Ship.AsNoTracking().First(v => v.IdPersonnel == IdUser && v.Id == itmsoom.IdShip);

                    string Soom = "";
                    if (itmsoom.FromDate != null)
                    {
                        Soom = PersianCalander.ToShamsi(itmsoom.FromDate);
                    }
                    if (itmsoom.ToDate != null)
                    {
                        Soom += " - " + PersianCalander.ToShamsi(itmsoom.ToDate);
                    }

                    return new GetInfohHeaderSafarViewModel() { IdShip = itmship.Id.ToString(), IdSoom = itmsoom.Id.ToString(), Ship = itmship.Title, Soom = Soom };
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new GetInfohHeaderSafarViewModel();
            }
        }

        public static GetInfohHeaderSafarViewModel GetInfohHeaderSafar2(int IdUser, int IdShoom)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var pp = x.TankhaGardanToShip.Where(b => b.IdTankhaGardan == IdUser).Select(v => v.IdShip).ToList();
                    x.Soom.First(b => pp.Contains(b.IdShip) && b.Id == IdShoom);


                    var itmsoom = x.Soom.AsNoTracking().First(v => v.Id == IdShoom);
                    var itmship = x.Ship.AsNoTracking().First(v => v.Id == itmsoom.IdShip);

                    string Soom = "";
                    if (itmsoom.FromDate != null)
                    {
                        Soom = PersianCalander.ToShamsi(itmsoom.FromDate);
                    }
                    if (itmsoom.ToDate != null)
                    {
                        Soom += " - " + PersianCalander.ToShamsi(itmsoom.ToDate);
                    }

                    return new GetInfohHeaderSafarViewModel() { IdShip = itmship.Id.ToString(), IdSoom = itmsoom.Id.ToString(), Ship = itmship.Title, Soom = Soom };
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new GetInfohHeaderSafarViewModel();
            }
        }


        public static bool CheckSafae2(string _f, string _t, int IdSoom, long IdSafar)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    return x.Database.SqlQuery<bool>("EXEC dbo.CheckSafae2 @FromDate = '" + _f + "',@ToDate = '" + _t + "',@IdSoom = " + IdSoom + ",@IdSafar = " + IdSafar).First();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool CheckSafae3(string _f, string _t, int IdSoom, long IdSafar)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    return x.Database.SqlQuery<bool>("EXEC dbo.CheckSafae3 @FromDate = '" + _f + "',@ToDate = '" + _t + "',@IdSoom = " + IdSoom + ",@IdSafar = " + IdSafar).First();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool CheckSafae4(string _f, string _t, int IdSoom, long IdSafar)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    return x.Database.SqlQuery<bool>("EXEC dbo.CheckSafae4 @FromDate = '" + _f + "',@ToDate = '" + _t + "',@IdSoom = " + IdSoom + ",@IdSafar = " + IdSafar).First();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool CheckSafae(string _f, string _t, int IdSoom, long IdSafar)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    return x.Database.SqlQuery<bool>("EXEC dbo.CheckSafae @FromDate = '" + _f + "',@ToDate = '" + _t + "',@IdSoom = " + IdSoom + ",@IdSafar = " + IdSafar).First();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool CheckSoom(string _f, string _t, int IdSip, int IdSoom, long IdUser)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    return x.Database.SqlQuery<bool>("EXEC dbo.CheckSoom @FromDate = '" + _f + "',@ToDate = '" + _t + "',@IdSip = " + IdSip + ",@IdSoom = " + IdSoom + ",@IdUser = " + IdUser).First();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static int AddSaveSafar(int IdUser, int IdSoom, string FromDate, string ToDate, string Tozihat, string Title, int PriceFood, decimal TamiratGolate)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.Soom.First(b => b.Id == IdSoom && b.IdPersonnel == IdUser && b.IsActive);

                    DateTime _f = PersianCalander.ToMiladi(FromDate.ToEnNum());
                    DateTime _t = PersianCalander.ToMiladi(ToDate.ToEnNum());

                    if (_t < _f)
                    {
                        return -2;
                    }

                    FromDate = FromDate.ToEnNum();
                    ToDate = ToDate.ToEnNum();

                    var _now = DateTime.Now;
                    if (x.PackageToShip.AsNoTracking().Any(v => v.IdShip == itm.IdShip && v.ExpireDate >= _now) == false)
                    {
                        return -11;
                    }
                    if (CheckSafae2(FromDate, ToDate, IdSoom, 0))
                    {
                        // بازه صوم
                        return -5;
                    }
                    if (CheckSafae3(FromDate, ToDate, IdSoom, 0))
                    {
                        // تداخل سفر
                        return -6;
                    }

                    itm.Safar.Add(new Safar()
                    {
                        CreateDate = DateTime.Now,
                        FromDate = _f,
                        ToDate = _t,
                        IdPersonnel = IdUser,
                        IsActive = true,
                        Title = Title,
                        PriceTamirat = 0,
                        PriceForosh = 0,
                        PriceKharid = 0,
                        PriceMahtaiaj = 0,
                        FactorMosaede = 0,
                        Tozihat = Tozihat,
                        PriceFood = PriceFood,
                        TamiratGolate = TamiratGolate,
                        IsCheckout = false
                    });

                    x.SaveChanges();

                    int a = 1;
                    var sfr = x.Soom.First(v => v.Id == IdSoom);
                    foreach (var cc in sfr.Safar.OrderBy(v => v.FromDate).ToList())
                    {
                        cc.SafarCount = a;
                        a = a + 1;
                    }

                    x.Soom.First(v => v.Id == IdSoom).SafarCounter = sfr.Safar.Count;
                    x.SaveChanges();


                    return 1;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return -3;
            }
        }
        public static int EditSaveSafarMaster(int IdUser, int IdSoom, long Id, string FromDate, string ToDate, string Tozihat, string Title, int PriceFood, decimal TamiratGolate)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.Safar.First(b => b.IdSoom == IdSoom && b.IdPersonnel == IdUser && b.IsActive && b.Id == Id);
                    FromDate = FromDate.ToEnNum();
                    ToDate = ToDate.ToEnNum();

                    if (CheckSafae4(FromDate, ToDate, (int)itm.IdSoom, Id))
                    {
                        return -6;
                    }

                    itm.FromDate = PersianCalander.ToMiladi(FromDate.ToEnNum());
                    itm.ToDate = PersianCalander.ToMiladi(ToDate.ToEnNum());
                    itm.Title = Title;
                    itm.Tozihat = Tozihat;
                    itm.PriceFood = PriceFood;
                    itm.TamiratGolate = TamiratGolate;

                    if (itm.SafarToAfrad.Any(v => v.IdPernonnel == 78))
                    {
                        itm.SafarToAfrad.First(v => v.IdPernonnel == 78).Ratio = itm.TamiratGolate;
                    }

                    if (itm.ToDate < itm.FromDate)
                    {
                        return -1;
                    }

                    int a = 1;
                    var sfr = x.Soom.First(v => v.Id == IdSoom);
                    foreach (var cc in sfr.Safar.OrderBy(v => v.FromDate).ToList())
                    {
                        cc.SafarCount = a;
                        a = a + 1;
                    }


                    if (itm.IsOne == null)
                    {
                        itm.IsOne = true;
                        long[] ids = new long[] { 13, 16, 18 };
                        foreach (var item in itm.SafarToFactor.Where(v => ids.Contains(v.IdTypeFactor)).ToList())
                        {
                            item.CreateDate = itm.FromDate;
                        }
                    }

                    x.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return -2;
            }
        }
        public static long GetPricePackage()
        {
            using (var x = new GolatehEntities())
            {
                if (x.Package.AsNoTracking().Any(c => c.IsActive))
                {
                    return x.Package.AsNoTracking().First(c => c.IsActive).Price;
                }
                else
                {
                    return 0;
                }
            }
        }



        public static GetDeteilSafarViewModel GetDeteilSafar(int IdUser, int IdSoom, long Id)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.Safar.First(b => b.IdSoom == IdSoom && b.IdPersonnel == IdUser && b.IsActive && b.Id == Id);




                    return null;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return null;
            }
        }
        public static object HandMoneyToSafar(int IdSoom, int IdSafar, long IdAfrad, int? IdKala, decimal? Weight, string Tozihat, long? Price, string CreateDateMoney, int? IdUnit, long IdNote)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    if (IdKala == null && IdNote > 0)
                    {
                        var lst = x.DebtHistory.AsNoTracking().Where(v => v.IdNote == IdNote).ToList();
                        decimal max = lst.Sum(v => v.Price) + Price.Value * 10;

                        if (max > x.Note.AsNoTracking().First(v => v.Id == IdNote).Price)
                        {
                            return -22;
                        }
                    }





                    var t = x.Safar.First(v => v.Id == IdSafar && v.IdSoom == IdSoom);
                    var IdShip = x.Soom.AsNoTracking().First(v => v.Id == IdSoom).IdShip;
                    var _now = DateTime.Now;
                    if (x.PackageToShip.AsNoTracking().Any(v => v.IdShip == IdShip && v.ExpireDate >= _now) == false)
                    {
                        return -11;
                    }

                    long? PriceIdKala = null;
                    long? Total_Price = null;

                    if (IdKala != null)
                    {
                        var _p = t.SafarToFactor.FirstOrDefault(b => b.IdTypeFactor == 14 && b.IdKala == IdKala);
                        if (_p != null)
                        {
                            if (Convert.ToInt64(_p.Price) == 0)
                            {
                                PriceIdKala = null;
                            }
                            else
                            {
                                PriceIdKala = _p.Price;
                                Total_Price = IdUnit == 6 ? (Convert.ToInt64((Weight * 1000) * _p.Price)) : Convert.ToInt64(Weight * _p.Price);
                            }
                        }
                    }
                    else
                    {
                        PriceIdKala = Price;
                        Total_Price = Price;
                    }


                    var rtrrr = new SafarToHandMoney()
                    {
                        CreateDate = DateTime.Now,
                        CreateDateMoney = PersianCalander.ToMiladi(CreateDateMoney.ToEnNum()),
                        IdKala = IdKala,
                        IdPersonnel = IdAfrad,
                        IdSafar = IdSafar,
                        Price = PriceIdKala,
                        Tozihat = Tozihat,
                        Weight = Weight,
                        TotalPrice = Total_Price,
                        IdUnit = IdUnit
                    };
                    t.SafarToHandMoney.Add(rtrrr);

                    t.FactorMosaede = t.SafarToHandMoney.Where(v => v.TotalPrice != null).Sum(v => v.TotalPrice.Value);
                    x.SaveChanges();

                    if (IdKala == null && IdNote > 0)
                    {
                        x.Note.First(c => c.Id == IdNote && c.IdShip == IdShip);
                        var _add = new DebtHistory()
                        {
                            CreateDate = DateTime.Now,
                            IdShip = IdShip,
                            IdPersonnel = IdAfrad,
                            Description = "مبلغ " + (Total_Price.Value).ToString("N0") + " تومان در صوم " + t.Soom.TiTle + " و سفر " + t.Title,
                            Price = Total_Price.Value * 10,
                            IdNote = IdNote,
                            IsActive = false
                        };
                        x.DebtHistory.Add(_add);

                        x.SaveChanges();
                        rtrrr.IdDebtHistory = _add.Id;
                        x.SaveChanges();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }
        public static bool DeleteSafarAfradRelation(int IdSafar, int IdSoom, long Id)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var t = x.SafarToAfrad.First(v => v.IdSafar == IdSafar && v.Safar.IdSoom == IdSoom && v.Id == Id);
                    x.SafarToAfrad.Remove(t);

                    x.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }

        public static object AddFactorForosh(int IdUser, int IdSoom, int IdSafar, int IdKala, decimal Weight, string Tozihat, long Price, string CreateDate, int Idunit)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var t = x.Safar.First(v => v.Id == IdSafar && v.IdSoom == IdSoom);

                    var tsom = x.Soom.First(v => v.Id == t.IdSoom);
                    var _now = DateTime.Now;
                    if (x.PackageToShip.AsNoTracking().Any(v => v.IdShip == tsom.IdShip && v.ExpireDate >= _now) == false)
                    {
                        return -11;
                    }

                    t.SafarToFactor.Add(new SafarToFactor()
                    {
                        IdTypeFactor = 14,
                        Price = Price,
                        CreateDate = DateTime.Now,
                        IdKala = IdKala,
                        DateFactor = PersianCalander.ToMiladi(CreateDate.ToEnNum()),
                        Tozihat = Tozihat,
                        Weight = 0,
                        Tedad = Weight,
                        IdSafar = t.Id,
                        TotalPrice = Idunit == 6 ? (Convert.ToInt64((Weight * 1000) * Price)) : Convert.ToInt64(Weight * Price),
                        IdUnit = Idunit
                    });

                    t.PriceForosh = t.SafarToFactor.Where(b => b.IdTypeFactor == 14).Sum(v => v.TotalPrice);

                    foreach (var cc in t.SafarToHandMoney.Where(v => v.IdKala == IdKala).ToList())
                    {
                        cc.Price = Price;
                        cc.TotalPrice = Price * Convert.ToInt64(cc.Weight);
                    }

                    t.FactorMosaede = t.SafarToHandMoney.Where(v => v.TotalPrice != null).Sum(v => v.TotalPrice.Value);

                    x.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }
        public static object AddFactorForoshTamirat(int IdUser, int IdSoom, int IdSafar, string IdKala, decimal? Weight, string Tozihat, long Price, string CreateDate, decimal? Tedad)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var t = x.Safar.First(v => v.Id == IdSafar && v.IdSoom == IdSoom);


                    var tsom = x.Soom.First(v => v.Id == t.IdSoom);
                    var _now = DateTime.Now;
                    if (x.PackageToShip.AsNoTracking().Any(v => v.IdShip == tsom.IdShip && v.ExpireDate >= _now) == false)
                    {
                        return -11;
                    }




                    int _idKala = 0;

                    IdKala = IdKala.Trim().ToEnNum();

                    if (x.Kala.AsNoTracking().Any(v => v.Title == IdKala))
                    {
                        _idKala = x.Kala.AsNoTracking().First(v => v.Title == IdKala).Id;
                    }
                    else
                    {
                        var max = x.Kala.AsNoTracking().AsEnumerable().Max(v => Convert.ToInt32(v.Sort)) + 1;

                        var _add = new Kala() { CreateDate = DateTime.Now, IdPersonnel = IdUser, IdTypeKala = 11, IdUnit = 1, IsActive = false, Title = IdKala, Sort = max };
                        x.Kala.Add(_add);
                        x.SaveChanges();

                        _idKala = _add.Id;
                    }


                    t.SafarToFactor.Add(new SafarToFactor()
                    {
                        IdTypeFactor = 16,
                        Price = Price,
                        Tedad = Tedad,
                        CreateDate = DateTime.Now,
                        IdKala = _idKala,
                        DateFactor = PersianCalander.ToMiladi(CreateDate.ToEnNum()),
                        Tozihat = Tozihat,
                        Weight = Weight,
                        IdSafar = t.Id,
                        TotalPrice = Convert.ToInt64(Tedad * Price)
                    });

                    t.PriceTamirat = t.SafarToFactor.Where(b => b.IdTypeFactor == 16).Sum(v => v.TotalPrice);

                    x.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }
        public static object AddFactorForoshMahtaiaj(int IdUser, int IdSoom, int IdSafar, int IdKala, decimal? Weight, string Tozihat, long Price, string CreateDate, decimal? Tedad, int Idunit)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var t = x.Safar.First(v => v.Id == IdSafar && v.IdSoom == IdSoom);


                    var tsom = x.Soom.First(v => v.Id == t.IdSoom);
                    var _now = DateTime.Now;
                    if (x.PackageToShip.AsNoTracking().Any(v => v.IdShip == tsom.IdShip && v.ExpireDate >= _now) == false)
                    {
                        return -11;
                    }


                    t.SafarToFactor.Add(new SafarToFactor()
                    {
                        IdTypeFactor = 18,
                        Price = Price,
                        Tedad = Tedad,
                        CreateDate = DateTime.Now,
                        IdKala = IdKala,
                        DateFactor = PersianCalander.ToMiladi(CreateDate.ToEnNum()),
                        Tozihat = Tozihat,
                        Weight = 0,
                        IdSafar = t.Id,
                        TotalPrice = Convert.ToInt64(Tedad * Price),
                        IdUnit = Idunit
                    });

                    t.PriceMahtaiaj = t.SafarToFactor.Where(b => b.IdTypeFactor == 18).Sum(v => v.TotalPrice);

                    x.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }
        public static object AddFactorForoshKharid(int IdUser, int IdSoom, int IdSafar, int IdKala, decimal? Weight, string Tozihat, long Price, string CreateDate, decimal? Tedad, int Idunit)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var t = x.Safar.First(v => v.Id == IdSafar && v.IdSoom == IdSoom);

                    var tsom = x.Soom.First(v => v.Id == t.IdSoom);
                    var _now = DateTime.Now;
                    if (x.PackageToShip.AsNoTracking().Any(v => v.IdShip == tsom.IdShip && v.ExpireDate >= _now) == false)
                    {
                        return -11;
                    }


                    t.SafarToFactor.Add(new SafarToFactor()
                    {
                        IdTypeFactor = 13,
                        Price = Price,
                        Tedad = Tedad,
                        CreateDate = DateTime.Now,
                        IdKala = IdKala,
                        DateFactor = PersianCalander.ToMiladi(CreateDate.ToEnNum()),
                        Tozihat = Tozihat,
                        Weight = 0,
                        IdSafar = t.Id,
                        TotalPrice = Convert.ToInt64(Price * Tedad),
                        IdUnit = Idunit
                    });

                    t.PriceKharid = t.SafarToFactor.Where(b => b.IdTypeFactor == 13).Sum(v => v.TotalPrice);

                    x.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }

        public static bool DeleteSafarToHandMoney(int IdSafar, int IdSoom, long Id)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var t = x.Safar.First(v => v.Id == IdSafar && v.IdSoom == IdSoom);
                    var sth = t.SafarToHandMoney.First(v => v.Id == Id);


                    x.DebtHistory.Remove(x.DebtHistory.First(d => d.Id == sth.IdDebtHistory));
                    x.SafarToHandMoney.Remove(sth);

                    t.FactorMosaede = t.SafarToHandMoney.Where(v => v.TotalPrice != null).Sum(v => v.TotalPrice.Value);

                    x.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }


        public static bool CheckerSoomOpen(int IdSoom)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var t = x.Soom.AsNoTracking().First(v => v.Id == IdSoom);

                    return t.IsActive;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }


        public static bool DeleteFactor2(int IdUser, int IdSafar, int IdSoom, long Id, int IdType)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var t = x.Safar.First(v => v.Id == IdSafar && v.IdSoom == IdSoom);

                    int idKala = t.SafarToFactor.First(b => b.IdTypeFactor == IdType && b.Id == Id && b.IdTankhaGardan == IdUser).IdKala;

                    x.SafarToFactor.Remove(t.SafarToFactor.First(b => b.IdTypeFactor == IdType && b.Id == Id && b.IdTankhaGardan == IdUser));

                    if (IdType == 14)
                    {
                        t.PriceForosh = t.SafarToFactor.Where(b => b.IdTypeFactor == 14).Sum(v => v.TotalPrice);

                        foreach (var ii in t.SafarToHandMoney.Where(v => v.IdKala == idKala))
                        {
                            ii.Price = null;
                            ii.TotalPrice = null;
                        }

                        t.FactorMosaede = t.SafarToHandMoney.Where(v => v.TotalPrice != null).Sum(v => v.TotalPrice.Value);
                    }

                    if (IdType == 16)
                    {
                        t.PriceTamirat = t.SafarToFactor.Where(v => v.IdTypeFactor == 16).Sum(v => v.TotalPrice);
                    }
                    if (IdType == 13)
                    {
                        t.PriceKharid = t.SafarToFactor.Where(v => v.IdTypeFactor == 13).Sum(v => v.TotalPrice);
                    }
                    if (IdType == 18)
                    {
                        t.PriceMahtaiaj = t.SafarToFactor.Where(v => v.IdTypeFactor == 18).Sum(v => v.TotalPrice);
                    }
                    x.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }


        public static bool DeleteFactor(int IdUser, int IdSafar, int IdSoom, long Id, int IdType)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var t = x.Safar.First(v => v.Id == IdSafar && v.IdSoom == IdSoom);

                    int idKala = t.SafarToFactor.First(b => b.IdTypeFactor == IdType && b.Id == Id).IdKala;

                    x.SafarToFactor.Remove(t.SafarToFactor.First(b => b.IdTypeFactor == IdType && b.Id == Id));

                    if (IdType == 14)
                    {
                        t.PriceForosh = t.SafarToFactor.Where(b => b.IdTypeFactor == 14).Sum(v => v.TotalPrice);

                        foreach (var ii in t.SafarToHandMoney.Where(v => v.IdKala == idKala))
                        {
                            ii.Price = null;
                            ii.TotalPrice = null;
                        }

                        t.FactorMosaede = t.SafarToHandMoney.Where(v => v.TotalPrice != null).Sum(v => v.TotalPrice.Value);
                    }

                    if (IdType == 16)
                    {
                        t.PriceTamirat = t.SafarToFactor.Where(v => v.IdTypeFactor == 16).Sum(v => v.TotalPrice);
                    }
                    if (IdType == 13)
                    {
                        t.PriceKharid = t.SafarToFactor.Where(v => v.IdTypeFactor == 13).Sum(v => v.TotalPrice);
                    }
                    if (IdType == 18)
                    {
                        t.PriceMahtaiaj = t.SafarToFactor.Where(v => v.IdTypeFactor == 18).Sum(v => v.TotalPrice);
                    }
                    x.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }
        public static List<GetListSafarToHandMoneyViewModel> GetListSafarToHandMoney(int IdSafar, int IdSoom)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tt = x.Safar.AsNoTracking().First(v => v.Id == IdSafar && v.IdSoom == IdSoom);

                    return tt.SafarToHandMoney.Select(b => new GetListSafarToHandMoneyViewModel()
                    {
                        FullNameAfrad = b.Personnel.FirstName + " " + b.Personnel.LastName,
                        Id = b.Id,
                        Weight = b.Weight,
                        Tozihat = b.Tozihat,
                        KalaName = (b.IdKala != null ? b.Kala.Title : ""),
                        Unit = (b.IdUnit != null ? b.Unit.Title : ""),
                        Price = b.Price,
                        CreateDateMoney = PersianCalander.ToShamsi(b.CreateDateMoney),
                        TotalPrice = b.TotalPrice,
                        TozihatNote = b.IdDebtHistory == null ? "" : b.DebtHistory.Description,
                        GropNote = b.IdDebtHistory == null ? "" : b.DebtHistory.Note.Description
                    }).OrderBy(v => v.FullNameAfrad).ThenBy(v => v.CreateDateMoney).ToList();
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<GetListSafarToHandMoneyViewModel>();
            }
        }

        public static List<GetListSafarToHandMoneyViewModel> GetListSafarToHandMoney3(int IdSafar)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tt = x.Safar.AsNoTracking().First(v => v.Id == IdSafar);

                    return tt.SafarToHandMoney.Select(b => new GetListSafarToHandMoneyViewModel()
                    {
                        FullNameAfrad = b.Personnel.FirstName + " " + b.Personnel.LastName,
                        Id = b.Id,
                        Weight = b.Weight,
                        Tozihat = b.Tozihat,
                        KalaName = (b.IdKala != null ? b.Kala.Title : ""),
                        Unit = (b.IdUnit != null ? b.Unit.Title : ""),
                        Price = b.Price,
                        CreateDateMoney = PersianCalander.ToShamsi(b.CreateDateMoney),
                        TotalPrice = b.TotalPrice,
                    }).OrderBy(v => v.FullNameAfrad).ThenBy(v => v.CreateDateMoney).ToList();
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<GetListSafarToHandMoneyViewModel>();
            }
        }
        public static List<GetListSafarToHandMoneyViewModel> GetListSafarToHandMoney2(int IdSafar, int IdUser)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tt = x.Safar.AsNoTracking().First(v => v.Id == IdSafar && v.Soom.IdPersonnel == IdUser);

                    return tt.SafarToHandMoney.Select(b => new GetListSafarToHandMoneyViewModel()
                    {
                        FullNameAfrad = b.Personnel.FirstName + " " + b.Personnel.LastName,
                        Id = b.Id,
                        Weight = b.Weight,
                        Tozihat = b.Tozihat,
                        KalaName = (b.IdKala != null ? b.Kala.Title : ""),
                        Unit = (b.IdUnit != null ? b.Unit.Title : ""),
                        Price = b.Price,
                        CreateDateMoney = PersianCalander.ToShamsi(b.CreateDateMoney),
                        TotalPrice = b.TotalPrice,
                    }).OrderBy(v => v.FullNameAfrad).ThenBy(v => v.CreateDateMoney).ToList();
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<GetListSafarToHandMoneyViewModel>();
            }
        }

        public static List<FactorListViewModel> GetListFactor2(int IdSafar)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tt = x.Safar.AsNoTracking().First(v => v.Id == IdSafar);

                    return tt.SafarToFactor
                        .Select(b => new FactorListViewModel()
                        {
                            Id = b.Id,
                            Weight = b.Weight,
                            Tozihat = b.Tozihat,
                            KalaName = b.Kala.Title,
                            Price = b.Price,
                            Moshtari = b.Moshtari,
                            ShomareFactor = b.ShomareFactor,
                            TaminKhonanade = b.TaminKhonanade,
                            Tedad = b.Tedad,
                            DateFactor = (b.DateFactor != null ? PersianCalander.ToShamsi(b.DateFactor.Value) : ""),
                            CreateDate = PersianCalander.ToShamsiTime(b.CreateDate),
                            TotoalPrice = b.TotalPrice,
                            Unitname = (b.IdUnit != null ? b.Unit.Title : ""),
                            TypeFactor = b.OptionParameter.Title,
                            IdTypeFactor = b.OptionParameter.Id
                        }).OrderBy(v => v.Id).ToList();
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<FactorListViewModel>();
            }
        }



        public static List<FactorListViewModel> GetListFactor2(int IdSafar, int IdSoom, int IdType, long IdUser)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tt = x.Safar.AsNoTracking().First(v => v.Id == IdSafar && v.IdSoom == IdSoom);

                    return tt.SafarToFactor.Where(v => v.IdTypeFactor == IdType && v.IdTankhaGardan == IdUser)
                        .Select(b => new FactorListViewModel()
                        {
                            Id = b.Id,
                            Weight = b.Weight,
                            Tozihat = b.Tozihat,
                            KalaName = b.Kala.Title,
                            Price = b.Price,
                            Moshtari = b.Moshtari,
                            ShomareFactor = b.ShomareFactor,
                            TaminKhonanade = b.TaminKhonanade,
                            Tedad = b.Tedad,
                            DateFactor = (b.DateFactor != null ? PersianCalander.ToShamsi(b.DateFactor.Value) : ""),
                            CreateDate = PersianCalander.ToShamsiTime(b.CreateDate),
                            TotoalPrice = b.TotalPrice,
                            Unitname = (b.IdUnit != null ? b.Unit.Title : ""),
                            UserSabtKonanade = (b.IdTankhaGardan != null ? b.TankhaGardan.FullName : b.Safar.Personnel.FirstName + " " + b.Safar.Personnel.LastName)
                        }).OrderBy(v => v.Id).ToList();
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<FactorListViewModel>();
            }
        }


        public static List<FactorListViewModel> GetListFactor(int IdSafar, int IdSoom, int IdType)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tt = x.Safar.AsNoTracking().First(v => v.Id == IdSafar && v.IdSoom == IdSoom);

                    return tt.SafarToFactor.Where(v => v.IdTypeFactor == IdType)
                        .Select(b => new FactorListViewModel()
                        {
                            Id = b.Id,
                            Weight = b.Weight,
                            Tozihat = b.Tozihat,
                            KalaName = b.Kala.Title,
                            Price = b.Price,
                            Moshtari = b.Moshtari,
                            ShomareFactor = b.ShomareFactor,
                            TaminKhonanade = b.TaminKhonanade,
                            Tedad = b.Tedad,
                            DateFactor = (b.DateFactor != null ? PersianCalander.ToShamsi(b.DateFactor.Value) : ""),
                            CreateDate = PersianCalander.ToShamsiTime(b.CreateDate),
                            TotoalPrice = b.TotalPrice,
                            Unitname = (b.IdUnit != null ? b.Unit.Title : ""),
                            UserSabtKonanade = (b.IdTankhaGardan != null ? b.TankhaGardan.FullName : b.Safar.Personnel.FirstName + " " + b.Safar.Personnel.LastName)
                        }).OrderBy(v => v.Id).ToList();
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<FactorListViewModel>();
            }
        }
        public static List<SafarAfradRelationViewModel> GetListAfradRelation(int IdSafar, int IdSoom)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    return x.SafarToAfrad.AsNoTracking().Where(v => v.IdSafar == IdSafar && v.Safar.IdSoom == IdSoom).Select(b => new SafarAfradRelationViewModel()
                    {
                        FirstName = b.Personnel.FirstName,
                        Id = b.Id,
                        LastName = b.Personnel.LastName,
                        Rate = b.Ratio,
                        SematName = b.Semat.Title,
                        TotalPrice = b.TotalPrice,
                        IdPersonnel = b.IdPernonnel,
                        IsBimeh = b.IsBimeh
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<SafarAfradRelationViewModel>();
            }
        }

        public static List<SafarAfradRelationViewModel> GetListAfradRelation2(int IdSafar, int IdUser)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var data = x.SafarToAfrad.AsNoTracking().AsEnumerable().Where(v => v.IdSafar == IdSafar && v.Safar.Soom.IdPersonnel == IdUser).Select(b => new SafarAfradRelationViewModel()
                    {
                        FirstName = b.Personnel.FirstName,
                        Id = b.Id,
                        LastName = b.Personnel.LastName,
                        Rate = b.Ratio,
                        SematName = b.Semat.Title,
                        TotalPrice = b.TotalPrice,
                        Bime = b.PriceBimeh,
                        Mosaede = 0,
                        IsBimeh = b.IsBimeh,
                        IdPersonnel = b.IdPernonnel,
                        khales = 0,
                        Nakhales = b.Nakhales,
                        KasriTabakhi = b.KasriTabakhi
                    }).ToList();

                    foreach (var item in data)
                    {
                        item.Mosaede = (long)x.SafarToHandMoney.AsEnumerable().Where(p => p.IdSafar == IdSafar && p.IdPersonnel == item.IdPersonnel).Sum(v => Convert.ToDecimal(v.TotalPrice));
                    }
                    foreach (var item in data)
                    {
                        item.khales = (long)Convert.ToDecimal(item.TotalPrice) - (item.Mosaede + item.Bime);
                    }

                    return data;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<SafarAfradRelationViewModel>();
            }
        }

        public static decimal CopySafar(int IdUser, int IdSafar, int IdSoom)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.Safar.AsNoTracking().First(b => b.Id == IdSafar && b.IdPersonnel == IdUser && b.IdSoom == IdSoom);

                    var maxdate = x.Safar.Where(v => v.IdSoom == IdSoom).Max(v => v.ToDate).AddDays(1);

                    itm.Title = "کپی " + itm.Title;

                    var _add = new Safar
                    {
                        IdPersonnel = itm.IdPersonnel,
                        IdSoom = itm.IdSoom,
                        FromDate = maxdate,
                        ToDate = maxdate,
                        PriceKharid = itm.PriceKharid,
                        PriceMahtaiaj = itm.PriceMahtaiaj,
                        PriceForosh = 0,
                        PriceTamirat = itm.TamiratGolate,
                        FactorMosaede = 0,
                        Tozihat = itm.Tozihat,
                        CreateDate = DateTime.Now,
                        IsActive = true,
                        Title = itm.Title,
                        PriceFood = itm.PriceFood,
                        TamiratGolate = itm.TamiratGolate,
                        GolateBase = 0,
                        GolateTedadKol = itm.GolateTedadKol,
                        SafarCount = itm.SafarCount,
                        JamKolSoodAzSafar = 0,
                        IsOne = null
                    };

                    foreach (var item in itm.SafarToAfrad.Where(b => b.IdPernonnel != 78).ToList())
                    {
                        _add.SafarToAfrad.Add(new SafarToAfrad()
                        {
                            IdPernonnel = item.IdPernonnel,
                            IdSemat = item.IdSemat,
                            Ratio = item.Ratio,
                            TotalPrice = 0,
                            IsBimeh = item.IsBimeh,
                            PriceBimeh = item.PriceBimeh
                        });
                    }

                    foreach (var item in itm.SafarToFactor.Where(v => v.IdTypeFactor != 14 && v.IdKala != 1151).ToList())
                    {
                        _add.SafarToFactor.Add(new SafarToFactor()
                        {
                            IdKala = item.IdKala,
                            IdTypeFactor = item.IdTypeFactor,
                            Tedad = item.Tedad,
                            Price = item.Price,
                            CreateDate = maxdate,
                            ShomareFactor = item.ShomareFactor,
                            DateFactor = item.DateFactor,
                            TaminKhonanade = item.TaminKhonanade,
                            Moshtari = item.Moshtari,
                            Tozihat = item.Tozihat,
                            Weight = item.Weight,
                            TotalPrice = item.TotalPrice,
                            IdUnit = item.IdUnit
                        });
                    }

                    x.Safar.Add(_add);
                    x.SaveChanges();

                    int a = 1;
                    var sfr = x.Soom.First(v => v.Id == IdSoom);
                    foreach (var cc in sfr.Safar.OrderBy(v => v.FromDate).ToList())
                    {
                        cc.SafarCount = a;
                        a = a + 1;
                    }

                    x.Soom.First(v => v.Id == IdSoom).SafarCounter = sfr.Safar.Count;

                    x.SaveChanges();
                    return _add.Id;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return 0;
            }
        }
        public static List<FactorListViewModel> GetListFactor2(int IdSafar, int IdUser, int IdType)
        {
            try
            {
                using (var x = new GolatehEntities())
                {

                    var tt = x.Safar.AsNoTracking().First(v => v.Id == IdSafar && v.Soom.IdPersonnel == IdUser);

                    return tt.SafarToFactor.Where(v => v.IdTypeFactor == IdType)
                        .Select(b => new FactorListViewModel()
                        {
                            Id = b.Id,
                            Weight = b.Weight,
                            Tozihat = b.Tozihat,
                            KalaName = b.Kala.Title,
                            Price = b.Price,
                            Moshtari = b.Moshtari,
                            ShomareFactor = b.ShomareFactor,
                            TaminKhonanade = b.TaminKhonanade,
                            Tedad = b.Tedad,
                            DateFactor = (b.DateFactor != null ? PersianCalander.ToShamsi(b.DateFactor.Value) : ""),
                            CreateDate = PersianCalander.ToShamsiTime(b.CreateDate),
                            TotoalPrice = b.TotalPrice,
                            Unitname = (b.IdUnit != null ? b.Unit.Title : "")
                        }).OrderBy(v => v.Id).ToList();
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<FactorListViewModel>();
            }
        }






        public static List<FactorListViewModel> GetListFactor3(int IdSoom)
        {
            try
            {
                using (var x = new GolatehEntities())
                {

                    var itm = x.Soom.AsNoTracking().First(b => b.Id == IdSoom);
                    var _IdSafar = itm.Safar.Where(v => v.IsActive == false).Select(v => v.Id).ToArray();


                    var data = x.SafarToFactor.AsNoTracking().Where(v => _IdSafar.Contains(v.IdSafar)).ToList();

                    List<FactorListViewModel> lst = new List<FactorListViewModel>();

                    foreach (var k in data.Where(v => v.IdTypeFactor == 13).Select(v => v.IdKala).Distinct().ToArray())
                    {
                        var _KalaName = x.Kala.AsNoTracking().First(v => v.Id == k).Title;

                        lst.Add(new FactorListViewModel()
                        {
                            IdTypeFactor = 13,
                            Tedad = data.Where(v => v.IdTypeFactor == 13 && v.IdKala == k).Sum(v => v.Tedad),
                            TotoalPrice = data.Where(v => v.IdTypeFactor == 13 && v.IdKala == k).Sum(v => v.TotalPrice),
                            KalaName = _KalaName
                        });
                    }
                    foreach (var k in data.Where(v => v.IdTypeFactor == 14).Select(v => v.IdKala).Distinct().ToArray())
                    {
                        var _KalaName = x.Kala.AsNoTracking().First(v => v.Id == k).Title;

                        lst.Add(new FactorListViewModel()
                        {
                            IdTypeFactor = 14,
                            Tedad = data.Where(v => v.IdTypeFactor == 14 && v.IdKala == k).Sum(v => v.Tedad),
                            TotoalPrice = data.Where(v => v.IdTypeFactor == 14 && v.IdKala == k).Sum(v => v.TotalPrice),
                            KalaName = _KalaName
                        });
                    }
                    foreach (var k in data.Where(v => v.IdTypeFactor == 16).Select(v => v.IdKala).Distinct().ToArray())
                    {
                        var _KalaName = x.Kala.AsNoTracking().First(v => v.Id == k).Title;

                        lst.Add(new FactorListViewModel()
                        {
                            IdTypeFactor = 16,
                            Tedad = data.Where(v => v.IdTypeFactor == 16 && v.IdKala == k).Sum(v => v.Tedad),
                            TotoalPrice = data.Where(v => v.IdTypeFactor == 16 && v.IdKala == k).Sum(v => v.TotalPrice),
                            KalaName = _KalaName
                        });
                    }
                    foreach (var k in data.Where(v => v.IdTypeFactor == 18).Select(v => v.IdKala).Distinct().ToArray())
                    {
                        var _KalaName = x.Kala.AsNoTracking().First(v => v.Id == k).Title;

                        lst.Add(new FactorListViewModel()
                        {
                            IdTypeFactor = 18,
                            Tedad = data.Where(v => v.IdTypeFactor == 18 && v.IdKala == k).Sum(v => v.Tedad),
                            TotoalPrice = data.Where(v => v.IdTypeFactor == 18 && v.IdKala == k).Sum(v => v.TotalPrice),
                            KalaName = _KalaName
                        });
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<FactorListViewModel>();
            }
        }
        public static string ToEnNum(string T)
        {
            if (string.IsNullOrEmpty(T)) return "";
            return T.Trim().Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9");
        }

        public static bool EditUser(string FirstName, string LastName, string NationalCode, string Phone, string Password, int IdUser, HttpPostedFileBase file)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.Personnel.First(v => v.Id == IdUser);

                    itm.FirstName = ToEnNum(FirstName.Trim());
                    itm.LastName = ToEnNum(LastName.Trim());
                    itm.NationalCode = ToEnNum(NationalCode.Trim());
                    itm.Password = ToEnNum(Password.Trim());
                    itm.Phone = ToEnNum(Phone.Trim());

                    if (file != null)
                    {
                        string Name = DateTime.Now.Ticks + Path.GetExtension(file.FileName);
                        file.SaveAs(System.Web.HttpContext.Current.Server.MapPath(@"~/Content/File/ImagePersonnel/" + Name));

                        string ImageUrl = "/Content/File/ImagePersonnel/" + Name;


                        itm.Image = ImageUrl;
                    }

                    x.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }



        public static GetPrintSafarViewModel GetPrintSafar(int IdSafar, int IdUser)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tt = x.Database.SqlQuery<GetPrintSafarViewModel>("dbo.GetPrintSafar @IdSafar, @IdPersonnel", new SqlParameter[]
                    {
                        new SqlParameter("IdSafar",IdSafar),
                        new SqlParameter("IdPersonnel",IdUser)
                    }).FirstOrDefault();

                    return tt;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new GetPrintSafarViewModel();
            }
        }


        public static List<GetPrintSoomViewModel> GetPrintSoom(int IdSoom, int IdUser)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tt = x.Database.SqlQuery<GetPrintSoomViewModel>("dbo.GetPrintSoom @IdSoom, @IdPersonnel", new SqlParameter[]
                    {
                        new SqlParameter("IdSoom",IdSoom),
                        new SqlParameter("IdPersonnel",IdUser)
                    }).ToList();

                    return tt;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<GetPrintSoomViewModel>();
            }
        }


        public static List<GetPrintPernonnelGolateSoomViewModel> GetPrintPernonnelGolateSoom(int IdSoom)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tt = x.Database.SqlQuery<GetPrintPernonnelGolateSoomViewModel>("dbo.GetPrintPernonnelGolateSoom @IdSoom", new SqlParameter[]
                    {
                        new SqlParameter("IdSoom",IdSoom)
                    }).ToList();

                    return tt;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<GetPrintPernonnelGolateSoomViewModel>();
            }
        }



        public static List<GetListKalaFromSoomViewModel> GetListKalaFromSoom(int IdSoom)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tt = x.Database.SqlQuery<GetListKalaFromSoomViewModel>("dbo.GetListKalaFromSoom @IdSoom", new SqlParameter[]
                    {
                        new SqlParameter("IdSoom",IdSoom)
                    }).ToList();

                    return tt;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<GetListKalaFromSoomViewModel>();
            }
        }




        public static List<GOlatePrintViewModel> PrintGolate(long IdSafar, long IdPernonnel)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tt = x.Database.SqlQuery<GOlatePrintViewModel>("dbo.PrintGolate @IdPernonnel,@IdSafar", new SqlParameter[]
                    {
                        new SqlParameter("IdSafar",IdSafar),
                        new SqlParameter("IdPernonnel",IdPernonnel)
                    }).ToList();

                    return tt;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<GOlatePrintViewModel>();
            }
        }

        public static int GetSafarNumber(long IdSoom, long IdSafar)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tt = x.Database.SqlQuery<int>("SELECT [dbo].SafarNumber(" + IdSoom + "," + IdSafar + ")").FirstOrDefault();

                    return tt;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return 0;
            }
        }

        public static List<PrintDashboard2ViewModel> PrintDashboard2(long IdPersonnel, int Type)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tt = x.Database.SqlQuery<PrintDashboard2ViewModel>("[dbo].PrintDashboard2 @IdPersonnel,@Type",
                        new SqlParameter[] { new SqlParameter("IdPersonnel", IdPersonnel), new SqlParameter("Type", Type) }).ToList();

                    return tt;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<PrintDashboard2ViewModel>();
            }
        }








        public static List<IdVal1ViewModel> GetListSafarByIdSoom(long IdSoom)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    return x.Safar.AsNoTracking().AsEnumerable().Where(b => IdSoom == b.IdSoom).Select(v => new IdVal1ViewModel()
                    {
                        Id = v.Id.ToString(),
                        IsActive = v.IsActive,
                        Title = v.Title + " " + PersianCalander.ToShamsi(v.FromDate) + " - " + PersianCalander.ToShamsi(v.ToDate)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<IdVal1ViewModel>();
            }
        }

        public static List<IdValViewModel> GetListSoomByIdShip(int IdShip)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    return x.Soom.AsNoTracking().AsEnumerable()
                        .Where(b => IdShip == b.IdShip).Select(v => new IdValViewModel()
                        {
                            Id = v.Id.ToString(),
                            Title = v.TiTle + " " + PersianCalander.ToShamsi(v.FromDate) + " - " + PersianCalander.ToShamsi(v.ToDate)
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<IdValViewModel>();
            }
        }


        public static bool RowBimeChange(long Id, bool Status)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var itm = x.SafarToAfrad.First(v => v.Id == Id);
                    if (Status)
                    {
                        var _year = PersianCalander.ToShamsi(DateTime.Now).Substring(0, 4);
                        var pricebime = x.Bimeh.AsNoTracking().First(v => v.Year == _year).Price;

                        itm.IsBimeh = true;
                        itm.PriceBimeh = pricebime;
                    }
                    else
                    {
                        itm.IsBimeh = false;
                        itm.PriceBimeh = 0;
                    }

                    x.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }

        public static Int64 TotalMosaede(long IdNote, long IdPersonnel)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var Notee = x.Note.First(v => v.Id == IdNote && v.IdPersonnel == IdPersonnel);

                    var debed = x.DebtHistory.AsNoTracking().Where(v => v.IdNote == Notee.Id && v.IsActive == true).ToList();
                    if (debed.Any())
                    {
                        return Notee.Price - (debed.Sum(v => v.Price));
                    }

                    return Notee.Price > 0 ? Notee.Price / 10 : 0;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return 0;
            }
        }
        public static int AfradYadashtKartexSave(int IdShip, string Date, string Desc, long IdPersonnel, long Price, int? IdNot)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    x.DebtHistory.Add(new DebtHistory() { IdNote = (IdNot == 0 ? null : IdNot), Price = Price * 10, CreateDate = PersianCalander.ToMiladi(Date.ToEnNum()), Description = Desc, IdPersonnel = IdPersonnel, IdShip = IdShip });
                    x.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return 0;
            }
        }
        public static int AfradYadashtSave(int IdShip, string Date, string Desc, long IdPersonnel, long Price)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    x.Note.Add(new Note() { Price = Price * 10, CreateDate = PersianCalander.ToMiladi(Date.ToEnNum()), Description = Desc, IdPersonnel = IdPersonnel, IdShip = IdShip });
                    x.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return 0;
            }
        }
        public static List<YadashtViewModel> GetListYadasht(int IdShip)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var tt = x.Note.AsNoTracking().AsEnumerable().Where(v => v.IdShip == IdShip)
                        .Select(v => new YadashtViewModel()
                        {
                            IdPersonnel = v.IdPersonnel,
                            Id = v.Id,
                            Price = v.Price.ToString(),
                            Name = v.Personnel.FirstName + " " + v.Personnel.LastName,
                            Desc = v.Description,
                            Date = v.CreateDate.ToString(),
                            IdSafar = v.IdSafar
                        }).ToList();

                    foreach (var item in tt)
                    {
                        var rodnote = x.DebtHistory.AsNoTracking().Where(v => v.IdNote == item.Id && v.IsActive == true).ToList();
                        long debed = 0;
                        if (rodnote.Any())
                        {
                            item.PriceMande = ((decimal.Parse(item.Price) - rodnote.Sum(v => v.Price)) / 10).ToString("N0");
                            item.PriceToPay = rodnote.Sum(v => v.Price / 10).ToString("N0");
                            item.Price = ((decimal.Parse(item.Price) / 10)).ToString("N0");
                        }
                        else
                        {
                            item.PriceMande = (decimal.Parse(item.Price) / 10).ToString("N0");
                            item.PriceToPay = "0";
                            item.Price = ((decimal.Parse(item.Price) / 10)).ToString("N0");
                        }
                    }

                    return tt;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<YadashtViewModel>();
            }
        }

        public static List<YadashtkartexViewModel> GetListYadashtkartex(int IdShip)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    return x.DebtHistory.AsNoTracking().AsEnumerable().Where(v => v.IdShip == IdShip)
                        .Select(v => new YadashtkartexViewModel()
                        {
                            IdPersonnel = v.IdPersonnel,
                            Id = v.Id,
                            Price = (v.Price / 10).ToString("N0"),
                            Name = v.Personnel.FirstName + " " + v.Personnel.LastName,
                            Desc = v.Description,
                            Date = v.CreateDate.ToString(),
                            DescNote = v.Note != null ? v.Note.Description : "",
                            IdNote = v.IdNote
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return new List<YadashtkartexViewModel>();
            }
        }


        public static bool DeleteYadasht(int IdShip, string Id)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    x.Note.Remove(x.Note.First(v => v.Id.ToString() == Id.ToString() && v.IdShip == IdShip));
                    x.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }


        public static bool DeleteYadashtKartex(int IdShip, string Id)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    x.DebtHistory.Remove(x.DebtHistory.First(v => v.Id.ToString() == Id.ToString() && v.IdShip == IdShip));
                    x.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }



        public static bool AddFactorForoshTamirat2(int IdUser, int IdSoom, int IdSafar, string IdKala, decimal? Weight, string Tozihat, long Price, string CreateDate, decimal? Tedad)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var t = x.Safar.First(v => v.Id == IdSafar && v.IdSoom == IdSoom);

                    int _idKala = 0;

                    IdKala = IdKala.Trim().ToEnNum();

                    if (x.Kala.AsNoTracking().Any(v => v.Title == IdKala))
                    {
                        _idKala = x.Kala.AsNoTracking().First(v => v.Title == IdKala).Id;
                    }
                    else
                    {
                        var max = x.Kala.AsNoTracking().AsEnumerable().Max(v => Convert.ToInt32(v.Sort)) + 1;

                        var _add = new Kala() { CreateDate = DateTime.Now, IdPersonnel = t.IdPersonnel, IdTypeKala = 11, IdUnit = 1, IsActive = false, Title = IdKala, Sort = max };
                        x.Kala.Add(_add);
                        x.SaveChanges();

                        _idKala = _add.Id;
                    }


                    t.SafarToFactor.Add(new SafarToFactor()
                    {
                        IdTypeFactor = 16,
                        Price = Price,
                        Tedad = Tedad,
                        CreateDate = DateTime.Now,
                        IdKala = _idKala,
                        DateFactor = PersianCalander.ToMiladi(CreateDate.ToEnNum()),
                        Tozihat = Tozihat,
                        Weight = Weight,
                        IdSafar = t.Id,
                        TotalPrice = Convert.ToInt64(Tedad * Price),
                        IdTankhaGardan = IdUser
                    });

                    t.PriceTamirat = t.SafarToFactor.Where(b => b.IdTypeFactor == 16).Sum(v => v.TotalPrice);

                    x.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }
        public static bool AddFactorForoshMahtaiaj2(int IdUser, int IdSoom, int IdSafar, int IdKala, decimal? Weight, string Tozihat, long Price, string CreateDate, decimal? Tedad, int Idunit)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var t = x.Safar.First(v => v.Id == IdSafar && v.IdSoom == IdSoom);

                    t.SafarToFactor.Add(new SafarToFactor()
                    {
                        IdTypeFactor = 18,
                        Price = Price,
                        Tedad = Tedad,
                        CreateDate = DateTime.Now,
                        IdKala = IdKala,
                        DateFactor = PersianCalander.ToMiladi(CreateDate.ToEnNum()),
                        Tozihat = Tozihat,
                        Weight = 0,
                        IdSafar = t.Id,
                        TotalPrice = Convert.ToInt64(Tedad * Price),
                        IdUnit = Idunit,
                        IdTankhaGardan = IdUser
                    });

                    t.PriceMahtaiaj = t.SafarToFactor.Where(b => b.IdTypeFactor == 18).Sum(v => v.TotalPrice);

                    x.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }
        public static bool AddFactorForoshKharid2(int IdUser, int IdSoom, int IdSafar, int IdKala, decimal? Weight, string Tozihat, long Price, string CreateDate, decimal? Tedad, int Idunit)
        {
            try
            {
                using (var x = new GolatehEntities())
                {
                    var t = x.Safar.First(v => v.Id == IdSafar && v.IdSoom == IdSoom);

                    t.SafarToFactor.Add(new SafarToFactor()
                    {
                        IdTypeFactor = 13,
                        Price = Price,
                        Tedad = Tedad,
                        CreateDate = DateTime.Now,
                        IdKala = IdKala,
                        DateFactor = PersianCalander.ToMiladi(CreateDate.ToEnNum()),
                        Tozihat = Tozihat,
                        Weight = 0,
                        IdSafar = t.Id,
                        TotalPrice = Convert.ToInt64(Price * Tedad),
                        IdUnit = Idunit,
                        IdTankhaGardan = IdUser
                    });

                    t.PriceKharid = t.SafarToFactor.Where(b => b.IdTypeFactor == 13).Sum(v => v.TotalPrice);

                    x.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                AddError(ex);
                return false;
            }
        }


    }
}