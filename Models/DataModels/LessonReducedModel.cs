using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Models.DataModels
{
	/// <summary>
	/// уменьшенная модель урока для просмотра на странице расписания 
	/// </summary>
	public class LessonReducedModel: BaseModel
	{
		public LessonReducedModel(Guid id, string name)
		{
			Id = id;
			Name = name;
		}

		/// <summary>
		/// название урока
		/// </summary>
		public string Name { get; set; }
	}
}
