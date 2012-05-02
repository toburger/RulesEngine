using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine.Models
{
    [Export(typeof(IRuleEngine))]
    [RuleEngine("PHP Rule")]
    public class PhpRuleEngine : IRuleEngine
    {
        public PhpRuleEngine()
        {
            CustomRuleCode = @"if (!$value)
    return false;
return true;";
        }

        public bool Validate(string text)
        {
            // TODO: howto?
            //var context = PHP.Core.ScriptContext.CurrentContext;
            throw new NotImplementedException("PHP Rule not implemented.");
        }

        public string CustomRuleCode
        {
            get;
            set;
        }
    }
}
