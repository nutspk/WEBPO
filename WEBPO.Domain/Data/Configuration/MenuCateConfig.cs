using WEBPO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace WEBPO.Domain.Data.Configuration
{
    class MenuCateConfig : IEntityTypeConfiguration<MS_MNCAT>
    {
        public void Configure(EntityTypeBuilder<MS_MNCAT> entity)
        {
            entity.HasKey(e => e.ICatId);

            entity.ToTable("MS_MNCAT");

            entity.Property(e => e.ICatId)
                .HasColumnName("I_CAT_ID")
                .HasMaxLength(10);

            entity.Property(e => e.ICatName)
                        .HasColumnName("I_CAT_NAME")
                        .HasMaxLength(100);

            entity.Property(e => e.IEntryDate)
                        .HasColumnName("I_ENTRY_DATE")
                        .HasColumnType("datetime");

            entity.Property(e => e.ISort)
                        .HasColumnName("I_SORT")
                        .HasColumnType("numeric(3, 0)");

            entity.Property(e => e.IUpdDate)
                        .HasColumnName("I_UPD_DATE")
                        .HasColumnType("datetime");

            entity.Property(e => e.IUpdUserId)
                        .HasColumnName("I_UPD_USERID")
                        .HasMaxLength(10);
        }

            
    }
}
