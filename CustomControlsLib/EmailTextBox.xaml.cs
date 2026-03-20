
using System.Text.RegularExpressions;

using System.Windows;
using System.Windows.Controls;

using System.Windows.Media;


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
            InnerTextBox.BorderThickness = new Thickness(1);
        }
        private static void OnEmailtextChanged(DependencyObject d,
        DependencyPropertyChangedEventArgs e)
        {
            var control = (EmailTextBox)d;
            string newEmail = (string)e.NewValue;
            bool isValid = IsValid(newEmail);

            if (control.InnerTextBox.Text != newEmail)
            {
                control.InnerTextBox.Text = newEmail;
            }




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
