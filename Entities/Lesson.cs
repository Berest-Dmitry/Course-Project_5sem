using course_proj_english_tutorial.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Entities
{
	/// <summary>
	/// урок
	/// </summary>
	public class Lesson: Entity
	{
		/// <summary>
		/// название урока
		/// </summary>
		public string Name { get; set; } 
		/// <summary>
		/// описание урока
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// время начала урока
		/// </summary>
		public DateTime? TimeStart { get; set; }
		/// <summary>
		/// время окончания урока
		/// </summary>
		public DateTime? TimeEnd { get; set; }
		/// <summary>
		/// ID элемента расписания, к которому привязан урок
		/// </summary>
		public Guid ScheduleId { get; set; }
		public Schedule Schedule { get; set; }
		/// <summary>
		/// список словарей к конкретному уроку
		/// </summary>
		public List<Vocabulary> LessonVocabularies { get; set; }
		/// <summary>
		/// список заданий данного урока
		/// </summary>
		public List<LessonTasks> LessonTasks { get; set; }

		public static Lesson Create(Guid ScheduleId, string Name, string Description)
		{
			var lesson = new Lesson()
			{
				Id = Guid.NewGuid(),
				ScheduleId = ScheduleId,
				Name = Name,
				Description = Description
			};
			return lesson;
		}
	}
}
