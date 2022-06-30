using course_proj_english_tutorial.Models.DataModels;
using course_proj_english_tutorial.Services;
using course_proj_english_tutorial.ViewModels.ContentViewModels;
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
	/// Логика взаимодействия для ExercisesListWindow.xaml
	/// </summary>
	public partial class ExercisesListWindow : Window
	{
		private ExercisesWindowViewModel contentModel;
		public ExercisesListWindow()
		{
			InitializeComponent();
			contentModel = new ExercisesWindowViewModel();
			DataContext = contentModel;
		}

		protected override async void OnActivated(EventArgs e)
		{
			await contentModel.GetAllExercises();
			contentModel.ThisLessonId = MainApplicationService.LessonItemId;
			ExercisesListBox.ItemsSource = contentModel.ExerciseModels;
			contentModel.NoExercisesCreated = (contentModel.ExerciseModels?.Count > 0)
				? Visibility.Hidden : Visibility.Visible;
		}

		private void GoBack_Clicked(object sender, RoutedEventArgs e)
		{
			var lessonsWindow = new LessonsWindow();
			contentModel.GoBack(this, lessonsWindow);
		}

		private void EditExercise_Clicked(object sender, RoutedEventArgs e)
		{
			var exerciseWindow = new ExercisesWindow();
			contentModel.CreateOrEditExercise("edit", exerciseWindow, this);
		}

		private void CreateExercise_Click(object sender, RoutedEventArgs e)
		{
			var exerciseWindow = new ExercisesWindow();
			contentModel.CreateOrEditExercise("create", exerciseWindow, this);
		}

		private async void AttachExercise_Clicked(object sender, RoutedEventArgs e)
		{
			await contentModel.AttachExerciseToLesson();
		}

		private void OnItemSelected_Event(object sender, SelectionChangedEventArgs e)
		{
			if(ExercisesListBox.SelectedItem != null)
			{
				var model = ExercisesListBox.SelectedItem as ExerciseModel;
				contentModel.ThisExerciseId = model.Id;
				contentModel.EditOrDeleteExerciseEnabled = true;
			}
		}

		private async void DelExercise_Clicked(object sender, RoutedEventArgs e)
		{
			await contentModel.RemoveExercise();
			ExercisesListBox.ItemsSource = contentModel.ExerciseModels;
			ExercisesListBox.SelectedItem = null;
			contentModel.NoExercisesCreated = (contentModel.ExerciseModels?.Count > 0)
				? Visibility.Hidden : Visibility.Visible;
			contentModel.EditOrDeleteExerciseEnabled = false;
		}
	}
}
