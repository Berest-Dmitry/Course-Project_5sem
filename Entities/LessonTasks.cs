using course_proj_english_tutorial.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Entities
{
	public class LessonTasks: Entity
	{
		public Lesson Lesson { get; set; }
		/// <summary>
		/// ID занятия
		/// </summary>
		public Guid LessonId { get; set; }
		public Exercise Exercise { get; set; }
		/// <summary>
		/// ID задания
		/// </summary>
		public Guid TaskId { get; set; }

		public static LessonTasks Create(Guid lessonId, Guid taskId)
		{
			LessonTasks lessonTasks = new LessonTasks()
			{
				Id = Guid.NewGuid(),
				LessonId = lessonId,
				TaskId = taskId
			};
			return lessonTasks;
		}
	}
}
