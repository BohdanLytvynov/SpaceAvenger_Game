using SpaceAvenger.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace SpaceAvenger.Services.Realizations
{
    public class PageManagerEventArgs<TFrameType> : EventArgs
        where TFrameType : struct, Enum
    {
        public Page Page { get; }

        public TFrameType FrameType { get; }

        public PageManagerEventArgs(Page p, TFrameType frameType)
        {
            Page = p;
            FrameType = frameType;
        }
    }

    public class PageManagerService<TFrameType> : IPageManagerService<TFrameType>
        where TFrameType : struct, Enum
    {
        static Dictionary<string, Page> m_Pages;

        private EventHandler<PageManagerEventArgs<TFrameType>>? m_OnSwitchScreenMethodInvoked;

        public EventHandler<PageManagerEventArgs<TFrameType>>? OnSwitchScreenMethodInvoked => 
            m_OnSwitchScreenMethodInvoked;

        static PageManagerService()
        {
            m_Pages = new Dictionary<string, Page>();
        }

        public void AddPages(IEnumerable<KeyValuePair<string, Page?>> pages)
        {
            foreach (var page in pages)
            {
                AddPage(page.Key, page.Value);
            }
        }

        public void AddPage(string key, Page? page)
        {
            if (!m_Pages.ContainsKey(key) && page is not null)
                m_Pages.Add(key, page);
            else
                throw new Exception($"Storage has already key-value pair with {key} key!");
        }

        public void RemovePage(string pageKey)
        {
            if (m_Pages.ContainsKey(pageKey))
                m_Pages.Remove(pageKey);
            else
                throw new Exception($"Storage has no key value pairs with {pageKey} key");
        }

        public Page? GetPage(string pageKey)
        {
            Page? temp = null;
            m_Pages.TryGetValue(pageKey, out temp);
            return temp;
        }

        public void SwitchPage(string pageKey, TFrameType frame = default)
        {
            Page? temp;

            m_Pages.TryGetValue(pageKey, out temp);

            if (temp != null)
            {
                m_OnSwitchScreenMethodInvoked?.Invoke(
                    null,
                    new PageManagerEventArgs<TFrameType>(temp, frame));
            }
            else
                throw new Exception("Storage has no key value pairs with {pageKey} key");
        }
    }
}
