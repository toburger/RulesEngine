using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine.Models
{
    [Export(typeof(IRuleEngine))]
    [RuleEngine("Default Rule Engine", IsDefault = true, NoCustomRuleCode = true)]
    public class DefaultRuleEngine : IRuleEngine
    {
        public bool Validate(string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;
            return true;
        }

        public string CustomRuleCode
        {
            get { return null; }
            set { throw new Exception("Cannot set Custom Rule Code."); }
        }
    }
}
