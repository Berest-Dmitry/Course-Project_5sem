using course_proj_english_tutorial.Models.DataModels;
using course_proj_english_tutorial.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static course_proj_english_tutorial.Common.DefaultEnums;

namespace course_proj_english_tutorial.ViewModels.ContentViewModels
{
	public class ExercisesWindowViewModel : BaseViewModel
	{
		private ExerciseService _exerciseService;
		private LessonTaskService _lessonTaskService;

		#region DataFields
		/// <summary>
		/// список всех заданий
		/// </summary>
		private ObservableCollection<ExerciseModel> _ExerciseModels = new ObservableCollection<ExerciseModel>();
		public ObservableCollection<ExerciseModel> ExerciseModels
		{
			get => _ExerciseModels;
			set
			{
				_ExerciseModels = value;
				OnPropertyChanged(nameof(ExerciseModels));
			}
		}
		/// <summary>
		/// модель данного задания
		/// </summary>
		private ExerciseModel _CurrentExerciseModel = new ExerciseModel();
		public ExerciseModel CurrentExerciseModel
		{
			get => _CurrentExerciseModel;
			set
			{
				_CurrentExerciseModel = value;
				OnPropertyChanged(nameof(CurrentExerciseModel));
			}
		}
		/// <summary>
		/// список ответов на задание
		/// </summary>
		private ObservableCollection<ExerciseAnswerModel> exerciseAnswerModels = new ObservableCollection<ExerciseAnswerModel>();
		public ObservableCollection<ExerciseAnswerModel> ExerciseAnswerModels
		{
			get => exerciseAnswerModels;
			set
			{
				exerciseAnswerModels = value;
				OnPropertyChanged(nameof(ExerciseAnswerModels));
			}
		}
		/// <summary>
		/// модель выбранного ответа
		/// </summary>
		public ExerciseAnswerModel SelectedAnswer { get; set; }
		/// <summary>
		/// ID выбранного занятия
		/// </summary>
		public Guid ThisExerciseId { get; set; }
		/// <summary>
		/// Id данного урока
		/// </summary>
		public Guid ThisLessonId { get; set; }
		#endregion

		#region VisibilitySettings
		private Visibility _NoExercisesCreated = Visibility.Visible;
		public Visibility NoExercisesCreated
		{
			get => _NoExercisesCreated;
			set
			{
				_NoExercisesCreated = value;
				OnPropertyChanged(nameof(NoExercisesCreated));
			}
		}
		#endregion

		#region ButtonSettings
		private bool _CanDeleteAnswer = false;
		public bool CanDeleteAnswer
		{
			get => _CanDeleteAnswer;
			set
			{
				_CanDeleteAnswer = value;
				OnPropertyChanged(nameof(CanDeleteAnswer));
			}
		}

		private bool _EditOrDeleteExerciseEnabled = false;
		public bool EditOrDeleteExerciseEnabled
		{
			get => _EditOrDeleteExerciseEnabled;
			set
			{
				_EditOrDeleteExerciseEnabled = value;
				OnPropertyChanged(nameof(EditOrDeleteExerciseEnabled));
			}
		}
		#endregion

