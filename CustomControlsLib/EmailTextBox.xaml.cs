using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomControlsLib
{
    /// <summary>
    /// Lógica de interacción para EmailTextBox.xaml
    /// </summary>
    public partial class EmailTextBox : UserControl
    {
        public static readonly DependencyProperty EmailTextProperty =
            DependencyProperty.Register(
                   "EmailText",
                   typeof(string),
                   typeof(EmailTextBox),
                   new FrameworkPropertyMetadata(
                       string.Empty,
                       FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                       OnEmailtextChanged));
        public string EmailText
        {
            get => (string)GetValue(EmailTextProperty);
            set => SetValue(EmailTextProperty, value);
        }

        public EmailTextBox()
        {
            InitializeComponent();
            InnerTextBox.TextChanged += (s, e) => EmailText = InnerTextBox.Text;
        }
        private static void OnEmailtextChanged(DependencyObject d,
        DependencyPropertyChangedEventArgs e)
        {
            var control = (EmailTextBox)d;
            string newEmail = (string)e.NewValue;
            bool isValid = IsValid(newEmail);
        }

        public static bool IsValid(string email)
        {

            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
    }
}
