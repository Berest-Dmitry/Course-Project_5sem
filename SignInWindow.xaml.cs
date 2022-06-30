using course_proj_english_tutorial.ViewModels.ContentViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace course_proj_english_tutorial
{
	/// <summary>
	/// Логика взаимодействия для SignInWindow.xaml
	/// </summary>
	public partial class SignInWindow : Window
	{
		private SignInViewModel contentModel { get; set; } = new SignInViewModel();

		public SignInWindow()
		{
			InitializeComponent();
			DataContext = contentModel;
		}

		private async void LoginUser_Clicked(object sender, RoutedEventArgs e)
		{
			var result = await contentModel.LoginUser();
			if (result)
			{
				var newForm = new MainWindow();
				newForm.Show();
				this.Close();
			}
		}
	}
}
