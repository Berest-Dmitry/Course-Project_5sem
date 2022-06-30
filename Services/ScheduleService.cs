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
	public class ScheduleService: BaseService<Schedule>, IScheduleService
	{
		/// <summary>
		/// Метод создания элемента расписания
		/// </summary>
		/// <param name="date"></param>
		/// <param name="UserId"></param>
		/// <returns></returns>
		public async Task<Schedule> CreateScheduleItem(DateTime date, Guid UserId)
		{
			try
			{
				var entity = Schedule.Create(date, UserId);
				if(entity != null)
				{
					var resultEntity = await AddAsync(entity);
					return resultEntity;
				}
				else
				{
					throw new Exception("Schedule item hasn't been generated properly!");

				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// <summary>
		/// метод получения списка элементов расписания данного пользователя
		/// </summary>
		/// <param name="userId"></param>
		public async Task<List<Schedule>> GetScheduleItemsByUserId(Guid userId)
		{
			var scheduleList = await _applicationContext.Schedules.Where(x => x.UserId == userId).ToListAsync();
			return scheduleList;
		}

		/// <summary>
		/// метод удаления элемента из расписания
		/// </summary>
		/// <param name="date"></param>
		/// <param name="UserId"></param>
		/// <returns></returns>
		public async Task<bool> DeleteScheduleItem(DateTime date, Guid UserId)
		{
			try
			{
				var entity = await _applicationContext.Schedules.Where(x => x.UserId == UserId && x.Date == date).FirstOrDefaultAsync();
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
