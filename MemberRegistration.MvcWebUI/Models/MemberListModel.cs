using DevFramework.Core.Entities;
using MemberRegistrationEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberRegistration.MvcWebUI.Models
{
    public class MemberListModel :IEntity
    {
        public List<Member> Members { get; set; }
    }
}