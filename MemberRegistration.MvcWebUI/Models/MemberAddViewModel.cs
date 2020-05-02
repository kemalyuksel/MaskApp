using DevFramework.Core.Entities;
using MemberRegistrationEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberRegistration.MvcWebUI.Models
{
    public class MemberAddViewModel : IEntity
    {
        public Member Member { get; set; }
    }
}