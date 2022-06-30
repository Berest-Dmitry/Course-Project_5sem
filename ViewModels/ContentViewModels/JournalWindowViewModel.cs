using course_proj_english_tutorial.Models.DataModels;
using course_proj_english_tutorial.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace course_proj_english_tutorial.ViewModels.ContentViewModels
{
	public class JournalWindowViewModel : BaseViewModel
	{
		private UserService userService;
		private JournalService journalService;

		#region DataModels
		/// <summary>
		/// Список преподавателей СДО
		/// </summary>
		private ObservableCollection<ShortUserModel> listOfTeachers = new ObservableCollection<ShortUserModel>();
		public ObservableCollection<ShortUserModel> ListOfTeachers
		{
			get => listOfTeachers;
			set
			{
				listOfTeachers = value;
				OnPropertyChanged(nameof(listOfTeachers));
			}
		}
		/// <summary>
		/// Список обучающихся на СДО
		/// </summary>
		private ObservableCollection<ShortUserModel> listOfStudents = new ObservableCollection<ShortUserModel>();
		public ObservableCollection<ShortUserModel> ListOfStudents
		{
			get => listOfStudents;
			set
			{
				listOfStudents = value;
				OnPropertyChanged(nameof(listOfStudents));
			}
		}
		/// <summary>
		/// список записей в журнале
		/// </summary>
		private ObservableCollection<JournalExtendedModel> journals = new ObservableCollection<JournalExtendedModel>();
		public ObservableCollection<JournalExtendedModel> Journals
		{
			get => journals;
			set
			{
				journals = value;
				OnPropertyChanged(nameof(journals));
			}
		}
		/// <summary>
		/// ID выбранного элемента журнала
		/// </summary>
		private Guid thisJournalId;
		public Guid ThisJournalId
		{
			get => thisJournalId;
			set
			{
				thisJournalId = value;
				OnPropertyChanged(nameof(thisJournalId));
			}
		}
		/// <summary>
		/// ID выбранного учителя
		/// </summary>
		private Guid selectedTeacherId;
		public Guid SelectedTeacherId
		{
			get => selectedTeacherId;
			set
			{
				selectedTeacherId = value;
				OnPropertyChanged(nameof(selectedTeacherId));
			}
		}
		/// <summary>
		/// ID выбранного ученика
		/// </summary>
		private Guid selectedStudentId;
		public Guid SelectedStudentId
		{
			get => selectedStudentId;
			set
			{
				selectedStudentId = value;
				OnPropertyChanged(nameof(selectedStudentId));
			}
		}
		/// <summary>
		/// время начала занятий
		/// </summary>
		private DateTime? startDT;
		public DateTime? StartDT
		{
			get => startDT;
			set
			{
				startDT = value;
				OnPropertyChanged(nameof(startDT));
			}
		}
		/// <summary>
		/// время окончания занятий
		/// </summary>
		private DateTime? endDT;
		public DateTime? EndDT
		{
			get => endDT;
			set
			{
				endDT = value;
				OnPropertyChanged(nameof(endDT));
			}
		}
		/// <summary>
		/// класс
		/// </summary>
		private int grade;
		public int Grade
		{
			get => grade;
			set
			{
				grade = value;
				OnPropertyChanged(nameof(grade));
			}
		}
		#endregion

		public JournalWindowViewModel()
		{
			userService = new UserService();
			journalService = new JournalService();
		}

		public void GoBack(JournalWindow thisForm, MainWindow otherForm)
		{
			otherForm.Show();
			thisForm.Close();
		} 
		/// <summary>
		/// получение списка всех учителей
		/// </summary>
		public async Task GetAllTeachers()
		{
			try
			{
				if (ListOfTeachers != null && ListOfTeachers.Count > 0)
					ListOfTeachers.Clear();

				var teachersList = await userService.GetListOfAllTeachers();
				if(teachersList != null)
				{
					foreach(var t in teachersList)
					{
						ListOfTeachers.Add(new ShortUserModel()
						{
							Id = t.Id,
							UserName = t.UserName,
							WorkExperience = t.WorkExperience,
							JournalId = t.JournalId
						});
					}
				}
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}
		/// <summary>
		/// метод получения списка всех учеников
		/// </summary>
		public async Task GetAllStudents()
		{
			try
			{
				if (ListOfStudents != null && ListOfStudents.Count > 0)
					ListOfStudents.Clear();

				var studentsList = await userService.GetListOfAllStudents();
				if(studentsList != null)
				{
					foreach(var s in studentsList)
					{
						ListOfStudents.Add(new ShortUserModel()
						{
							Id = s.Id,
							UserName = s.UserName,
							Grade = s.Grade,
							JournalId = s.JournalId
						});
					}
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		/// <summary>
		/// метод добавления записи в журнал
		/// </summary>
		public async Task<bool> AddJournalEntry()
		{
			try
			{
				if(StartDT == null || EndDT == null || Grade == 0)
				{
					MessageBox.Show("Заполните данные правильно!");
					return false;
				}
				var res = await journalService.CreateJournalNote(Grade, StartDT ?? DateTime.MinValue, EndDT ?? DateTime.MinValue);
				if (res != null)
				{
					MessageBox.Show("Создание записи прошло успешно!");
					return true;
				}
				else {
					MessageBox.Show("При создании записи произошла ошибка!");
					return false;
				}
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
				return false;
			}
		}
		/// <summary>
		/// метод получения всех записей в журнале
		/// </summary>
		public async Task GetJournalEntries()
		{
			try
			{
				if(Journals != null && Journals.Count > 0)
				{
					Journals.Clear();
				}
				var entries = await journalService.GetJournalNotes();
				if(entries != null && entries.Count > 0)
				{
					foreach(var entry in entries)
					{
						Guid? teacherId = null;
						List<Guid> studentsIds = new List<Guid>();
						if(ListOfTeachers != null)
						{
							teacherId = ListOfTeachers.Where(t => t.JournalId == entry.Id).Select(t => t.Id).FirstOrDefault();
						}
						if(ListOfStudents != null)
						{
							foreach(var s in ListOfStudents)
							{
								if (s.JournalId == entry.Id) studentsIds.Add(s.Id);
							}
						}
						string dateString = entry.StartTime.ToString("d") + " - " + entry.EndTime.ToString("d");
						Journals.Add(new JournalExtendedModel()
						{
							Id = entry.Id,
							Grade = entry.Grade,
							StartTime = entry.StartTime,
							EndTime = entry.EndTime,
							TeacherId = (teacherId != Guid.Empty) ? teacherId : null ,
							StudentsIds = studentsIds,
							TimePeriod = dateString
						});
					}
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return;
			}
		}
		/// <summary>
		/// связывание записи в журнале с выбранным человеком
		/// </summary>
		/// <param name="mode"></param>
		public async Task AttachPersonToJournal(string mode)
		{
			switch (mode)
			{
				case "student":
					if (SelectedStudentId == Guid.Empty || ThisJournalId == Guid.Empty)
					{
						MessageBox.Show("Вы не выбрали ученика или запись в журнале!");
						return;
					}
					bool result = await userService.AttachUserToJournal(ThisJournalId, SelectedStudentId);
					if (result) {
						var selectedJournal = Journals.Where(j => j.Id == ThisJournalId).FirstOrDefault();
						//int selectedIndex = Journals.IndexOf(selectedJournal);
						selectedJournal.StudentsIds?.Add(SelectedStudentId);
						//Journals.RemoveAt(selectedIndex);
						//Journals.Insert(selectedIndex, selectedJournal);
						CheckAttachment(ThisJournalId, SelectedStudentId);
						MessageBox.Show("Ученик успешно привязан  к записи в журнале!"); 
					}
					break;
				case "teacher":
					if (SelectedTeacherId == Guid.Empty || ThisJournalId == Guid.Empty)
					{
						MessageBox.Show("Вы не выбрали преподавателя или запись в журнале!");
						return;
					}
					bool decision = await userService.CheckIfTeacherAttachedToJournalEntry(ThisJournalId);
					if (decision)
					{
						MessageBox.Show("Невозможно привязать более 1 преподавателя к записи в журнале!");
						return;
					}
					else
					{
						bool result1 = await userService.AttachUserToJournal(ThisJournalId, SelectedTeacherId);
						if (result1) {
							var selectedJournal = Journals.Where(j => j.Id == ThisJournalId).FirstOrDefault();						
							selectedJournal.TeacherId = SelectedTeacherId;
							CheckAttachment(ThisJournalId, null, SelectedTeacherId);
							MessageBox.Show("Преподаватель успешно привязан  к записи в журнале!"); 
						}
					}
					break;
			}
		}
		/// <summary>
		/// отвязывание записи в журнале от выбранного человека
		/// </summary>
		/// <param name="mode"></param>
		public async Task DetachPersonFromJournal(string mode)
		{
			switch (mode)
			{
				case "student":
					if (SelectedStudentId == Guid.Empty)
					{
						MessageBox.Show("Вы не выбрали ученика!");
						return;
					}
					bool result = await userService.DetachUserFromJournal(SelectedStudentId);
					if (result) {
						var selectedJournal = Journals
							.Where(j => j.StudentsIds != null && j.StudentsIds.Contains(SelectedStudentId)).FirstOrDefault();
						selectedJournal.StudentsIds.Remove(SelectedStudentId);
						MessageBox.Show("Ученик успешно откреплен от записи в журнале!");
					}
					break;
				case "teacher":
					if (SelectedTeacherId == Guid.Empty)
					{
						MessageBox.Show("Вы не выбрали ученика!");
						return;
					}
					bool result1 = await userService.DetachUserFromJournal(SelectedTeacherId);
					if (result1) {
						var selectedJournal = Journals
							.Where(j => j.TeacherId == SelectedTeacherId).FirstOrDefault();
						selectedJournal.TeacherId = null;
						MessageBox.Show("Преподаватель успешно откреплен от записи в журнале!");
					}
					break;
			}
		}

		public void CheckAttachment(Guid journalId, Guid? studentId = null, Guid? teacherId = null)
		{
			if(studentId != null) { 
				foreach(var journal in Journals)
				{
					if(journal.Id != journalId 
						&& journal.StudentsIds != null && journal.StudentsIds.Contains(studentId ?? Guid.Empty))
					{
						journal.StudentsIds.Remove(studentId ?? Guid.Empty);
					}
				}
			}
			else if(teacherId != null)
			{
				foreach(var journal in Journals)
				{
					if (journal.Id != journalId
						&& journal.TeacherId == teacherId)
					{
						journal.TeacherId = null;
					}
				}
			}
		}
		/// <summary>
		/// метод удаления записи из журнала
		/// </summary>
		public async Task DeleteJournalEntry()
		{
			try
			{
				if(ThisJournalId == Guid.Empty)
				{
					MessageBox.Show("Вы не выбрали журнал!");
					return;
				}
				bool detachResult = await userService.DetachAllUsersFromCurrentJournalEntry(ThisJournalId);
				if (!detachResult)
				{
					MessageBox.Show("При откреплении пользователей произошла ошибка!");
					return;
				}
				else
				{
					bool deleteResult = await journalService.DeleteJournal(ThisJournalId);
					if(deleteResult) MessageBox.Show("Удаление прошло успешно!");
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
