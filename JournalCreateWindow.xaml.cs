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
	/// Логика взаимодействия для JournalCreateWindow.xaml
	/// </summary>
	public partial class JournalCreateWindow : Window
	{
		private JournalWindowViewModel contentModel { get; set; } = new JournalWindowViewModel();
		public JournalCreateWindow()
		{
			InitializeComponent();
			DataContext = contentModel;
		}

		private async void Accept_Click(object sender, RoutedEventArgs e)
		{		
			bool AddResult = await contentModel.AddJournalEntry();
			if (AddResult) this.Close();
		}

		private void MinDateCahged_Event(object sender, SelectionChangedEventArgs e)
		{
			var calendar = sender as Calendar;
			contentModel.StartDT = calendar.SelectedDate;
		}

		private void MaxDateCahged_Event(object sender, SelectionChangedEventArgs e)
		{
			var calendar = sender as Calendar;
			contentModel.EndDT = calendar.SelectedDate;
		}

		private void OnInputChanged_Event(object sender, TextChangedEventArgs e)
		{
			var textBox = sender as TextBox;
			int newValue = 0;
			int.TryParse(textBox.Text, out newValue);
			if (newValue != 0) contentModel.Grade = newValue;
		}
	}
}
