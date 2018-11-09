﻿// <auto-generated />
using Guests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Guests.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20180809144255_InitialSystemUser")]
    partial class InitialSystemUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846");

            modelBuilder.Entity("Guests.Models.SystemUser", b =>
                {
                    b.Property<int>("SystemUserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Login");

                    b.Property<string>("Password");

                    b.HasKey("SystemUserId");

                    b.ToTable("SystemUsers");
                });

            modelBuilder.Entity("Guests.Models.UserInfo", b =>
                {
                    b.Property<int>("UserInfoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<int>("State");

                    b.HasKey("UserInfoId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}