using course_proj_english_tutorial.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Entities
{
	/// <summary>
	/// достижение
	/// </summary>
	public class Achievement: Entity
	{
		/// <summary>
		/// дата
		/// </summary>
		public DateTime Date { get; set; }
		/// <summary>
		/// описание
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// оценка
		/// </summary>
		public int Mark { get; set; }
		/// <summary>
		/// ID пользователя, к которому относится достижение
		/// </summary>
		public Guid UserId { get; set; }
		public User User { get; set; }

		public static Achievement Create(string description, int Mark, Guid userId, DateTime dateTime)
		{
			var achievement = new Achievement()
			{
				Id = Guid.NewGuid(),
				Description = description,
				Mark = Mark,
				UserId = userId,
				Date = dateTime
			};
			return achievement;
		} 
	}
}
