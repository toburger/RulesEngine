using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHP.Core;
using RulesEngine.Contracts;
using MEFExport = System.ComponentModel.Composition.ExportAttribute;

namespace RulesEngine.Models
{
    [MEFExport(typeof(IRuleEngine))]
    [RuleEngine("PHP Rule Engine")]
    public class PhpRuleEngine : IRuleEngine
    {
        private readonly ScriptContext _context;

        public PhpRuleEngine()
        {
            Trace.WriteLine("PHP Rule Engine loaded...");

            _context = ScriptContext.CurrentContext;

            var assemblyLoader = _context.ApplicationContext.AssemblyLoader;
            assemblyLoader.Load(typeof(string).Assembly, null);                 // mscorlib.dll
            assemblyLoader.Load(typeof(Uri).Assembly, null);                    // System.dll
            assemblyLoader.Load(typeof(PHP.Library.PhpStrings).Assembly, null); // PhpNetClassLibrary.dll

            CustomRuleCode = @"// PHP Code

function validate($text) {
    if (strlen($text) == 0)
        return false;
    return true;
}

// explicit return
return validate($text);";
        }

        public bool Validate(string text)
        {
            var scope = _context.Fork();
            var localVars = new Dictionary<string, object> { { "text", text } };
            return System.Convert.ToBoolean(
                DynamicCode.Eval(
                    CustomRuleCode,
                    false,
                    scope,
                    localVars,
                    null,
                    null,
                    "dummy.cs",
                    1,
                    1,
                    -1,
                    null));
        }

        public string CustomRuleCode
        {
            get;
            set;
        }
    }
}
