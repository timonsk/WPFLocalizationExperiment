using System;
using System.Windows;

namespace WPFLocalizationExperiment
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public enum Languages
    {
        ru,
        en
    }

    public partial class MainWindow : Window
    {
        private const string DefaultPath = @"Resources\Languages\";
        private const string DefaultLanguage = "en";
        private int _languageMergedDictionaryIndex = -1;

        public MainWindow()
        {
            InitializeComponent();
            SetLanguage(DefaultLanguage);
            CurrentLanguages = Languages.en;
        }

        private Languages CurrentLanguages { get; set; }

        private void SetLanguage(string language)
        {
            SetLanguageResourceDictionary(string.Format("{0}{1}.xaml", DefaultPath, language));
        }

        private void SetLanguageResourceDictionary(string path)
        {
            var languageDictionary = new ResourceDictionary();
            try
            {
                languageDictionary.Source = new Uri(path, UriKind.Relative);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("No languages resources found");
            }

            if (_languageMergedDictionaryIndex == -1)
            {
                // Add in newly loaded Resource Dictionary
                Application.Current.Resources.MergedDictionaries.Add(languageDictionary);
                _languageMergedDictionaryIndex =
                    Application.Current.Resources.MergedDictionaries.IndexOf(languageDictionary);
            }
            else
            {
                // Replace the current langage dictionary with the new one
                Application.Current.Resources.MergedDictionaries[_languageMergedDictionaryIndex] = languageDictionary;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (CurrentLanguages == Languages.en)
            {
                SetLanguage("ru");
                CurrentLanguages = Languages.ru;
            }
            else if (CurrentLanguages == Languages.ru)
            {
                SetLanguage("en");
                CurrentLanguages = Languages.en;
            }
            else throw new NotImplementedException();
        }
    }
}