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
	/// Логика взаимодействия для ScheduleWindow.xaml
	/// </summary>
	public partial class ScheduleWindow : Window
	{
		private ScheduleWindowViewModel contentModel;
		public ScheduleWindow()
		{
			InitializeComponent();
			contentModel = new ScheduleWindowViewModel();
			DataContext = contentModel;
			ScheduleBox.ItemsSource = contentModel.ScheduleItemDates;
			
		}

		protected override async void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			await contentModel.GetUserSchedule();
		}

		private void OnSelectedDate_changed(object sender, SelectionChangedEventArgs e)
		{
			var calendar = sender as Calendar;
			contentModel.SelectedDate = calendar.SelectedDate;
		}

		private async void CreateSchedule_Click(object sender, RoutedEventArgs e)
		{
			await contentModel.CreateUserSchedule();
		}

		private void GoBack_Clicked(object sender, RoutedEventArgs e)
		{
			var mainWindow = new MainWindow();
			contentModel.GoBack(this, mainWindow);
		}

		private async void OnChangeItem_Event(object sender, SelectionChangedEventArgs e)
		{
			var combobox = sender as ComboBox;
			string date = (combobox.SelectedItem != null) ? combobox.SelectedItem.ToString() : "";
			await contentModel.GetSelectedItem(date);
			LessonsBox.ItemsSource = contentModel.LessonNames;
			contentModel.CheckSelectedLesson();
		}

		private async void DeleteSchedule_Click(object sender, RoutedEventArgs e)
		{
			await contentModel.DeleteItem(ScheduleBox);
			LessonsBox.ItemsSource = null;
		}

		private void OnSelectedLesson_Changed(object sender, SelectionChangedEventArgs e)
		{
			var combobox = sender as ComboBox;
			contentModel.SelectedLessonName = (combobox.SelectedItem != null) ? combobox.SelectedItem.ToString() : "";
			contentModel.CheckSelectedLesson();
		}

		private void CreateLesson_Click(object sender, RoutedEventArgs e)
		{
			Guid? scheduleId = contentModel.SelectedItem?.Id;
			var lessonsWindow = new LessonsWindow();
			contentModel.GoToLessonsWindow(this, lessonsWindow, "create", scheduleId);
		}

		private void EditLesson_Click(object sender, RoutedEventArgs e)
		{
			Guid? scheduleId = contentModel.SelectedItem?.Id;
			var lessonsWindow = new LessonsWindow();
			contentModel.GoToLessonsWindow(this, lessonsWindow, "edit", scheduleId);
		}

		private async void DeleteLesson_Click(object sender, RoutedEventArgs e)
		{
			await contentModel.DeleteLesson(LessonsBox);
		}
	}
}
