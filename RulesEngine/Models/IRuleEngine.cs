using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine.Models
{
    public interface IRuleEngine
    {
        bool Validate(string text);

        string CustomRuleCode { get; set; }
    }
}
