using course_proj_english_tutorial.AppContext;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace course_proj_english_tutorial
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private MainWIndowViewModel contentModel { get; set; } = new MainWIndowViewModel();
		public MainWindow()
		{
			InitializeComponent();
			DataContext = contentModel;
			#region RedactionSettings
			EnableEditing.IsEnabled = contentModel.OpenInReviewMode;
			NameText.IsReadOnly = contentModel.OpenInReviewMode;
			AgeText.IsReadOnly = contentModel.OpenInReviewMode;
			GradeText.IsReadOnly = contentModel.OpenInReviewMode;
			FinishEditing.IsEnabled = contentModel.OpenInRedactionMode;
			#endregion

		}

		private void SignIn_Click(object sender, RoutedEventArgs e)
		{
			var newForm = new SignInWindow();
			newForm.Show();
			this.Close();
		}

		private void SignUp_Click(object sender, RoutedEventArgs e)
		{
			var newForm = new SignUpWindow();
			newForm.Show();
			this.Close();
		}

		private void LogOut_Click(object sender, RoutedEventArgs e)
		{
			contentModel.LogOutUser();
		}

		private void EditProfile_Click(object sender, RoutedEventArgs e)
		{
			contentModel.OpenInRedactionMode = true;
			contentModel.OpenInReviewMode = false;
			#region RedactionSettings
			EnableEditing.IsEnabled = contentModel.OpenInReviewMode;
			NameText.IsReadOnly = contentModel.OpenInReviewMode;
			AgeText.IsReadOnly = contentModel.OpenInReviewMode;
			GradeText.IsReadOnly = contentModel.OpenInReviewMode;
			FinishEditing.IsEnabled = contentModel.OpenInRedactionMode;
			#endregion
		}

		private async void FinishEditing_Click(object sender, RoutedEventArgs e)
		{
			contentModel.OpenInRedactionMode = false;
			contentModel.OpenInReviewMode = true;
			#region RedactionSettings
			EnableEditing.IsEnabled = contentModel.OpenInReviewMode;
			NameText.IsReadOnly = contentModel.OpenInReviewMode;
			AgeText.IsReadOnly = contentModel.OpenInReviewMode;
			GradeText.IsReadOnly = contentModel.OpenInReviewMode;
			FinishEditing.IsEnabled = contentModel.OpenInRedactionMode;
			#endregion
			bool result = await contentModel.EditUser();
		}

		private void EditShedule_Click(object sender, RoutedEventArgs e)
		{
			var scheduleForm = new ScheduleWindow();
			contentModel.GoToSchedule(this, scheduleForm);
		}

		private void EditJournal_Click(object sender, RoutedEventArgs e)
		{
			var journalForm = new JournalWindow();
			contentModel.GoToJournal(this, journalForm);
		}

		private async void GoToLessons_Click(object sender, RoutedEventArgs e)
		{
			var lessonCompleteForm = new LessonCompleteWindow();
			await contentModel.PrepareForLesson(this, lessonCompleteForm);
		}
	}
}
