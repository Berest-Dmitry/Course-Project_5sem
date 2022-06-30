using course_proj_english_tutorial.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Entities
{
	/// <summary>
	/// расписание
	/// </summary>
	public class Schedule: Entity
	{
		/// <summary>
		/// дата
		/// </summary>
		public DateTime Date { get; set; }
		/// <summary>
		/// ID пользователя
		/// </summary>
		public Guid UserId { get; set; }
		public User User { get; set; }
		/// <summary>
		/// список занятий в конкретный день
		/// </summary>
		public List<Lesson> Lessons { get; set; }

		public static Schedule Create(DateTime date, Guid UserId)
		{
			var schedule = new Schedule()
			{
				Id = Guid.NewGuid(),
				Date = date,
				UserId = UserId,
				Lessons = new List<Lesson>()
			};
			return schedule;
		}
	}
}
