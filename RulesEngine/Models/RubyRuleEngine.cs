using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine.Models
{
    [Export(typeof(IRuleEngine))]
    [RuleEngine("Ruby Rule Engine")]
    public class RubyRuleEngine : IRuleEngine
    {
        public RubyRuleEngine()
        {
            CustomRuleCode = @"# Ruby Code...

if Text == NIL or Text == """"
    return false
else
    return true
end
";
        }

        public bool Validate(string text)
        {
            var engine = IronRuby.Ruby.CreateEngine();
            engine.Runtime.Globals.SetVariable("Text", text);            
            return engine.Execute<bool>(CustomRuleCode);
        }

        public string CustomRuleCode
        {
            get;
            set;
        }
    }
}
