using MemberRegistration.Business.Abstarct;
using MemberRegistration.MvcWebUI.Filters;
using MemberRegistration.MvcWebUI.Models;
using MemberRegistrationEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MemberRegistration.MvcWebUI.Controllers
{
    public class MemberController : Controller
    {
        private IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }


        public ActionResult Index()
        {
            var model = new MemberListModel
            {
                Members = _memberService.GetAll()
            };

            return View(model);
        }

        public ActionResult MemberDetails(int id)
        {
            var member = _memberService.GetById(id);
            return View(member);
        }

        [HttpPost]
        [ExceptionHandler]
        public ActionResult Update(int id, string key)
        {
            var member = _memberService.GetById(id);
            if (member != null)
            {
                if (member.Code == key)
                {
                    TempData["give"] = "Başarılı Kod";
                    _memberService.Update(member);
                    return RedirectToAction("MemberDetails/" + member.Id, "Member");
                }
                TempData["error"] = "Geçersiz Kod";
                return RedirectToAction("MemberDetails/" + member.Id, "Member");
            }
            return RedirectToAction("Index", "Member");

        }

        // GET: Member
        public ActionResult Add()
        {
            return View(new MemberAddViewModel());
        }

        [HttpPost]
        [ExceptionHandler]
        public ActionResult Add(Member member)
        {
            if (ModelState.IsValid)
            {
                _memberService.Add(member);
                ViewBag.mesaj = "Kaydınız alınmıştır.Maske kodunuz mail adresinize gönderilecektir.";
                return View();
            }


            return View(new MemberAddViewModel());
        }
    }
}