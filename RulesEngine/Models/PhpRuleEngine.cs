using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHP.Core;
using MEFExport = System.ComponentModel.Composition.ExportAttribute;

namespace RulesEngine.Models
{
    [MEFExport(typeof(IRuleEngine))]
    [RuleEngine("PHP Rule Engine")]
    public class PhpRuleEngine : IRuleEngine
    {
        public PhpRuleEngine()
        {
            CustomRuleCode = @"# PHP Code:

if (!$text)
    return false;
return true;";
        }

        public bool Validate(string text)
        {
            var localVars = new Dictionary<string, object> { { "text", text } };
            var context = PHP.Core.ScriptContext.CurrentContext;
            return System.Convert.ToBoolean(
                DynamicCode.Eval(
                    CustomRuleCode,
                    false,
                    context,
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
