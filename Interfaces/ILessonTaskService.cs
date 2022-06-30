using course_proj_english_tutorial.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Interfaces
{
	public interface ILessonTaskService
	{
		/// <summary>
		/// метод прикрепления задания к уроку
		/// </summary>
		/// <param name="lessonId"></param>
		/// <param name="taskId"></param>
		Task<LessonTasks> PinTaskToLesson(Guid lessonId, Guid taskId);
		/// <summary>
		/// проверка, прикреплено ли уже данное задание к данному уроку
		/// </summary>
		/// <param name="lessonId"></param>
		/// <param name="taskId"></param>
		Task<bool> CheckIfTaskIsPinnedToLesson(Guid lessonId, Guid taskId);
		/// <summary>
		/// метод открепления задания от урока
		/// </summary>
		/// <param name="lessonId"></param>
		/// <param name="taskId"></param>
		/// <returns></returns>
		Task<bool> UnpinTaskFromLesson(Guid lessonId, Guid taskId);
		/// <summary>
		/// метод получения всех записей, содержащих ID данного задания
		/// </summary>
		/// <param name="exerciseId"></param>
		Task<List<LessonTasks>> GetAllEntriesOfCurrentExercise(Guid exerciseId);
		/// <summary>
		/// метод удаления списка записей
		/// </summary>
		/// <param name="lessonTasks"></param>
		Task<bool> RemoveEntries(List<LessonTasks> lessonTasks);
	}
}
