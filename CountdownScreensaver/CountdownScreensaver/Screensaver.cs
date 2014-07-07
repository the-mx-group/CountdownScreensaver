using System;
using System.ComponentModel;

namespace CountdownScreensaver
{
    public class Screensaver : INotifyPropertyChanged
    {
        private readonly Counter _counter;
        private readonly ILanguage _language;
        private string _message;
        private bool _expired;


        public string Message
        {
            get { return _message; }
            private set
            {
                if (value == _message) return;
                _message = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Message"));
            }
        }

        public bool Expired
        {
            get { return _expired; }
            private set
            {
                if (value.Equals(_expired)) return;
                _expired = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Expired"));
            }
        }

        public Screensaver(Counter counter, ILanguage language)
        {
            if (counter == null) throw new ArgumentNullException("counter");
            if (language == null) throw new ArgumentNullException("language");

            _counter = counter;
            _language = language;

            //set the message on start
            Message = _language.GetMessage(_counter.RemainingTime);

            _counter.Tick += (sender, args) =>
            {
                if (_counter.RemainingTime == 0)
                {
                    Expired = true;
                }

                Message = _language.GetMessage(_counter.RemainingTime);
            };
            _counter.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
