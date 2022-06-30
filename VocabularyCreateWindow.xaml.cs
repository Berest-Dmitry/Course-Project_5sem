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
	/// Логика взаимодействия для VocabularyCreateWindow.xaml
	/// </summary>
	public partial class VocabularyCreateWindow : Window
	{
		private VocabularyCreateWindowViewModel contentModel { get; set; }
		public Guid ThisLessonId { get; set; }
		public Guid ThisVocId { get; set; }
		public bool AddResult = false;
		public VocabularyCreateWindow()
		{
			InitializeComponent();
			contentModel = new VocabularyCreateWindowViewModel();
			DataContext = contentModel;
			VocabularyObjects.ItemsSource = contentModel.vocabularyListItems;			
		}

		protected override async void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			contentModel.LessonId = ThisLessonId;
			if (ThisVocId != Guid.Empty)
				await contentModel.GetCurrentVocabulary(ThisVocId);
		}

		private async void Accept_Click(object sender, RoutedEventArgs e)
		{
			if (ThisVocId != Guid.Empty)
				AddResult = await contentModel.AddVocabularyToLesson(ThisVocId);
			else AddResult = await contentModel.AddVocabularyToLesson();

			if (AddResult)
				this.Close();
		}

		private void AddElement_Click(object sender, RoutedEventArgs e)
		{
			contentModel.AddItemToList();
			VocabularyObjects.ItemsSource = contentModel.vocabularyListItems;
		}
	}
}
