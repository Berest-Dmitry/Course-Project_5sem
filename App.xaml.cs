using course_proj_english_tutorial.Common;
using course_proj_english_tutorial.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace course_proj_english_tutorial
{
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application
	{
		private MainApplicationService MainApplicationService;
		private SQLMethods methods;
		public App()
		{
			MainApplicationService = new MainApplicationService();
			methods = new SQLMethods();
			Exception error = null;
			PrepareDatabase(out error);
		}

		/// <summary>
		/// Метод подготовки БД
		/// </summary>
		bool PrepareDatabase(out Exception error)
		{
			var databaseExists = methods.CheckDatabase(MainApplicationService.ConnectionString, "mobiledb1", "localhost\\SQLEXPRESS", "sa", "qwerty_123");
			var result = false;
			string fullPath = Path.GetFullPath(MainApplicationService.CreateDBScriptFileName);
			if (!databaseExists)
			{						
				//result = methods.ExecuteScriptFromFile(MainApplicationService.PathToCreateDBScript, MainApplicationService.ConnectionString, out error, true);
				result = methods.ExecuteScriptFromFile(fullPath, MainApplicationService.ConnectionString, out error, true);
				if (!result) return false;
			}
			error = null;
			return true;
		}
	}
}
