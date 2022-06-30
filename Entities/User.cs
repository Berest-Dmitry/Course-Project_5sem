using course_proj_english_tutorial.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static course_proj_english_tutorial.Common.DefaultEnums;

namespace course_proj_english_tutorial.Entities
{
	/// <summary>
	/// пользователь
	/// </summary>
	public class User: Entity
	{
		/// <summary>
		/// ФИО
		/// </summary>
		public string UserName { get; set; }
		/// <summary>
		/// пароль
		/// </summary>
		public string Password { get; set; }
		/// <summary>
		/// дата рождения
		/// </summary>
		public DateTime? DateOfBirth { get; set; }
		/// <summary>
		/// класс
		/// </summary>
		public int? Grade { get; set; }
		/// <summary>
		/// опыт работы
		/// </summary>
		public int? WorkExperience { get; set; }
		/// <summary>
		/// роль пользователя
		/// </summary>
		public UserRoles Role { get; set; }
		/// <summary>
		/// уровень владения языком
		/// </summary>
		public KnowledgeLevels knowledgeLevel { get; set; } 
		/// <summary>
		/// список записей о расписании
		/// </summary>
		public List<Schedule> ScheduleList { get; set; }
		/// <summary>
		/// список логов
		/// </summary>
		public List<Log> Logs { get; set; }
		/// <summary>
		/// список достижений пользователя
		/// </summary>
		public List<Achievement> Achievements { get; set; }
		/// <summary>
		/// ID журнала, к которому относится пользователь
		/// </summary>
		public Guid? JournalId { get; set; }
		public Journal Journal { get; set; }

		public static User Create(string UserName, string Password, int role)
		{
			var user = new User()
			{
				Id = Guid.NewGuid(),
				UserName = UserName,
				Password = Password,
				Role = (UserRoles)role
			};
			return user;
		}
	}
}
