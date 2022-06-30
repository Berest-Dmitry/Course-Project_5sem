using course_proj_english_tutorial.Entities;
using course_proj_english_tutorial.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Interfaces
{
	public interface ILessonService
	{
		/// <summary>
		/// метод получения всех уроков данного дня расисания
		/// </summary>
		/// <param name="scheduleItemId"></param>
		Task<List<Lesson>> GetAllLessonsOfCurrentScheduleItem(Guid scheduleItemId);
		/// <summary>
		/// метод создания урока
		/// </summary>
		/// <param name="Name"></param>
		/// <param name="Description"></param>
		Task<Lesson> CreateLesson(Guid ScheduleId, string Name, string Description);
		/// <summary>
		/// метод удаления урока
		/// </summary>
		/// <param name="lessonId"></param>
		Task<bool> DeleteLesson(Guid lessonId);
		/// <summary>
		/// метод изменения урока
		/// </summary>
		/// <param name="lessonId"></param>
		Task<LessonModel> UpdateLesson(LessonModel model);
		/// <summary>
		/// метод удаления всех уроков данного дня расписания
		/// </summary>
		/// <param name="scheduleItemId"></param>
		Task<bool> DeleteAllLessonsOfCurrentScheduleItem(Guid scheduleItemId);
		
	}
}
