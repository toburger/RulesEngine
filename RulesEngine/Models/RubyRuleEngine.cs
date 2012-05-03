using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronRuby;
using Microsoft.Scripting.Hosting;

namespace RulesEngine.Models
{
    [Export(typeof(IRuleEngine))]
    [RuleEngine("Ruby Rule Engine")]
    public class RubyRuleEngine : IRuleEngine
    {
        private readonly ScriptEngine _engine;

        public RubyRuleEngine()
        {
            System.Diagnostics.Debug.WriteLine("Ruby Rule Engine loaded...");

            _engine = Ruby.CreateEngine();

            CustomRuleCode = @"# Ruby Code

if text.nil? or text.empty?
    return false
else
    return true
end";
        }

        public bool Validate(string text)
        {
            var scope = _engine.CreateScope();
            scope.SetVariable("Text", text);
            return _engine.Execute<bool>(CustomRuleCode, scope);
        }

        public string CustomRuleCode
        {
            get;
            set;
        }
    }
}
