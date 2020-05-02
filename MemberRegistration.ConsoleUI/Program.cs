using MemberRegistration.Business.Abstarct;
using MemberRegistration.Business.DependencyResolvers.Ninject;
using MemberRegistrationEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistration.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int code = random.Next(65, 91);
            string codes = code.ToString();

            var memberService = InstanceFactory.GetInstance<IMemberService>();
            memberService.Add(new Member
            {
                FirstName = "",
                LastName = "",
                DateOfBirth = new DateTime(1998, 5, 22),
                TcNo = "",
                Email = "",
                

            });
            Console.WriteLine("Eklendi");
            Console.ReadLine();

        }
    }
}
