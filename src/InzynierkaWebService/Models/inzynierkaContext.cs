using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InzynierkaWebService.Models
{
    //public partial class InzynierkaContext : DbContext
    public partial class CostSharingContext : DbContext
    {
        public virtual DbSet<CostParticipants> CostParticipants { get; set; }
        public virtual DbSet<CostTypes> CostTypes { get; set; }
        public virtual DbSet<Costs> Costs { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Instances> Instances { get; set; }
        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<Notes> Notes { get; set; }
        public virtual DbSet<OperationsLog> OperationsLog { get; set; }
        public virtual DbSet<OperationsLogParticipants> OperationsLogParticipants { get; set; }
        public virtual DbSet<Settlements> Settlements { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //            optionsBuilder.UseSqlServer(@"Data Source=PAWLOWYPC;Initial Catalog=inzynierka;Integrated Security=True;Trusted_Connection=True;");
        //        }

        public CostSharingContext(DbContextOptions<CostSharingContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CostParticipants>(entity =>
            {
                entity.HasKey(e => e.DesignId)
                    .HasName("PK_COST_PARTICIPANTS");

                entity.ToTable("COST_PARTICIPANTS");

                entity.Property(e => e.DesignId)
                    .HasColumnName("DESIGN_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CostId).HasColumnName("COST_ID");

                entity.Property(e => e.MemberId).HasColumnName("MEMBER_ID");

                entity.Property(e => e.Paid)
                    .HasColumnName("PAID")
                    .HasColumnType("money");

                entity.Property(e => e.WholeAmount)
                    .HasColumnName("WHOLE_AMOUNT")
                    .HasColumnType("money");

                entity.HasOne(d => d.Cost)
                    .WithMany(p => p.CostParticipants)
                    .HasForeignKey(d => d.CostId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("cost participant belongs to cost");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.CostParticipants)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("cost participant corresponds to member");
            });

            modelBuilder.Entity<CostTypes>(entity =>
            {
                entity.HasKey(e => e.CostTypeId)
                    .HasName("PK_COST_TYPES");

                entity.ToTable("COST_TYPES");

                entity.Property(e => e.CostTypeId)
                    .HasColumnName("COST_TYPE_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EqualDivision).HasColumnName("EQUAL_DIVISION");

                entity.Property(e => e.InstanceId).HasColumnName("INSTANCE_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.OwnerId).HasColumnName("OWNER_ID");

                entity.HasOne(d => d.Instance)
                    .WithMany(p => p.CostTypes)
                    .HasForeignKey(d => d.InstanceId)
                    .HasConstraintName("cost type is saved for instance");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.CostTypes)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("cost type is saved for user");
            });

            modelBuilder.Entity<Costs>(entity =>
            {
                entity.HasKey(e => e.CostId)
                    .HasName("PK_COSTS");

                entity.ToTable("COSTS");

                entity.Property(e => e.CostId)
                    .HasColumnName("COST_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount)
                    .HasColumnName("AMOUNT")
                    .HasColumnType("money");

                entity.Property(e => e.CostTypeId).HasColumnName("COST_TYPE_ID");

                entity.Property(e => e.InstanceId).HasColumnName("INSTANCE_ID");

                entity.Property(e => e.MemberId).HasColumnName("MEMBER_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.CostType)
                    .WithMany(p => p.Costs)
                    .HasForeignKey(d => d.CostTypeId)
                    .HasConstraintName("cost is of cost type");

                entity.HasOne(d => d.Instance)
                    .WithMany(p => p.Costs)
                    .HasForeignKey(d => d.InstanceId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("cost belongs to instance");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Costs)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("member creates cost");
            });

            //modelBuilder.Entity<Groups>().Ignore(g => g.GroupOwnerNavigation);
            modelBuilder.Entity<Groups>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("PK_GROUPS");

                entity.ToTable("GROUPS");

                entity.HasIndex(e => e.GroupOwner)
                    .HasName("IX_GROUPS");

                entity.Property(e => e.GroupId)
                    .HasColumnName("GROUP_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasColumnName("GROUP_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.GroupOwner).HasColumnName("GROUP_OWNER");

                entity.HasOne(d => d.GroupOwnerNavigation)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.GroupOwner)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("user creates group");
            });


            modelBuilder.Entity<Instances>(entity =>
            {
                entity.HasKey(e => e.InstanceId)
                    .HasName("PK_INSTANCES");

                entity.ToTable("INSTANCES");

                entity.Property(e => e.InstanceId)
                    .HasColumnName("INSTANCE_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.GroupId).HasColumnName("GROUP_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Instances)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("instance belongs to group");
            });

            modelBuilder.Entity<Members>(entity =>
            {
                entity.HasKey(e => e.MemberId)
                    .HasName("PK_MEMBERS");

                entity.ToTable("MEMBERS");

                entity.Property(e => e.MemberId)
                    .HasColumnName("MEMBER_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CorrespondingUserId).HasColumnName("CORRESPONDING_USER_ID");

                entity.Property(e => e.GroupId).HasColumnName("GROUP_ID");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.CorrespondingUser)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.CorrespondingUserId)
                    .HasConstraintName("member corresponds to user");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("member belongs to");
            });

            modelBuilder.Entity<Notes>(entity =>
            {
                entity.HasKey(e => e.NoteId)
                    .HasName("PK_NOTES");

                entity.ToTable("NOTES");

                entity.Property(e => e.NoteId)
                    .HasColumnName("NOTE_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Contents)
                    .IsRequired()
                    .HasColumnName("CONTENTS");

                entity.Property(e => e.CostParentId).HasColumnName("COST_PARENT_ID");

                entity.Property(e => e.ImagePath)
                    .HasColumnName("IMAGE_PATH")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.NoteParentId).HasColumnName("NOTE_PARENT_ID");

                entity.Property(e => e.OwnerId).HasColumnName("OWNER_ID");

                entity.HasOne(d => d.CostParent)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.CostParentId)
                    .HasConstraintName("note is child of cost");

                entity.HasOne(d => d.Note)
                    .WithOne(p => p.InverseNote)
                    .HasForeignKey<Notes>(d => d.NoteId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("note is child of note");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("member creates note");
            });

            modelBuilder.Entity<OperationsLog>(entity =>
            {
                entity.ToTable("OPERATIONS_LOG");

                entity.Property(e => e.OperationsLogId)
                    .HasColumnName("OPERATIONS_LOG_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount)
                    .HasColumnName("AMOUNT")
                    .HasColumnType("money");

                entity.Property(e => e.CostId).HasColumnName("COST_ID");

                entity.Property(e => e.CostTypeId).HasColumnName("COST_TYPE_ID");

                entity.Property(e => e.MemberId).HasColumnName("MEMBER_ID");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.OperationType).HasColumnName("OPERATION_TYPE");

                entity.Property(e => e.ParticipantsChanged).HasColumnName("PARTICIPANTS_CHANGED");

                entity.HasOne(d => d.Cost)
                    .WithMany(p => p.OperationsLog)
                    .HasForeignKey(d => d.CostId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("operation concerns cost");

                entity.HasOne(d => d.CostType)
                    .WithMany(p => p.OperationsLog)
                    .HasForeignKey(d => d.CostTypeId)
                    .HasConstraintName("cost type changed by operation");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.OperationsLog)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("member performs operation");
            });

            modelBuilder.Entity<OperationsLogParticipants>(entity =>
            {
                entity.HasKey(e => e.OperationsLogParticipantId)
                    .HasName("PK_OPERATIONS_LOG_PARTICIPANTS");

                entity.ToTable("OPERATIONS_LOG_PARTICIPANTS");

                entity.Property(e => e.OperationsLogParticipantId)
                    .HasColumnName("OPERATIONS_LOG_PARTICIPANT_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OperationLogId).HasColumnName("OPERATION_LOG_ID");

                entity.Property(e => e.Paid)
                    .HasColumnName("PAID")
                    .HasColumnType("money");

                entity.Property(e => e.ParticipantId).HasColumnName("PARTICIPANT_ID");

                entity.Property(e => e.ParticipantOperationType).HasColumnName("PARTICIPANT_OPERATION_TYPE");

                entity.Property(e => e.WholeAmount)
                    .HasColumnName("WHOLE_AMOUNT")
                    .HasColumnType("money");

                entity.HasOne(d => d.OperationLog)
                    .WithMany(p => p.OperationsLogParticipants)
                    .HasForeignKey(d => d.OperationLogId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("participants changed by operation");

                entity.HasOne(d => d.Participant)
                    .WithMany(p => p.OperationsLogParticipants)
                    .HasForeignKey(d => d.ParticipantId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("participants changed correspond to cost participants");
            });

            modelBuilder.Entity<Settlements>(entity =>
            {
                entity.HasKey(e => e.SettlementId)
                    .HasName("PK_SETTLEMENTS");

                entity.ToTable("SETTLEMENTS");

                entity.Property(e => e.SettlementId)
                    .HasColumnName("SETTLEMENT_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CostId).HasColumnName("COST_ID");

                entity.Property(e => e.MemberInDebt).HasColumnName("MEMBER_IN_DEBT");

                entity.Property(e => e.MemberOwed).HasColumnName("MEMBER_OWED");

                entity.Property(e => e.Value)
                    .HasColumnName("VALUE")
                    .HasColumnType("money");

                entity.HasOne(d => d.Cost)
                    .WithMany(p => p.Settlements)
                    .HasForeignKey(d => d.CostId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("settlement is child of cost");

                entity.HasOne(d => d.MemberInDebtNavigation)
                    .WithMany(p => p.SettlementsMemberInDebtNavigation)
                    .HasForeignKey(d => d.MemberInDebt)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("member is in debt");

                entity.HasOne(d => d.MemberOwedNavigation)
                    .WithMany(p => p.SettlementsMemberOwedNavigation)
                    .HasForeignKey(d => d.MemberOwed)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("member is owed");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_USERS");

                entity.ToTable("USERS");

                entity.Property(e => e.UserId)
                    .HasColumnName("USER_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("LOGIN")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("SURNAME")
                    .HasMaxLength(50);
            });
        }
    }
}