using course_proj_english_tutorial.Entities.Base;
using System;
using System.Collections.Generic;

namespace course_proj_english_tutorial.Entities
{
	/// <summary>
	/// словарь
	/// </summary>
	public class Vocabulary: Entity
	{
		/// <summary>
		/// список слов с переводом
		/// </summary>
		public Dictionary<string, string> WordsWithTranslations { get; set; }
		/// <summary>
		/// байтовый массив, хранящий список слов с переводом
		/// </summary>
		public byte[] WordsTranslationsByteArray { get; set; }
		/// <summary>
		/// ID урока
		/// </summary>
		public Guid LessonId { get; set; }
		public Lesson Lesson { get; set; }

		public static Vocabulary Create(byte[] WordsTranslationsByteArray, Guid LessonId)
		{
			var vocabulary = new Vocabulary()
			{
				Id = Guid.NewGuid(),
				LessonId = LessonId,
				WordsTranslationsByteArray = WordsTranslationsByteArray
				//WordsWithTranslations = WordsWithTranslations
			};
			return vocabulary;
		}
	}
}
