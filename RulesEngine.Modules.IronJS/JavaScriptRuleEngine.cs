﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RulesEngine.Contracts;

namespace RulesEngine.Modules.IronJS
{
    [Export(typeof(IRuleEngine))]
    [RuleEngine("JavaScript Rule Engine")]
    public class JavaScriptRuleEngine : IRuleEngine
    {
        private readonly global::IronJS.Hosting.CSharp.Context _context;

        public JavaScriptRuleEngine()
        {
            System.Diagnostics.Debug.WriteLine("JavaScript Rule Engine loaded...");

            _context = new global::IronJS.Hosting.CSharp.Context();

            CustomRuleCode = @"if (!text)
    false;
else
    true;";
        }

        public bool Validate(string text)
        {
            _context.SetGlobal("text", text);
            return _context.Execute<bool>(CustomRuleCode);
        }

        public string CustomRuleCode
        {
            get;
            set;
        }
    }
}
