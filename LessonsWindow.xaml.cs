using course_proj_english_tutorial.Models.DataModels;
using course_proj_english_tutorial.Services;
using course_proj_english_tutorial.ViewModels.ContentViewModels;
using course_proj_english_tutorial.ViewModels.ControlViewModels;
using System;
using System.Collections.Generic;
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
	/// Логика взаимодействия для LessonsWindow.xaml
	/// </summary>
	public partial class LessonsWindow : Window
	{
		private LessonsWindowViewModel contentModel;
		public LessonsWindow()
		{
			InitializeComponent();
			contentModel = new LessonsWindowViewModel();
			DataContext = contentModel;

		}

		protected override async void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			contentModel.ThisScheduleItemId = MainApplicationService.ScheduleItemId;
			contentModel.ThisLessonId = MainApplicationService.LessonItemId;
			contentModel.EditLessonFields = (contentModel.ThisLessonId != Guid.Empty) ? Visibility.Visible : Visibility.Hidden;
			await contentModel.GetCurrentLesson();
			//VocabulariesList.ItemsSource = contentModel.CurrentLesson?.LessonVocabularies;
			VocabulariesList.ItemsSource = contentModel.vocabularyListItemViewModels;
			ExercisesBox.ItemsSource = contentModel.LessonExerciseModels;
		}

		private async void CreateLesson_Clicked(object sender, RoutedEventArgs e)
		{
			await contentModel.CreateOrEditLesson();
		}

		private void GoBack_Clicked(object sender, RoutedEventArgs e)
		{
			var scheduleWindow = new ScheduleWindow();
			contentModel.GoBack(this, scheduleWindow);
		}

		private void AddVocabulary_Clicked(object sender, RoutedEventArgs e)
		{
			VocabularyCreateWindow vocabularyCreateWindow = new VocabularyCreateWindow();
			vocabularyCreateWindow.ThisLessonId = contentModel.ThisLessonId;
			if (vocabularyCreateWindow.ShowDialog() == true)
			{
				
			}
		}

		private void OnSelectionChanged_Event(object sender, SelectionChangedEventArgs e)
		{
			if(VocabulariesList.SelectedItem != null)
			{
				contentModel.VocabularySelected = true;
				//var model = VocabulariesList.SelectedItem as VocabularyModel;
				var model = VocabulariesList.SelectedItem as VocabularyListItemViewModel;
				contentModel.GetCurrentVocabularyId(model.Index);
			}
		}

		private async void ChangeVocabulary_Clicked(object sender, RoutedEventArgs e)
		{
			VocabularyCreateWindow vocabularyCreateWindow = new VocabularyCreateWindow();
			vocabularyCreateWindow.ThisLessonId = contentModel.ThisLessonId;
			if (contentModel.chosenVocabularyId != Guid.Empty)
				vocabularyCreateWindow.ThisVocId = contentModel.chosenVocabularyId;

			if(vocabularyCreateWindow.ShowDialog() == false)
			{
				await contentModel.UpdateLessonVocabulariesAfterChange(vocabularyCreateWindow.ThisVocId);
				VocabulariesList.ItemsSource = null;
				VocabulariesList.ItemsSource = contentModel.vocabularyListItemViewModels;
				VocabulariesList.Items.Refresh();
			}
		}

		private async void DeleteVocabulary_Clicked(object sender, RoutedEventArgs e)
		{
			await contentModel.DeleteVocabulary();
			//VocabulariesList.ItemsSource = contentModel.CurrentLesson?.LessonVocabularies;
			VocabulariesList.ItemsSource = contentModel.vocabularyListItemViewModels;
			VocabulariesList.SelectedItem = null;
		}

		private void AttachExercise_Clicked(object sender, RoutedEventArgs e)
		{
			var exercisesWindow = new ExercisesListWindow();
			contentModel.GoToExercisesWindow(this, exercisesWindow);
		}

		private void CreateExercise_Clicked(object sender, RoutedEventArgs e)
		{
			var exercisesWindow = new ExercisesListWindow();
			contentModel.GoToExercisesWindow(this, exercisesWindow);
		}

		private async void DetachExercise_clicked(object sender, RoutedEventArgs e)
		{
			await contentModel.DetachExerciseFromLesson();
			ExercisesBox.ItemsSource = contentModel.LessonExerciseModels;
			ExercisesBox.SelectedItem = null;
			contentModel.UnpinExerciseEnabled = false;
		}

		private void OnItemSelected_Event(object sender, SelectionChangedEventArgs e)
		{
			if(ExercisesBox.SelectedItem != null)
			{
				contentModel.UnpinExerciseEnabled = true;
				var model = ExercisesBox.SelectedItem as LessonExerciseModel;
				if(model != null)
				{
					contentModel.chosenExerciseId = model.ExerciseId;
				}
			}
		}
	}
}
