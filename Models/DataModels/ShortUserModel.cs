using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Models.DataModels
{
	public class ShortUserModel:BaseModel
	{
		/// <summary>
		/// ФИО
		/// </summary>
		public string UserName { get; set; }
		/// <summary>
		/// класс
		/// </summary>
		public int? Grade { get; set; }
		/// <summary>
		/// опыт работы
		/// </summary>
		public int? WorkExperience { get; set; }

		public Guid? JournalId { get; set; }
	}
}
