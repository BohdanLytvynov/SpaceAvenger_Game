using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAvenger.Managers.CommunicationManager
{
    public static class CommunicationManager<TEntity>
    {
        private static Dictionary<string, TEntity> m_storage;

        static CommunicationManager()
        {
            m_storage = new Dictionary<string, TEntity>();
        }

        public static void Add(string key, TEntity entity)
        {
            if (!m_storage.ContainsKey(key))
                m_storage.Add(key, entity);
            else
                throw new Exception("Storage alredy contains key!");
        }

        public static void Delete(string key)
        {
            if (m_storage.ContainsKey(key))
                m_storage.Remove(key);  
            else
                throw new KeyNotFoundException("There is no key!");
        }

        public static TEntity? Get(string key) 
        {            
            TEntity? entity;
            var r = m_storage.TryGetValue(key, out entity);

            return entity;
        }
    }
}
