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
	/// Логика взаимодействия для SignUpWindow.xaml
	/// </summary>
	public partial class SignUpWindow : Window
	{
		private SignInViewModel contentModel { get; set; } = new SignInViewModel();
		public SignUpWindow()
		{
			InitializeComponent();
			DataContext = contentModel;
		}

		private async void RegisterUser_Clicked(object sender, RoutedEventArgs e)
		{
			var result = await contentModel.RegisterUser();
			if (result)
			{
				var newForm = new MainWindow();
				newForm.Show();
				this.Close();
			}
		}

		private void RadioButton_Checked(object sender, RoutedEventArgs e)
		{
			var rButton = sender as RadioButton;
			if(rButton != null)
			{
				string roleName = rButton.Content as string;
				contentModel.RoleChanged(roleName);
			}
			
		}
	}
}
