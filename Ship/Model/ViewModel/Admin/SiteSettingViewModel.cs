using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Ship.Model.ViewModel.Admin
{
    public class LanguageViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class MessageViewModel
    {
        public bool Status { get; set; }
        public string Msg { get; set; }
    }


    public class TankhaGardanToShipViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ShipList { get; set; }
        public int[] IdShipList { get; set; }
        public string FullName { get; set; }
    }



    public class SahamDarViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int Rate { get; set; }
        public long TotalPrice { get; set; }
        public int Dong { get; set; }
    }

    public class GetListBimehAfradViewModel
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public int IdShip { get; set; }
        public string Name { get; set; }
        public int Tedad { get; set; }

    }


    public class Print1ViewModel
    {
        public string Semat { get; set; }
        public string Tedad { get; set; }
        public string Kala { get; set; }
        public string Unit { get; set; }
        public string Safar { get; set; }
        public string SafarCount { get; set; }
        public string SommConter { get; set; }
        public string Price { get; set; }
        public string TotalPrice { get; set; }

        public string TypeFactor { get; set; }
        public string Soom_Safar { get; set; }
        public string FullName { get; set; }
        public string Tozihat { get; set; }


        public string Daramad { get; set; }
        public string Hazine { get; set; }
        public string Sood { get; set; }

        public  string ToDate { get; set; }
        public string ToDateSafar { get; set; }
    }


    public class SematViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Rate { get; set; }
    }

    public class IdVal1ViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
    }
    public class YadashtkartexViewModel
    {
        public decimal Id { get; set; }
        public decimal? IdNote { get; set; }
        public long IdPersonnel { get; set; }
        public string DescNote { get; set; }
        public string Desc { get; set; }
        public string Price { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
    }
    public class YadashtViewModel
    {
        public decimal Id { get; set; }
        public long IdPersonnel { get; set; }
        public string Desc { get; set; }
        public string Price { get; set; }
        public string PriceToPay { get; set; }
        public string PriceMande { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public long? IdSafar { get; set; }
    }
    public class IdValViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }
    public class KeshavarziViewModel
    {
        public string mablag { get; set; }
        public string sheba { get; set; }
        public string name { get; set; }
        public string serial { get; set; }
    }
    public class MelatViewModel
    {
        public string sheba { get; set; }
        public string date { get; set; }
        public string serial { get; set; }
        public string tedadhavale { get; set; }
        public string mablag { get; set; }
        public string name { get; set; }
    }


    public class FileBankList2ViewModel
    {
        public long TotalPrice { get; set; }
        public long AttrId { get; set; }
    }

    public class FileBankListViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NationalCode { get; set; }
        public string Family { get; set; }
        public string Semat { get; set; }
        public Int64 TotalPrice { get; set; }
    }




    public class AfradViewModel
    {
        public bool IsActive { get; set; }
        public long Id { get; set; }
        public int? IdBank { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string Phone { get; set; }
        public string ShebaNumber { get; set; }
        public string ShomareHesab { get; set; }
        public string NameBank { get; set; }
        public string Namebank { get; set; }
        public string ShomareCart14 { get; set; }
        public string Logo { get; set; }
    }

    public class Afrad2ViewModel
    {
        public int? Sort { get; set; }
        public long Id { get; set; }
        public long IdTypeKala { get; set; }
        public string Title { get; set; }

        public long Id2 { get; set; }
    }
    public class ShipViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class CategoryTreeIUViewModel
    {
        public int Id { get; set; }
        public int IdParent { get; set; }
        public string Title { get; set; }
    }


    public class PersonnelManagementViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NationalCode { get; set; }
        public string Phone { get; set; }
        public string Logo { get; set; }
        public bool IsActive { get; set; }
        public bool IsRolePostArticle { get; set; }
    }

    public class CategoryTreeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }

        public int? Sort { get; set; }
    }

    public class PostViewModel
    {
        public decimal Id { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public string CategoryTree { get; set; }
        public string CreateDate { get; set; }
        public string RegisterUserPost { get; set; }
        public int IdLanguage { get; set; }
        public string SeoShortDescription { get; set; }
        public Int32 IdCategory { get; set; }

        public string SeoMetaKeyWord { get; set; }

        public string NoRecord { get; set; }

        public string File { get; set; }
        public int IsProduct { get; set; }

        public int IdParentMenu { get; set; }
    }
    public class PrintDashboard3ViewModel
    {
        public Int64 Val { get; set; }
        public string Title { get; set; }
    }


    public class PrintDashboard93ViewModel
    {
        public decimal Val { get; set; }
        public string Title { get; set; }
    }

    public class PrintDashboard4ViewModel
    {
        public Int64 A { get; set; }
        public Int64 B { get; set; }
        public Int64 C { get; set; }

        public string D { get; set; }
    }

    public class PrintDashboard2ViewModel
    {
        public string Val { get; set; }
        public string Month { get; set; }
    }

    public class PrintDashboard99ViewModel
    {
        public decimal Val { get; set; }
        public string Month { get; set; }
    }

    public class GOlatePrintViewModel
    {
        public string PriceFoodTotal { get; set; }
        public string PriceFood { get; set; }
        public string Nakhoda { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SematName { get; set; }
        public string Ratio { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ShipName { get; set; }
        public string SoomName { get; set; }
        public string SafarName { get; set; }
        public string HajSeid { get; set; }
        public string NoeSeid { get; set; }
        public string PriceForosh { get; set; }
        public Int32? TedadJasho { get; set; }
        public string PriceMahtaiaj { get; set; }
        public string MablagBimeh { get; set; }
        public string PriceMsaede { get; set; }
        public string PriceMsaedeKala { get; set; }
        public string GolateBase { get; set; }
        public string SahmJasho { get; set; }
        public string Kosorat { get; set; }
        public string KhalesPardakhti { get; set; }
        public int IdSemat { get; set; }
        public int SahmAzLeng { get; set; }
        public string SahmAzLengToman { get; set; }
    }
    public class SliderViewModel
    {
        public int Id { get; set; }
        public int IdLanguage { get; set; }
        public string Title { get; set; }
        public string Title2 { get; set; }
        public string Url { get; set; }
    }


    public class GetListKalaFromSoomViewModel
    {
        public string NameKala { get; set; }
        public string Unit { get; set; }
        public string Tedad { get; set; }
        public string TotalPrice { get; set; }
        public Int64 IdTypeFactor { get; set; }
    }


    public class GetPrintPernonnelGolateSoomViewModel
    {
        public string Name { get; set; }
        public string Golate { get; set; }
        public string Semat { get; set; }
        public string Total { get; set; }
        public int TedadSafar { get; set; }
        public string Mosaede { get; set; }
    }


    public class TeamRelationViewModel
    {
        public decimal Id { get; set; }
        public int IdLanguage { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Adress { get; set; }
    }
    public class GetPrintSoomViewModel
    {
        public string PriceKharid { get; set; }
        public string PriceMahtaiaj { get; set; }
        public string PriceForosh { get; set; }
        public string PriceTamirat { get; set; }
        public string FactorMosaede { get; set; }
        public string TotalHazine { get; set; }
        public string PriceFood { get; set; }
        public string TamiratGolate { get; set; }
        public string NameShip { get; set; }
        public string NumberShip { get; set; }
        public string SafarName { get; set; }
        public int SumKolSafrha { get; set; }
        public Int64? SafarNumber { get; set; }
        public Int32? SoomNumber { get; set; }
        public string FromDateSoom { get; set; }
        public string ToDateSoom { get; set; }
        public string FromDateSafar { get; set; }
        public string ToDateSafar { get; set; }
        public string Tozihat { get; set; }
        public string TedadKolGolate { get; set; }
        public string PriceHarGolate { get; set; }
    }
    public class GetPrintSafarViewModel
    {
        public string NameShip { get; set; }
        public string NumberShip { get; set; }
        public Int32? SafarNumber { get; set; }
        public Int32? SoomNumber { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Nakhoda { get; set; }
        public string PriceKharid { get; set; }
        public string PriceMahtaiaj { get; set; }
        public string PriceForosh { get; set; }
        public string PriceTamirat { get; set; }
        public string FactorMosaede { get; set; }
        public string TotalHazine { get; set; }
        public string TozihatSafar { get; set; }
        public string NameSafar { get; set; }
        public string TedadKolGolate { get; set; }
        public string PriceHarGolate { get; set; }
        public string PriceFood { get; set; }
        public string TamiratGolate { get; set; }
        public string MosaedeMahiPrice { get; set; }
        public string HajmSeidBeTon { get; set; }

        public string DaramadSafarFrosh_Machele { get; set; }
        public string DaramadSafarFrosh_PerMachele { get; set; }
    }
    public class GetListCategoryViewModel
    {
        public int Id { get; set; }
        public int? IdParent { get; set; }
        public string Title { get; set; }
        public int IsProduct { get; set; }
    }
    public class ContactUsViewModel
    {
        public decimal Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateDateString { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
    public class SubscribeViewModel
    {
        public decimal Id { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateDateString { get; set; }
    }


    public class SiteLinkViewModel
    {
        public string Title { get; set; }
        public string Value { get; set; }

        public int IdLanguage { get; set; }
        public int Id { get; set; }
    }

    public class AboutAsViewModel
    {
        public Int32 Id { get; set; }
        public string ShortDesc1 { get; set; }
        public string ShortDesc2 { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Image { get; set; }
    }






    public class AboutAsSaveViewModel
    {
        public Int32 Id { get; set; }
        public string ShortDesc1 { get; set; }
        public string ShortDesc2 { get; set; }
        public string MaxDesc { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Image { get; set; }

        public HttpPostedFileBase Logo2 { get; set; }
    }


    public class Dashbor2ViewModel
    {
        public string Soom { get; set; }
        public int Safar { get; set; }

        public decimal Sood { get; set; }
        public decimal Hazine { get; set; }

        public decimal Daramad { get; set; }

        public string DateStart { get; set; }
        public string DateFinish { get; set; }
    }


    public class Dashbor99ViewModel
    {
        public string Name { get; set; }
        public decimal IdYadasht { get; set; }
    }

    public class Dashboard1ViewModel
    {
        
        public string ExpireDate { get; set; }
        public string Name { get; set; }
        public string TedadSoom { get; set; }
        public string TedadSafar { get; set; }
        public Int64 PriceForosh { get; set; }
        public Int64 Sood { get; set; }
        public Int64 HazineHa { get; set; }
        public string Id { get; set; }

        public bool IsEnd { get; set; }
    }



    public class SiteSettingViewModel
    {
        public Int32 Id { get; set; }
        public string Title { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string ContactUs { get; set; }
        public string Logo { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }


        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Linkedin { get; set; }
        public string Dribbble { get; set; }
        public string Inestagram { get; set; }


    }



    public class TeamRelationSubmitViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Adress { get; set; }
        public int IdLanguage { get; set; }

        public HttpPostedFileBase Logo2 { get; set; }
    }



    public class SliderSubmitViewModel
    {
        public string Title { get; set; }
        public string Title2 { get; set; }


        public int IdLanguage { get; set; }

        public HttpPostedFileBase Logo2 { get; set; }
    }

    public class SiteSettingSaveViewModel
    {
        public Int32 Id { get; set; }
        public string Title { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string ContactUs { get; set; }
        public string Logo { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Linkedin { get; set; }
        public string Dribbble { get; set; }
        public string Inestagram { get; set; }


        public HttpPostedFileBase Logo2 { get; set; }
    }


    public class ProductOperationViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int IdCategory { get; set; }
        public List<HttpPostedFileBase> Image2 { get; set; } = new List<HttpPostedFileBase>();
    }

    public class PostOperationViewModel
    {
        public decimal Id { get; set; }
        public Int32 IdLanguage { get; set; }
        public Int32 IdPersonnel { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }
        public string SeoShortDescription { get; set; }
        public string SeoMetaKeyWord { get; set; }
        public string Image { get; set; }
        public string File { get; set; }
        public bool IsActive { get; set; }



        public HttpPostedFileBase File1 { get; set; }
        public HttpPostedFileBase Image2 { get; set; }
        public Int32 IdCategory { get; set; }
        public List<SelectListItem> CategoryTree { get; set; } = new List<SelectListItem>();
    }

    public class GetidValViewModel
    {
        public string Id { get; set; }
        public string Val { get; set; }
        public string Val2 { get; set; }
    }


    public class BimeViewModel
    {
        public int IdShip { get; set; }
        public long Id { get; set; }
        public string Sal { get; set; }
        public string Mah { get; set; }
        public List<long> List { get; set; } = new List<long>();
    }



    public class ListHandMoneyViewModel
    {
        public int IdShip { get; set; }
        public long I { get; set; }
        public long IdPersonnel { get; set; }
        public string FullName { get; set; }
        public string TotalPrice { get; set; }
    }


    public class GetListShipViewModel
    {
        
        public int Id { get; set; }
        public string ExpireDate { get; set; }
        public string Title { get; set; }
        public string Number { get; set; }
        public string Image { get; set; }
        public string YearProduction { get; set; }
        public DateTime YearProduction2 { get; set; }
        public int? Tul { get; set; }
        public int? Arz { get; set; }
        public string NameMotor { get; set; }
        public int? HajMotor { get; set; }
        public string ShomareBime { get; set; }
        public int TedadSahamDar { get; set; }
        public int TedadAfrad { get; set; }

        public int Yadasht { get; set; }
        public bool IsEnded { get; set; }
    }

    public class GetInfohHeaderSafarViewModel
    {
        public string IdShip { get; set; }
        public string Ship { get; set; }
        public string IdSoom { get; set; }
        public string Soom { get; set; }
    }

    public class GetListSoomViewModel
    {
        public long Id { get; set; }
        public long IdPersonnel { get; set; }
        public int IdShip { get; set; }
        public bool IsActive { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string SoomName { get; set; }
        public string Tozihat { get; set; }

        public string ShipName { get; set; }
        public string StatusName { get; set; }


        public System.DateTime? FromDate2 { get; set; }
        public System.DateTime? ToDate2 { get; set; }

        public int TedadSafar { get; set; }
    }


    public class GetListSafarToHandMoneyViewModel
    {
        public long Id { get; set; }
        public string FullNameAfrad { get; set; }
        public string GropNote { get; set; }
        public string TozihatNote { get; set; }
        public string Unit { get; set; }
        public long? Price { get; set; }
        public long? TotalPrice { get; set; }
        public string CreateDateMoney { get; set; }

        public decimal? Weight { get; set; }
        public string KalaName { get; set; }
        public string Tozihat { get; set; }

    }



    public class FactorListViewModel
    {
        public long Id { get; set; }

        public string UserSabtKonanade { get; set; }

        public string KalaName { get; set; }
        public string Unitname { get; set; }
        public string TypeFactor { get; set; }
        public long IdTypeFactor { get; set; }
        public Nullable<decimal> Tedad { get; set; }
        public long Price { get; set; }
        public long TotoalPrice { get; set; }
        public string CreateDate { get; set; }
        public string ShomareFactor { get; set; }
        public string DateFactor { get; set; }
        public string TaminKhonanade { get; set; }
        public string Moshtari { get; set; }
        public string Tozihat { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public int Saraf { get; set; }
    }

    public class PriceViewMOdel
    {
        public long Id { get; set; }
        public long Fi { get; set; }
        public long Tedad { get; set; }
    }

    public class SafarAfradRelationViewModel
    {
        public long Id { get; set; }
        public bool IsBimeh { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Rate { get; set; }
        public string SematName { get; set; }
        public Int64? TotalPrice { get; set; }
        public long IdPersonnel { get; set; }

        public long? KasriTabakhi { get; set; }
        public long? Nakhales { get; set; }
        public long khales { get; set; }
        public long Bime { get; set; }
        public long Mosaede { get; set; }
    }

    public class GetListSafarViewModel
    {
        public long Id { get; set; }
        public int IdPersonnel { get; set; }
        public long IdSoom { get; set; }
        public bool IsActive { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public decimal FactorMosaede { get; set; }
        public decimal FactorTamirat { get; set; }
        public decimal FactorKharid { get; set; }
        public decimal FactorMahtaiaj { get; set; }
        public decimal FactorForosh { get; set; }
        public string Tozihat { get; set; }
        public string Title { get; set; }
        public string NamesOOM { get; set; }
        public bool IsActiveSoom { get; set; }
        public int IdShip { get; set; }

        public DateTime? FromDate2 { get; set; }
        public DateTime? ToDate2 { get; set; }



        public int PriceFood { get; set; }
        public decimal PriceTamiratGolate { get; set; }

    }





    public class GetDeteilSafarViewModel
    {

    }

}