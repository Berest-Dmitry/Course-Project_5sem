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
	public class JournalService : BaseService<Journal>, IJournalService
	{
		/// <summary>
		/// метод создания записи в журнале
		/// </summary>
		/// <param name="grade"></param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		public async Task<Journal> CreateJournalNote(int grade, DateTime start, DateTime end)
		{
			try
			{
				var entry = Journal.Create(grade, start, end);
				if(entry != null)
				{
					var result = await AddAsync(entry);
					return result;
				}
				else
				{
					throw new Exception("Journal entry hasn't been generated properly!");
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// <summary>
		/// метод получения списка записей в журнале
		/// </summary>
		public async Task<List<Journal>> GetJournalNotes()
		{
			try
			{
				var list = await _applicationContext.Journals.ToListAsync();
				return list;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// метод удаления записи из журнала
		/// </summary>
		/// <param name="journalId"></param>
		public async Task<bool> DeleteJournal(Guid journalId)
		{
			try
			{
				var entity = await GetEntityById(journalId);
				if (entity != null)
				{
					await DeleteAsync(entity);
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
