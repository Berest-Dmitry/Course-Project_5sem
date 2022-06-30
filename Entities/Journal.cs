using course_proj_english_tutorial.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Entities
{
	/// <summary>
	/// журнал
	/// </summary>
	public class Journal: Entity
	{
		/// <summary>
		/// класс
		/// </summary>
		public int Grade { get; set; }
		/// <summary>
		/// начало периода времени
		/// </summary>
		public DateTime StartTime { get; set; }
		/// <summary>
		/// конец периода времени
		/// </summary>
		public DateTime EndTime { get; set; }
		/// <summary>
		/// список пользователей
		/// </summary>
		public List<User> Users { get; set; }

		public static Journal Create(int grade, DateTime start, DateTime end)
		{
			var journal = new Journal()
			{
				Id = Guid.NewGuid(),
				Grade = grade,
				StartTime = start,
				EndTime = end
			};
			return journal;
		}
	}
}
