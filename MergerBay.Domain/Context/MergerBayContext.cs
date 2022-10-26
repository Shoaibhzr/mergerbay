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
using MergerBay.Domain.Entities.Company;
using MergerBay.Domain.Entities.Deals;
using MergerBay.Domain.Entities.Smtp;
using MergerBay.Domain.Entities.Logs;

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
        public DbSet<Property> Properties { get; set; }
        public DbSet<ContractDuration> ContractDurations{ get; set; }
        public DbSet<City> Cities{ get; set; }
        public DbSet<RevenuePreference> RevenuePreferences{ get; set; }
        public DbSet<NotificationSetting> NotificationSetting { get; set; }
        public DbSet<TransactionType> TransactionTypes{ get; set; }
        public DbSet<Currency> Currencies{ get; set; }
        public DbSet<Attachments> AttachmentsGeneral { get; set; }
        public DbSet<CompanyCategory> CompanyCategory { get; set; }
        public DbSet<Smtp> Smtp { get; set; }
        public DbSet<CompanyCategoryStore> CompanyCategoryStore { get; set; }
        public DbSet<SectorDetail> SectorDetail { get; set; }
        public DbSet<SectorDetail_Items> SectorDetailItem { get; set; }
        public DbSet<SectorMain> SectorMain { get; set; }
        public DbSet<RecommendedDeal> RecommendedDeals{ get; set; }
        #endregion

        #region Forms
        public DbSet<SellOut> SellOuts { get; set; }
        public DbSet<BuyOut> BuyOuts { get; set; }
        #endregion

        #region ApplicationLogs
        public DbSet<LogEntity> Logs{ get; set; }
        #endregion

        #region StoredProcedures

        public DbSet<SP_SELLOUTS> SP_SELLOUTS { get; set; }
        public DbSet<SP_BUYOUTS> SP_BUYOUTS { get; set; }
        public DbSet<SP_RecommendedDeals> SP_RecommendedDeals { get; set; }
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
            modelBuilder.Entity<SP_SELLOUTS>().HasNoKey();
            modelBuilder.Entity<SP_BUYOUTS>().HasNoKey();
            modelBuilder.Entity<SP_RecommendedDeals>().HasNoKey();

        }
    }
}
