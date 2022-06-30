using course_proj_english_tutorial.AppContext;
using course_proj_english_tutorial.Entities.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Services.Base
{
	public class BaseService<T> where T : Entity
    {
		public ApplicationContext _applicationContext;
		public BaseService()
		{
			_applicationContext = new ApplicationContext();
		}

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                _applicationContext.Set<T>().Add(entity);
                await _applicationContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            _applicationContext.Entry(entity).State = EntityState.Modified;
            await _applicationContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _applicationContext.Set<T>().Remove(entity);
            await _applicationContext.SaveChangesAsync();
        }
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _applicationContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> GetEntityById(Guid Id)
		{
            return await _applicationContext.Set<T>().Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        // Convert an object to a byte array
        public byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        // Convert a byte array to an Object
        public Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);

            return obj;
        }
    }
}
