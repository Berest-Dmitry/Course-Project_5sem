using course_proj_english_tutorial.Models.DataModels;
using course_proj_english_tutorial.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace course_proj_english_tutorial.ViewModels.ContentViewModels
{
	/// <summary>
	/// Модель представления работы с расписанием
	/// </summary>
	public class ScheduleWindowViewModel: BaseViewModel
	{
		private ScheduleService _scheduleService;
		private LessonService _lessonService;
		private ExerciseService _exerciseService;
		private LessonTaskService _lessonTaskService;
		private VocabularyServce _vocabularyServce;

		#region DataFields
		/// <summary>
		/// Модель данных пользователя
		/// </summary>
		public UserModel UserData { get; set; }
		/// <summary>
		/// элементы расписания пользователя
		/// </summary>
		private ObservableCollection<ScheduleModel> _ScheduleItems;
		public ObservableCollection<ScheduleModel> ScheduleItems
		{
			get => _ScheduleItems;
			set
			{
				_ScheduleItems = value;
				OnPropertyChanged(nameof(_ScheduleItems));
			}
		}
		/// <summary>
		/// выбранная в календаре дата
		/// </summary>
		private DateTime? _SelectedDate;
		public DateTime? SelectedDate
		{
			get => _SelectedDate;
			set
			{
				_SelectedDate = value;
				OnPropertyChanged(nameof(_SelectedDate));
			}
		}
		/// <summary>
		/// кол-во элементов в расписании
		/// </summary>
		private int _ScheduleCount = 0;
		public int ScheduleCount
		{
			get => _ScheduleCount;
			set
			{
				_ScheduleCount = value;
				OnPropertyChanged(nameof(ScheduleCount));
			}
		}
		/// <summary>
		/// список названий уроков
		/// </summary>
		private ObservableCollection<string> _LessonNames;
		public ObservableCollection<string> LessonNames
		{
			get => _LessonNames;
			set
			{
				_LessonNames = value;
				OnPropertyChanged(nameof(LessonNames));
			}
		}
		public List<LessonReducedModel> CurrentLessons { get; set; } = new List<LessonReducedModel>();
		/// <summary>
		/// название выбранного урока
		/// </summary>
		public string SelectedLessonName { get; set; }

		/// <summary>
		/// список заполненных дней расписания
		/// </summary>
		private ObservableCollection<string> _ScheduleItemDates = new ObservableCollection<string>();
		public ObservableCollection<string> ScheduleItemDates
		{
			get => _ScheduleItemDates;
			set
			{
				_ScheduleItemDates = value;
				OnPropertyChanged(nameof(ScheduleItemDates));
			}
		}
		/// <summary>
		/// выбранный элемент расписания
		/// </summary>
		private ScheduleModel _SelectedItem = new ScheduleModel();
		public ScheduleModel SelectedItem
		{
			get => _SelectedItem;
			set
			{
				_SelectedItem = value;
				OnPropertyChanged(nameof(SelectedItem));
			}
		}
		#endregion

		#region Visibility
		private Visibility _ScheduleAlreadyExists;
		public Visibility ScheduleAlreadyExists {
			get => _ScheduleAlreadyExists;
			set
			{
				_ScheduleAlreadyExists = value;
				OnPropertyChanged(nameof(ScheduleAlreadyExists));
			}
		}

		private Visibility _ScheduleAlreadyExists1;
		public Visibility ScheduleAlreadyExists1
		{
			get => _ScheduleAlreadyExists1;
			set
			{
				_ScheduleAlreadyExists1 = value;
				OnPropertyChanged(nameof(ScheduleAlreadyExists1));
			}
		}
		#endregion

		#region ButtonSettings
		/// <summary>
		/// флаг - доступна кнопка создания урока
		/// </summary>
		private bool _EditLessonEnabled = false;
		public bool EditLessonEnabled
		{
			get => _EditLessonEnabled;
			set
			{
				_EditLessonEnabled = value;
				OnPropertyChanged(nameof(EditLessonEnabled));
			}
		}
		#endregion

		public ScheduleWindowViewModel()
		{
			_scheduleService = new ScheduleService();
			_lessonService = new LessonService();
			_exerciseService = new ExerciseService();
			_lessonTaskService = new LessonTaskService();
			_vocabularyServce = new VocabularyServce();
			if(MainApplicationService.CurrentUser != null)
			{
				UserData = new UserModel()
				{
					Id = MainApplicationService.CurrentUser.Id,
					UserName = MainApplicationService.CurrentUser.UserName,
					Password = MainApplicationService.CurrentUser.Password,
					Role = MainApplicationService.CurrentUser.Role,
					WorkExperience = MainApplicationService.CurrentUser.WorkExperience
				};				
			}
			ScheduleItems = new ObservableCollection<ScheduleModel>();			
		}
		/// <summary>
		/// полуение расписания данного пользователя
		/// </summary>
		public async Task GetUserSchedule()
		{
			var schedules = await _scheduleService.GetScheduleItemsByUserId(UserData.Id);
			if(schedules != null)
			{
				foreach(var item in schedules)
				{
					if(ScheduleItems.Where(x => x.Id == item.Id).FirstOrDefault() == null)
					{
						ScheduleItems.Add(new ScheduleModel()
						{
							Id = item.Id,
							UserId = item.UserId,
							Date = item.Date
						});
						ScheduleItemDates.Add(item.Date.ToString("MM/dd/yyyy"));
					}						
				}					
			}
			ScheduleAlreadyExists = (ScheduleItems.Count > 0) ? Visibility.Hidden : Visibility.Visible;
			ScheduleAlreadyExists1 = (ScheduleItems.Count > 0) ? Visibility.Visible : Visibility.Hidden;
			ScheduleCount = ScheduleItems.Count;
		}
		/// <summary>
		/// Создание элемента расписания
		/// </summary>
		/// <returns></returns>
		public async Task CreateUserSchedule()
		{
			if(SelectedDate != null && UserData != null)
			{
				if (ScheduleItems.Where(x => x.Date == SelectedDate).FirstOrDefault() != null)
				{
					System.Windows.MessageBox.Show("Уже существует расписание на выбранный день!");
					return;
				}
				var scheduleItem = await _scheduleService.CreateScheduleItem(SelectedDate ?? DateTime.UtcNow, UserData.Id);				
				ScheduleItems.Add(new ScheduleModel()
				{
					Id = scheduleItem.Id,
					Date = scheduleItem.Date,
					UserId = scheduleItem.UserId,					
				});
				ScheduleItemDates.Add(scheduleItem.Date.ToString("MM/dd/yyyy"));
				ScheduleCount = ScheduleItems.Count;
				System.Windows.MessageBox.Show("Добавление произведено успешно!");
			}
		}
		/// <summary>
		/// Получение выбранного элемента расписания
		/// </summary>
		/// <param name="Date"></param>
		public async Task GetSelectedItem(string Date)
		{
			DateTime current = new DateTime();
			DateTime.TryParseExact(Date, "MM.dd.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out current);
			if(ScheduleItems != null && ScheduleItems.Count > 0)
			{
				SelectedItem = ScheduleItems
					.Where(x => x.Date.Day == current.Day && x.Date.Month == current.Month && x.Date.Year == current.Year)
					.FirstOrDefault();

				await GetLessonsOfThisDay();
			}
		}
		/// <summary>
		/// метод удаления элемента из расписания
		/// </summary>
		/// <returns></returns>
		public async Task DeleteItem(ComboBox box)
		{
			if(SelectedItem != null)
			{
				bool prepareToDelete = await PrepareToDeleteSheduleItem(SelectedItem.Id);
				if (!prepareToDelete)
				{
					System.Windows.MessageBox.Show("Произошла ошибка при удалении элемента расписания!");
					return;
				}
				bool res = await _scheduleService.DeleteScheduleItem(SelectedItem.Date, SelectedItem.UserId);
				if (!res)
				{
					System.Windows.MessageBox.Show("ПРроизошла ошибка при удалении элемента расписания!");
				}
				else
				{					
					ScheduleItems.Remove(SelectedItem);
					string thisDate = SelectedItem.Date.ToString("MM.dd.yyyy");
					box.SelectedItem = null;
					ScheduleItemDates.Remove(thisDate);
					SelectedItem = null;
					ScheduleCount = ScheduleItems.Count;
					System.Windows.MessageBox.Show("Удаление произведено успешно!");
				}
			}
			else
			{
				System.Windows.MessageBox.Show("Выбранный элемент не найден!");
				return;
			}

		}
		/// <summary>
		/// Получение списка уроков на данный день
		/// </summary>
		public async Task GetLessonsOfThisDay()
		{
			if(SelectedItem != null)
			{
				LessonNames = new ObservableCollection<string>();
				var lessons = await _lessonService.GetAllLessonsOfCurrentScheduleItem(SelectedItem.Id);
				CurrentLessons = lessons.Select(l => new LessonReducedModel(l.Id, l.Name)).ToList();
				foreach(var item in lessons)
				{		
					LessonNames.Add(item.Name);
				}	
			}
		}

		public void GoBack(ScheduleWindow thisForm, MainWindow otherForm)
		{
			otherForm.Show();
			thisForm.Close();
		}
		/// <summary>
		/// переход на страницу редактирования урока
		/// </summary>
		public void GoToLessonsWindow(ScheduleWindow thisForm, LessonsWindow otherForm,string mode, Guid? scheduleId = null)
		{
			if(scheduleId != null && scheduleId != Guid.Empty)
			{
				MainApplicationService.ScheduleItemId = scheduleId ?? Guid.Empty;
				if (mode == "edit")
					MainApplicationService.LessonItemId = CurrentLessons.Where(l => l.Name == SelectedLessonName).Select(l => l.Id).FirstOrDefault();
				else if (mode == "create")
					MainApplicationService.LessonItemId = Guid.Empty;
			}
			else
			{
				System.Windows.MessageBox.Show("Вы не выбрали элемент расписания!");
				return;
			}
			otherForm.Show();
			thisForm.Close();
		}

		public void CheckSelectedLesson()
		{
			if (!string.IsNullOrEmpty(SelectedLessonName))
			{
				EditLessonEnabled = true;
			}
			else
			{
				EditLessonEnabled = false;
			}
		}

		public async Task DeleteLesson(ComboBox LessonsBox)
		{
			if (string.IsNullOrEmpty(SelectedLessonName))
			{
				System.Windows.MessageBox.Show("Урок не выбран!");
			}
			else
			{
				Guid ThisLessonId = CurrentLessons.Where(l => l.Name == SelectedLessonName).Select(l => l.Id).FirstOrDefault();
				// здесь происходит удаление словарей и отвязывание заданий от урока
				bool delVocab = await _vocabularyServce.DeleteVocabulariesOfCurrentLesson(ThisLessonId);
				var Exercises = await _exerciseService.GetListExercisesOfCurrentLesson(ThisLessonId);
				bool UnpinExercises = false;
				bool UnpinResult = true;
				foreach (var ex in Exercises)
				{
					UnpinExercises = await _lessonTaskService.UnpinTaskFromLesson(ThisLessonId, ex.Id);
					if (!UnpinExercises) UnpinResult = false;

				}
				if (!delVocab || !UnpinResult)
				{
					MessageBox.Show("Удаление урока прошло с ошибками!");
					return;
				}
				//здесь происходит удаление самого урока
				bool result = await _lessonService.DeleteLesson(ThisLessonId);
				if (!result)
					System.Windows.MessageBox.Show("Произошла ошибка при удалении урока!");
				else
				{
					LessonNames.Remove(SelectedLessonName);
					var lessons = await _lessonService.GetAllLessonsOfCurrentScheduleItem(SelectedItem.Id);
					CurrentLessons = lessons.Select(l => new LessonReducedModel(l.Id, l.Name)).ToList();
					LessonsBox.ItemsSource = LessonNames;
					SelectedLessonName = null;
					
				}
			}
		}
		/// <summary>
		/// подготовка к удалению дня в распискании
		/// </summary>
		/// <param name="scheduleItemId"></param>
		public async Task<bool> PrepareToDeleteSheduleItem(Guid scheduleItemId)
		{
			var lessons = await _lessonService.GetAllLessonsOfCurrentScheduleItem(scheduleItemId);
			if (lessons == null || lessons != null && lessons.Count == 0)
				return true;
			else
			{
				bool PrepareResult = true; // изначально считаем, что можно убирать день из расписания
				foreach(var item in lessons)
				{
					bool delVocab = true, unpinResult = true;
					delVocab = await _vocabularyServce.DeleteVocabulariesOfCurrentLesson(item.Id);
					var Exercises = await _exerciseService.GetListExercisesOfCurrentLesson(item.Id);
					bool UnpinExercises = false;
					foreach (var ex in Exercises)
					{
						UnpinExercises = await _lessonTaskService.UnpinTaskFromLesson(item.Id, ex.Id);
						if (!UnpinExercises) unpinResult = false;

					}
					if (!delVocab || !unpinResult)
					{
						MessageBox.Show("Удаление урока " + item.Name + " прошло с ошибками!");
						PrepareResult = false; //если не получилось удалить словари или отвязать уроки, то удалять день из расписания нельзя
						break;
					}
				}

				if (PrepareResult)
				{
					bool deleteLessonsResult = await _lessonService.DeleteAllLessonsOfCurrentScheduleItem(scheduleItemId);
					if (!deleteLessonsResult)
					{
						MessageBox.Show("Произошла ошибка при попытке удалить се уроки данного дня расписания!");
						PrepareResult = false; // если не получилось удалить уроки, то нельзя удалять день из расписания
					}
				}
				return PrepareResult;
			}
		}
	}
}
