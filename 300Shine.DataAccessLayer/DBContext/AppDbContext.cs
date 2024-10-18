
using _300Shine.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<AppointmentDetailEntity> AppointmentDetails { get; set; }
        public DbSet<AppointmentEntity> Appointments { get; set; }
        public DbSet<AppointmentDetailSlotEntity> AppointmentSlots { get; set; }
        public DbSet<FeedbackEntity> Feedbacks { get; set; }
        public DbSet<RevenueEntity> Revenues { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<SalonEntity> Salons { get; set; }
        public DbSet<ServiceEntity> Services { get; set; }
        public DbSet<ServiceStyleEntity> ServiceStyles { get; set; }
        public DbSet<ShiftEntity> Shifts { get; set; }
        public DbSet<SlotEntity> Slots { get; set; }
        public DbSet<StapleWorkingEntity> StapleWorkings { get; set; }
        public DbSet<StyleEntity> Styles { get; set; }
        public DbSet<StylistEntity> Stylists { get; set; }
        public DbSet<StylistShiftEntity> StylistShifts { get; set; }
        public DbSet<StylistStyleEntity> StylistStyles { get; set; }
        public DbSet<UserEntity> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=dpg-crugtplumphs73en7km0-a.oregon-postgres.render.com; Port = 5432; Username = admin; Password = Sg1wdcJKbxrJjY688Qy0YR6324rolk76; Database = prn221_300shinedb; SSL Mode = Require");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureModel(modelBuilder);
        }

        private void ConfigureModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StylistShiftEntity>()
                .HasOne(s => s.Stylist)
                .WithMany(st => st.StylistShifts)
                .HasForeignKey(s => s.StylistId)
                .OnDelete(DeleteBehavior.Restrict);//khi xóa stylist sẽ không xóa bản ghi liên quan đến stylistShift

            modelBuilder.Entity<AppointmentDetailEntity>()
                .HasOne(ad => ad.Stylist)
                .WithMany(s => s.AppointmentDetails) 
                .HasForeignKey(ad => ad.StylistId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<AppointmentEntity>()
                .HasOne(a => a.Salon)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.SalonId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
