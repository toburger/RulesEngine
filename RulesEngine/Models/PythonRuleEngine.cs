using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine.Models
{
    [Export(typeof(IRuleEngine))]
    [RuleEngine("Python Rule Engine")]
    public class PythonRuleEngine:IRuleEngine
    {
        public PythonRuleEngine()
        {
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
            var engine = IronPython.Hosting.Python.CreateEngine();
            //engine.Runtime.Globals.SetVariable("Text", text);
            var scope = engine.CreateScope();
            scope.SetVariable("Text", text);
            return engine.Execute<bool>(CustomRuleCode, scope);
        }

        public string CustomRuleCode
        {
            get;
            set;
        }
    }
}
