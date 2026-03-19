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
        //Binding 
        public static readonly DependencyProperty EmailTextProperty =
            DependencyProperty.Register(
                   "EmailText",
                   typeof(string),
                   typeof(EmailTextBox),
                   new FrameworkPropertyMetadata(
                       string.Empty,
                       FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                       OnEmailtextChanged));

        public static readonly DependencyProperty TooltipMessageProperty =
            DependencyProperty.Register(
                "TooltipMessage", 
                typeof(string), 
                typeof(EmailTextBox), 
                new PropertyMetadata(string.Empty));

        public string TooltipMessage 
        { 
            get => (string)GetValue(TooltipMessageProperty); 
            set => SetValue(TooltipMessageProperty, value); 
        }



        public string EmailText
        {
            get => (string)GetValue(EmailTextProperty);
            set => SetValue(EmailTextProperty, value);
        }

        public EmailTextBox()
        {
            InitializeComponent();
            InnerTextBox.TextChanged += (s, e) => EmailText = InnerTextBox.Text;
            TooltipMessage = "Introdueix un correu";
        }
        private static void OnEmailtextChanged(DependencyObject d,
        DependencyPropertyChangedEventArgs e)
        {
            var control = (EmailTextBox)d;
            string newEmail = (string)e.NewValue;
            bool isValid = IsValid(newEmail);
            if (string.IsNullOrWhiteSpace(newEmail))
            {
                control.InnerTextBox.ClearValue(TextBox.BorderBrushProperty);
                control.InnerTextBox.ClearValue(TextBox.BorderThicknessProperty);

            }
            else if (isValid)
            {
                control.InnerTextBox.BorderBrush = Brushes.Green;
                control.InnerTextBox.BorderThickness = new Thickness(1);
                control.TooltipMessage = "Email vàlid";
                
            }
            else
            {
                control.InnerTextBox.BorderBrush = Brushes.Red;
                control.InnerTextBox.BorderThickness = new Thickness(2);
                control.TooltipMessage = "El correu introduit no és valid";
                
            }
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
