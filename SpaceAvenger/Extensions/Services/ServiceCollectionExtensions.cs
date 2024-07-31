using Domain.Utilities;
using Microsoft.Extensions.DependencyInjection;
using SpaceAvenger.Attributes.PageManager;
using SpaceAvenger.Enums.FrameTypes;
using SpaceAvenger.Services.Interfaces.PageManager;
using SpaceAvenger.Services.Realizations.PageManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ViewModelBaseLibDotNetCore.VM;

namespace SpaceAvenger.Extensions.Services
{
    internal static class ServiceCollectionExtensions
    {
        public static void AddPageViewModelsAsSingleton(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var viewModels = ReflexionUtility.GetObjectsTypeInfo(assembly,
                (TypeInfo t) => 
                t is not null && t.Name.Contains("ViewModel")
                && (t.GetCustomAttribute<ViewModelType>()?.Usage.Equals(ViewModelUsage.Page) ?? false)
                && t.GetCustomAttribute<ReflexionDetectionIgnore>() is null);

            foreach (var viewModel in viewModels)
            {
                services.AddSingleton(viewModel.AsType());
            }
        }
    }
}
