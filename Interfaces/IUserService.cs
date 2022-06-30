using course_proj_english_tutorial.Entities;
using course_proj_english_tutorial.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Interfaces
{
	public interface IUserService
	{
		/// <summary>
		/// метод регистрации пользователя
		/// </summary>
		Task<User> RegisterUser(string UserName, string password, int Role);
		/// <summary>
		/// метод входа в приложение
		/// </summary>
		Task<bool> LoginUser(string UserName, string password);
		/// <summary>
		/// метод получения данного пользователя
		/// </summary>
		Task<User> GetCurrentUser(string UserName);
		/// <summary>
		/// метод получения списка всех преподавателей
		/// </summary>
		Task<List<User>> GetListOfAllTeachers();
		/// <summary>
		/// проверка, привязан ли преподаватель к журналу
		/// </summary>
		/// <param name="journalId"></param>
		Task<bool> CheckIfTeacherAttachedToJournalEntry(Guid journalId);
		/// <summary>
		/// привязка пользователя к записи в журнале
		/// </summary>
		/// <param name="journalId"></param>
		/// <param name="teacherId"></param>
		Task<bool> AttachUserToJournal(Guid journalId, Guid userId);
		/// <summary>
		/// открепление пользователя от записи в журнале
		/// </summary>
		/// <param name="journalId"></param>
		/// <param name="userId"></param>
		Task<bool> DetachUserFromJournal(Guid userId);
		/// <summary>
		/// метод получения списка всех учеников
		/// </summary>
		Task<List<User>> GetListOfAllStudents();
		/// <summary>
		/// метод отписки всех привязанных к записи в журнале пользователей
		/// </summary>
		/// <param name="journalId"></param>
		Task<bool> DetachAllUsersFromCurrentJournalEntry(Guid journalId);
		/// <summary>
		/// метод получения преподавателя по Id журнала, к которому он прикреплен
		/// </summary>
		/// <param name="journalId"></param>
		Task<Guid> GetTeacherByHisJournalId(Guid journalId);
	}
}
