using MergerBay.Utilities;
using MergerBay.Domain.Entities.Login;
using MergerBay.Domain.Entities.Seller;
using MergerBay.Domain.Entities.Setups;
using MergerBay.Domain.Entities.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MergerBay.Domain.Entities.Sectors;

namespace MergerBay.Domain.Context
{
    public class MergerBayContext: IdentityDbContext
    {
        public DbSet<Tbl_User_Management> UserPersonalInformation { get; set; }
        public DbSet<Authentication> Authentication { get; set; }

        #region Setup Properties
        public DbSet<Country> Countries { get; set; }
        public DbSet<EstablishmentYears> EstablishmentYears { get; set; }
        public DbSet<OwnerShipPrefs> OwnerShipPrefs { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<SubSectors> SubSectors { get; set; }
        public DbSet<SubSector_Items> SubSector_Items { get; set; }
        public DbSet<TransactionRoles> TransactionRoles { get; set; }

        public DbSet<SectorDetail> SectorDetail { get; set; }
        public DbSet<SectorDetail_Items> SectorDetailItem { get; set; }
        public DbSet<SectorMain> SectorMain { get; set; }
        #endregion

        #region Seller
        public DbSet<SellOut> SellOuts { get; set; }
        #endregion
        public MergerBayContext(DbContextOptions<MergerBayContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.UseSqlServer(Config.ConnectionString);
            //optionsBuilder.UseNpgsql(Config.Config.WarehouseConnectionStringOverride);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tbl_User_Management>();
        }
        }
}
