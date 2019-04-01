﻿// <auto-generated />
using System;
using ClientCommon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ClientCommon.Migrations
{
    [DbContext(typeof(ClientDBContext))]
    [Migration("20190401183828_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("ClientCommon.Task", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TaskTypeId");

                    b.Property<string>("Text");

                    b.HasKey("TaskId");

                    b.HasIndex("TaskTypeId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ClientCommon.TaskInstance", b =>
                {
                    b.Property<int>("TaskInstanceId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("SeqNo");

                    b.Property<int>("TaskId");

                    b.Property<int>("TestId");

                    b.HasKey("TaskInstanceId");

                    b.HasIndex("TaskId");

                    b.HasIndex("TestId");

                    b.ToTable("TaskInstances");
                });

            modelBuilder.Entity("ClientCommon.TaskItem", b =>
                {
                    b.Property<int>("TaskItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ParentId");

                    b.Property<int>("SeqNo");

                    b.Property<int?>("TaskId");

                    b.Property<int?>("TaskInstanceId");

                    b.Property<int>("TaskItemTypeId");

                    b.Property<int?>("ValueInt");

                    b.Property<string>("ValueString");

                    b.HasKey("TaskItemId");

                    b.HasIndex("ParentId");

                    b.HasIndex("TaskId");

                    b.HasIndex("TaskInstanceId");

                    b.HasIndex("TaskItemTypeId");

                    b.ToTable("TaskItems");
                });

            modelBuilder.Entity("ClientCommon.TaskItemType", b =>
                {
                    b.Property<int>("TaskItemTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("TaskItemTypeId");

                    b.ToTable("TaskItemTypes");
                });

            modelBuilder.Entity("ClientCommon.TaskType", b =>
                {
                    b.Property<int>("TaskTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("TaskTypeId");

                    b.ToTable("TaskTypes");
                });

            modelBuilder.Entity("ClientCommon.Test", b =>
                {
                    b.Property<int>("TestId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Header");

                    b.Property<int>("UserId");

                    b.HasKey("TestId");

                    b.HasIndex("UserId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("ClientCommon.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ClientCommon.Task", b =>
                {
                    b.HasOne("ClientCommon.TaskType", "TaskType")
                        .WithMany("Tasks")
                        .HasForeignKey("TaskTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ClientCommon.TaskInstance", b =>
                {
                    b.HasOne("ClientCommon.Task", "Task")
                        .WithMany("TaskInstances")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ClientCommon.Test", "Test")
                        .WithMany("TaskInstances")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ClientCommon.TaskItem", b =>
                {
                    b.HasOne("ClientCommon.TaskItem", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.HasOne("ClientCommon.Task", "Task")
                        .WithMany("TaskItems")
                        .HasForeignKey("TaskId");

                    b.HasOne("ClientCommon.TaskInstance", "TaskInstance")
                        .WithMany("TaskItems")
                        .HasForeignKey("TaskInstanceId");

                    b.HasOne("ClientCommon.TaskItemType", "TaskItemType")
                        .WithMany("TaskItems")
                        .HasForeignKey("TaskItemTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ClientCommon.Test", b =>
                {
                    b.HasOne("ClientCommon.User", "User")
                        .WithMany("Tests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}