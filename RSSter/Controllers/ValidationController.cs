using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Services.RssReader;
using Services.RssReader.Implementation;

namespace RSSter.Controllers
{
    public class ValidationController : Controller
    {
        private readonly IValidateService _validateService;

        public ValidationController(IValidateService validateService)
        {            
            _validateService = validateService;
        } 
        
        public JsonResult IsLinkInUserDatabe(string url)
        {
            var userId = User.Identity.GetUserId();
            var result = _validateService.IsUrlUniqueInUserChannels(userId, url);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsLinkValid(string url)
        {
            var result = _validateService.IsUrlValid(url);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /*
        public JsonResult LinkValidation(string url)
        {
            return Json(_validateService.IsLinkExist(url), JsonRequestBehavior.AllowGet);
        }
         */
    }
}


        ////GET: Validation
        //public JsonResult LinkValidation(string url)
        //{            
        //    return Json(_validateService.IsLinkUniqueInChannels(url), JsonRequestBehavior.AllowGet);
        //}

        //public void ChannelValidation(string url)
        //{
        //    var IsLinkUniqueInChannels = _validateService.IsLinkUniqueInChannels(url);
            
        //}