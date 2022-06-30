using course_proj_english_tutorial.Models.DataModels;
using course_proj_english_tutorial.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace course_proj_english_tutorial.ViewModels.ContentViewModels
{
	public class LessonCompleteWindowViewModel: BaseViewModel
	{
		private LessonService lessonService;
		private ExerciseService exerciseService;
		private VocabularyServce vocabularyServce;
		private AchievementService achievementService;

		#region DataModels
		/// <summary>
		/// список уроков в данный день
		/// </summary>
		private ObservableCollection<LessonModel> todayLessons = new ObservableCollection<LessonModel>();
		public ObservableCollection<LessonModel> TodayLessons
		{
			get => todayLessons;
			set
			{
				todayLessons = value;
				OnPropertyChanged(nameof(todayLessons));
			}
		}
		/// <summary>
		/// список заданий данного урока
		/// </summary>
		private ObservableCollection<ExerciseModel> chosenLessonExercises = new ObservableCollection<ExerciseModel>();
		public ObservableCollection<ExerciseModel> ChosenLessonExercises
		{
			get => chosenLessonExercises;
			set
			{
				chosenLessonExercises = value;
				OnPropertyChanged(nameof(chosenLessonExercises));
			}
		}
		/// <summary>
		/// список словарей данного урока
		/// </summary>
		private ObservableCollection<VocabularyModel> vocabulariesOfLesson = new ObservableCollection<VocabularyModel>();
		public ObservableCollection<VocabularyModel> VocabulariesOfLesson {
			get => vocabulariesOfLesson;
			set
			{
				vocabulariesOfLesson = value;
				OnPropertyChanged(nameof(vocabulariesOfLesson));
			}
		}
		/// <summary>
		/// Id выбранного урока
		/// </summary>
		public Guid? ChosenLessonId { get; set; }
		/// <summary>
		/// текущее задание
		/// </summary>
		private ExerciseModel currentExercise;
		public ExerciseModel CurrentExercise
		{
			get => currentExercise;
			set
			{
				currentExercise = value;
				OnPropertyChanged(nameof(currentExercise));
			}
		}
		/// <summary>
		/// список результатов выполнения заданий данного урока
		/// </summary>
		private ObservableCollection<ExerciseCompletionModel> exerciseCompletionModels;
		public ObservableCollection<ExerciseCompletionModel> ExerciseCompletionModels
		{
			get => exerciseCompletionModels;
			set
			{
				exerciseCompletionModels = value;
				OnPropertyChanged(nameof(exerciseCompletionModels));
			}
		}
		/// <summary>
		/// ID проходимого урока
		/// </summary>
		public Guid LessonInProgressId { get; set; }
		public string LessonInProgressName { get; set; }
		/// <summary>
		/// ID выполняемого задания
		/// </summary>
		public Guid ExerciseInProgressId { get; set; }
		public string CurrentAnswer { get; set; }
		
		private bool _AllLessonCompleted = false;
		/// <summary>
		/// флаг - все уроки пройдены
		/// </summary>
		public bool AllLessonCompleted
		{
			get => _AllLessonCompleted;
			set
			{
				_AllLessonCompleted = value;
				OnPropertyChanged(nameof(_AllLessonCompleted));
			}
		}

		#endregion

		#region WIndowSettings
		/// <summary>
		/// маркер начала урока
		/// </summary>
		private bool lessonStarted = false;
		public bool LessonStarted
		{
			get => lessonStarted;
			set
			{
				lessonStarted = value;
				OnPropertyChanged(nameof(lessonStarted));
			}
		}
		#endregion

		public LessonCompleteWindowViewModel()
		{
			lessonService = new LessonService();
			exerciseService = new ExerciseService();
			vocabularyServce = new VocabularyServce();
			achievementService = new AchievementService();
		}
		/// <summary>
		/// метод получения уроков данного дня занятий
		/// </summary>
		public async Task GetLessonsOfCurrentDay()
		{
			try
			{
				if (TodayLessons != null && TodayLessons.Count > 0)
				{
					TodayLessons.Clear();
				}
				var lessonsList = await lessonService
					.GetAllLessonsOfCurrentScheduleItem(MainApplicationService.CurrentStudyingDayId);
				if(lessonsList != null && lessonsList.Count > 0)
				{
					foreach(var les in lessonsList)
					{
						TodayLessons.Add(new LessonModel()
						{
							Id = les.Id,
							Name = les.Name,
							Description = les.Description,

						});
					}
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// <summary>
		/// метод получения заданий выбранного урока
		/// </summary>
		public async Task LoadExercisesOfCurrentLesson()
		{
			if (ChosenLessonExercises != null && ChosenLessonExercises.Count > 0)
			{
				ChosenLessonExercises.Clear();
			}
			if (ChosenLessonId != Guid.Empty)
			{
				var exercisesList = await exerciseService.GetListExercisesOfCurrentLesson(ChosenLessonId ?? Guid.Empty);
				if(exercisesList != null && exercisesList.Count > 0)
				{
					foreach(var ex in exercisesList)
					{
						var answers = ex.Answers.Split('+').ToList();
						var ansList = new List<ExerciseAnswerModel>();
						if(answers != null)
						{
							int i = 0;
							foreach (var item in answers)
							{
								ansList.Add(new ExerciseAnswerModel() { 
									AnswerNumber = ++i,
									AnswerString = item
								});
							}
						}
						
						ChosenLessonExercises.Add(new ExerciseModel()
						{
							Id= ex.Id,
							Number = ex.Number,
							Description = ex.Description,
							CorrectAnswer = ex.CorrectAnswer,
							ExerciseAnswers = ansList,
							ParentLessonId = ChosenLessonId
							//Answers = ansCol
						});
					}
				}
			}
		}
		/// <summary>
		/// метод загрузки словарей к данному уроку
		/// </summary>
		public async Task LoadVocabulariesOfCurrentLesson()
		{
			if(VocabulariesOfLesson != null && VocabulariesOfLesson.Count > 0)
			{
				VocabulariesOfLesson.Clear();
			}
			if(ChosenLessonId != Guid.Empty)
			{
				var vocabulariesList = await vocabularyServce.GetListVocabulariesOfCurrentLesson(ChosenLessonId ?? Guid.Empty);
				if(vocabulariesList != null)
				{
					int i = 0;
					foreach (var item in vocabulariesList)
					{
						var WordsTranslations = vocabularyServce.ByteArrayToObject(item.WordsTranslationsByteArray);
						var vocabularyItems = new List<VocabularyListItem>();
						var dict = WordsTranslations as Dictionary<string, string>;
						foreach(var d in dict){
							vocabularyItems.Add(new VocabularyListItem() { 
								Word = d.Key,
								Definition = d.Value
							});
						}
						VocabulariesOfLesson.Add(new VocabularyModel() { 
							Id = item.Id,
							LessonId = item.LessonId,
							WordsCount = dict.Count,
							WordsWithTranslations = dict,
							Index = ++i,
							VocabularyListItems = vocabularyItems
						});
					}
				}
			}
		}
		/// <summary>
		/// метод выбора текущего задания
		/// </summary>
		public void GetCurrentExercise(Guid exId)
		{
			if(ChosenLessonExercises != null && ChosenLessonExercises.Count > 0)
			{
				CurrentExercise = ChosenLessonExercises.Where(ex => ex.Id == exId).FirstOrDefault();
				ExerciseInProgressId = CurrentExercise.Id;
			}
		}

		public void CollapseAllNodesExceptChosen(List<TreeViewItem> allItems, TreeViewItem item)
		{
			foreach(var it in allItems)
			{
				if (it != item) it.IsExpanded = false;
			}
		}

		public childItem FindVisualChild<childItem>(DependencyObject obj)
	    where childItem : DependencyObject
		{
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(obj, i);
				if (child != null && child is childItem)
				{
					return (childItem)child;
				}
				else
				{
					childItem childOfChild = FindVisualChild<childItem>(child);
					if (childOfChild != null)
						return childOfChild;
				}
			}
			return null;
		}
		public List<TreeViewItem> FindTreeViewItems(Visual @this)
		{
			if (@this == null)
				return null;

			var result = new List<TreeViewItem>();

			var frameworkElement = @this as FrameworkElement;
			if (frameworkElement != null)
			{
				frameworkElement.ApplyTemplate();
			}

			Visual child = null;
			for (int i = 0, count = VisualTreeHelper.GetChildrenCount(@this); i < count; i++)
			{
				child = VisualTreeHelper.GetChild(@this, i) as Visual;

				var treeViewItem = child as TreeViewItem;
				if (treeViewItem != null)
				{
					result.Add(treeViewItem);				
				}
				foreach (var childTreeViewItem in FindTreeViewItems(child))
				{
					result.Add(childTreeViewItem);
				}
			}
			return result;
		}
		#region CompleteExercises
		public void StartLesson()
		{
			
			if(ChosenLessonId != null && ChosenLessonExercises != null && ChosenLessonExercises.Count > 0)
			{
				LessonStarted = true;
				LessonInProgressId = ChosenLessonId ?? Guid.Empty;
				ExerciseCompletionModels = new ObservableCollection<ExerciseCompletionModel>();
				LessonInProgressName = TodayLessons.Where(l => l.Id == ChosenLessonId).Select(l => l.Name).FirstOrDefault();
			}
		}
		/// <summary>
		/// метод ответа на задание
		/// </summary>
		public void CompleteExercise(TreeView lessonsView)
		{
			//bool result = false;
			var treeNodes = FindTreeViewItems(lessonsView);
			if(treeNodes != null)
			{
				var thisLessonNode = treeNodes.Where(n => n.Header.GetType() == typeof(string) && (n.Header as string) == LessonInProgressName).FirstOrDefault();
				foreach (var item in thisLessonNode.Items) {
					Guid chosenId = Guid.Empty;
					var myContentPresenter = FindVisualChild<ContentPresenter>(thisLessonNode.ItemContainerGenerator.ContainerFromItem(item));
					DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
					Grid grid = (Grid)myDataTemplate.FindName("grid", myContentPresenter);
					if(grid != null)
					{						
						string thisExerciseID = (grid.Children[2] as Label)?.Content?.ToString();
						if (!string.IsNullOrEmpty(thisExerciseID)) Guid.TryParse(thisExerciseID, out chosenId);
						// проверка на то, что задание уже было выполнено
						bool alreadyCompleted = ExerciseCompletionModels.Where(x => x.Id == chosenId).FirstOrDefault() != null;
						if(chosenId == ExerciseInProgressId && !alreadyCompleted)
						{
							grid.IsEnabled = false;
							ExerciseCompletionModels.Add(new ExerciseCompletionModel() { 
							Id = chosenId,
							Number = Convert.ToInt32((grid.Children[1] as Label)?.Content),
							IsCompleted = CheckAnswer(CurrentAnswer)
							});
							break;
						}
					}

				}

			}
		}
		/// <summary>
		/// проверка количества выполненных заданий
		/// </summary>
		/// <returns></returns>
		public bool CheckNumberOfCompletedExercises()
		{
			if (ExerciseCompletionModels != null
				&& ExerciseCompletionModels.Count == ChosenLessonExercises.Count) return true;
			else return false;
		}
		/// <summary>
		/// метод проверки ответа
		/// </summary>
		/// <param name="answer"></param>
		public bool CheckAnswer(string answer)
		{
			if (ExerciseInProgressId != Guid.Empty)
			{
				var currentExercise = ChosenLessonExercises.Where(e => e.Id == ExerciseInProgressId).FirstOrDefault();
				if (currentExercise != null)
				{
					return currentExercise.CorrectAnswer == answer;
				}
				else return false;
			}
			else return false;
		}

		public async Task CheckResultOfLesson()
		{
			if(ExerciseCompletionModels != null && ExerciseCompletionModels.Count > 0)
			{
				int CorrectAnswersCount = 0;
				string resultString = "";
				string exerciseNumbersString = "";
				foreach (var item in ExerciseCompletionModels)
				{

					if (item.IsCompleted) { 
						CorrectAnswersCount++;
						exerciseNumbersString += item.Number.ToString() + ", ";
					}
				}
				if(CorrectAnswersCount == ExerciseCompletionModels.Count)
				{

					resultString
						= achievementService.FormatMessageString(LessonInProgressName, LessonInProgressId, exerciseNumbersString, true);
				}
				else if(CorrectAnswersCount == 0)
				{
					resultString =
						achievementService.FormatMessageString(LessonInProgressName, LessonInProgressId, exerciseNumbersString, false, true);

				}
				else
				{
					resultString
						= achievementService.FormatMessageString(LessonInProgressName, LessonInProgressId, exerciseNumbersString);
				}
				double mark = Math.Round((double)CorrectAnswersCount / (double)ExerciseCompletionModels.Count * 5, MidpointRounding.AwayFromZero);
				var LessonCompleteResult = await achievementService.CreateAchievementEntry(resultString, Convert.ToInt32(mark), MainApplicationService.CurrentUser.Id);
				ExerciseCompletionModels = new ObservableCollection<ExerciseCompletionModel>();
				LessonStarted = false;
			}
		}
		/// <summary>
		/// проверка количества выполненных уроков
		/// </summary>
		/// <param name="lessonsTreeView"></param>
		public async Task CheckIfCurrentLessonIsCompleted(TreeView lessonsTreeView)
		{

			var lessonIDs = TodayLessons?.Select(x => x.Id).ToList();
			var completedList = await achievementService.CheckIfStudentCompletedCurrentLesson(MainApplicationService.CurrentUser.Id, lessonIDs);
			if(completedList != null && completedList.Count > 0)
			{
				var treeViewItems = FindTreeViewItems(lessonsTreeView);
				if(treeViewItems != null)
				{
					foreach(var node in treeViewItems)
					{
						var lessonId = (node.DataContext as LessonModel)?.Id;
						if (completedList.Contains(lessonId ?? Guid.Empty)) node.IsEnabled = false; 
					}
					// если все уроки пройдены на данный лень
					if (lessonIDs.Count == completedList.Count) AllLessonCompleted = true;
				}
			}
		}
		/// <summary>
		/// метод, срабатывающий, когда все уроки пройдены
		/// </summary>
		public void OnAllLessonsCompletedEvent(Button start)
		{
			LessonStarted = false;
			start.IsEnabled = false;
			MessageBox.Show("Все уроки на сегодня пройдены!");
		}
		#endregion
	}
}
