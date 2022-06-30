using course_proj_english_tutorial.Common;
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
	public class UserService: BaseService<User>, IUserService
	{
		/// <summary>
		/// метод регистрации пользователя
		/// </summary>
		public async Task<User> RegisterUser(string UserName, string password, int Role)
		{
			try
			{
				var entity = User.Create(UserName, password, Role);
				if(entity != null)
				{
					var resultEntity = await AddAsync(entity);
					return resultEntity;
				}
				else
				{
					return new User();
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// <summary>
		/// метод входа в приложение
		/// </summary>
		public async Task<bool> LoginUser(string UserName, string password)
		{
			try
			{
				var list = await GetAsync(u => u.UserName == UserName && u.Password == password);
				var signed_user = list.FirstOrDefault();
				if (signed_user != null)
					return true;
				else return false;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// <summary>
		/// метод получения данного пользователя
		/// </summary>
		public async Task<User> GetCurrentUser(string UserName)
		{
			var user = await GetAsync(u => u.UserName == UserName);
			return user.FirstOrDefault();
		}
		/// <summary>
		/// метод редактирования пользователя
		/// </summary>
		public async Task<User> EditUser(UserModel userData)
		{
			try
			{
				var o_user = await GetEntityById(userData.Id);
				if (o_user != null)
				{
					o_user.UserName = userData.UserName;
					//o_user.DateOfBirth = userData.DateOfBirth;
					o_user.Grade = userData.Grade;
					o_user.knowledgeLevel = userData.knowledgeLevel;
					await _applicationContext.SaveChangesAsync();
					return o_user;
				}
				else return new User();
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// <summary>
		/// метод получения списка всех преподавателей
		/// </summary>
		public async Task<List<User>> GetListOfAllTeachers()
		{
			var teachersList = await _applicationContext.Users.Where(u => u.Role == DefaultEnums.UserRoles.Tutor).ToListAsync();
			return teachersList;
		}
		/// <summary>
		/// метод получения списка всех учеников
		/// </summary>
		public async Task<List<User>> GetListOfAllStudents()
		{
			var studentsList = await _applicationContext.Users.Where(u => u.Role == DefaultEnums.UserRoles.Student).ToListAsync();
			return studentsList;
		}

		/// <summary>
		/// проверка, привязан ли преподаватель к журналу
		/// </summary>
		/// <param name="journalId"></param>
		public async Task<bool> CheckIfTeacherAttachedToJournalEntry(Guid journalId)
		{
			int teacherAttached = await _applicationContext.Users
				.Where(u => u.Role == DefaultEnums.UserRoles.Tutor && u.JournalId == journalId).CountAsync();
			return teacherAttached > 0;
		}
		/// <summary>
		/// привязка преподавателя/ученика к записи в журнале
		/// </summary>
		/// <param name="journalId"></param>
		/// <param name="teacherId"></param>
		public async Task<bool> AttachUserToJournal(Guid journalId, Guid userId)
		{
			try
			{
				var selectedUser = await GetEntityById(userId);
				if (selectedUser == null)
					return false;
				else
				{
					selectedUser.JournalId = journalId;
					await UpdateAsync(selectedUser);
					return true;
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// <summary>
		/// открепление пользователя от записи в журнале
		/// </summary>
		/// <param name="journalId"></param>
		/// <param name="userId"></param>
		public async Task<bool> DetachUserFromJournal( Guid userId)
		{
			try
			{
				var selectedUser = await GetEntityById(userId);
				if (selectedUser == null)
					return false;
				else
				{
					selectedUser.JournalId = null;
					await UpdateAsync(selectedUser);
					return true;
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// <summary>
		/// метод отписки всех привязанных к записи в журнале пользователей
		/// </summary>
		/// <param name="journalId"></param>
		public async Task<bool> DetachAllUsersFromCurrentJournalEntry(Guid journalId)
		{
			try
			{
				var usersList = await _applicationContext.Users.Where(u => u.JournalId == journalId)
					.ToListAsync();
				if (usersList != null)
				{
					foreach (var user in usersList)
					{
						user.JournalId = null;
						_applicationContext.Entry(user).State = EntityState.Modified;
					}
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
		/// метод получения преподавателя по Id журнала, к которому он прикреплен
		/// </summary>
		/// <param name="journalId"></param>
		public async Task<Guid> GetTeacherByHisJournalId(Guid journalId)
		{
			var teacherId = await _applicationContext.Users
				.Where(u => u.Role == DefaultEnums.UserRoles.Tutor && u.JournalId == journalId).Select(u => u.Id).FirstOrDefaultAsync();
			return teacherId;
		}
	}
}
