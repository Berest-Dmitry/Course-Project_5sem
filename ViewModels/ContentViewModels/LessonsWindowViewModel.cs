using course_proj_english_tutorial.Models.DataModels;
using course_proj_english_tutorial.Services;
using course_proj_english_tutorial.ViewModels.ControlViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace course_proj_english_tutorial.ViewModels.ContentViewModels
{
	public class LessonsWindowViewModel : BaseViewModel
	{
		private LessonService _lessonService;
		private VocabularyServce _vocabularyServce;
		private ExerciseService _exerciseService;
		private LessonTaskService _lessonTaskService;
		#region DataFields
		/// <summary>
		/// ID выбранного задания
		/// </summary>
		public Guid chosenExerciseId { get; set; }
		/// <summary>
		/// Id выбранного словаря
		/// </summary>
		public Guid chosenVocabularyId { get; set; }

		/// <summary>
		/// модель данного урока
		/// </summary>
		private LessonModel _CurrentLesson;
		public LessonModel CurrentLesson
		{
			get => _CurrentLesson;
			set
			{
				_CurrentLesson = value;
				OnPropertyChanged(nameof(CurrentLesson));
			}
		}

		/// <summary>
		/// название урока
		/// </summary>
		private string _LessonName = "";
		public string LessonName
		{
			get => _LessonName;
			set
			{
				_LessonName = value;
				OnPropertyChanged(nameof(LessonName));
			}
		}
		/// <summary>
		/// наименование урока
		/// </summary>
		private string _LessonDesc = "";
		public string LessonDesc
		{
			get => _LessonDesc;
			set
			{
				_LessonDesc = value;
				OnPropertyChanged(nameof(LessonDesc));
			}
		}
		/// <summary>
		/// данный элемент расписания
		/// </summary>
		public Guid ThisScheduleItemId { get; set; }
		public Guid ThisLessonId { get; set; }
		public ObservableCollection<VocabularyListItemViewModel> vocabularyListItemViewModels { get; set; }
		/// <summary>
		/// список заданий текущего урока
		/// </summary>
		public ObservableCollection<LessonExerciseModel> LessonExerciseModels { get; set; }
		#endregion


		public LessonsWindowViewModel()
		{
			_lessonService = new LessonService();
			_vocabularyServce = new VocabularyServce();
			_exerciseService = new ExerciseService();
			_lessonTaskService = new LessonTaskService();
			vocabularyListItemViewModels = new ObservableCollection<VocabularyListItemViewModel>();
			LessonExerciseModels = new ObservableCollection<LessonExerciseModel>();
		}

		#region VisibilitySettings
		/// <summary>
		/// свойство видимости полей редактирования урока
		/// </summary>
		private Visibility _EditLessonFields = Visibility.Hidden;
		public Visibility EditLessonFields
		{
			get => _EditLessonFields;
			set
			{
				_EditLessonFields = value;
				OnPropertyChanged(nameof(EditLessonFields));
			}
		}
		#endregion

		#region ButtonSettings
		private bool _VocabularySelected = false;
		public bool VocabularySelected
		{
			get => _VocabularySelected;
			set
			{
				_VocabularySelected = value;
				OnPropertyChanged(nameof(VocabularySelected));
			}
		}
		private bool _UnpinExerciseEnabled = false;
		public bool UnpinExerciseEnabled
		{
			get => _UnpinExerciseEnabled;
			set
			{
				_UnpinExerciseEnabled = value;
				OnPropertyChanged(nameof(UnpinExerciseEnabled));
			}
		}
		#endregion

		public void GoBack(LessonsWindow thisForm, ScheduleWindow otherForm)
		{
			otherForm.Show();
			thisForm.Close();
		}
		public void GoToExercisesWindow(LessonsWindow thisForm, ExercisesListWindow otherForm)
		{
			if(ThisLessonId != Guid.Empty)
			{
				MainApplicationService.LessonItemId = ThisLessonId;
				otherForm.Show();
				thisForm.Close();
			}
			else
			{
				System.Windows.MessageBox.Show("Произошла ошибка при переходе в окно заданий, данный урок не найден!");
			}
		}

		/// <summary>
		/// метод создания/редактирования урока
		/// </summary>
		public async Task CreateOrEditLesson()
		{
			var lesson = await _lessonService.GetEntityById(ThisLessonId);
			if(lesson == null)
			{
				//var lessonResult = await _lessonService.CreateLesson(ThisScheduleItemId, LessonName, LessonDesc);
				var lessonResult = await _lessonService.CreateLesson(ThisScheduleItemId, CurrentLesson.Name, CurrentLesson.Description);
				if(lessonResult == null)
				{
					System.Windows.MessageBox.Show("Произошла ошибка при добавлении урока!");
				}
				else
				{
					System.Windows.MessageBox.Show("Добавление урока прошло успешно!");
					ThisLessonId = lessonResult.Id;
					this.EditLessonFields = (this.ThisLessonId != Guid.Empty) ? Visibility.Visible : Visibility.Hidden;
					CurrentLesson = await LoadLastCreatedLesson(ThisLessonId);
					if (MainApplicationService.LessonItemId == Guid.Empty) MainApplicationService.LessonItemId = ThisLessonId;
				}
			}
			else
			{
				var resultModel = await _lessonService.UpdateLesson(CurrentLesson);
				if(resultModel.Error != null)
				{
					System.Windows.MessageBox.Show("Произошла ошибка при изменении урока!" + resultModel.Error.Message);
				}
				else
				{
					System.Windows.MessageBox.Show("Изменение урока прошло успешно!");
					CurrentLesson = resultModel;
				}
			}
		}
		/// <summary>
		/// метод получения данного урока
		/// </summary>
		public async Task GetCurrentLesson()
		{
			if(ThisLessonId == Guid.Empty)
			{
				CurrentLesson = new LessonModel();
			}
			else
			{
				var lesson = await _lessonService.GetEntityById(ThisLessonId);
				var vocabulariesList = await _vocabularyServce.GetListVocabulariesOfCurrentLesson(ThisLessonId);
				var lessonTasks = await _exerciseService.GetListExercisesOfCurrentLesson(ThisLessonId);
				if (LessonExerciseModels != null && LessonExerciseModels.Count > 0) LessonExerciseModels.Clear();
				foreach(var voc in vocabulariesList)
				{
					if(voc.WordsTranslationsByteArray != null)
					{
						var dict = _lessonService.ByteArrayToObject(voc.WordsTranslationsByteArray) as Dictionary<string, string>;
						voc.WordsWithTranslations = dict;
					}					
				}
				lesson.LessonVocabularies = new List<Entities.Vocabulary>();
				lesson.LessonTasks = new List<Entities.LessonTasks>();
				lesson.LessonVocabularies.AddRange(vocabulariesList);
				foreach (var ex in lessonTasks)
				{
					lesson.LessonTasks.Add(new Entities.LessonTasks()
					{
						LessonId = lesson.Id,
						TaskId = ex.Id
					});
					LessonExerciseModels.Add(new LessonExerciseModel() {
						LessonId = lesson.Id,
						ExerciseId = ex.Id,
						Number = ex.Number,
						Description = ex.Description
					});
				}
				vocabularyListItemViewModels = new ObservableCollection<VocabularyListItemViewModel>();
				CurrentLesson = new LessonModel()
				{
					Id = lesson.Id,
					Name = lesson.Name,
					Description = lesson.Description,
					ScheduleId = lesson.ScheduleId,
					LessonTasks = new List<LessonTaskModel>(),
					LessonVocabularies = new ObservableCollection<VocabularyModel>()
				};
				if (lesson.LessonVocabularies != null && lesson.LessonVocabularies.Count > 0)
				{
					int index = 0;
					foreach (var item in lesson.LessonVocabularies)
					{					
						CurrentLesson.LessonVocabularies.Add(new VocabularyModel()
						{
							Id = item.Id,
							WordsWithTranslations = (item.WordsWithTranslations != null) ? item.WordsWithTranslations : new Dictionary<string, string>(),
							LessonId = item.LessonId,
							WordsCount = (item.WordsWithTranslations != null) ? item.WordsWithTranslations.Count() : 0,
							Index = ++index
						});
						vocabularyListItemViewModels.Add(new VocabularyListItemViewModel() { 
							Index = index,
							WordsCount = (item.WordsWithTranslations != null) ? item.WordsWithTranslations.Count() : 0
						});
					}
				}
				if (lesson.LessonTasks != null && lessonTasks.Count > 0)
				{
					foreach (var item in lesson.LessonTasks)
					{
						CurrentLesson.LessonTasks.Add(new LessonTaskModel()
						{
							Id = item.Id,
							LessonId = item.LessonId,
							TaskId = item.TaskId
						});
					}
				}			
			}
		}
		/// <summary>
		/// метод получения ID выбранного словаря
		/// </summary>
		/// <param name="Index"></param>
		public void GetCurrentVocabularyId(int Index)
		{
			if(CurrentLesson?.LessonVocabularies != null && CurrentLesson?.LessonVocabularies.Count > 0)
			{
				chosenVocabularyId = CurrentLesson.LessonVocabularies.Where(v => v.Index == Index)
					.Select(v => v.Id).FirstOrDefault();
			}
		}
		/// <summary>
		/// метод обновления списка словарей после редактирования словаря
		/// </summary>
		public async Task UpdateLessonVocabulariesAfterChange(Guid? VocId = null)
		{
			var currentVoc = await _vocabularyServce.GetVocabularyById(VocId ?? Guid.Empty);
			if(currentVoc != null && CurrentLesson?.LessonVocabularies != null)
			{
				int index = CurrentLesson.LessonVocabularies.Where(v => v.Id == VocId).Select(v => v.Index).FirstOrDefault();
				var model = CurrentLesson.LessonVocabularies.Where(v => v.Id == VocId).FirstOrDefault();
				var indexModel = vocabularyListItemViewModels.Where(v => v.Index == index).FirstOrDefault();
				CurrentLesson.LessonVocabularies.Remove(model);
				var dict = _lessonService.ByteArrayToObject(currentVoc.WordsTranslationsByteArray) as Dictionary<string, string>;
				CurrentLesson.LessonVocabularies.Add(new VocabularyModel()
				{
					Id = currentVoc.Id,
					WordsWithTranslations = (currentVoc.WordsTranslationsByteArray != null) 
					? dict : new Dictionary<string, string>(),
					LessonId = currentVoc.LessonId,
					//WordsCount = (dict != null) ? dict.Count() : 0,
					WordsCount = MainApplicationService.WordsCount,
					Index = index
				});
				vocabularyListItemViewModels.Remove(indexModel);
				vocabularyListItemViewModels.Add(new VocabularyListItemViewModel()
				{
					Index = index,
					WordsCount = MainApplicationService.WordsCount
				});
				MainApplicationService.WordsCount = 0;
			}
		}
		/// <summary>
		/// метод удаления словаря
		/// </summary>
		public async Task DeleteVocabulary()
		{
			if(chosenVocabularyId != Guid.Empty)
			{
				if(CurrentLesson != null && CurrentLesson.LessonVocabularies != null
					&& CurrentLesson.LessonVocabularies.Count > 0)
				{
					var model = CurrentLesson.LessonVocabularies.Where(v => v.Id == chosenVocabularyId).FirstOrDefault();
					int modelIndex = model.Index;
					var itemNumber = vocabularyListItemViewModels.Where(m => m.Index == modelIndex).FirstOrDefault();
					CurrentLesson.LessonVocabularies.Remove(model);
					vocabularyListItemViewModels.Remove(itemNumber);
					var result = await _vocabularyServce.DeleteVocabulary(chosenVocabularyId);
					VocabularySelected =  false;
					foreach (var item in CurrentLesson.LessonVocabularies)
					{
						if (item.Index > modelIndex) item.Index--;
					}
					foreach(var it in vocabularyListItemViewModels)
					{
						if (it.Index > modelIndex) it.Index--;
					}
					chosenVocabularyId = Guid.Empty;
				}
			}

		}
		/// <summary>
		/// метод получения заданий текущего урока
		/// </summary>
		public async Task GetTasksOfCurrentLesson()
		{
			var exercisesList = await _exerciseService.GetListExercisesOfCurrentLesson(ThisLessonId);
			if(exercisesList != null && exercisesList.Count > 0)
			{
				if (LessonExerciseModels != null && LessonExerciseModels.Count > 0)
					LessonExerciseModels.Clear();

				foreach(var item in exercisesList)
				{
					LessonExerciseModels.Add(new LessonExerciseModel() { 
						LessonId = ThisLessonId,
						ExerciseId = item.Id,
						Number = item.Number,
						Description = item.Description
					});
				}
			}
		}
		/// <summary>
		/// метод открепления задания от урока
		/// </summary>
		public async Task DetachExerciseFromLesson()
		{
			if(chosenExerciseId != Guid.Empty && ThisLessonId != Guid.Empty)
			{
				bool result = await _lessonTaskService.UnpinTaskFromLesson(ThisLessonId, chosenExerciseId);
				if (result)
				{
					var thisModel = LessonExerciseModels
						.Where(m => m.LessonId == ThisLessonId && m.ExerciseId == chosenExerciseId).FirstOrDefault();

					if (thisModel != null) LessonExerciseModels.Remove(thisModel);

					if(CurrentLesson?.LessonTasks != null)
					{
						var thisListElem = CurrentLesson.LessonTasks
							 .Where(x => x.LessonId == ThisLessonId && x.TaskId == chosenExerciseId).FirstOrDefault();
						if (thisListElem != null) CurrentLesson.LessonTasks.Remove(thisListElem);
					}
					MessageBox.Show("Открепление адания прошло успешно!");
				}
			}
			else
			{
				MessageBox.Show("Произошла ошибка при откреплении задания. Возможно, задание не выбрано.");
			}
		}
		/// <summary>
		/// метод получения только что созданного урока
		/// </summary>
		/// <param name="thisID"></param>
		public async Task<LessonModel> LoadLastCreatedLesson(Guid thisID)
		{
			var createdEntity = await _lessonService.GetEntityById(thisID);
			return new LessonModel()
			{
				Id = createdEntity.Id,
				ScheduleId = createdEntity.ScheduleId,
				Name = createdEntity.Name,
				Description = createdEntity.Description
			};
		}
	}
}
