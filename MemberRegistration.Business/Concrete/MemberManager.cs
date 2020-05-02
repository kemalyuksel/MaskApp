using MemberRegistration.Business.Abstarct;
using MemberRegistration.DataAccess.Abstract;
using MemberRegistrationEntities.Concrete;
using System;
using System.Collections.Generic;
using MemberRegistration.Business.KpsServiceReference;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberRegistration.Business.ServiceAdapters;
using MemberRegistration.Business.ValidationRules.FluentValidation;
using DevFramework.Core.Aspects.Postsharp.ValidationAspects;
using AutoMapper;
using System.Net.Mail;
using System.Net;

namespace MemberRegistration.Business.Concrete
{
    public class MemberManager : IMemberService
    {
        private IMemberDal _memberDal;
        private readonly IMapper _mapper;
        private IKpsService _kpsService;

        public MemberManager(IMemberDal memberDal, IKpsService kpsService, IMapper mapper)
        {
            _memberDal = memberDal;
            _kpsService = kpsService;
            _mapper = mapper;
        }

        private void CheckIfMemberExist(Member member)
        {
            if (_memberDal.Get(m => m.TcNo == member.TcNo) != null)
            {
                throw new Exception("Bu kullanıcı zaten kayıtlı.");
            }
        }

        private void CheckIfMemberValidFromKps(Member member)
        {
            if (_kpsService.ValidateUser(member) == false)
            {
                throw new Exception("Hatalı kullanıcı doğrulaması.");
            }
        }

        private void CodeGenerator(Member member)
        {
            Random random = new Random();
            int rndm1, rndm2, rndm3;
            int chr;
            rndm1 = random.Next(1, 9);
            rndm2 = random.Next(0, 9);
            rndm3 = random.Next(0, 9);
            chr = random.Next(65, 91);
            char chrc = Convert.ToChar(chr);

            member.Code = rndm1.ToString() + rndm2.ToString() + rndm3.ToString() + chrc;
        }

        public void Mail(string sendMailAdress, string body, string code)
        {
            SmtpClient client = new SmtpClient();
            MailAddress from = new MailAddress("mailadresi@gmail.com");
            MailAddress to = new MailAddress(sendMailAdress);
            MailMessage msg = new MailMessage(from, to);
            msg.IsBodyHtml = true;
            msg.Subject = "Maske Kodunuz";
            msg.Body += "<h3> " + body + "</h3>" + "<h2> " + code + " </h2>" + " <p>" + " ile maskenizi alabilirsiniz." + "<p>";
            NetworkCredential info = new NetworkCredential("mailadresiniz", "şifreniz");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Credentials = info;
            client.Send(msg);
        }

        [FluentValidationAspect(typeof(MemberValidator))]
        public void Add(Member member)
        {
            CheckIfMemberExist(member);
            CheckIfMemberValidFromKps(member);
            CodeGenerator(member);
            Mail(member.Email, "MASKE KODUNUZ: ", member.Code);

            member.IsMask = false;
            _memberDal.Add(member);
        }

        public List<Member> GetAll()
        {

            var members = _mapper.Map<List<Member>>(_memberDal.GetList());

            return members;
        }

        public Member GetById(int id)
        {
            return _memberDal.Get(p => p.Id == id);
        }

        public Member Update(Member member)
        {
            if (member.IsMask == true)
            {
                throw new Exception("Maske zaten alınmış.");
            }
            member.IsMask = true;
            return _memberDal.Update(member);
        }
    }
}
