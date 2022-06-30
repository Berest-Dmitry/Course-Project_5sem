using course_proj_english_tutorial.Entities;
using course_proj_english_tutorial.Interfaces;
using course_proj_english_tutorial.Services.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Services
{
	public class AchievementService: BaseService<Achievement>, IAchievementService
	{
		/// <summary>
		/// метод создания записи о достижении
		/// </summary>
		/// <param name="description"></param>
		/// <param name="mark"></param>
		/// <param name="userId"></param>
		public async Task<Achievement> CreateAchievementEntry(string description, int mark, Guid userId)
		{
			try
			{
				var entry = Achievement.Create(description, mark, userId, DateTime.Now);
				if(entry != null)
				{
					var result = await AddAsync(entry);
					return result;
				}
				else
				{
					throw new Exception("Achievement item hasn't been generated properly!");
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// метод форматирования строки результата прохождения урока
		/// </summary>
		/// <param name="LessonName"></param>
		/// <param name="ExerciseNumbers"></param>
		/// <param name="AllExercisesDone"></param>
		public string FormatMessageString(string LessonName, Guid LessonId, string ExerciseNumbers, bool AllExercisesDone = false, bool AllExercisesFailed = false)
		{
			string message = "";
			if (AllExercisesDone)
			{
				message += "В уроке " + LessonName + " успешно пройдены все задания! ";
				
			}
			else if (AllExercisesFailed)
			{
				message += "В уроке " + LessonName + " неправильно выполнены все задания! ";
				
			}
			else
			{
				message += "В уроке " + LessonName + " успешно пройдены следующие задания: " + ExerciseNumbers;
				
			}
			string thisLessonId = LessonId.ToString();
			message += "\n ID пройденного урока: " + thisLessonId;
			return message;
		}
		/// <summary>
		/// проверка, прошел ли ученик данный урок
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="lessonId"></param>
		public async Task<List<Guid>> CheckIfStudentCompletedCurrentLesson(Guid userId, List<Guid> lessonIds)
		{
			List<Guid> completedLessonIds = new List<Guid>();
			foreach(var lessonId in lessonIds)
			{
				string IDstring = lessonId.ToString();
				bool check = await _applicationContext.Achievements
					.Where(x => x.UserId == userId && x.Description.Contains(IDstring)).CountAsync() > 0;
				if (check) completedLessonIds.Add(lessonId);
			}
			return completedLessonIds;
			
		}
	}
}
