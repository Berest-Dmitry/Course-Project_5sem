using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Models.DataModels
{
	public class VocabularyModel: BaseModel
	{
		/// <summary>
		/// список слов с переводом
		/// </summary>
		public Dictionary<string, string> WordsWithTranslations { get; set; }
		/// <summary>
		/// ID урока
		/// </summary>
		public Guid LessonId { get; set; }
		/// <summary>
		/// кол-во слов в словаре
		/// </summary>
		public int WordsCount { get; set; }
		/// <summary>
		/// индекс
		/// </summary>
		public int Index { get; set; }

		public List<VocabularyListItem> VocabularyListItems { get; set; }
	}
}
