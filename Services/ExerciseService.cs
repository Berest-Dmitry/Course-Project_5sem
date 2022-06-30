using course_proj_english_tutorial.Entities;
using course_proj_english_tutorial.Interfaces;
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
	public class ExerciseService: BaseService<Exercise>, IExerciseService
	{
		/// <summary>
		/// метод создания упражнения
		/// </summary>
		/// <param name="Number"></param>
		/// <param name="Description"></param>
		/// <param name="CorrectAnswer"></param>
		/// <param name="Answers"></param>
		public async Task<Exercise> CreateExercise(int Number, string Description, string CorrectAnswer, List<string> Answers)
		{
			try
			{
				string answers = "";
				if(Answers != null && Answers.Count > 0)
				{
					foreach (var ans in Answers)
					{
						answers += ans + "+";
					}
				}				
				var entity = Exercise.Create(Number, Description, CorrectAnswer, answers);
				if(entity != null)
				{
					var resultEntity = await AddAsync(entity);
					return resultEntity;
				}
				else{
					throw new Exception("Exercise item hasn't been generated properly!");
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// <summary>
		/// получение списка заданий данного урока
		/// </summary>
		/// <param name="lessonId"></param>
		public async Task<List<Exercise>> GetListExercisesOfCurrentLesson(Guid lessonId)
		{
			try
			{
				
				var exercises = _applicationContext.Exercises.AsQueryable();
				var lessonTasks = _applicationContext.LessonTasks.AsQueryable();
				var query = await (from exercise in exercises
							join lessonTask in lessonTasks
							on exercise.Id equals lessonTask.TaskId
							where lessonId == lessonTask.LessonId
							select new {
								Id = exercise.Id,
								Number = exercise.Number,
								Description = exercise.Description,
								Answers = exercise.Answers,
								CorrectAnswer = exercise.CorrectAnswer
							}).ToListAsync();
				var data = query.Select(x => new Exercise() { 
					Id = x.Id,
					Number = x.Number,
					Description = x.Description,
					Answers = x.Answers,
					CorrectAnswer = x.CorrectAnswer
				}).ToList();

				var orderedData = data.OrderBy(x => x.Number).ToList();
				return orderedData;
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// <summary>
		/// получение списка всех заданий
		/// </summary>
		public async Task<List<Exercise>> GetAllExercises()
		{
			return await _applicationContext.Exercises.OrderBy(e => e.Number).ToListAsync();
		}
		/// <summary>
		/// метод обновления записи о задании
		/// </summary>
		/// <param name="Number"></param>
		/// <param name="Description"></param>
		/// <param name="CorrectAnswer"></param>
		/// <param name="Answers"></param>
		/// <returns></returns>
		public async Task<ExerciseModel> UpdateExercise(ExerciseModel model)
		{
			try
			{
				var entity = await GetEntityById(model.Id);
				if(entity == null)
				{
					return new ExerciseModel()
					{
						Result = Common.DefaultEnums.Result.error,
						Error = new Exception("такого задания не существует!")
					};
				}
				else{
					entity.Number = model.Number;
					entity.Description = model.Description;
					entity.CorrectAnswer = model.CorrectAnswer;
					entity.Answers = "";
					for(int i = 0; i < model.Answers.Count; i++)
					{
						if (i < model.Answers.Count - 1)
							entity.Answers += model.Answers[i] + "+";
						else entity.Answers += model.Answers[i];
					}
					await UpdateAsync(entity);
					return model;
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// <summary>
		/// метод удаления задания
		/// </summary>
		/// <param name="Id"></param>
		public async Task<bool> DeleteExercise(Guid Id)
		{
			try
			{
				var entity = await GetEntityById(Id);
				if (entity != null)
				{
					await DeleteAsync(entity);
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
