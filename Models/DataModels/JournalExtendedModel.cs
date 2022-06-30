using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Models.DataModels
{
	public class JournalExtendedModel: BaseModel
	{
		/// <summary>
		/// класс
		/// </summary>
		public int Grade { get; set; }
		/// <summary>
		/// начало периода времени
		/// </summary>
		public DateTime StartTime { get; set; }
		/// <summary>
		/// конец периода времени
		/// </summary>
		public DateTime EndTime { get; set; }
		/// <summary>
		/// ID учителя, относящегося к классу
		/// </summary>
		public Guid? TeacherId { get; set; }
		/// <summary>
		/// список ID учеников класса
		/// </summary>
		public List<Guid> StudentsIds { get; set; } = new List<Guid>();

		public string TimePeriod { get; set; }
	}
}
