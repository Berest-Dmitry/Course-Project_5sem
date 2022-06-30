using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Models.DataModels
{
	public class ExerciseCompletionModel: BaseModel
	{
		public int Number { get; set; }
		public bool IsCompleted { get; set; }
	}
}
