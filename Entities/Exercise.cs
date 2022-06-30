using course_proj_english_tutorial.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Entities
{
	/// <summary>
	/// задание
	/// </summary>
	public class Exercise: Entity
	{
		/// <summary>
		/// Номер задания
		/// </summary>
		public int Number { get; set; }
		/// <summary>
		/// Описание
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// Список ответов
		/// </summary>
		public string Answers { get; set; }
		/// <summary>
		///  Правильный ответ
		/// </summary>
		public string CorrectAnswer { get; set; }
		/// <summary>
		/// список уроков, в которых встречается данное задание
		/// </summary>
		public List<LessonTasks> LessonTasks { get; set; }

		public static Exercise Create(int Number, string Description, string CorrectAnswer, string Answers)
		{
			Exercise exercise = new Exercise()
			{
				Id = Guid.NewGuid(),
				Number = Number,
				Description = Description,
				CorrectAnswer = CorrectAnswer,
				Answers = Answers
			};
			return exercise;
		} 
	}

}
