using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GraphFinder
{
    /// <summary>
    /// Логика взаимодействия для CheatInput.xaml
    /// </summary>
    public partial class CheatInput : Window
    {
        MainWindow _mainWindow;

        const string alfabetRu = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        const string alfabetEn = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string cheatCodeRu = "Дьнчйх";
        const string cheatCodeEn = "Nfrhmjfyjw";

        private string CodeEncode(string text, int k)
        {

            var fullAlfabetRu = alfabetRu + alfabetRu.ToLower();
            var fullAlfabetEn = alfabetEn + alfabetEn.ToLower();

            string fullAlfabet = null;

            try
            {
                if (fullAlfabetRu.Contains(text.Trim()[0]))
                {
                    fullAlfabet = fullAlfabetRu;
                }
                else if (fullAlfabetEn.Contains(text.Trim()[0]))
                {
                    fullAlfabet = fullAlfabetEn;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            var letterQty = fullAlfabet.Length;
            var retVal = "";
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                var index = fullAlfabet.IndexOf(c);
                if (index < 0)
                {
                    retVal += c.ToString();
                }
                else
                {
                    var codeIndex = (letterQty + index + k) % letterQty;
                    retVal += fullAlfabet[codeIndex];
                }
            }

            return retVal;
        }

        public CheatInput(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CodeEncode(cheatInput.Text, 5).Equals(cheatCodeRu) || 
                CodeEncode(cheatInput.Text, 5).Equals(cheatCodeEn))
            {
                _mainWindow.CheatActive();
            }
            else
            {
                errText.Content = "Ты не читер!!!";
            }
        }

        private void CheatInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            errText.Content = "";
        }
    }
}
