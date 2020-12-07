using WEBPO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace WEBPO.Domain.Data.Configuration
{
    class VendorConfig : IEntityTypeConfiguration<MS_VS>
    {
        public void Configure(EntityTypeBuilder<MS_VS> entity)
        {
            entity.ToTable("MS_VS");
            entity.HasKey(e => e.IVsCd);

            entity.Property(e => e.IVsCd)
                .HasColumnName("I_VS_CD")
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.IEdiType)
                .HasColumnName("I_EDI_TYPE")
                .HasMaxLength(2);

            entity.Property(e => e.IEntryDate)
                        .HasColumnName("I_ENTRY_DATE")
                        .HasColumnType("datetime");

            entity.Property(e => e.IGroupId)
                        .HasColumnName("I_GROUP_ID")
                        .HasMaxLength(10);

            entity.Property(e => e.IPathId)
                        .HasColumnName("I_PATH_ID")
                        .HasMaxLength(100);

            entity.Property(e => e.ISectionCd)
                        .HasColumnName("I_SECTION_CD")
                        .HasMaxLength(10);

            entity.Property(e => e.IUpdDate)
                        .HasColumnName("I_UPD_DATE")
                        .HasColumnType("datetime");

            entity.Property(e => e.IUpdUserId)
                        .HasColumnName("I_UPD_USERID")
                        .HasMaxLength(8);

            entity.Property(e => e.IVsCd)
                        .HasColumnName("I_VS_CD")
                        .HasMaxLength(10);

            entity.Property(e => e.IVsDesc)
                        .HasColumnName("I_VS_DESC")
                        .HasMaxLength(100);

            entity.Property(e => e.IVsType)
                        .HasColumnName("I_VS_TYPE")
                        .HasMaxLength(2);
        }

               
    }
}
