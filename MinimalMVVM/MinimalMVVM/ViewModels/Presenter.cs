using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MinimalMVVM.Models;

namespace MinimalMVVM.ViewModels
{
    public class Presenter : ObservableObject
    {
        private readonly TextConverter _textConverter = new TextConverter(s => s.ToUpper());
        private string _someText;
        private readonly ObservableCollection<string> _history = new ObservableCollection<string>();

        public string SomeText
        {
            get { return _someText; }
            set
            {
                _someText = value;
                RaisePropertyChangedEvent("SomeText");
            }
        }

        public IEnumerable<string> History
        {
            get { return _history; }
        }

        public ICommand SaveTextCommand
        {
            get { return new DelegateCommand(SaveText); }
        }

        private void SaveText()
        {
            if (string.IsNullOrWhiteSpace(SomeText)) return;
            AddToHistory(_textConverter.SaveText(SomeText));
            SomeText = string.Empty;
        }

        private void AddToHistory(string item)
        {
            if (!_history.Contains(item))
                _history.Add(item);
        }
    }
}
