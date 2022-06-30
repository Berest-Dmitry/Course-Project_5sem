using course_proj_english_tutorial.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.ViewModels.ContentViewModels
{
	public class SignInViewModel: BaseViewModel
	{
		private UserService userService;
		//private MainApplicationService MainApplicationService;
		public SignInViewModel() {
			userService = new UserService();
			//MainApplicationService = new MainApplicationService();
		}
		/// <summary>
		/// имя пользователя
		/// </summary>
		private string _UserName;
		public string UserName
		{
			get => _UserName;
			set
			{
				_UserName = value;
				OnPropertyChanged(nameof(_UserName));
			}
		}
		/// <summary>
		/// пароль
		/// </summary>
		private string _Password;
		public string Password
		{
			get => _Password;
			set
			{
				_Password = value;
				OnPropertyChanged(nameof(_Password));
			}
		}
		/// <summary>
		/// роль пользователя
		/// </summary>
		private int _Role = -1;
		public int Role
		{
			get => _Role;
			set
			{
				_Role = value;
				OnPropertyChanged(nameof(_Role));
			}
		}

		public void RoleChanged(string roleName)
		{
			switch (roleName)
			{
				case "Администратор":
					Role = 1;
					break;

				case "Учитель":
					Role = 2;
					break;

				case "Студент":
					Role = 3;
					break;
			}
		}
		/// <summary>
		/// метод регистрации пользователя
		/// </summary>
		public async Task<bool> RegisterUser()
		{
			UserName = (UserName != null) ? UserName.Trim() : "";
			Password = (Password != null) ? Password.Trim() : "";
			if(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password) || Role == -1)
			{
				System.Windows.MessageBox.Show("Необходимые данные не заполнены!");
				return false;
			}
			var user = await userService.RegisterUser(UserName, Password, Role);
			if(user != null)
			{
				System.Windows.MessageBox.Show("Регистрация прошла успешно!");
				return true;
			}
			else
			{
				System.Windows.MessageBox.Show("При прохождении регистрации произошла ошибка!");
				return false;
			}
		}

		public async Task<bool> LoginUser()
		{
			UserName = (UserName != null) ? UserName.Trim() : "";
			Password = (Password != null) ? Password.Trim() : "";
			if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
			{
				System.Windows.MessageBox.Show("Необходимые данные не заполнены!");
				return false;
			}
			else
			{
				var result = await userService.LoginUser(UserName, Password);
				if (!result)
				{
					System.Windows.MessageBox.Show("Такого пользователя не существует, введите данные еще раз.");
					MainApplicationService.UserAuthorized = false;
				}
				else
				{
					MainApplicationService.UserAuthorized = true;
					MainApplicationService.CurrentUser = await userService.GetCurrentUser(UserName);
				}
				return result;
			}
		}
	}
}
