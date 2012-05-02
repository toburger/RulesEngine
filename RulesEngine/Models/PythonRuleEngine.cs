using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace RulesEngine.Models
{
    [Export(typeof(IRuleEngine))]
    [RuleEngine("Python Rule Engine")]
    public class PythonRuleEngine:IRuleEngine
    {
        private readonly ScriptEngine _engine;

        public PythonRuleEngine()
        {
            _engine = Python.CreateEngine();

            CustomRuleCode = @"# Python Code

def validate(text):
  if text:
    return True
  else:
    return False

validate(Text)";
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
