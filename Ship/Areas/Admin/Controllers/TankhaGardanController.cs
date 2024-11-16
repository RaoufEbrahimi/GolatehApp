using Ship.Model.Db;
using Ship.Model.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ship.Model.Extension;


namespace Ship.Areas.Admin.Controllers
{
    public class TankhaGardanController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public JsonResult Add(string[] IdShip, string U, string P, string FullName)
        {
            int IdUser = int.Parse(User.Identity.Name);

            try
            {
                using (var x = new GolatehEntities())
                {
                    U = U.ToEnNum();
                    P = P.ToEnNum();
                    FullName = FullName.ToEnNum();

                    if (x.TankhaGardan.AsNoTracking().Any(b => b.UserName == U))
                    {
                        return Json(-1);
                    }

                    var gg = new TankhaGardan() { IdUser = IdUser, PassWord = P, UserName = U, IsActive = true, FullName = FullName };
                    foreach (var item in IdShip)
                    {
                        gg.TankhaGardanToShip.Add(new TankhaGardanToShip() { IdShip = int.Parse(item) });
                    }

                    x.TankhaGardan.Add(gg);
                    x.SaveChanges();

                    return Json(1);
                }
            }
            catch (Exception)
            {
                return Json(0);
            }
        }


        [HttpPost]
        public JsonResult Add2(string[] IdShip, string U, string P, long Id, string FullName)
        {
            int IdUser = int.Parse(User.Identity.Name);

            try
            {
                using (var x = new GolatehEntities())
                {
                    var tt = x.TankhaGardan.First(v => v.IdUser == IdUser && v.Id == Id);

                    tt.UserName = U.ToEnNum();
                    tt.PassWord = P.ToEnNum();
                    tt.FullName = FullName.ToEnNum();


                    if (x.TankhaGardan.AsNoTracking().Any(b => b.UserName == tt.UserName && b.Id != tt.Id))
                    {
                        return Json(-1);
                    }

                    x.TankhaGardanToShip.RemoveRange(tt.TankhaGardanToShip.ToList());

                    foreach (var item in IdShip)
                    {
                        tt.TankhaGardanToShip.Add(new TankhaGardanToShip() { IdShip = int.Parse(item) });
                    }

                    x.SaveChanges();

                    return Json(1);
                }
            }
            catch (Exception)
            {
                return Json(0);
            }
        }


        [HttpPost]
        public JsonResult Delete(long Id)
        {
            int IdUser = int.Parse(User.Identity.Name);

            try
            {
                using (var x = new GolatehEntities())
                {
                    x.TankhaGardan.First(v => v.Id == Id && v.IdUser == IdUser).IsActive = false;
                    x.SaveChanges();
                    return Json(1);
                }
            }
            catch (Exception)
            {
                return Json(0);
            }
        }




        [HttpPost]
        public PartialViewResult ToEdit(long Id)
        {
            int IdUser = int.Parse(User.Identity.Name);

            using (var x = new GolatehEntities())
            {
                var gg = x.TankhaGardan.AsNoTracking().AsEnumerable().Where(b => b.IdUser == IdUser && Convert.ToBoolean(b.IsActive) == true).Select(v => new TankhaGardanToShipViewModel()
                {
                    Id = v.Id,
                    UserName = v.UserName,
                    Password = v.PassWord,
                    IdShipList = v.TankhaGardanToShip.Select(b => b.Ship.Id).ToArray(),
                    FullName = v.FullName
                }).First(v => v.Id == Id);

                return PartialView("~/Areas/Admin/Views/tankhagardan/_Add.cshtml", gg);
            }
        }



        [HttpPost]
        public PartialViewResult GetListTankhagardan()
        {
            int IdUser = int.Parse(User.Identity.Name);

            List<TankhaGardanToShipViewModel> gg = new List<TankhaGardanToShipViewModel>();

            using (var x = new GolatehEntities())
            {
                gg = x.TankhaGardan.AsNoTracking().AsEnumerable().Where(b => b.IdUser == IdUser && Convert.ToBoolean(b.IsActive) == true).Select(v => new TankhaGardanToShipViewModel()
                {
                    Id = v.Id,
                    UserName = v.UserName,
                    Password = v.PassWord,
                    ShipList = string.Join(",", v.TankhaGardanToShip.Select(b => b.Ship.Title).ToArray()),
                    FullName = v.FullName
                }).ToList();
                return PartialView("~/Areas/Admin/Views/TankhaGardan/_List.cshtml", gg);
            }
        }

    }
}