using System;
using Microsoft.EntityFrameworkCore;
using Kids.EF.Models;
using Microsoft.Extensions.Configuration;

namespace Kids.EF.Internal
{
	internal class KidsContext : DbContext
	{
		private const string EnvironmentVariablePrefix = "KIDSVAR_";
		private const string UserSecretConnectionStringName = "ConnectionString";

		private static string GetConnectionString()
		{
			var _Config = new ConfigurationBuilder().AddEnvironmentVariables(EnvironmentVariablePrefix).Build();
			return _Config[UserSecretConnectionStringName];
		}

		private static Lazy<string> _ConnectionString = new Lazy<string>(GetConnectionString);
		private static string ConnectionString => _ConnectionString.Value;

		public KidsContext()
		{
		}

		public KidsContext(DbContextOptions<KidsContext> options)
				: base(options)
		{
		}

		public virtual DbSet<Event> Event { get; set; }
		public virtual DbSet<Family> Family { get; set; }
		public virtual DbSet<Kid> Kid { get; set; }
		public virtual DbSet<KidFamily> KidFamily { get; set; }
		public virtual DbSet<PointLogEntry> PointLogEntry { get; set; }
		public virtual DbSet<User> User { get; set; }
		public virtual DbSet<UserFamily> UserFamily { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseNpgsql(ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

			modelBuilder.Entity<Event>(entity =>
			{
				entity.ToTable("event");

				entity.HasIndex(e => e.FamilyId)
									.HasName("fki_event_family");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.Description)
									.IsRequired()
									.HasColumnName("description")
									.HasMaxLength(255);

				entity.Property(e => e.FamilyId).HasColumnName("familyId");

				entity.Property(e => e.Points).HasColumnName("points");

				entity.HasOne(d => d.Family)
									.WithMany(p => p.Event)
									.HasForeignKey(d => d.FamilyId)
									.OnDelete(DeleteBehavior.ClientSetNull)
									.HasConstraintName("event_familyId_fkey");
			});

			modelBuilder.Entity<Family>(entity =>
			{
				entity.ToTable("family");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.Name)
									.IsRequired()
									.HasColumnName("name")
									.HasMaxLength(100);
			});

			modelBuilder.Entity<Kid>(entity =>
			{
				entity.ToTable("kid");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.BirthDate)
									.HasColumnName("birth_date")
									.HasColumnType("date");

				entity.Property(e => e.Name)
									.IsRequired()
									.HasColumnName("name")
									.HasColumnType("character varying");

				entity.Property(e => e.Points).HasColumnName("points");

				entity.Property(e => e.Version).HasColumnName("version");
			});

			modelBuilder.Entity<KidFamily>(entity =>
			{
				entity.HasKey(e => new { e.KidId, e.FamilyId })
									.HasName("kid_family_pkey");

				entity.ToTable("kid_family");

				entity.HasIndex(e => e.FamilyId)
									.HasName("fki_kid_family_familyId");

				entity.HasIndex(e => e.KidId)
									.HasName("fki_kid_family_kidId");

				entity.Property(e => e.KidId).HasColumnName("kidId");

				entity.Property(e => e.FamilyId).HasColumnName("familyId");

				entity.HasOne(d => d.Family)
									.WithMany(p => p.KidFamily)
									.HasForeignKey(d => d.FamilyId)
									.OnDelete(DeleteBehavior.ClientSetNull)
									.HasConstraintName("kid_family_familyId");

				entity.HasOne(d => d.Kid)
									.WithMany(p => p.KidFamily)
									.HasForeignKey(d => d.KidId)
									.OnDelete(DeleteBehavior.ClientSetNull)
									.HasConstraintName("kid_id");
			});

			modelBuilder.Entity<PointLogEntry>(entity =>
			{
				entity.ToTable("point_log_entry");

				entity.HasIndex(e => e.EventId)
									.HasName("fki_point_log_entry_eventId");

				entity.HasIndex(e => e.FamilyId)
									.HasName("fki_point_log_entry_familyId");

				entity.HasIndex(e => e.KidId)
									.HasName("fki_point_log_entry_kidId");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.EventId).HasColumnName("eventId");

				entity.Property(e => e.FamilyId).HasColumnName("familyId");

				entity.Property(e => e.KidId).HasColumnName("kidId");

				entity.Property(e => e.UserId).HasColumnName("userId");

				entity.Property(e => e.Note)
									.HasColumnName("note")
									.HasMaxLength(255);

				entity.Property(e => e.Points).HasColumnName("points");

				entity.Property(e => e.Timestamp)
									.HasColumnName("timestamp")
									.HasColumnType("timestamp with time zone")
									.HasDefaultValueSql("now()");

				entity.HasOne(d => d.Event)
									.WithMany(p => p.PointLogEntry)
									.HasForeignKey(d => d.EventId)
									.HasConstraintName("point_log_entry_eventId");

				entity.HasOne(d => d.Family)
									.WithMany(p => p.PointLogEntry)
									.HasForeignKey(d => d.FamilyId)
									.OnDelete(DeleteBehavior.ClientSetNull)
									.HasConstraintName("point_log_entry_familyId");

				entity.HasOne(d => d.Kid)
									.WithMany(p => p.PointLogEntry)
									.HasForeignKey(d => d.KidId)
									.OnDelete(DeleteBehavior.ClientSetNull)
									.HasConstraintName("point_log_entry_kidId");

				entity.HasOne(d => d.User)
									.WithMany(p => p.PointLogEntry)
									.HasForeignKey(d => d.UserId)
									.OnDelete(DeleteBehavior.ClientSetNull)
									.HasConstraintName("point_log_entry_userId");
			});

			modelBuilder.Entity<User>(entity =>
			{
				entity.ToTable("user");

				entity.HasIndex(e => e.EmailUpper)
									.HasName("user_email_upper_key")
									.IsUnique();

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.Email)
									.IsRequired()
									.HasColumnName("email")
									.HasMaxLength(255);

				entity.Property(e => e.EmailUpper)
									.IsRequired()
									.HasColumnName("email_upper")
									.HasColumnType("character(255)");

				entity.Property(e => e.Name)
									.IsRequired()
									.HasColumnName("name")
									.HasMaxLength(100);

				entity.Property(e => e.Password)
									.IsRequired()
									.HasColumnName("password")
									.HasMaxLength(255);
			});

			modelBuilder.Entity<UserFamily>(entity =>
			{
				entity.HasKey(e => new { e.UserId, e.FamilyId })
									.HasName("user_family_pkey");

				entity.ToTable("user_family");

				entity.HasIndex(e => e.FamilyId)
									.HasName("fki_user_family_familyId");

				entity.HasIndex(e => e.UserId)
									.HasName("fki_user_family_userId");

				entity.Property(e => e.UserId).HasColumnName("userId");

				entity.Property(e => e.FamilyId).HasColumnName("familyId");

				entity.HasOne(d => d.Family)
									.WithMany(p => p.UserFamily)
									.HasForeignKey(d => d.FamilyId)
									.OnDelete(DeleteBehavior.ClientSetNull)
									.HasConstraintName("user_family_familyId");

				entity.HasOne(d => d.User)
									.WithMany(p => p.UserFamily)
									.HasForeignKey(d => d.UserId)
									.OnDelete(DeleteBehavior.ClientSetNull)
									.HasConstraintName("user_family_userId");
			});
		}
	}
}
