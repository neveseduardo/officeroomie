﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Data;

#nullable disable

namespace WebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("WebApi.Models.Client", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("created_at")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("email")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<string>("updated_at")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("WebApi.Models.Reservation", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("client_id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("created_at")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("finish_hour")
                        .HasColumnType("TEXT");

                    b.Property<string>("initial_hour")
                        .HasColumnType("TEXT");

                    b.Property<string>("reservation_date")
                        .HasColumnType("TEXT");

                    b.Property<int>("room_id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("status")
                        .HasColumnType("TEXT");

                    b.Property<string>("updated_at")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("user_id")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("WebApi.Models.Room", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("capacity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("category_id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("created_at")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("description")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<string>("updated_at")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("user_id")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("WebApi.Models.RoomCategory", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("cescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("created_at")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("updated_at")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("user_id")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("RoomCategories");
                });

            modelBuilder.Entity("WebApi.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("created_at")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("email")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<string>("password")
                        .HasColumnType("TEXT");

                    b.Property<string>("updated_at")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
