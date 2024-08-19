using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Qinshit.DataAccess.Domain
{
    public partial class NotesScaffoldDbContext : DbContext
    {
        public NotesScaffoldDbContext()
        {
        }

        public NotesScaffoldDbContext(DbContextOptions<NotesScaffoldDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Note> Notes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=NotesScaffoldDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			#region Notes
			modelBuilder.Entity<Note>(entity =>
			{
				entity.Property(e => e.Color).HasMaxLength(30);

				entity.Property(e => e.Text).HasMaxLength(100);

				entity.HasOne(d => d.User)
					.WithMany(p => p.Notes)
					.HasForeignKey(d => d.UserId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Notes__UserId__267ABA7A");
			});
			#endregion

			#region Users
			modelBuilder.Entity<User>(entity =>
			{
				entity.Property(e => e.FirstName).HasMaxLength(50);

				entity.Property(e => e.LastName).HasMaxLength(50);

				entity.Property(e => e.Username).HasMaxLength(30);
			});
            #endregion

            modelBuilder.Entity<Meal>(entity =>
            {
                entity.ToTable("Meals");

                entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("MealName")
                .IsRequired();

                entity.HasOne(x => x.User)
                .WithMany(x => x.Meals)
                .HasForeignKey(x => x.UserId)
                .HasConstraintName("MyCustomUserId_FK");


                // This will skip the Decrtiption property from the 
                // Meal domain model, and will not create Description column in the Meals Table in db

                //entity.Ignore(x => x.Description);

            });










			OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
