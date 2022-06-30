using course_proj_english_tutorial.Models.DataModels;
using course_proj_english_tutorial.ViewModels.ContentViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace course_proj_english_tutorial
{
	/// <summary>
	/// Логика взаимодействия для LessonCompleteWindow.xaml
	/// </summary>
	public partial class LessonCompleteWindow : Window
	{
		private LessonCompleteWindowViewModel contentModel { get; set; } = new LessonCompleteWindowViewModel();
		public LessonCompleteWindow()
		{
			InitializeComponent();
			DataContext = contentModel;
		}

		protected override async void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			await contentModel.GetLessonsOfCurrentDay();
			LessonsTree.ItemsSource = contentModel.TodayLessons;
			AnswersBox.IsEnabled = contentModel.LessonStarted;
			AnswerTask.IsEnabled = contentModel.LessonStarted;
			VocabulariesList.IsEnabled = contentModel.LessonStarted;
			await contentModel.CheckIfCurrentLessonIsCompleted(LessonsTree);
			if (contentModel.AllLessonCompleted)
			{
				contentModel.LessonStarted = false;
				startLessonButton.IsEnabled = false;
				VocabulariesList.ItemsSource = null;
				AnswersBox.ItemsSource = null;
				HeaderLabel.Content = "Все уроки на сегодня пройдены!";
				
			}
		}

		private async void TasksExpanded_Event(object sender, RoutedEventArgs e)
		{
			var treeItem = sender as TreeViewItem;
			if(contentModel.ChosenLessonId != null)
			{				
				await contentModel.LoadExercisesOfCurrentLesson();
				await contentModel.LoadVocabulariesOfCurrentLesson();
				treeItem.ItemsSource = contentModel.ChosenLessonExercises;
				VocabulariesList.ItemsSource = contentModel.VocabulariesOfLesson;
				contentModel.CurrentExercise = null;
				List<TreeViewItem> allTreeViewItems = new List<TreeViewItem>();
				allTreeViewItems = contentModel.FindTreeViewItems(LessonsTree);
				contentModel.CollapseAllNodesExceptChosen(allTreeViewItems, treeItem);
			}
			
		}

		private void TreeItemClicked(object sender, MouseButtonEventArgs e)
		{
			var selectedNode = sender as TreeViewItem;
			var context = selectedNode.DataContext as LessonModel;
			if (context != null) { 
				contentModel.ChosenLessonId = context.Id;
				startLessonButton.IsEnabled = true;
				
			}
		}

		private void ExerciseClicked_Event(object sender, MouseButtonEventArgs e)
		{
			var grid = sender as Grid;
			if(grid != null)
			{
				Guid thisExId = Guid.Empty;
				var label = grid.Children[2] as Label;
				if (label != null) thisExId = (Guid)label.Content;
				contentModel.GetCurrentExercise(thisExId);
			}
		}

		private void OnItemSelected_Event(object sender, RoutedEventArgs e)
		{
			var RB = sender as RadioButton;
			if(RB != null)
			{
				var answer = RB.DataContext as ExerciseAnswerModel;
				if(AnswersBox != null && answer != null && AnswersBox.Items != null && AnswersBox.Items.Count > 0)
				{
					foreach (var item in AnswersBox.Items)
					{
						var myContentPresenter = contentModel.FindVisualChild<ContentPresenter>(AnswersBox.ItemContainerGenerator.ContainerFromItem(item));
						DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
						RadioButton rb = (RadioButton)myDataTemplate.FindName("rb", myContentPresenter);
						if (rb != null && rb.Content != RB.Content) rb.IsChecked = false;
						contentModel.CurrentAnswer = RB.Content.ToString();
					}

				}
			}
		}

		private async void AnswerQuestion_Clicked(object sender, RoutedEventArgs e)
		{
			contentModel.CompleteExercise(LessonsTree);
			contentModel.CurrentExercise = null;
			bool res = contentModel.CheckNumberOfCompletedExercises();
			if (res)
			{
				await contentModel.CheckResultOfLesson();
				contentModel.ExerciseCompletionModels = new ObservableCollection<ExerciseCompletionModel>();
				MessageBox.Show("Данный урок пройден!");
			}
				
		}

		private void LessonStarted_Click(object sender, RoutedEventArgs e)
		{
			contentModel.StartLesson();
			AnswersBox.IsEnabled = contentModel.LessonStarted;
			AnswerTask.IsEnabled = contentModel.LessonStarted;
			VocabulariesList.IsEnabled = contentModel.LessonStarted;
		}
	}
}
