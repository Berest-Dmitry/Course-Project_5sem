using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.ViewModels.ControlViewModels
{
	/// <summary>
	/// модель для отображения списка элементов словарей
	/// </summary>
	public class VocabularyListItemViewModel: BaseViewModel
	{
		/// <summary>
		/// индекс элемента в списке
		/// </summary>
		private int _Index;
		public int Index
		{
			get => _Index;
			set
			{
				_Index = value;
				OnPropertyChanged(nameof(Index));
			}
		}
		/// <summary>
		/// кол-во слов в словаре
		/// </summary>
		private int _WordsCount;
		public int WordsCount
		{
			get => _WordsCount;
			set
			{
				_WordsCount = value;
				OnPropertyChanged(nameof(_WordsCount));
			}
		}
	}
}
