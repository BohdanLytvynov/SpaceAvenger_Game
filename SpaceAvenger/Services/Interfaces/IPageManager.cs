using SpaceAvenger.Enums.FrameTypes;
using SpaceAvenger.Services.Realizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SpaceAvenger.Services.Interfaces
{
    internal interface IPageManagerService<TFrameType>
        where TFrameType : struct, Enum
    {
        public EventHandler<PageManagerEventArgs<TFrameType>>? OnSwitchScreenMethodInvoked { get; }

        public void AddPages(IEnumerable<KeyValuePair<string, Page?>> pages);

        public void AddPage(string key, Page? page);

        public void RemovePage(string pageKey);

        public Page? GetPage(string pageKey);

        public void SwitchPage(string pageKey, TFrameType frame = default);       
    }
}
