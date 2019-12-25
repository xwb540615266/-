using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextView
{
    class MyModel : INotifyPropertyChanged
    {
        public void Load(string aFileName, Encoding aEncoding)
        {
            Text = File.ReadAllText(aFileName, aEncoding);
            if (!CanStartFilte)
                FiltedText = Text;
            else
                StartFilte();
        }

        public string Text
        {
            get => _Text;
            set
            {
                if (_Text == value) return;
                _Text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public string FileText { get; private set; }

        private string _Text;

        public string FiltedText
        {
            get => _FiltedText;
            set
            {
                if (_FiltedText == value) return;
                _FiltedText = value;
                OnPropertyChanged(nameof(FiltedText));
            }
        }
        private string _FiltedText;

        public string Pattern
        {
            get => _Pattern;
            set
            {
                if (_Pattern == value) return;
                _Pattern = value;
                OnPropertyChanged(nameof(Pattern));
            }
        }
        private string _Pattern;

        private void OnPropertyChanged(string aPropertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(aPropertyName));
        public event PropertyChangedEventHandler PropertyChanged;

        public void StartFilte()
        {
            string[] aLines = Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder aStringBuilder = new StringBuilder();
            Regex aRegex = new Regex(Pattern);
            foreach (var aLine in aLines)
            {
                if (aRegex.IsMatch(aLine))
                {
                    aStringBuilder.AppendLine(aLine);
                }
            }
            FiltedText = aStringBuilder.ToString();
        }
        public bool CanStartFilte =>
            !string.IsNullOrWhiteSpace(Text) &&
            !string.IsNullOrWhiteSpace(Pattern);

        private string _ImageName;
        public string ImageName
        {
            get => _ImageName;
            set
            {
                if (_ImageName == value) return;
                _ImageName = value;
                OnPropertyChanged(nameof(ImageName));
            }
        }
        public void OpenImage()
        {
            OpenFileDialog aDlg = new OpenFileDialog()
            {
                Title = "选择图片",
                Filter = "jpg|*.jpg|jpeg|*.jpeg",
            };
            if (aDlg.ShowDialog() != true) return;
            ImageName = aDlg.FileName;
        }
    }
}
