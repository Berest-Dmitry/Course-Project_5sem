using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Models.DataModels
{
	public class LessonExerciseModel: BaseModel
	{
		/// <summary>
		/// ID урока
		/// </summary>
		public Guid LessonId { get; set; }
		/// <summary>
		/// ID задания
		/// </summary>
		public Guid ExerciseId { get; set; }
		/// <summary>
		/// Номер задания
		/// </summary>
		public int Number { get; set; }
		/// <summary>
		/// Описание
		/// </summary>
		public string Description { get; set; }
	}
}
