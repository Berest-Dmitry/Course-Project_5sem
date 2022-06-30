using course_proj_english_tutorial.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Interfaces
{
	public interface IScheduleService
	{
		/// <summary>
		/// Метод создания элемента расписания
		/// </summary>
		/// <param name="date"></param>
		/// <param name="UserId"></param>
		Task<Schedule> CreateScheduleItem(DateTime date, Guid UserId);
		/// <summary>
		/// метод получения списка элементов расписания данного пользователя
		/// </summary>
		/// <param name="userId"></param>
		Task<List<Schedule>> GetScheduleItemsByUserId(Guid userId);
		/// <summary>
		/// метод удаления элемента из расписания
		/// </summary>
		/// <param name="date"></param>
		/// <param name="UserId"></param>
		/// <returns></returns>
		Task<bool> DeleteScheduleItem(DateTime date, Guid UserId);
	}
}
