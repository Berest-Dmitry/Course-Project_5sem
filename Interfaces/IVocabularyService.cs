using course_proj_english_tutorial.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Interfaces
{
	public interface IVocabularyService
	{
		/// <summary>
		/// метод добавления словаря к уроку
		/// </summary>
		/// <param name="WordsWithTranslations"></param>
		/// <param name="LessonId"></param>
		Task<Vocabulary> AddVocabularyToLesson(Dictionary<string, string> WordsWithTranslations, Guid LessonId, Guid? VocId = null);
		/// <summary>
		/// получение списка словарей к данному уроку
		/// </summary>
		/// <param name="LessonId"></param>
		Task<List<Vocabulary>> GetListVocabulariesOfCurrentLesson(Guid LessonId);
		/// <summary>
		/// метод получения словаря по его Id
		/// </summary>
		/// <param name="Id"></param>
		Task<Vocabulary> GetVocabularyById(Guid Id);
		/// <summary>
		/// метод удаления словаря
		/// </summary>
		/// <param name="vocId"></param>
		Task<bool> DeleteVocabulary(Guid vocId);
		/// <summary>
		/// метод удаления всех словарей, принадлежащих к данному уроку
		/// </summary>
		/// <param name="LessonId"></param>
		Task<bool> DeleteVocabulariesOfCurrentLesson(Guid LessonId);
	}
}
