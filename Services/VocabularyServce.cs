using course_proj_english_tutorial.Entities;
using course_proj_english_tutorial.Interfaces;
using course_proj_english_tutorial.Services.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Services
{
	/// <summary>
	/// интерфейс словарей
	/// </summary>
	public class VocabularyServce : BaseService<Vocabulary>, IVocabularyService
	{
		/// <summary>
		/// метод добавления словаря к уроку
		/// </summary>
		/// <param name="WordsWithTranslations"></param>
		/// <param name="LessonId"></param>
		public async Task<Vocabulary> AddVocabularyToLesson(Dictionary<string, string> WordsWithTranslations, Guid LessonId, Guid? VocId = null)
		{
			try
			{
				if (VocId == null)
				{
					var byteArray = ObjectToByteArray(WordsWithTranslations);
					var entity = Vocabulary.Create(byteArray, LessonId);
					if (entity != null)
					{
						var resultEntity = _applicationContext.Vocabularies.Add(entity);
						await _applicationContext.SaveChangesAsync();
						return resultEntity;
					}
					else
					{
						throw new Exception("Lesson item hasn't been generated properly!");
					}
				}
				else
				{
					var byteArray = ObjectToByteArray(WordsWithTranslations);
					var vocab = await GetVocabularyById(VocId ?? Guid.Empty);
					if (vocab != null)
					{
						vocab.LessonId = LessonId;
						vocab.WordsTranslationsByteArray = byteArray;
						_applicationContext.Entry(vocab).State = EntityState.Modified;
						await _applicationContext.SaveChangesAsync();
						return vocab;
					}
					else return new Vocabulary();
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// <summary>
		/// получение списка словарей к данному уроку
		/// </summary>
		/// <param name="LessonId"></param>
		public async Task<List<Vocabulary>> GetListVocabulariesOfCurrentLesson(Guid LessonId)
		{
			try
			{
				var vList = await _applicationContext.Vocabularies.Where(v => v.LessonId == LessonId).ToListAsync();
				return vList;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// <summary>
		/// метод получения словаря по его Id
		/// </summary>
		/// <param name="Id"></param>
		public async Task<Vocabulary> GetVocabularyById(Guid Id)
		{
			try
			{
				var vocab = await _applicationContext.Vocabularies.Where(v => v.Id == Id).FirstOrDefaultAsync();
				return vocab;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// <summary>
		/// метод удаления словаря
		/// </summary>
		/// <param name="vocId"></param>
		public async Task<bool> DeleteVocabulary(Guid vocId)
		{
			try
			{
				var entity = await GetVocabularyById(vocId);
				if (entity != null)
				{
					//_applicationContext.Vocabularies.Remove(entity);
					_applicationContext.Entry(entity).State = EntityState.Deleted;
					await _applicationContext.SaveChangesAsync();
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// метод удаления всех словарей, принадлежащих к данному уроку
		/// </summary>
		/// <param name="LessonId"></param>
		public async Task<bool> DeleteVocabulariesOfCurrentLesson(Guid LessonId)
		{
			try
			{
				var vocabulariesOfLesson = await _applicationContext.Vocabularies
					.Where(v => v.LessonId == LessonId).ToListAsync();

				if (vocabulariesOfLesson != null)
				{
					_applicationContext.Vocabularies.RemoveRange(vocabulariesOfLesson);
					await _applicationContext.SaveChangesAsync();
					return true;
				}
				else return false;
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
