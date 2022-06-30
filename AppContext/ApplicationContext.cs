using course_proj_english_tutorial.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.AppContext
{
	public class ApplicationContext: DbContext
	{
		public ApplicationContext(): base("DefaultConnection")
		{
			Database.SetInitializer<ApplicationContext>(new CreateDatabaseIfNotExists<ApplicationContext>());
		}
		#region Database sets
		public DbSet<User> Users { get; set; }
		public DbSet<Lesson> Lessons { get; set; }
		public DbSet<Exercise> Exercises { get; set; }
		public DbSet<Schedule> Schedules { get; set; }
		public DbSet<LessonTasks> LessonTasks { get; set; }
		public DbSet<Journal> Journals { get; set; }
		public DbSet<Vocabulary> Vocabularies { get; set; }
		public DbSet<Log> Logs { get; set; }
		public DbSet<Achievement> Achievements { get; set; }

		#endregion
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			ConfigureLog(modelBuilder);
			ConfigureAchievement(modelBuilder);
			ConfigureExercise(modelBuilder);
			ConfigureLesson(modelBuilder);
			ConfigureVocabulary(modelBuilder);
			ConfigureLessonTasks(modelBuilder);
			ConfigureSchedule(modelBuilder);
			ConfigureJournal(modelBuilder);
			ConfigureUser(modelBuilder);
		}
		/// <summary>
		/// конфигурация таблицы логов
		/// </summary>
		private void ConfigureLog(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Log>().ToTable("Logs")
				.HasKey(l => l.Id);

			modelBuilder.Entity<Log>()
				.Property(l => l.Id)
				.IsRequired();

			modelBuilder.Entity<Log>().HasRequired<User>(l => l.User)
				.WithMany(u => u.Logs).HasForeignKey(l => l.UserId).WillCascadeOnDelete(false);
		}
		/// <summary>
		/// конфигурация таблицы достижений
		/// </summary>
		private void ConfigureAchievement(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Achievement>().ToTable("Achievements")
				.HasKey(l => l.Id);

			modelBuilder.Entity<Achievement>()
				.Property(l => l.Id)
				.IsRequired();

			modelBuilder.Entity<Achievement>().HasRequired(a => a.User)
				.WithMany(u => u.Achievements).HasForeignKey(l => l.UserId).WillCascadeOnDelete(false);
		}
		/// <summary>
		/// конфигурация таблицы заданий
		/// </summary>
		private void ConfigureExercise(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Exercise>().ToTable("Exercises")
				.HasKey(l => l.Id);

			modelBuilder.Entity<Exercise>()
				.Property(l => l.Id)
				.IsRequired();

		}
		/// <summary>
		///  конфигурация таблицы уроков
		/// </summary>
		private void ConfigureLesson(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Lesson>().ToTable("Lessons")
				.HasKey(l => l.Id);

			modelBuilder.Entity<Lesson>()
				.Property(l => l.Id)
				.IsRequired();

			modelBuilder.Entity<Lesson>().HasRequired(l => l.Schedule)
				.WithMany(s => s.Lessons).HasForeignKey(l => l.ScheduleId).WillCascadeOnDelete(false);
		}
		/// <summary>
		/// конфигурация таблицы словарей
		/// </summary>
		private void ConfigureVocabulary(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Vocabulary>().ToTable("Vocabularies")
				.HasKey(l => l.Id);

			modelBuilder.Entity<Vocabulary>()
				.Property(l => l.Id)
				.IsRequired();

			modelBuilder.Entity<Vocabulary>().HasRequired(l => l.Lesson)
				.WithMany(s => s.LessonVocabularies).HasForeignKey(l => l.LessonId).WillCascadeOnDelete(false);
		}
		/// <summary>
		/// конфигурация таблицы заданий уроков
		/// </summary>
		private void ConfigureLessonTasks(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<LessonTasks>().ToTable("LessonTasks")
				.HasKey(l => l.Id);

			modelBuilder.Entity<LessonTasks>()
				.Property(l => l.Id)
				.IsRequired();

			modelBuilder.Entity<LessonTasks>().HasRequired(l => l.Lesson)
				.WithMany(s => s.LessonTasks).HasForeignKey(l => l.LessonId).WillCascadeOnDelete(false);

			modelBuilder.Entity<LessonTasks>().HasRequired(l => l.Exercise)
				.WithMany(s => s.LessonTasks).HasForeignKey(l => l.TaskId).WillCascadeOnDelete(false);
		}
		/// <summary>
		/// конфигурация таблицы расписания
		/// </summary>
		private void ConfigureSchedule(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Schedule>().ToTable("Schedules")
				.HasKey(l => l.Id);

			modelBuilder.Entity<Schedule>()
				.Property(l => l.Id)
				.IsRequired();

			modelBuilder.Entity<Schedule>().HasRequired(l => l.User)
				.WithMany(s => s.ScheduleList).HasForeignKey(l => l.UserId).WillCascadeOnDelete(false);
		}
		/// <summary>
		/// конфигурация таблицы журналов
		/// </summary>
		private void ConfigureJournal(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Journal>().ToTable("Journals")
				.HasKey(l => l.Id);

			modelBuilder.Entity<Journal>()
				.Property(l => l.Id)
				.IsRequired();
		}
		/// <summary>
		/// конфигурация таблицы пользователей
		/// </summary>
		private void ConfigureUser(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().ToTable("Users")
				.HasKey(l => l.Id);

			modelBuilder.Entity<User>()
				.Property(l => l.Id)
				.IsRequired();

			modelBuilder.Entity<User>()
				.Property(u => u.UserName)
				.IsRequired()
				.HasMaxLength(50);

			modelBuilder.Entity<User>()
				.Property(u => u.DateOfBirth)
				.IsOptional();

			modelBuilder.Entity<User>()
				.Property(u => u.Password)
				.IsRequired()
				.HasMaxLength(30);

			modelBuilder.Entity<User>().HasOptional(l => l.Journal)
				.WithMany(s => s.Users).HasForeignKey(l => l.JournalId).WillCascadeOnDelete(false);
		}
	}
}
