﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectManagement.Infrastructure.Data;

#nullable disable

namespace ProjectManagement.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("ProjectManagement.Domain.Models.Achievement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("CompletedProject")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompletionLevel")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DeployedToProduction")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("DeveloperId")
                        .HasColumnType("TEXT");

                    b.Property<int>("OngoingProject")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProjectOnHold")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DeveloperId");

                    b.ToTable("Achievements");
                });

            modelBuilder.Entity("ProjectManagement.Domain.Models.Developer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("DevNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Developers");
                });

            modelBuilder.Entity("ProjectManagement.Domain.Models.Achievement", b =>
                {
                    b.HasOne("ProjectManagement.Domain.Models.Developer", "Developer")
                        .WithMany("Achievements")
                        .HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_Achivements_Driver");

                    b.Navigation("Developer");
                });

            modelBuilder.Entity("ProjectManagement.Domain.Models.Developer", b =>
                {
                    b.Navigation("Achievements");
                });
#pragma warning restore 612, 618
        }
    }
}
