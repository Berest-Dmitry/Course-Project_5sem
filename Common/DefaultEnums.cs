using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Common
{
	public class DefaultEnums
	{
		public enum UserRoles
		{
			[Description("Rights of System Admin")]
			SystemAdmin = 1,
			[Description("Rights of Teacher")]
			Tutor = 2,
			[Description("Rights of Student")]
            Student = 3,
			[Description("Not registered yet")]
			NotRegistered = 0,
		}

		public enum KnowledgeLevels
		{
			[Description("Basic level")]
			A,
			[Description("Beginner/Elementary")]
			A1,
			[Description("Pre-Intermediate")]
			A2,
			[Description("Independent")]
			B,
			[Description("Intermediate")]
			B1,
			[Description("Upper-Intermediate")]
			B2
		}
		/// <summary>
		/// Ответы
		/// </summary>
		public enum Result
		{
			ok = 1,
			error = 0
		}
	}
}
