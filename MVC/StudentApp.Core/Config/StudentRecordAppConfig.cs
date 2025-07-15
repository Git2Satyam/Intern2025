using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentApp.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Config
{
    public class StudentRecordAppConfig : IEntityTypeConfiguration<StudentRecord>
    {
        public void Configure(EntityTypeBuilder<StudentRecord> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
