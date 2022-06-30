using course_proj_english_tutorial.Models.DataModels;
using course_proj_english_tutorial.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace course_proj_english_tutorial.ViewModels.ContentViewModels
{
	/// <summary>
	/// модель представления создания словарей
	/// </summary>
	public class VocabularyCreateWindowViewModel: BaseViewModel
	{
		private LessonService lessonService;
		private VocabularyServce vocabularyServce;
		public Guid LessonId { get; set; }
		public VocabularyCreateWindowViewModel() {
			vocabularyListItems = new ObservableCollection<VocabularyListItem>();
			lessonService = new LessonService();
			vocabularyServce = new VocabularyServce();
		}
		public ObservableCollection<VocabularyListItem> vocabularyListItems { get; set; }
		
		public void AddItemToList(string word = null, string defin = null)
		{
			var item = new VocabularyListItem()
			{
				Word = word,
				Definition = defin
			};
			if (vocabularyListItems != null)
				vocabularyListItems.Add(item);
		}
		/// <summary>
		/// метод добавления словаря к уроку
		/// </summary>
		public async Task<bool> AddVocabularyToLesson(Guid? VocId = null)
		{
			try
			{
				if (vocabularyListItems == null || vocabularyListItems.Count == 0)
				{
					System.Windows.MessageBox.Show("Вы не добавили ни одной записи в словарь!");
					return false;
				}
				foreach(var item in vocabularyListItems)
				{
					if(string.IsNullOrEmpty(item.Word) || string.IsNullOrEmpty(item.Definition))
					{
						System.Windows.MessageBox.Show("Нельзя добавлять пустые записи в словарь!");
						return false;
					}
				}
				var dict = new Dictionary<string, string>();
				foreach(var item in vocabularyListItems)
				{
					dict.Add(item.Word, item.Definition);
				}
				MainApplicationService.WordsCount = dict.Count();
				var result = await vocabularyServce.AddVocabularyToLesson(dict, LessonId, VocId);
				if (result != null && result.LessonId != Guid.Empty)
					return true;
				else return false;

			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// <summary>
		/// получение данного словаря
		/// </summary>
		/// <param name="VocId"></param>
		public async Task GetCurrentVocabulary(Guid VocId)
		{
			var vocabulary = await vocabularyServce.GetVocabularyById(VocId);
			if(vocabularyListItems.Count > 0)
			{
				vocabularyListItems.Clear();
			}
			if (vocabulary.WordsTranslationsByteArray != null)
			{
				var WordsWithTranslations = lessonService.ByteArrayToObject(vocabulary.WordsTranslationsByteArray) as Dictionary<string, string>;
				if (WordsWithTranslations != null)
				{
					foreach (var item in WordsWithTranslations)
					{
						AddItemToList(item.Key, item.Value);
					}
				}
			}
			
		}
	}
}
