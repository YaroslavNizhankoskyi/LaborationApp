using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Persistance.Configuration
{
    public class TipConfiguration : IEntityTypeConfiguration<Tip>
    {
        public void Configure(EntityTypeBuilder<Tip> builder)
        {
            
        }
    }
}
