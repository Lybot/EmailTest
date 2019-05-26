using System;
using EmailTest.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace EmailTest.ViewModels
{
    class EmailSendingVm:BindableBase
    {
        public DelegateCommand ChooseFile { get; }
        public DelegateCommand<string> CheckConnection { get; }
        public DelegateCommand<string> SendEmail { get; }
        public object Password { get; set; }
        readonly EmailSending _emailSending = new EmailSending();
        public EmailSendingVm()
        {
            _emailSending.PropertyChanged+=(s,e)=>RaisePropertyChanged(e.PropertyName);
            CheckConnection = new DelegateCommand<string>(str =>
            {
                try
                {
                    string[] inputStrings = str.Split(' ');
                    _emailSending.CheckConnection(inputStrings[0], inputStrings[1], Password.ToString(),
                        inputStrings[2]);
                }
                catch (Exception)
                {                  
                }
            });
            ChooseFile = new DelegateCommand(delegate
            {
                _emailSending.ChooseFile();
            });
            SendEmail = new DelegateCommand<string>(str=>
            {
                try
                {
                    string[] inputStrings = str.Split(' ');
                    _emailSending.SendEmail(inputStrings[0], inputStrings[1], Password.ToString(), inputStrings[2],
                        inputStrings[3], inputStrings[4], FilePath);
                }
                catch
                {

                }
            });
        }
        public string ConnectionResult => _emailSending.Result;
        public string FilePath => _emailSending.FilePath;
        public string SendResult => _emailSending.SendResult;
    }
}
