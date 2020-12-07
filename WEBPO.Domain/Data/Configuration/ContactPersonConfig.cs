using WEBPO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace WEBPO.Domain.Data.Configuration
{
    class ContactPersonConfig : IEntityTypeConfiguration<MS_PIC>
    {
        public void Configure(EntityTypeBuilder<MS_PIC> entity)
        {
            //entity.HasKey(e => new { e.IPicId, e.IVsCd })
            //        .HasName("PK_MS_PIC");

            entity.HasKey(e => new { e.IPicId })
                    .HasName("PK_MS_PIC");

            entity.ToTable("MS_PIC");

            entity.HasOne(d => d.MsVs)
                    .WithMany(p => p.MsContrPerson)
                    .HasForeignKey(d => d.IVsCd)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

            entity.Property(e => e.IPicId)
                .UseIdentityColumn()
                .HasColumnName("I_PIC_ID");

            entity.Property(e => e.IVsCd)
                .HasMaxLength(10)
                .HasColumnName("I_VS_CD");

            entity.Property(e => e.IEntryDate)
                .HasColumnType("datetime")
                .ValueGeneratedOnAdd()
                .HasColumnName("I_ENTRY_DATE");

            entity.Property(e => e.ILang)
                .HasMaxLength(2)
                .HasColumnName("I_LANG");

            entity.Property(e => e.IMail)
                .HasMaxLength(50)
                .HasColumnName("I_MAIL");

            entity.Property(e => e.IMailFlg)
                .HasMaxLength(10)
                .HasColumnName("I_MAIL_FLG");

            entity.Property(e => e.IMobileNo)
                .HasMaxLength(50)
                .HasColumnName("I_MOBILE_NO");

            entity.Property(e => e.IPicName)
                .HasMaxLength(200)
                .HasColumnName("I_PIC_NAME");

            entity.Property(e => e.ISectionCd)
                .HasMaxLength(10)
                .HasColumnName("I_SECTION_CD");

            entity.Property(e => e.ITelNo)
                .HasMaxLength(50)
                .HasColumnName("I_TEL_NO");

            entity.Property(e => e.IUpdDate)
                .HasColumnType("datetime")
                .ValueGeneratedOnUpdate()
                .HasColumnName("I_UPD_DATE");

            entity.Property(e => e.IUpdUserId)
                .HasMaxLength(10)
                .HasColumnName("I_UPD_USERID");
        }

            
    }
}
