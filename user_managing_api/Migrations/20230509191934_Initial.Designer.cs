﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using user_managing_api.Models;

#nullable disable

namespace user_managing_api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230509191934_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("user_managing_api.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("User_GroupId")
                        .HasColumnType("bigint");

                    b.Property<long?>("User_StateId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("User_GroupId");

                    b.HasIndex("User_StateId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("user_managing_api.Models.User_Group", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("user_managing_api.Models.User_State", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserStates");
                });

            modelBuilder.Entity("user_managing_api.Models.User", b =>
                {
                    b.HasOne("user_managing_api.Models.User_Group", "User_Group")
                        .WithMany("Users")
                        .HasForeignKey("User_GroupId");

                    b.HasOne("user_managing_api.Models.User_State", "User_State")
                        .WithMany("Users")
                        .HasForeignKey("User_StateId");

                    b.Navigation("User_Group");

                    b.Navigation("User_State");
                });

            modelBuilder.Entity("user_managing_api.Models.User_Group", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("user_managing_api.Models.User_State", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}