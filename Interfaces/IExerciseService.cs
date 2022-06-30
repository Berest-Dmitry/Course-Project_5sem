using course_proj_english_tutorial.Entities;
using course_proj_english_tutorial.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Interfaces
{
	public interface IExerciseService
	{
		/// <summary>
		/// метод создания упражнения
		/// </summary>
		/// <param name="Number"></param>
		/// <param name="Description"></param>
		/// <param name="CorrectAnswer"></param>
		/// <param name="Answers"></param>
		Task<Exercise> CreateExercise(int Number, string Description, string CorrectAnswer, List<string> Answers);
		/// <summary>
		/// получение списка заданий данного урока
		/// </summary>
		/// <param name="lessonId"></param>
		Task<List<Exercise>> GetListExercisesOfCurrentLesson(Guid lessonId);
		/// <summary>
		/// получение списка всех заданий
		/// </summary>
		Task<List<Exercise>> GetAllExercises();
		/// <summary>
		/// метод обновления записи о задании
		/// </summary>
		/// <param name="Number"></param>
		/// <param name="Description"></param>
		/// <param name="CorrectAnswer"></param>
		/// <param name="Answers"></param>
		/// <returns></returns>
		Task<ExerciseModel> UpdateExercise(ExerciseModel model);
		/// <summary>
		/// метод удаления задания
		/// </summary>
		/// <param name="Id"></param>
		Task<bool> DeleteExercise(Guid Id);
	}
}
