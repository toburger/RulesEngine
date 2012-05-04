using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace RulesEngine.Contracts
{
    [MetadataAttribute]
    public class RuleEngineAttribute : Attribute
    {
        public RuleEngineAttribute(string ruleName)
        {
            RuleName = ruleName;
        }

        public string RuleName { get; set; }

        public bool IsDefault { get; set; }

        public bool NoCustomRuleCode { get; set; }
    }
}
