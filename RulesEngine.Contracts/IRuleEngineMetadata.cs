using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RulesEngine.Models
{
    public interface IRuleEngineMetadata
    {
        string RuleName { get; }

        bool IsDefault { get; }

        bool NoCustomRuleCode { get; }
    }
}
