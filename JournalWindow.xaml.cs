using course_proj_english_tutorial.Models.DataModels;
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
	/// Логика взаимодействия для JournalWindow.xaml
	/// </summary>
	public partial class JournalWindow : Window
	{
		private JournalWindowViewModel contentModel { get; set; } = new JournalWindowViewModel();

		public JournalWindow()
		{
			InitializeComponent();
			DataContext = contentModel;
		}

		protected override async void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			await contentModel.GetAllTeachers();
			await contentModel.GetAllStudents();
			TeachersBox.ItemsSource = contentModel.ListOfTeachers;
			StudentsBox.ItemsSource = contentModel.ListOfStudents;
			await contentModel.GetJournalEntries();
			JournalEntriesBox.ItemsSource = contentModel.Journals;
		}

		private void GoBack_Clicked(object sender, RoutedEventArgs e)
		{
			var mainWindow = new MainWindow();
			contentModel.GoBack(this, mainWindow);
		}

		private void AddEntry_Clicked(object sender, RoutedEventArgs e)
		{
			JournalCreateWindow journalCreateWindow = new JournalCreateWindow();
			if(journalCreateWindow.ShowDialog() == true)
			{

			}
		}

		private void OnSelectedTeacherChanged_Event(object sender, SelectionChangedEventArgs e)
		{
			var selected = TeachersBox.SelectedItem as ShortUserModel;
			if (selected != null) contentModel.SelectedTeacherId = selected.Id;
		}

		private void OnJournalSelected_Event(object sender, SelectionChangedEventArgs e)
		{
			var selected = JournalEntriesBox.SelectedItem as JournalExtendedModel;
			if (selected != null) contentModel.ThisJournalId = selected.Id;
		}

		private async void RemoveEntry_Clicked(object sender, RoutedEventArgs e)
		{
			await contentModel.DeleteJournalEntry();
		}

		private async void AttachTeacher_Clicked(object sender, RoutedEventArgs e)
		{
			await contentModel.AttachPersonToJournal("teacher");
		}

		private async void DetachTeacher_clicked(object sender, RoutedEventArgs e)
		{
			await contentModel.DetachPersonFromJournal("teacher");
		}

		private async void DetachStudent_clicked(object sender, RoutedEventArgs e)
		{
			await contentModel.DetachPersonFromJournal("student");
		}

		private async void AttachStudent_Clicked(object sender, RoutedEventArgs e)
		{
			await contentModel.AttachPersonToJournal("student");
		}

		private void OnSelectedStudentChanged_Event(object sender, SelectionChangedEventArgs e)
		{
			var selected = StudentsBox.SelectedItem as ShortUserModel;
			if (selected != null) contentModel.SelectedStudentId = selected.Id;
		}
	}
}
