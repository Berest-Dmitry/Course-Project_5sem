using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Models.DataModels
{
	public class ExerciseModel: BaseModel
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
		public ObservableCollection<string> Answers { get; set; }
		/// <summary>
		///  Правильный ответ
		/// </summary>
		public string CorrectAnswer { get; set; }
		/// <summary>
		/// флаг - задание пройдено учеником
		/// </summary>
		public bool IsCompleted { get; set; }
		/// <summary>
		/// ID родительского урока (для процесса выполнения заданий)
		/// </summary>
		public Guid? ParentLessonId { get; set; }

		public List<ExerciseAnswerModel> ExerciseAnswers { get; set; }
	}
}
