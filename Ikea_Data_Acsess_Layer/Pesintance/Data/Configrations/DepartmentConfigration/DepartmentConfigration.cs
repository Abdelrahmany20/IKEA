
using Ikea_Data_Acsess_Layer.Models.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ikea_Data_Acsess_Layer.Pesintance.Data.Configrations.DepartmentConfigration
{

    public class DepartmentConfigration : IEntityTypeConfiguration<Departement>
    {
        void IEntityTypeConfiguration<Departement>.Configure(EntityTypeBuilder<Departement> builder)
        {
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);

            builder.Property(D => D.Name).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(D => D.Code).HasColumnType("nvarchar(10)").IsRequired();


            //Development 
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GetDate()");
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GetDate()");


        }
    }
}
