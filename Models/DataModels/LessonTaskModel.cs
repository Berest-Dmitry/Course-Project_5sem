using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Models.DataModels
{
	public class LessonTaskModel: BaseModel
	{
		/// <summary>
		/// ID занятия
		/// </summary>
		public Guid LessonId { get; set; }
		/// <summary>
		/// ID задания
		/// </summary>
		public Guid TaskId { get; set; }
	}
}
