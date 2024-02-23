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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CaesarCipher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _cipherName.Text = "Caesar Cipher";
            ShiftedAlphabet();
        }


        private char[] userText;
        Dictionary<char, char> alphabet = new Dictionary<char, char>
        {
            {'A', 'Ć'}, {'Ą', 'D'}, {'B', 'E'}, {'C', 'Ę'}, {'Ć', 'F'}, 
            {'D', 'G'}, {'E', 'H'}, {'Ę', 'I'}, {'F', 'J'}, {'G', 'K'}, 
            {'H', 'L'}, {'I', 'Ł'}, {'J', 'M'}, {'K', 'N'}, {'L', 'Ń'}, 
            {'Ł', 'O'}, {'M', 'Ó'}, {'N', 'P'}, {'Ń', 'Q'}, {'O', 'R'}, 
            {'Ó', 'S'}, {'P', 'Ś'}, {'Q', 'T'}, {'R', 'U'}, {'S', 'V'}, 
            {'Ś', 'W'}, {'T', 'X'}, {'U', 'Y'}, {'V', 'Z'}, {'W', 'Ź'}, 
            {'X', 'Ż'}, {'Y', 'A'}, {'Z', 'Ą'}, {'Ź', 'B'}, {'Ż', 'C'}
        };

        Dictionary<char, char> shiftedAlphabet = new Dictionary<char, char>();

        public void ShiftedAlphabet()
        {
            foreach (var pair in alphabet)
            {
                shiftedAlphabet[pair.Value] = pair.Key;
            }
        }

        private bool IsInputTextValid()
        {
            return !string.IsNullOrWhiteSpace(_input.Text) && !(_input.Text == "Pole tekstowe...");
        }

        private void InputToChars()
        {
            userText = _input.Text.Replace(" ", "").ToUpper().ToCharArray();
        }


        private void EncryptText(object sender, MouseEventArgs e)
        {
            if (IsInputTextValid())
            {
                InputToChars();
                string codedText = "";

                foreach (var c in userText)
                {
                    codedText += alphabet[c];
                }
                _output.Text = codedText;
            }
        }

        private void DecryptText(object sender, MouseEventArgs e)
        {
            if (IsInputTextValid())
            {
                InputToChars();
                string decodedText = new string(userText.Select(c => shiftedAlphabet.ContainsKey(c) ? shiftedAlphabet[c] : c).ToArray());
                _output.Text = decodedText;
            }

        }



        private void TxtBorder_OnHover(object sender, MouseEventArgs e)
        {
            ((TextBlock)((Border)sender).Child).Foreground = new SolidColorBrush(Color.FromRgb(251, 234, 235));
            ((TextBlock)((Border)sender).Child).Effect = new DropShadowEffect
            {
                ShadowDepth = 3,
                Color = Color.FromRgb(251, 234, 235),
                Direction = 0,
                Opacity = 0.5
            };
        }

        private void TxtBorder_OnLeave(object sender, MouseEventArgs e)
        {
            var textBlock = (TextBlock)((Border)sender).Child;
            textBlock.Foreground = new SolidColorBrush(Color.FromRgb(251, 234, 235));
            textBlock.Effect = null;
        }


        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Foreground = new SolidColorBrush(Color.FromRgb(47, 60, 126));
            if (textBox.Text == "Pole tekstowe...")
            {
                textBox.Text = "";
            }
        }


        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Foreground = new SolidColorBrush(Color.FromRgb(47, 60, 126));
                textBox.Text = "Pole tekstowe...";
            }
        }


        private void CloseApp(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

    }
}
