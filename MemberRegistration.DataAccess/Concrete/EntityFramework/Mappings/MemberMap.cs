using MemberRegistrationEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistration.DataAccess.Concrete.EntityFramework.Mappings
{
    public class MemberMap : EntityTypeConfiguration<Member>
    {
        public MemberMap()
        {
            ToTable(@"Members", @"dbo");
            HasKey(x => x.Id);

            Property(x => x.FirstName).HasColumnName("FirstName");
            Property(x => x.LastName).HasColumnName("LastName");
            Property(x => x.DateOfBirth).HasColumnName("DateOfBirth");
            Property(x => x.Email).HasColumnName("Email");
            Property(x => x.IsMask).HasColumnName("IsMask");
            Property(x => x.Code).HasColumnName("Code");
            Property(x => x.TcNo).HasColumnName("TcNo");

        }
    }
}
