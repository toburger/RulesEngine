using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using RulesEngine.ViewModels;

namespace RulesEngine
{
    public sealed class Bootstrapper : IDisposable
    {
        private readonly CompositionContainer _container;
        private static bool? _isInDesignMode;

        public Bootstrapper()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            if (!IsInDesignMode)
                catalog.Catalogs.Add(new DirectoryCatalog("Modules", "RulesEngine.Modules.*.dll"));
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

        public static bool IsInDesignMode
        {
            get
            {
                if (_isInDesignMode == null)
                {
                    var prop = DesignerProperties.IsInDesignModeProperty;
                    _isInDesignMode = (bool)DependencyPropertyDescriptor
                        .FromProperty(prop, typeof(FrameworkElement))
                        .Metadata.DefaultValue;
                }
                return _isInDesignMode.Value;
            }
        }
    }
}
