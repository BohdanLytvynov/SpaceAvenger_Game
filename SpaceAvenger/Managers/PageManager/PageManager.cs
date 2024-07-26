using SpaceAvenger.Managers.PageManager;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace SpaceAvenger.Managers.PageManager
{
    public class PageManagerEventArgs<TFrameType> : EventArgs
        where TFrameType : struct, Enum
    {
        public  Page Page { get; }

        public TFrameType FrameType { get; }

        public PageManagerEventArgs(Page p, TFrameType frameType)
        {
            Page = p;
            FrameType = frameType;
        }
    }

    public static class PageManager<TFrameType>       
        where TFrameType : struct, Enum
    {
        static Dictionary<string, Page> m_Pages;

        public static EventHandler<PageManagerEventArgs<TFrameType>> OnSwitchScreenMethodInvoked;

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

        public static void SwitchPage(string pageKey, TFrameType frame = default)
        {
            Page? temp;

            m_Pages.TryGetValue(pageKey, out temp);

            if (temp != null)
            {
                OnSwitchScreenMethodInvoked?.Invoke(
                    null, 
                    new PageManagerEventArgs<TFrameType>(temp, frame));
            }
            else
                throw new Exception("Storage has no key value pairs with {pageKey} key");            
        }
    }
}