		public ExercisesWindowViewModel()
		{
			_exerciseService = new ExerciseService();
			_lessonTaskService = new LessonTaskService();
		}
		/// <summary>
		/// возврат к окну урока
		/// </summary>
		public void GoBack(ExercisesWindow thisForm, LessonsWindow otherForm)
		{
			otherForm.Show();
			thisForm.Close();
		}
		/// <summary>
		/// возврат к окну урока
		/// </summary>
		public void GoBack(ExercisesListWindow thisForm, LessonsWindow otherForm)
		{
			otherForm.Show();
			thisForm.Close();
		}
		/// <summary>
		/// переход к окну создания/редактирования задания
		/// </summary>
		public void GoToCreateExerciseWindow(ExercisesListWindow thisForm, ExercisesWindow otherForm)
		{			
			otherForm.Show();
			thisForm.Close();
		}
		/// <summary>
		/// метод получения всех существующих заданий
		/// </summary>
		public async Task GetAllExercises()
		{
			if(ExerciseModels != null && ExerciseModels.Count > 0)
			{
				ExerciseModels.Clear();				
			}
			else
			{
				ExerciseModels = new ObservableCollection<ExerciseModel>();
			}
			var list = await _exerciseService.GetAllExercises();
			if (list != null)
			{
				foreach (var item in list)
				{
					ExerciseModels.Add(new ExerciseModel()
					{
						Id = item.Id,
						Number = item.Number,
						Description = item.Description
					});
				}
			}
		}
		/// <summary>
		/// метод перехода к созданию/редактированию задания
		/// </summary>
		/// <param name="mode"></param>
		public void CreateOrEditExercise(string mode, ExercisesWindow exercisesWindow, ExercisesListWindow exercisesListWindow)
		{
			switch (mode)
			{
				case "create":
					MainApplicationService.CurrentExerciseItemId = Guid.Empty;
					GoToCreateExerciseWindow(exercisesListWindow, exercisesWindow);
					break;

				case "edit":
					MainApplicationService.CurrentExerciseItemId = ThisExerciseId;					
					GoToCreateExerciseWindow(exercisesListWindow, exercisesWindow);
					break;
			}
		}
		/// <summary>
		/// основной метод создания/редактирования задания
		/// </summary>
		public async Task CreateOrEditExerciseFinish()
		{
			if(CurrentExerciseModel != null)
			{
				var current = await _exerciseService.GetEntityById(CurrentExerciseModel.Id);
				if(current == null)
				{
					var ansList = new List<string>();
					
					foreach(var item in ExerciseAnswerModels)
					{
						ansList.Add(item.AnswerString);
					}
					var result = await _exerciseService.CreateExercise(CurrentExerciseModel.Number, CurrentExerciseModel.Description
						, CurrentExerciseModel.CorrectAnswer, ansList);
				}
				else
				{
					if(CurrentExerciseModel?.Answers != null && CurrentExerciseModel?.Answers.Count > 0)
					{
						CurrentExerciseModel.Answers.Clear();
					}
					else
					{
						CurrentExerciseModel.Answers = new ObservableCollection<string>();
					}
					foreach (var item in ExerciseAnswerModels)
					{
						CurrentExerciseModel.Answers.Add(item.AnswerString);
					}
					var editedEx = await _exerciseService.UpdateExercise(CurrentExerciseModel);
					if(editedEx.Result != Result.ok)
					{
						MessageBox.Show(editedEx.Error.Message);
					}
				}
			}
		}
		/// <summary>
		/// получение данного элемента задания
		/// </summary>
		/// <param name="Id"></param>
		public async Task GetCurrentExercise(Guid Id)
		{
			var entity = await _exerciseService.GetEntityById(Id);
			if(entity != null)
			{
				CurrentExerciseModel = new ExerciseModel()
				{
					Id = entity.Id,
					Number = entity.Number,
					Description = entity.Description,
					CorrectAnswer = entity.CorrectAnswer
				};
				string curString = entity.Answers.Trim();
				if (!string.IsNullOrEmpty(curString))
				{
					if(ExerciseAnswerModels != null && ExerciseAnswerModels.Count > 0)
					{
						ExerciseAnswerModels.Clear();
					}
					var answerStrings = entity.Answers.Split('+');
					if (answerStrings != null)
					{
						if (CurrentExerciseModel.Answers == null)
							CurrentExerciseModel.Answers = new ObservableCollection<string>();
						var ansList = answerStrings.ToList<string>();
						int index = 0;
						foreach(var str in ansList)
						{
							++index;
							CurrentExerciseModel.Answers.Add(str);
							ExerciseAnswerModels.Add(new ExerciseAnswerModel()
							{
								AnswerNumber = index,
								AnswerString = str
							}); 
						}
					}
				}
				
			}
		}
		/// <summary>
		/// добавление ответа в список
		/// </summary>
		public void AddAnswerToList()
		{
			if(CurrentExerciseModel != null)
			{
				if (CurrentExerciseModel.Answers != null)
					//CurrentExerciseModel.Answers.Add(string.Empty);
					ExerciseAnswerModels.Add(new ExerciseAnswerModel()
					{
						AnswerString = ""
					});
				else
				{
					//CurrentExerciseModel.Answers = new ObservableCollection<string>();
					//CurrentExerciseModel.Answers.Add(string.Empty);
					ExerciseAnswerModels.Add(new ExerciseAnswerModel()
					{
						AnswerString = ""
					});
				}
			}

		}
		/// <summary>
		/// удаление ответа из списка
		/// </summary>
		public void RemoveAnswerFromList()
		{
			if(SelectedAnswer != null)
			{
				var ans = ExerciseAnswerModels
					.Where(a => a.AnswerNumber == SelectedAnswer.AnswerNumber && a.AnswerString == SelectedAnswer.AnswerString)
					.FirstOrDefault();
				if(ans != null)
				{
					ExerciseAnswerModels.Remove(ans);
				}
			}
		}
		/// <summary>
		/// прикрепление занятия к уроку
		/// </summary>
		public async Task AttachExerciseToLesson()
		{
			if(ThisLessonId != Guid.Empty && ThisExerciseId != Guid.Empty)
			{
				bool TaskAlreadyPinned = await _lessonTaskService.CheckIfTaskIsPinnedToLesson(ThisLessonId, ThisExerciseId);
				if (TaskAlreadyPinned)
				{
					MessageBox.Show("Выбранное вами задание уже прикреплено к данному уроку!");
					return;
				}
				var result = await _lessonTaskService.PinTaskToLesson(ThisLessonId, ThisExerciseId);
				if(result != null)
				{
					MessageBox.Show("Выбранное вами задание успешно прикреплено к уроку!");
				}
				else
				{
					MessageBox.Show("При прикреплении задания к уроку произошла ошибка!");
				}
			}
		}
		/// <summary>
		/// метод удаления задания
		/// </summary>
		public async Task RemoveExercise()
		{
			if(ThisExerciseId != Guid.Empty)
			{
				var thisExerciseModel = ExerciseModels.Where(x => x.Id == ThisExerciseId).FirstOrDefault();
				var entries = await _lessonTaskService.GetAllEntriesOfCurrentExercise(ThisExerciseId);
				if(entries != null && entries.Count > 0)
				{
					var delResult = await _lessonTaskService.RemoveEntries(entries);
					if (!delResult)
					{
						MessageBox.Show("Произошла ошибка при удалении задания!");
						return;
					}
				}
				var result = await _exerciseService.DeleteExercise(ThisExerciseId);
				if (!result)
				{
					MessageBox.Show("Произошла ошибка при удалении задания!");
					return;
				}
				else
				{
					ExerciseModels.Remove(thisExerciseModel);
					MessageBox.Show("Удаление задания прошло успешно!");
				}
			}
		}
	}
}
