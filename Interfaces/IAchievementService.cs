using course_proj_english_tutorial.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Interfaces
{
	public interface IAchievementService
	{
		/// <summary>
		/// метод создания записи о достижении
		/// </summary>
		/// <param name="description"></param>
		/// <param name="mark"></param>
		/// <param name="userId"></param>
		Task<Achievement> CreateAchievementEntry(string description, int mark, Guid userId);
		/// <summary>
		/// метод форматирования строки результата прохождения урока
		/// </summary>
		/// <param name="LessonName"></param>
		/// <param name="ExerciseNumbers"></param>
		/// <param name="AllExercisesDone"></param>
		string FormatMessageString(string LessonName, Guid LessonId, string ExerciseNumbers, bool AllExercisesDone = false, bool AllExercisesFailed = false);
		/// <summary>
		/// проверка, прошел ли ученик данный урок
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="lessonId"></param>
		Task<List<Guid>> CheckIfStudentCompletedCurrentLesson(Guid userId, List<Guid> lessonIds);
	}
}
