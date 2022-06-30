using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static course_proj_english_tutorial.Common.DefaultEnums;

namespace course_proj_english_tutorial.Models.DataModels
{
	public class UserModel: BaseModel
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
		public DateTime DateOfBirth { get; set; }
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
	}
}
