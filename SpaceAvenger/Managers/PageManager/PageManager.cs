using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SpaceAvenger.Managers.PageManager
{
    public static class PageManager
    {
        static Dictionary<string, Page> m_Pages;

        public static event Action<Page>? OnSwitchScreenMethodInvoked;

        static PageManager()
        {
            m_Pages = new Dictionary<string, Page>();
        }

        public static void AddPages(IEnumerable<KeyValuePair<string, Page?>> pages)
        {
            foreach (var page in pages)
            {
                AddPage(page.Key, page.Value);
            }
        }

        public static void AddPage(string key, Page? page)
        {
            if (!m_Pages.ContainsKey(key) && page is not null)
                m_Pages.Add(key, page);
            else
                throw new Exception($"Storage has already key-value pair with {key} key!");
        }

        public static void RemovePage(string pageKey)
        {
            if (m_Pages.ContainsKey(pageKey))
                m_Pages.Remove(pageKey);
            else
                throw new Exception($"Storage has no key value pairs with {pageKey} key");
        }

        public static Page? GetPage(string pageKey)
        {
            Page? temp = null;
            m_Pages.TryGetValue(pageKey, out temp);
            return temp;
        }

        public static void SwitchPage(string pageKey)
        {
            Page? temp;

            m_Pages.TryGetValue(pageKey, out temp);

            if (temp != null)
            {
                OnSwitchScreenMethodInvoked?.Invoke(temp);
            }
            else
                throw new Exception("Storage has no key value pairs with {pageKey} key");            
        }
    }
}
