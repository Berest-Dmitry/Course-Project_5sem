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
	/// Логика взаимодействия для ExercisesWindow.xaml
	/// </summary>
	public partial class ExercisesWindow : Window
	{
		private ExercisesWindowViewModel contentModel;
		public ExercisesWindow()
		{
			InitializeComponent();
			contentModel = new ExercisesWindowViewModel();
			DataContext = contentModel;
		}

		protected override async void OnActivated(EventArgs e)
		{
			contentModel.ThisExerciseId = MainApplicationService.CurrentExerciseItemId;
			if(contentModel.ThisExerciseId != Guid.Empty)
			{
				await contentModel.GetCurrentExercise(contentModel.ThisExerciseId);
				if(contentModel.CurrentExerciseModel != null)
				{
					//AnswerBox.ItemsSource = contentModel.CurrentExerciseModel.Answers;
					AnswerBox.ItemsSource = contentModel.ExerciseAnswerModels;

				}
			}
		}

		private void GoBack_Clicked(object sender, RoutedEventArgs e)
		{
			var lessonsWindow = new LessonsWindow();
			contentModel.GoBack(this, lessonsWindow);
		}

		private async void Create_Clicked(object sender, RoutedEventArgs e)
		{
			await contentModel.CreateOrEditExerciseFinish();
		}

		private void AddAnswer_Clicked(object sender, RoutedEventArgs e)
		{
			contentModel.AddAnswerToList();
			//AnswerBox.ItemsSource = contentModel.CurrentExerciseModel.Answers;
			AnswerBox.ItemsSource = contentModel.ExerciseAnswerModels;
		}

		private void OnSelectedAnswer_Event(object sender, SelectionChangedEventArgs e)
		{
			contentModel.CanDeleteAnswer = true;
			var listBox = sender as ListBox;
			var selected = listBox.SelectedItem as ExerciseAnswerModel;
			if (selected != null) contentModel.SelectedAnswer = selected;
		}

		private void DelAnswer_Clicked(object sender, RoutedEventArgs e)
		{
			contentModel.RemoveAnswerFromList();
			AnswerBox.ItemsSource = contentModel.ExerciseAnswerModels;
		}
	}
}
