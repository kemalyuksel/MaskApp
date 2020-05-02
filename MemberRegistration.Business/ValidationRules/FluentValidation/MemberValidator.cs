using FluentValidation;
using MemberRegistrationEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistration.Business.ValidationRules.FluentValidation
{
    public class MemberValidator : AbstractValidator<Member>
    {
        public MemberValidator()
        {
            RuleFor(m => m.FirstName).NotEmpty().WithMessage("Adınızı Giriniz");
            RuleFor(m => m.LastName).NotEmpty().WithMessage("Soyadınızı Giriniz");
            RuleFor(m => m.DateOfBirth).NotEmpty().WithMessage("Doğum Tarihinizi Giriniz");
            RuleFor(m => m.Email).NotEmpty().WithMessage("Mail Adresinizi Giriniz");
            RuleFor(m => m.TcNo).NotEmpty().WithMessage("Tc Kimlik Numaranızı Giriniz");
            RuleFor(m => m.Email).EmailAddress();
            RuleFor(m => m.TcNo).Length(11);
        }
    }
}
