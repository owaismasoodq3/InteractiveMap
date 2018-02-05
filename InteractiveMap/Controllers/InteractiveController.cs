using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Data;
using InteractiveMap.Models;

namespace InteractiveMap.Controllers
{
    //test
    public class InteractiveController : Controller
    {
        // GET: Interactive

        //Use As 
        public ActionResult StationInformation(string cityName)
        {
            StationInformation stationInfo = GetStationInformation(cityName);
            JsonResult result = new JsonResult();
            result = Json(stationInfo, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            result.RecursionLimit = int.MaxValue;
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Getinfo()
        {
            List<Models.StationInformationDTO> stationList = GetAllStationInformation();
            JsonResult result = new JsonResult();
            result = Json(stationList, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            result.RecursionLimit = int.MaxValue;
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            try
            {
                // /*stationInformation_AllType*/
                // List<Models.StationInformation> modelFull = GetAllStationInformation();
                // //  return View(modelFull);
                // JsonResult result = new JsonResult();
                // result = Json(modelFull, JsonRequestBehavior.AllowGet);
                // result.MaxJsonLength = int.MaxValue;
                // result.RecursionLimit = int.MaxValue;
                //// string result = "hi";
                // return Json(result);
                return View("../Renderings/Map");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        public ActionResult Legends()
        {
            Legends_AllType modelFull = GetAllLegends("/sitecore/content/Home/iMap/Configuration/Legends/");
            return View(modelFull);
        }

        public ActionResult SubLegends()
        {
            Legends_AllType modelFull = GetAllLegends("/sitecore/content/Home/iMap/Configuration/SubLegends/");
            return View(modelFull);
        }

        public ActionResult TrainPosition()
        {
            Legends_AllType modelFull = GetAllLegends("/sitecore/content/Home/iMap/Configuration/TrainPosition/");
            return View(modelFull);
        }

        public Legends_AllType GetAllLegends(string path)
        {
            Legends_AllType allType = new Legends_AllType();
            allType.legends_All = new List<Models.Legends>();
            Database DB = Sitecore.Context.Database;
            Sitecore.Data.Items.Item legendsItem = DB.GetItem(path);
            foreach (Sitecore.Data.Items.Item child in legendsItem.Children)
            {

                Legends legendInfoAll = new Legends();
                legendInfoAll.icon = child.Fields[Templates.LegendInformation.LegendIcon];
                legendInfoAll.imageURL = Sitecore.Resources.Media.MediaManager.GetMediaUrl(legendInfoAll.icon.MediaItem);
                legendInfoAll.subtitle = child.Fields[Templates.LegendInformation.LegendSubtitle].ToString();

                allType.legends_All.Add(legendInfoAll);

            }

            return allType;
        }
        public ActionResult StationMap()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult StationInformation()
        //{
        //    stationInformation_AllType modelFull = GetAllStationInformation();
        //    return Json(modelFull);
        //}
        //public List<Models.StationInformation> /*stationInformation_AllType*/ GetAllStationInformation()
        //{
        //    //  stationInformation_AllType allType = new stationInformation_AllType();
        //    //allType.stationInformation_All = new List<Models.StationInformation>();
        //    List<Models.StationInformation> allType = new List<Models.StationInformation>();
        //    //Database master = Sitecore.Configuration.Factory.GetDatabase("master");
        //    Database DB = Sitecore.Context.Database;
        //    Sitecore.Data.Items.Item stationInfo_Item = DB.GetItem("/sitecore/content/Home/iMap/Configuration/Station Information/");
        //    foreach (Sitecore.Data.Items.Item child in stationInfo_Item.Children)
        //    {

        //        Sitecore.Data.Fields.Field temp = child.Fields[Templates.StationInformation.StationName];

        //        StationInformation stationInfoAll = new StationInformation();
        //        stationInfoAll.stationName = child.Fields[Templates.StationInformation.StationName].ToString();
        //        stationInfoAll.CRSCode = child.Fields[Templates.StationInformation.CRSCode].ToString();
        //        //stationInfoAll.stationAddress = child.Fields[Templates.StationInformation.StationAddress].ToString();
        //        stationInfoAll.xPos = child.Fields[Templates.StationInformation.XPox].ToString();
        //        stationInfoAll.yPox = child.Fields[Templates.StationInformation.YPos].ToString();
        //        stationInfoAll.status = child.Fields[Templates.StationInformation.StationStatus].ToString();
        //        //stationInfoAll.startDate = child.Fields[Templates.StationInformation.ValidFrom];//.Value.ToString();
        //        //stationInfoAll.endDate = child.Fields[Templates.StationInformation.ValidTo];//.Value.ToString();
        //        //station status
        //        if (stationInfoAll.status == null || stationInfoAll.status == "")
        //        {
        //            //default status
        //            //stationInfoAll.statusIcon = "";
        //            //stationInfo.statusTitle = null;
        //            //stationInfo.statusMessage = null;
        //        }
        //        else
        //        {
        //            Sitecore.Data.Items.Item stationStatusDetails = DB.GetItem("/sitecore/content/Home/iMap/Configuration/Station Status/" + stationInfoAll.status);
        //            //stationInfoAll.statusIcon = stationStatusDetails.Fields[Templates.StationStatus.StationStatusIcon];
        //            Sitecore.Data.Fields.ImageField statusIcon = stationStatusDetails.Fields[Templates.StationStatus.StationStatusIcon];
        //            stationInfoAll.imageURL = Sitecore.Resources.Media.MediaManager.GetMediaUrl(statusIcon.MediaItem);

        //            //stationInfoAll.statusTitle = stationStatusDetails.Fields[Templates.StationStatus.StationStatusTitle].Value;
        //            //stationInfoAll.statusMessage = stationStatusDetails.Fields[Templates.StationStatus.StationStatusDetails].Value.Replace("startDate", stationInfoAll.startDate.ToString());
        //            //stationInfoAll.statusMessage = stationInfoAll.statusMessage.Replace("endDate", stationInfoAll.endDate.ToString());

        //            //Legends legendInfoAll = new Legends();
        //            //legendInfoAll.icon = stationStatusDetails.Fields[Templates.LegendInformation.LegendIcon];
        //            //legendInfoAll.imageURL = Sitecore.Resources.Media.MediaManager.GetMediaUrl(legendInfoAll.icon.MediaItem);
        //            //legendInfoAll.subtitle = child.Fields[Templates.LegendInformation.LegendSubtitle].ToString();



        //            //stationInfoAll.statusTitle = stationStatusDetails.Fields[Templates.StationStatus.StationStatusTitle].ToString();
        //            //stationInfoAll.statusMessage = stationStatusDetails.Fields[Templates.StationStatus.StationStatusDetails].ToString();

        //        }
        //        allType.Add(stationInfoAll);
        //    }

        //    return allType;
        //}

        public StationInformation GetStationInformation(string city)
        {
            StationInformation stationInfo = new StationInformation();
            //Database master = Sitecore.Configuration.Factory.GetDatabase("master");
            Database DB = Sitecore.Context.Database;
            Sitecore.Data.Items.Item stationInfo_Item = DB.GetItem("/sitecore/content/Home/iMap/Configuration/Station Information/" + city);
            if (stationInfo_Item != null)
            {
                stationInfo.stationName = stationInfo_Item.Fields[Templates.StationInformation.StationName].ToString();
                stationInfo.CRSCode = stationInfo_Item.Fields[Templates.StationInformation.CRSCode].ToString();
                if (stationInfo_Item.Fields[Templates.StationInformation.StationAddress].ToString() != null || stationInfo_Item.Fields[Templates.StationInformation.StationAddress].ToString() != "")
                {
                    stationInfo.stationAddress = stationInfo_Item.Fields[Templates.StationInformation.StationAddress].ToString();
                }

                stationInfo.xPos = stationInfo_Item.Fields[Templates.StationInformation.XPox].ToString();
                stationInfo.yPox = stationInfo_Item.Fields[Templates.StationInformation.YPos].ToString();
                stationInfo.status = stationInfo_Item.Fields[Templates.StationInformation.StationStatus].ToString();
                //stationInfo.startDate = stationInfo_Item.Fields[Templates.StationInformation.ValidFrom];//.ToString();
                //stationInfo.endDate = stationInfo_Item.Fields[Templates.StationInformation.ValidTo];//.Value.ToString();
                Sitecore.Data.Fields.DateField startDate = stationInfo_Item.Fields[Templates.StationInformation.ValidFrom];//.ToString();
                Sitecore.Data.Fields.DateField endDate = stationInfo_Item.Fields[Templates.StationInformation.ValidTo];//.Value.ToString();


                //station status
                if (stationInfo.status == null || stationInfo.status == "" || (StatusValid(endDate.DateTime) || startValid(startDate.DateTime)))
                {
                    //default status
                    //stationInfo.statusIcon = null;
                    //stationInfo.statusTitle = null;
                    //stationInfo.statusMessage = null;

                }
                else
                {
                    Sitecore.Data.Items.Item stationStatusDetails = DB.GetItem("/sitecore/content/Home/iMap/Configuration/Station Status/" + stationInfo.status);
                    //stationInfo.statusIcon = stationStatusDetails.Fields[Templates.StationStatus.StationStatusIcon];
                    Sitecore.Data.Fields.ImageField statusIcon = stationStatusDetails.Fields[Templates.StationStatus.StationStatusIcon];
                    if (statusIcon.MediaItem != null)
                        stationInfo.imageURL = Sitecore.Resources.Media.MediaManager.GetMediaUrl(statusIcon.MediaItem);

                    stationInfo.statusTitle = stationStatusDetails.Fields[Templates.StationStatus.StationStatusTitle].ToString();

                    stationInfo.statusMessage = stationStatusDetails.Fields[Templates.StationStatus.StationStatusDetails].ToString();
                    stationInfo.statusMessage = stationStatusDetails.Fields[Templates.StationStatus.StationStatusDetails].Value.Replace("startDate", startDate.ToString());
                    stationInfo.statusMessage = stationInfo.statusMessage.Replace("endDate", endDate.ToString());

                    //Sitecore.Data.Items.Item stationStatusDetails = DB.GetItem("/sitecore/content/Home/iMap/Configuration/Station Status/" + stationInfo.status);
                    //stationInfo.statusIcon = stationStatusDetails.Fields[Templates.StationStatus.StationStatusIcon];
                    //stationInfo.imageURL = Sitecore.Resources.Media.MediaManager.GetMediaUrl(stationInfo.statusIcon.MediaItem);
                    //stationInfo.statusIcon = stationStatusDetails.Fields[Templates.StationStatus.StationStatusIcon];
                    //stationInfo.statusTitle = stationStatusDetails.Fields[Templates.StationStatus.StationStatusTitle].Value;
                    //stationInfo.statusMessage = stationStatusDetails.Fields[Templates.StationStatus.StationStatusDetails].Value.Replace("startDate", stationInfo.startDate.ToString());
                    //stationInfo.statusMessage = stationInfo.statusMessage.Replace("endDate", stationInfo.endDate.ToString());

                }
            }

            return stationInfo;
        }

        [HttpPost]
        public ActionResult GetAllStationInformationNew()
        {
            try
            {
                /*stationInformation_AllType*/
                List<Models.StationInformationDTO> modelFull = GetAllStationInformation();
                return Json(new { response = modelFull }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        public List<Models.StationInformationDTO> /*stationInformation_AllType*/ GetAllStationInformation()
        {
            //  stationInformation_AllType allType = new stationInformation_AllType();
            //allType.stationInformation_All = new List<Models.StationInformation>();
            List<Models.StationInformationDTO> allType = new List<Models.StationInformationDTO>();
            //Database master = Sitecore.Configuration.Factory.GetDatabase("master");
            Database DB = Sitecore.Context.Database;
            Sitecore.Data.Items.Item stationInfo_Item = DB.GetItem("/sitecore/content/Home/iMap/Configuration/Station Information/");
            foreach (Sitecore.Data.Items.Item child in stationInfo_Item.Children)
            {

                Sitecore.Data.Fields.Field temp = child.Fields[Templates.StationInformation.StationName];

                StationInformationDTO stationInfoAll = new StationInformationDTO();
                stationInfoAll.stationName = child.Fields[Templates.StationInformation.StationName].ToString();
                stationInfoAll.CRSCode = child.Fields[Templates.StationInformation.CRSCode].ToString();
                if (child.Fields[Templates.StationInformation.StationAddress].ToString() != null || child.Fields[Templates.StationInformation.StationAddress].ToString() != "")
                {
                    stationInfoAll.stationAddress = child.Fields[Templates.StationInformation.StationAddress].ToString();
                }
                stationInfoAll.stationAddress = child.Fields[Templates.StationInformation.StationAddress].ToString();
                stationInfoAll.xPos = child.Fields[Templates.StationInformation.XPox].ToString();
                stationInfoAll.yPox = child.Fields[Templates.StationInformation.YPos].ToString();
                stationInfoAll.status = child.Fields[Templates.StationInformation.StationStatus].ToString();
                //stationInfoAll.startDate = child.Fields[Templates.StationInformation.ValidFrom];//.Value.ToString();
                //stationInfoAll.endDate = child.Fields[Templates.StationInformation.ValidTo];//.Value.ToString();
                //station status

                Sitecore.Data.Fields.DateField startDate = child.Fields[Templates.StationInformation.ValidFrom];
                Sitecore.Data.Fields.DateField endDate = child.Fields[Templates.StationInformation.ValidTo];

                if (stationInfoAll.status == null || stationInfoAll.status == "" || StatusValid(endDate.DateTime) || startValid(startDate.DateTime))
                {
                        //default status
                        //stationInfo.statusIcon = null;
                        //stationInfo.statusTitle = null;
                        //stationInfo.statusMessage = null;
                }
                else
                {
                    Sitecore.Data.Items.Item stationStatusDetails = DB.GetItem("/sitecore/content/Home/iMap/Configuration/Station Status/" + stationInfoAll.status);
                    //stationInfoAll.statusIcon = stationStatusDetails.Fields[Templates.StationStatus.StationStatusIcon];
                    Sitecore.Data.Fields.ImageField statusIcon = stationStatusDetails.Fields[Templates.StationStatus.StationStatusIcon];
                    stationInfoAll.imageURL = Sitecore.Resources.Media.MediaManager.GetMediaUrl(statusIcon.MediaItem);

                    //stationInfoAll.statusTitle = stationStatusDetails.Fields[Templates.StationStatus.StationStatusTitle].Value;
                    //stationInfoAll.statusMessage = stationStatusDetails.Fields[Templates.StationStatus.StationStatusDetails].Value.Replace("startDate", stationInfoAll.startDate.ToString());
                    //stationInfoAll.statusMessage = stationInfoAll.statusMessage.Replace("endDate", stationInfoAll.endDate.ToString());

                    //Legends legendInfoAll = new Legends();
                    //legendInfoAll.icon = stationStatusDetails.Fields[Templates.LegendInformation.LegendIcon];
                    //legendInfoAll.imageURL = Sitecore.Resources.Media.MediaManager.GetMediaUrl(legendInfoAll.icon.MediaItem);
                    //legendInfoAll.subtitle = child.Fields[Templates.LegendInformation.LegendSubtitle].ToString();



                    //stationInfoAll.statusTitle = stationStatusDetails.Fields[Templates.StationStatus.StationStatusTitle].ToString();
                    //stationInfoAll.statusMessage = stationStatusDetails.Fields[Templates.StationStatus.StationStatusDetails].ToString();

                }
                allType.Add(stationInfoAll);
            }

            return allType;
        }


        private bool StatusValid(DateTime endDate)
        {
            DateTime dateToCompare = endDate.AddHours(5).AddMinutes(30);
            if (DateTime.Compare(dateToCompare, DateTime.Now) < 1)
                return true;
            else
                return false;

        }

        private bool startValid(DateTime startDate)
        {
            DateTime dateToCompare = startDate.AddHours(5).AddMinutes(30);
            if (DateTime.Compare(DateTime.Now, dateToCompare) <  1)
                return true;
            else
                return false;
        }
    }
}