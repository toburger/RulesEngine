using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RulesEngine.Models;

namespace RulesEngine.ViewModels
{
    public sealed class ViewModelLocator : IDisposable
    {
        private readonly CompositionContainer _container;

        public ViewModelLocator()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(App).Assembly));
            catalog.Catalogs.Add(new DirectoryCatalog("Modules", "*.Modules.*.dll"));
            _container = new CompositionContainer(catalog);
            _container.ComposeParts(this);
        }

        [Import]
        public RuleEngineViewModel RuleEngineViewModel { get; private set; }

        public void Dispose()
        {
            if (_container != null)
                _container.Dispose();
        }
    }
}
