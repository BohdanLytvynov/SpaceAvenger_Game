using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SpaceAvenger
{
    public static class PageManager
    {
        static Dictionary<string ,Page> m_Pages;
       
        public static event Action<Page> OnSwitchScreenMethodInvoked; 

        static PageManager()
        {
            m_Pages = new Dictionary<string, Page>();
        }

        public static void AddPage(string key, Page page)
        {
            m_Pages.Add(key, page);            
        }

        public static void RemovePage(string pageKey)
        {
            m_Pages.Remove(pageKey);
        }

        public static void SwitchPage(string pageKey)
        {
            OnSwitchScreenMethodInvoked?.Invoke(m_Pages[pageKey]);
        }
    }
}
