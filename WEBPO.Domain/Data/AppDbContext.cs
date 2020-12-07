using System;
using WEBPO.Domain.Data.Configuration;
using WEBPO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace WEBPO.Domain.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MS_USER> MsUser { get; set; }
        public virtual DbSet<MS_VS> MsVendor { get; set; }
        public virtual DbSet<MS_MNCAT> MsMenucate { get; set; }
        public virtual DbSet<MS_MNFUNC> MsMenuFunc { get; set; }
        public virtual DbSet<MS_MNUSR> MsMenuUser { get; set; }
        public virtual DbSet<TR_PUB> TrPublicMsg { get; set; }
        public virtual DbSet<MS_PIC> MsContractPerson { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new VendorConfig());
            modelBuilder.ApplyConfiguration(new MenuCateConfig());
            modelBuilder.ApplyConfiguration(new MenuFuncConfig());
            modelBuilder.ApplyConfiguration(new MenuUserConfig());
            modelBuilder.ApplyConfiguration(new PublicMessageConfig());
            modelBuilder.ApplyConfiguration(new ContactPersonConfig());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }

}
