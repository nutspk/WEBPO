using WEBPO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace WEBPO.Domain.Data.Configuration
{
    class UserConfig : IEntityTypeConfiguration<MS_USER>
    {
        public void Configure(EntityTypeBuilder<MS_USER> entity)
        {
            entity.ToTable("MS_USER");

            entity.HasKey(e => e.IUserId);

            entity.HasOne(d => d.MS_VS)
                    .WithMany(p => p.MsUsers)
                    .HasForeignKey(d => d.IVsCd)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

            entity.Property(e => e.IUserId)
                .HasColumnName("I_USER_ID")
                .HasMaxLength(10);

            entity.Property(e => e.IEntryDate)
                .HasColumnName("I_ENTRY_DATE")
                .HasColumnType("datetime");

            //entity.Property(e => e.IGroupId)
            //    .HasColumnName("I_GROUP_ID")
            //    .HasMaxLength(10);

            entity.Property(e => e.ILang)
                .HasColumnName("I_LANG")
                .HasMaxLength(2);

            entity.Property(e => e.IMail)
                .HasColumnName("I_MAIL")
                .HasMaxLength(50);

            entity.Property(e => e.IMailFlg)
                .HasColumnName("I_MAIL_FLG")
                .HasMaxLength(1);

            //entity.Property(e => e.IMngType)
            //    .HasColumnName("I_MNG_TYPE")
            //    .HasMaxLength(1);

            entity.Property(e => e.ISectionCd)
                .HasColumnName("I_SECTION_CD")
                .HasMaxLength(10);

            //entity.Property(e => e.ITelNo)
            //    .HasColumnName("I_TEL_NO")
            //    .HasMaxLength(20);

            entity.Property(e => e.IUpdDate)
                .HasColumnName("I_UPD_DATE")
                .HasColumnType("datetime");

            entity.Property(e => e.IUpdUserId)
                .HasColumnName("I_UPD_USERID")
                .HasMaxLength(10);

            entity.Property(e => e.IUserName)
                .HasColumnName("I_USER_NAME")
                .HasMaxLength(100);

            entity.Property(e => e.IUserType)
                .HasColumnName("I_USER_TYPE")
                .HasMaxLength(10);

            entity.Property(e => e.IVsCd)
                .HasColumnName("I_VS_CD")
                .HasMaxLength(10);

            entity.Property(e => e.IUserPwd)
                .HasColumnName("I_PASS")
                .HasMaxLength(100);

            entity.Property(e => e.IResetPin)
               .HasColumnName("I_RESET_PIN")
               .HasMaxLength(10);
        }
    }
}
