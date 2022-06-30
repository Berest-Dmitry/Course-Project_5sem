using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Models.DataModels
{
	public class ScheduleModel : BaseModel
	{
		/// <summary>
		/// дата
		/// </summary>
		public DateTime Date { get; set; }
		/// <summary>
		/// ID пользователя
		/// </summary>
		public Guid UserId { get; set; }
		public UserModel User { get; set; }
		
	}
}
