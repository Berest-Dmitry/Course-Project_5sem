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
	public class LessonTaskService : BaseService<LessonTasks>, ILessonTaskService
	{
		/// <summary>
		/// метод прикрепления задания к уроку
		/// </summary>
		/// <param name="lessonId"></param>
		/// <param name="taskId"></param>
		public async Task<LessonTasks> PinTaskToLesson(Guid lessonId, Guid taskId)
		{
			try
			{
				var entity = LessonTasks.Create(lessonId, taskId);
				if(entity != null)
				{
					var resultEntity = await AddAsync(entity);
					return resultEntity;
				}
				else
				{
					throw new Exception("LessonTask item hasn't been generated properly!");
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// <summary>
		/// проверка, прикреплено ли уже данное задание к данному уроку
		/// </summary>
		/// <param name="lessonId"></param>
		/// <param name="taskId"></param>
		public async Task<bool> CheckIfTaskIsPinnedToLesson(Guid lessonId, Guid taskId)
		{
			var currentItemCount = await _applicationContext.LessonTasks.Where(x => x.LessonId == lessonId && x.TaskId == taskId)
				.CountAsync();

			return currentItemCount > 0;
		}
		/// <summary>
		/// метод открепления задания от урока
		/// </summary>
		/// <param name="lessonId"></param>
		/// <param name="taskId"></param>
		/// <returns></returns>
		public async Task<bool> UnpinTaskFromLesson(Guid lessonId, Guid taskId)
		{
			try
			{
				var current = await _applicationContext.LessonTasks.Where(x => x.LessonId == lessonId && x.TaskId == taskId)
				.FirstOrDefaultAsync();

				if (current != null)
				{
					_applicationContext.LessonTasks.Remove(current);
					await _applicationContext.SaveChangesAsync();
					return true;
				}
				else return false;
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// <summary>
		/// метод получения всех записей, содержащих ID данного задания
		/// </summary>
		/// <param name="exerciseId"></param>
		public async Task<List<LessonTasks>> GetAllEntriesOfCurrentExercise(Guid exerciseId)
		{
			var listOfElements = await _applicationContext.LessonTasks.Where(x => x.TaskId == exerciseId)
				.ToListAsync();
			return listOfElements;				
		}
		/// <summary>
		/// метод удаления списка записей
		/// </summary>
		/// <param name="lessonTasks"></param>
		public async Task<bool> RemoveEntries(List<LessonTasks> lessonTasks)
		{
			try
			{
				if (lessonTasks != null && lessonTasks.Count > 0)
				{
					_applicationContext.LessonTasks.RemoveRange(lessonTasks);
					await _applicationContext.SaveChangesAsync();
					return true;
				}
				else return false;
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

	}
}
