using course_proj_english_tutorial.Common;
using course_proj_english_tutorial.Models.DataModels;
using course_proj_english_tutorial.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace course_proj_english_tutorial.ViewModels.ContentViewModels
{
	public class MainWIndowViewModel: BaseViewModel
	{
		private UserService userService;
		//private JournalService journalService;
		private ScheduleService scheduleService;
		#region VisibilitySettings
		private Visibility visibility;
		public Visibility Visibility
		{
			get => visibility;
			set
			{
				visibility = value;
				OnPropertyChanged(nameof(visibility));
			}
		}
		private Visibility logOutVisibility;
		public Visibility LogOutVisibility
		{
			get => logOutVisibility;
			set
			{
				logOutVisibility = value;
				OnPropertyChanged(nameof(logOutVisibility));
			}
		}

		private Visibility _StudentOrTeacher;
		public Visibility StudentOrTeacher
		{
			get => _StudentOrTeacher;
			set
			{
				_StudentOrTeacher = value;
				OnPropertyChanged(nameof(_StudentOrTeacher));
			}
		}
		#endregion]
		private bool _OpenInRedactionMode = false;
		public bool OpenInRedactionMode
		{
			get => _OpenInRedactionMode;
			set
			{
				_OpenInRedactionMode = value;
				OnPropertyChanged(nameof(_OpenInRedactionMode));
			}
		}

		private bool _OpenInReviewMode = true;
		public bool OpenInReviewMode
		{
			get => _OpenInReviewMode;
			set
			{
				_OpenInReviewMode = value;
				OnPropertyChanged(nameof(_OpenInReviewMode));
			}
		}

		public UserModel UserData { get; set; }

		public MainWIndowViewModel() {
			userService = new UserService();
			//journalService = new JournalService();
			scheduleService = new ScheduleService();
			visibility = (!MainApplicationService.UserAuthorized) ? Visibility.Visible : Visibility.Hidden;
			LogOutVisibility = (MainApplicationService.UserAuthorized) ? Visibility.Visible : Visibility.Hidden;
			UserData = new UserModel()
			{
				Id = MainApplicationService.CurrentUser.Id,
				UserName = MainApplicationService.CurrentUser.UserName,
				Password = MainApplicationService.CurrentUser.Password,
				Role = MainApplicationService.CurrentUser.Role,				
			};
			StudentOrTeacher = (UserData.Role != Common.DefaultEnums.UserRoles.NotRegistered && UserData.Role != Common.DefaultEnums.UserRoles.SystemAdmin)
				? Visibility.Visible : Visibility.Hidden;
		}
		
		public void LogOutUser()
		{
			if (MainApplicationService.UserAuthorized)
			{
				MainApplicationService.UserAuthorized = false;
				Visibility = (!MainApplicationService.UserAuthorized) ? Visibility.Visible : Visibility.Hidden;
				LogOutVisibility = (MainApplicationService.UserAuthorized) ? Visibility.Visible : Visibility.Hidden;
				MainApplicationService.CurrentUser = null;
				UserData = new UserModel();
			}
		}

		public async Task<bool> EditUser()
		{
			if (UserData != null)
			{
				var result = await userService.EditUser(UserData);
				if (result.Id == UserData.Id)
					return true;
				else return false;
			}
			else return false;
		}
		/// <summary>
		/// метод перехода к редактированию расписания
		/// </summary>
		/// <param name="thisForm"></param>
		/// <param name="otherForm"></param>
		public void GoToSchedule(MainWindow thisForm, ScheduleWindow otherForm)
		{
			if(MainApplicationService.CurrentUser == null 
			|| MainApplicationService.CurrentUser != null && MainApplicationService.CurrentUser.Role != DefaultEnums.UserRoles.Tutor)
			{
				System.Windows.MessageBox.Show("Вам не разрешено редактировать расписание занятий!");
				return;
			}
			otherForm.Show();
			thisForm.Close();
		}
		/// <summary>
		/// метод перехода к редактированию журнала
		/// </summary>
		/// <param name="thisForm"></param>
		/// <param name="otherForm"></param>
		public void GoToJournal(MainWindow thisForm, JournalWindow otherForm)
		{
			if (MainApplicationService.CurrentUser == null
			|| MainApplicationService.CurrentUser != null && MainApplicationService.CurrentUser.Role != DefaultEnums.UserRoles.SystemAdmin)
			{
				System.Windows.MessageBox.Show("Вам не разрешено редактировать журнал!");
				return;
			}
			otherForm.Show();
			thisForm.Close();
		}
		/// <summary>
		/// метод подготовки данных для прохождения урока
		/// </summary>
		public async Task PrepareForLesson(MainWindow thisForm, LessonCompleteWindow otherForm)
		{
			if (MainApplicationService.CurrentUser == null
			|| MainApplicationService.CurrentUser != null && MainApplicationService.CurrentUser.Role != DefaultEnums.UserRoles.Student)
			{
				System.Windows.MessageBox.Show("Вы не можете проходить уроки!");
				return;
			}
			if(MainApplicationService.CurrentUser.JournalId == null)
			{
				System.Windows.MessageBox.Show("Вы не записаны к преподавателю, пожалуйста, обратитесь к администратору системы, чтобы он произвел запись.");
				return;
			}
			else
			{
				Guid currentSchedule = Guid.Empty;
				Guid yourTeacherId = await userService.GetTeacherByHisJournalId(MainApplicationService.CurrentUser.JournalId ?? Guid.Empty);
				var studyingDays = await scheduleService.GetScheduleItemsByUserId(yourTeacherId);
				if(studyingDays != null && studyingDays.Count > 0)
				{
					foreach(var day in studyingDays)
					{
						if(day.Date.Date == DateTime.Now.Date)
						{
							currentSchedule = day.Id;
							break;
						}
					}
				}
				if(currentSchedule != Guid.Empty)
				{
					MainApplicationService.CurrentStudyingDayId = currentSchedule;
					GoToLessons(thisForm, otherForm);
				}
				else
				{
					System.Windows.MessageBox.Show("На сегодня не запланировано занятий!");
					return;
				}
			}
		}

		/// <summary>
		/// метод перехода к прохождению уроков
		/// </summary>
		/// <param name="thisForm"></param>
		/// <param name="otherForm"></param>
		public void GoToLessons(MainWindow thisForm, LessonCompleteWindow otherForm)
		{
			otherForm.Show();
			thisForm.Close();
		}
	}
}
