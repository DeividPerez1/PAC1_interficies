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
    /// Lógica de interacción para DNITextBox.xaml
    /// </summary>
    public partial class DNITextBox : UserControl
    {
        //Binding
        public static readonly DependencyProperty DNITextProperty =
            DependencyProperty.Register(
                   "DNIText",
                   typeof(string),
                   typeof(DNITextBox),
                   new FrameworkPropertyMetadata(
                       string.Empty,
                       FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                       OnDNItextChanged));

        public static readonly DependencyProperty TooltipMessageProperty =
            DependencyProperty.Register(
                "TooltipMessage", 
                typeof(string), 
                typeof(DNITextBox), 
                new PropertyMetadata(string.Empty));

        
        public string TooltipMessage 
        { 
            get => (string)GetValue(TooltipMessageProperty); 
            set => SetValue(TooltipMessageProperty, value); 
        }

        public string DNIText
        {
            get => (string)GetValue(DNITextProperty);
            set => SetValue(DNITextProperty, value);
        }
        public DNITextBox()
        {
            InitializeComponent();
            InnerTextBox.TextChanged += (s, e) => DNIText = InnerTextBox.Text; //Marti esto és lo que tu has llamado Internal és lo mismo
            TooltipMessage = "Indtrodueix un DNI";
        }

        private static void OnDNItextChanged(DependencyObject d,
        DependencyPropertyChangedEventArgs e)
        {
            var control = (DNITextBox)d;
            string newDNI = (string)e.NewValue;
            bool isValid = IsValid(newDNI);

            if (string.IsNullOrWhiteSpace(newDNI))
            {
                control.ControlBorder.BorderBrush = Brushes.Gray;
                               
            }
            else if (isValid)
            {
                control.ControlBorder.BorderBrush = Brushes.Green;
                control.TooltipMessage = "DNI vàlid";
                
            }
            else
            {
                control.ControlBorder.BorderBrush = Brushes.Red;
                control.ControlBorder.BorderThickness = new Thickness(2);
                control.TooltipMessage = "El DNI introduit no és vàlid";
                
            }
        }

        public static bool IsValid(string DNI)
        {

            if (string.IsNullOrWhiteSpace(DNI))
            {
                return false;
            }
            string pattern = @"^\d{8}[A-Z]$";
            if (!Regex.IsMatch(DNI, pattern, RegexOptions.IgnoreCase))
            {
                return false;
            }

            String numText = DNI.Substring(0, 8);
            char Letter = char.ToUpper(DNI[8]); //por si acaso

            int num = int.Parse(numText);
            string LetterDNI = "TRWAGMYFPDXBNJZSQVHLCKE";
            int resto = num % 23;
            char letterCalcul = LetterDNI[resto];
            return letterCalcul == Letter; //return false sino asi me ahorro un if 
        }
    }
}
