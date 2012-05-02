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
    public class ViewModelLocator
    {
        private readonly CompositionContainer _container;

        public ViewModelLocator()
        {
            var catalog = new AssemblyCatalog(typeof(App).Assembly);
            _container = new CompositionContainer(catalog);
            _container.ComposeParts(this);
        }

        [Import]
        public RuleEngineViewModel RuleEngineViewModel { get; private set; }
    }
}
