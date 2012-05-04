using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using RulesEngine.Contracts;

namespace RulesEngine.ViewModels
{
    [Export]
    public class RuleEngineViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IEnumerable<Lazy<IRuleEngine, IRuleEngineMetadata>> _ruleEngines;
        private IRuleEngine _selectedRuleEngine;
        private string _textToValidate;
        private bool? _isValid;
        private string _validationErrors;

        [ImportingConstructor]
        public RuleEngineViewModel([ImportMany]IEnumerable<Lazy<IRuleEngine, IRuleEngineMetadata>> ruleEngines)
        {
            _ruleEngines = ruleEngines
                .OrderBy(re => re.Metadata.RuleName);
            _selectedRuleEngine = _ruleEngines
                .Where(re => re.Metadata.IsDefault)
                .Select(re => re.Value)
                .FirstOrDefault();

            RegisterCommands();
        }

        private void RegisterCommands()
        {
            ValidateCommand = new RelayCommand(
                ValidateAction,
                CanValidateAction);
        }

        public IEnumerable<Lazy<IRuleEngine, IRuleEngineMetadata>> RuleEngines
        {
            get { return _ruleEngines; }
        }

        public Lazy<IRuleEngine, IRuleEngineMetadata> SelectedRuleEngine
        {
            get
            {
                if (_selectedRuleEngine == null)
                    return null;
                return _ruleEngines
                    .FirstOrDefault(re => re.Value == _selectedRuleEngine);
            }
            set
            {
                ResetForm();

                try
                {
                    _selectedRuleEngine = value.Value;
                }
                catch (CompositionException ex)
                {
                    _selectedRuleEngine = null;

                    ValidationErrors = string.Format(
                        "Error initializing: {0}\n{2}\n\n{1}",
                        value.Metadata.RuleName,
                        string.Join("\n", ex.Errors.Select(e => e.Exception.Message)),
                        new string('-', 50));
                }

                RaisePropertyChanged("SelectedRuleEngine");
                RaisePropertyChanged("CustomRuleCode");
            }
        }

        public string TextToValidate
        {
            get { return _textToValidate; }
            set
            {
                _textToValidate = value;
                RaisePropertyChanged("TextToValidate");
            }
        }

        public bool? IsValid
        {
            get { return _isValid; }
            set
            {
                _isValid = value;
                RaisePropertyChanged("IsValid");
            }
        }

        public string CustomRuleCode
        {
            get
            {
                return
                    _selectedRuleEngine == null ?
                    null :
                    _selectedRuleEngine.CustomRuleCode;
            }
            set
            {
                _selectedRuleEngine.CustomRuleCode = value;
                RaisePropertyChanged("CustomRuleCode");

                ResetForm();
            }
        }

        public string ValidationErrors
        {
            get { return _validationErrors; }
            set
            {
                _validationErrors = value;
                RaisePropertyChanged("ValidationErrors");
            }
        }

        public RelayCommand ValidateCommand
        {
            get;
            private set;
        }

        private void ValidateAction()
        {
            try
            {
                IsValid = _selectedRuleEngine.Validate(TextToValidate);
                ValidationErrors = null;
            }
            catch (Exception ex)
            {
                IsValid = null;
                // TODO: loosely couple
                //MessageBox.Show(ex.Message);
                ValidationErrors = ex.Message;
            }
        }

        private bool CanValidateAction()
        {
            return ValidationErrors == null;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            var pc = PropertyChanged;
            if (pc != null)
                pc(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ResetForm()
        {
            IsValid = null;
            ValidationErrors = null;
        }
    }
}
