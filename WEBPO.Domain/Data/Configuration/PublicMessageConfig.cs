using WEBPO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace WEBPO.Domain.Data.Configuration
{
    class PublicMessageConfig : IEntityTypeConfiguration<TR_PUB>
    {
        public void Configure(EntityTypeBuilder<TR_PUB> entity)
        {
            entity.HasKey(e => new { e.IPubNo, e.IVsCd, e.IRegDate });

            entity.ToTable("TR_PUB");

            entity.Property(e => e.IPubNo)
                .UseIdentityColumn()
                .HasColumnName("I_PUB_NO");

            entity.Property(e => e.IVsCd)
                .HasMaxLength(10)
                .HasColumnName("I_VS_CD");

            entity.Property(e => e.IRegDate)
                .HasColumnType("datetime")
                .HasColumnName("I_REG_DATE");

            entity.Property(e => e.IAllFlg)
                .HasMaxLength(1)
                .HasColumnName("I_ALL_FLG");

            entity.Property(e => e.IEndDate)
                .HasColumnType("datetime")
                .HasColumnName("I_END_DATE");

            entity.Property(e => e.IEntryDate)
                .HasColumnType("datetime")
                .HasColumnName("I_ENTRY_DATE");

            entity.Property(e => e.IMessage)
                .HasColumnName("I_MESSAGE");

            entity.Property(e => e.IReadFlg)
                .HasMaxLength(1)
                .HasColumnName("I_READ_FLG")
                .HasDefaultValueSql("((0))");

            entity.Property(e => e.IStartDate)
                .HasColumnType("datetime")
                .HasColumnName("I_START_DATE");

            entity.Property(e => e.ISubject)
                .HasMaxLength(50)
                .HasColumnName("I_SUBJECT");

            entity.Property(e => e.IUpdDate)
                .HasColumnType("datetime")
                
                .HasColumnName("I_UPD_DATE");

            entity.Property(e => e.IUpdUserId)
                .HasMaxLength(50)
                .HasColumnName("I_UPD_USERID");

            entity.Property(e => e.IUserId)
                .HasMaxLength(10)
                .HasColumnName("I_USER_ID");
        }

    }
}
