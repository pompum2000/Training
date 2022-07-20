﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Training.Models;

#nullable disable

namespace Training.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Training.Models.Department", b =>
                {
                    b.Property<int>("IdDepartment")
                        .HasPrecision(10)
                        .HasColumnType("int")
                        .HasColumnName("IdDepartment");

                    b.Property<string>("NameDepartment")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("NameDepartment");

                    b.HasKey("IdDepartment");

                    b.ToTable("Department", "dbo");
                });

            modelBuilder.Entity("Training.Models.NhanVien", b =>
                {
                    b.Property<int>("IdNv")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(10)
                        .HasColumnType("int")
                        .HasColumnName("IdNv");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdNv"), 1L, 1);

                    b.Property<int>("IdDepartment")
                        .HasPrecision(10)
                        .HasColumnType("int")
                        .HasColumnName("IdDepartment");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Name");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Position");

                    b.HasKey("IdNv");

                    b.HasIndex("IdDepartment");

                    b.ToTable("NhanVien", "dbo");
                });

            modelBuilder.Entity("Training.Models.NhanVien", b =>
                {
                    b.HasOne("Training.Models.Department", "Department")
                        .WithMany("NhanVien")
                        .HasForeignKey("IdDepartment")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Training.Models.Department", b =>
                {
                    b.Navigation("NhanVien");
                });
#pragma warning restore 612, 618
        }
    }
}
