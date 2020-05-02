using MemberRegistrationEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistration.Business.Abstarct
{
    public interface IMemberService
    {
        void Add(Member member);
        List<Member> GetAll();
        Member GetById(int id);
        Member Update(Member member);

    }
}
