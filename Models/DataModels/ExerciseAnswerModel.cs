using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Models.DataModels
{
	/// <summary>
	/// модель ответа на задание
	/// </summary>
	public class ExerciseAnswerModel: BaseModel
	{
		/// <summary>
		/// номер ответа
		/// </summary>
		public int AnswerNumber { get; set; }
		/// <summary>
		/// строка ответа
		/// </summary>
		public string AnswerString { get; set; }
	}
}
