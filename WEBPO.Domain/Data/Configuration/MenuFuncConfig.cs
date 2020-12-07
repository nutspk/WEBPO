using WEBPO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace WEBPO.Domain.Data.Configuration
{
    class MenuFuncConfig : IEntityTypeConfiguration<MS_MNFUNC>
    {
        public void Configure(EntityTypeBuilder<MS_MNFUNC> entity)
        {
            entity.HasKey(e => new { e.ISysName, e.IFnId, e.IFnName, e.ICatId, e.ISort });

            entity.ToTable("MS_MNFUNC");

            entity.HasMany(d => d.MsMnUsrs)
                .WithOne(p => p.MsMnFunc)
                .HasForeignKey(d => d.IFnId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            entity.Property(e => e.ISysName)
                .HasColumnName("I_SYS_NAME")
                .HasMaxLength(10);

            entity.Property(e => e.IFnId)
                .HasColumnName("I_FN_ID")
                .HasMaxLength(10);

            entity.Property(e => e.IFnName)
                .HasColumnName("I_FN_NAME")
                .HasMaxLength(50);

            entity.Property(e => e.ICatId)
                .HasColumnName("I_CAT_ID")
                .HasMaxLength(20);

            entity.Property(e => e.ISort)
                .HasColumnName("I_SORT")
                .HasColumnType("numeric(3, 0)");

            entity.Property(e => e.IEntryDate)
                .HasColumnName("I_ENTRY_DATE")
                .HasColumnType("datetime");

            entity.Property(e => e.IStatus)
                .HasColumnName("I_STATUS")
                .HasMaxLength(100);

            entity.Property(e => e.IUpdDate)
                .HasColumnName("I_UPD_DATE")
                .HasColumnType("datetime");

            entity.Property(e => e.IUpdUserId)
                .HasColumnName("I_UPD_USERID")
                .HasMaxLength(10);

            entity.Property(e => e.IUrl)
                .HasColumnName("I_URL")
                .HasMaxLength(100);
        }
    }
}
