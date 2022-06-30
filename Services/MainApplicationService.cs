using course_proj_english_tutorial.Entities;
using course_proj_english_tutorial.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Services
{
	public class MainApplicationService
	{
		public static bool UserAuthorized { get; set; } = false;
		public static User CurrentUser { get; set; } = new User();
		public static Guid ScheduleItemId { get; set; }
		public static Guid LessonItemId { get; set; }
		public static Guid CurrentExerciseItemId { get; set; }
		public static Guid CurrentStudyingDayId { get; set; }
		public static int WordsCount { get; set; } // кол-во слов в словаре (для обновления в интерфейсе окна урока)
		public static List<VocabularyModel> LessVoc { get; set; } // список моделей словарей для переопределения ресурса элемента разметки
		public static string ConnectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=mobiledb1;user id=sa;password=qwerty_123;Integrated Security=True";

		#region SQLScriptFilePaths
		public static string CreateDBScriptFileName = @"Common\ScriptsSQL\CreateDatabaseCP.sql";
		public static string PathToCreateDBScript = "D:\\курсовой проект\\course_proj_english_tutorial\\Common\\ScriptsSQL\\CreateDatabaseCP.sql";
		#endregion
	}
}
