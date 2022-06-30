using course_proj_english_tutorial.Entities;
using course_proj_english_tutorial.Interfaces;
using course_proj_english_tutorial.Models;
using course_proj_english_tutorial.Models.DataModels;
using course_proj_english_tutorial.Services.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Services
{
	public class LessonService : BaseService<Lesson>, ILessonService
	{
		/// <summary>
		/// метод получения всех уроков данного дня расписания
		/// </summary>
		/// <param name="scheduleItemId"></param>
		public async Task<List<Lesson>> GetAllLessonsOfCurrentScheduleItem(Guid scheduleItemId)
		{
			try
			{
				var lessonList = await _applicationContext.Lessons.Where(l => l.ScheduleId == scheduleItemId).ToListAsync();
				return lessonList;
			}
			catch(Exception e)
			{
				return new List<Lesson>();
			}
		}
		/// <summary>
		/// метод создания урока
		/// </summary>
		/// <param name="Name"></param>
		/// <param name="Description"></param>
		public async Task<Lesson> CreateLesson(Guid ScheduleId,string Name, string Description)
		{
			try
			{
				var entity = Lesson.Create(ScheduleId, Name, Description);
				if (entity != null)
				{
					var resultEntity = await AddAsync(entity);
					return resultEntity;
				}
				else
				{
					throw new Exception("Lesson item hasn't been generated properly!");
				}
			}
			catch(Exception exp)
			{
				throw new Exception(exp.Message);
			}
		}

		/// <summary>
		/// метод удаления урока
		/// </summary>
		/// <param name="lessonId"></param>
		public async Task<bool> DeleteLesson(Guid lessonId)
		{
			try
			{
				var entity = await GetEntityById(lessonId);
				if(entity != null)
				{
					// здесь должна быть логика удаления словарей и отвязывания заданий
					await DeleteAsync(entity);
					return true;
				}
				else
				{
					return false;
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// <summary>
		/// метод изменения урока
		/// </summary>
		/// <param name="lessonId"></param>
		public async Task<LessonModel> UpdateLesson(LessonModel model)
		{
			try
			{
				var entity = await GetEntityById(model.Id);
				if(entity == null)
				{
					return new LessonModel()
					{
						Error = new Exception("такого урока не существует!")
					};
				}
				else
				{
					entity.Name = model.Name;
					entity.Description = model.Description;
					//entity.ScheduleId = model.ScheduleId;
					if(model.LessonVocabularies != null && model.LessonVocabularies.Count() > 0)
					{
						if(entity.LessonVocabularies == null)
						{
							entity.LessonVocabularies = new List<Vocabulary>();
						}
						else
						{
							entity.LessonVocabularies.Clear();
						}						
						foreach(var item in model.LessonVocabularies)
						{
							entity.LessonVocabularies.Add(new Vocabulary() { 
							 Id= item.Id,
							 WordsWithTranslations = item.WordsWithTranslations,
							 LessonId = item.LessonId
							});
						}
					}
					if (model.LessonTasks != null && model.LessonTasks.Count() > 0)
					{
						if (entity.LessonTasks == null)
						{
							entity.LessonTasks = new List<LessonTasks>();
						}
						else
						{
							entity.LessonTasks.Clear();
						}
						foreach (var item in model.LessonTasks)
						{
							entity.LessonTasks.Add(new LessonTasks()
							{
								Id = item.Id,
								TaskId = item.TaskId,
								LessonId = item.LessonId
							});
						}
					}
					await UpdateAsync(entity);
					return model;
				}
			}
			catch(Exception e)
			{
				return BaseModelUtilities<LessonModel>.ErrorFormat(e);
			}
		}
		/// <summary>
		/// метод удаления всех уроков данного дня расписания
		/// </summary>
		/// <param name="scheduleItemId"></param>
		public async Task<bool> DeleteAllLessonsOfCurrentScheduleItem(Guid scheduleItemId)
		{
			try
			{
				var lessonList = await _applicationContext.Lessons.Where(l => l.ScheduleId == scheduleItemId).ToListAsync();
				if (lessonList == null) return false;
				else
				{
					_applicationContext.Lessons.RemoveRange(lessonList);
					await _applicationContext.SaveChangesAsync();
					return true;
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}	
}
