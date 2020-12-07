using WEBPO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace WEBPO.Domain.Data.Configuration
{
    class MenuUserConfig : IEntityTypeConfiguration<MS_MNUSR>
    {
        public void Configure(EntityTypeBuilder<MS_MNUSR> entity)
        {
            entity.HasKey(e => new { e.IUserType, e.ISysName, e.ICatId, e.IFnId });

            entity.ToTable("MS_MNUSR");

            entity.HasOne(d => d.MsMnFunc)
                .WithMany(p => p.MsMnUsrs)
                .HasForeignKey(d => d.IFnId)
                .HasPrincipalKey(d => d.IFnId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            entity.Property(e => e.IUserType)
                .HasColumnName("I_USER_TYPE")
                .HasMaxLength(10);

            entity.Property(e => e.ISysName)
                .HasColumnName("I_SYS_NAME")
                .HasMaxLength(10);

            entity.Property(e => e.ICatId)
                .HasColumnName("I_CAT_ID")
                .HasMaxLength(10);

            entity.Property(e => e.IFnId)
                .HasColumnName("I_FN_ID")
                .HasMaxLength(10);

            entity.Property(e => e.IEntryDate)
                .HasColumnName("I_ENTRY_DATE")
                .HasColumnType("datetime");

            entity.Property(e => e.IUpdDate)
                .HasColumnName("I_UPD_DATE")
                .HasColumnType("datetime");

            entity.Property(e => e.IUpdUserId)
                .HasColumnName("I_UPD_USERID")
                .HasMaxLength(10);
        }

    }
}
