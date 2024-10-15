﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using _300Shine.DataAccessLayer.DBContext;

#nullable disable

namespace _300Shine.DataAccessLayer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241011040748_AddImageUrlUser")]
    partial class AddImageUrlUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.AppointmentDetailEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AppointmentId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StylistId")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("StylistId");

                    b.ToTable("AppointmentDetail");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.AppointmentDetailSlotEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AppointmentDetailId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("SlotId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentDetailId");

                    b.HasIndex("SlotId");

                    b.ToTable("AppointmentSlot");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.AppointmentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SalonId")
                        .HasColumnType("integer");

                    b.Property<int?>("ServiceEntityId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SalonId");

                    b.HasIndex("ServiceEntityId");

                    b.HasIndex("UserId");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.FeedbackEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AppointmentDetailId")
                        .HasColumnType("integer");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentDetailId");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.RevenueEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("SalonId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SalonId");

                    b.ToTable("Revenue");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.SalonEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("Phone")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Salon");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.ServiceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("SalonId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SalonId");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.ServiceStyleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer");

                    b.Property<int>("StyleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.HasIndex("StyleId");

                    b.ToTable("ServiceStyle");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.ShiftEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("MaxStaff")
                        .HasColumnType("integer");

                    b.Property<int>("MinStaff")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SalonId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SalonId");

                    b.ToTable("Shift");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.SlotEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Slot");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.StapleWorkingEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int>("Date")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("Month")
                        .HasColumnType("integer");

                    b.Property<decimal>("SalaryFromService")
                        .HasColumnType("numeric");

                    b.Property<decimal>("SalaryFromWorkingHour")
                        .HasColumnType("numeric");

                    b.Property<decimal>("SalaryPerHour")
                        .HasColumnType("numeric");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StylistId")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StylistId");

                    b.ToTable("StapleWorking");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.StyleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Style")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Style");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.StylistEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Commission")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Salary")
                        .HasColumnType("numeric");

                    b.Property<decimal>("SalaryPerDay")
                        .HasColumnType("numeric");

                    b.Property<int>("SalonId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SalonId");

                    b.HasIndex("UserId");

                    b.ToTable("Stylist");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.StylistShiftEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("ShiftId")
                        .HasColumnType("integer");

                    b.Property<int>("StylistId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ShiftId");

                    b.HasIndex("StylistId");

                    b.ToTable("StylistShift");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.StylistStyleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("StyleId")
                        .HasColumnType("integer");

                    b.Property<int>("StylistId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StyleId");

                    b.HasIndex("StylistId");

                    b.ToTable("StylistStyle");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Gender")
                        .HasColumnType("boolean");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Phone")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("SalonId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("SalonId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.AppointmentDetailEntity", b =>
                {
                    b.HasOne("_300Shine.DataAccessLayer.Entities.AppointmentEntity", "Appointment")
                        .WithMany("AppointmentDetails")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_300Shine.DataAccessLayer.Entities.ServiceEntity", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_300Shine.DataAccessLayer.Entities.StylistEntity", "Stylist")
                        .WithMany("AppointmentDetails")
                        .HasForeignKey("StylistId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Service");

                    b.Navigation("Stylist");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.AppointmentDetailSlotEntity", b =>
                {
                    b.HasOne("_300Shine.DataAccessLayer.Entities.AppointmentDetailEntity", "AppointmentDetail")
                        .WithMany("AppointmentDetailSlots")
                        .HasForeignKey("AppointmentDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_300Shine.DataAccessLayer.Entities.SlotEntity", "Slot")
                        .WithMany("AppointmentSlots")
                        .HasForeignKey("SlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppointmentDetail");

                    b.Navigation("Slot");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.AppointmentEntity", b =>
                {
                    b.HasOne("_300Shine.DataAccessLayer.Entities.SalonEntity", "Salon")
                        .WithMany("Appointments")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("_300Shine.DataAccessLayer.Entities.ServiceEntity", null)
                        .WithMany("Appointments")
                        .HasForeignKey("ServiceEntityId");

                    b.HasOne("_300Shine.DataAccessLayer.Entities.UserEntity", "User")
                        .WithMany("Appointments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Salon");

                    b.Navigation("User");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.FeedbackEntity", b =>
                {
                    b.HasOne("_300Shine.DataAccessLayer.Entities.AppointmentDetailEntity", "AppointmentDetail")
                        .WithMany()
                        .HasForeignKey("AppointmentDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppointmentDetail");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.RevenueEntity", b =>
                {
                    b.HasOne("_300Shine.DataAccessLayer.Entities.SalonEntity", "Salon")
                        .WithMany("Revenues")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.ServiceEntity", b =>
                {
                    b.HasOne("_300Shine.DataAccessLayer.Entities.SalonEntity", "Salon")
                        .WithMany("Services")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.ServiceStyleEntity", b =>
                {
                    b.HasOne("_300Shine.DataAccessLayer.Entities.ServiceEntity", "Service")
                        .WithMany("ServiceStyles")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_300Shine.DataAccessLayer.Entities.StyleEntity", "Style")
                        .WithMany("ServiceStyles")
                        .HasForeignKey("StyleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("Style");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.ShiftEntity", b =>
                {
                    b.HasOne("_300Shine.DataAccessLayer.Entities.SalonEntity", "Salon")
                        .WithMany("Shifts")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.StapleWorkingEntity", b =>
                {
                    b.HasOne("_300Shine.DataAccessLayer.Entities.StylistEntity", "Stylist")
                        .WithMany("StapleWorkings")
                        .HasForeignKey("StylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stylist");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.StylistEntity", b =>
                {
                    b.HasOne("_300Shine.DataAccessLayer.Entities.SalonEntity", "Salon")
                        .WithMany("Stylists")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_300Shine.DataAccessLayer.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Salon");

                    b.Navigation("User");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.StylistShiftEntity", b =>
                {
                    b.HasOne("_300Shine.DataAccessLayer.Entities.ShiftEntity", "Shift")
                        .WithMany("StylistShifts")
                        .HasForeignKey("ShiftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_300Shine.DataAccessLayer.Entities.StylistEntity", "Stylist")
                        .WithMany("StylistShifts")
                        .HasForeignKey("StylistId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Shift");

                    b.Navigation("Stylist");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.StylistStyleEntity", b =>
                {
                    b.HasOne("_300Shine.DataAccessLayer.Entities.StyleEntity", "Style")
                        .WithMany("StylistStyles")
                        .HasForeignKey("StyleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_300Shine.DataAccessLayer.Entities.StylistEntity", "Stylist")
                        .WithMany("StylistStyles")
                        .HasForeignKey("StylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Style");

                    b.Navigation("Stylist");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.UserEntity", b =>
                {
                    b.HasOne("_300Shine.DataAccessLayer.Entities.RoleEntity", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_300Shine.DataAccessLayer.Entities.SalonEntity", "Salon")
                        .WithMany("Users")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.AppointmentDetailEntity", b =>
                {
                    b.Navigation("AppointmentDetailSlots");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.AppointmentEntity", b =>
                {
                    b.Navigation("AppointmentDetails");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.RoleEntity", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.SalonEntity", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Revenues");

                    b.Navigation("Services");

                    b.Navigation("Shifts");

                    b.Navigation("Stylists");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.ServiceEntity", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("ServiceStyles");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.ShiftEntity", b =>
                {
                    b.Navigation("StylistShifts");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.SlotEntity", b =>
                {
                    b.Navigation("AppointmentSlots");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.StyleEntity", b =>
                {
                    b.Navigation("ServiceStyles");

                    b.Navigation("StylistStyles");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.StylistEntity", b =>
                {
                    b.Navigation("AppointmentDetails");

                    b.Navigation("StapleWorkings");

                    b.Navigation("StylistShifts");

                    b.Navigation("StylistStyles");
                });

            modelBuilder.Entity("_300Shine.DataAccessLayer.Entities.UserEntity", b =>
                {
                    b.Navigation("Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}
