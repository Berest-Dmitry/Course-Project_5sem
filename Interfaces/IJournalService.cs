using course_proj_english_tutorial.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Interfaces
{
	public interface IJournalService
	{
		/// <summary>
		/// метод создания записи в журнале
		/// </summary>
		/// <param name="grade"></param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		Task<Journal> CreateJournalNote(int grade, DateTime start, DateTime end);
		/// <summary>
		/// метод получения списка записей в журнале
		/// </summary>
		Task<List<Journal>> GetJournalNotes();
		/// <summary>
		/// метод удаления записи из журнала
		/// </summary>
		/// <param name="journalId"></param>
		Task<bool> DeleteJournal(Guid journalId);
	}
}
