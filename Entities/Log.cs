using course_proj_english_tutorial.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Entities
{
	/// <summary>
	/// лог
	/// </summary>
	public class Log: Entity
	{
		/// <summary>
		/// дата события
		/// </summary>
		public DateTime Date { get; set; }
		/// <summary>
		/// описание события
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// ID пользователя, с которым связано событие
		/// </summary>
		public Guid UserId { get; set; }
		public User User { get; set; }
	}
}
