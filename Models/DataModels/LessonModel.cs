using course_proj_english_tutorial.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Models.DataModels
{
	public class LessonModel: BaseModel
	{
		/// <summary>
		/// название урока
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// описание урока
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// ID элемента расписания, к которому привязан урок
		/// </summary>
		public Guid ScheduleId { get; set; }
		public Schedule Schedule { get; set; }
		/// <summary>
		/// список словарей к конкретному уроку
		/// </summary>
		public ObservableCollection<VocabularyModel> LessonVocabularies { get; set; }
		/// <summary>
		/// список заданий данного урока
		/// </summary>
		public List<LessonTaskModel> LessonTasks { get; set; }
	}
}
