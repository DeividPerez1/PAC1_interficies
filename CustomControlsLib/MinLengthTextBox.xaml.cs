
using System.Windows;
using System.Windows.Controls;

using System.Windows.Media;


namespace CustomControlsLib
{
    public partial class MinLengthTextBox : UserControl
    {
        public static readonly DependencyProperty TextValueProperty =
            DependencyProperty.Register(
                "TextValue", 
                typeof(string), 
                typeof(MinLengthTextBox),
                new FrameworkPropertyMetadata(
                    string.Empty, 
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, 
                    OnTextValueChanged));

        public static readonly DependencyProperty MinLengthProperty =
            DependencyProperty.Register(
                "MinLength", 
                typeof(int), 
                typeof(MinLengthTextBox),
                new PropertyMetadata(0, (d, e) => ((MinLengthTextBox)d).Validate()));

        // Propietat per al Tooltip (Part opcional de la PAC)
        public static readonly DependencyProperty TooltipMessageProperty =
            DependencyProperty.Register(
                "TooltipMessage", 
                typeof(string), 
                typeof(MinLengthTextBox), 
                new PropertyMetadata(string.Empty));

        public string TextValue { get => (string)GetValue(TextValueProperty); set => SetValue(TextValueProperty, value); }
        public int MinLength { get => (int)GetValue(MinLengthProperty); set => SetValue(MinLengthProperty, value); }
        public string TooltipMessage { get => (string)GetValue(TooltipMessageProperty); set => SetValue(TooltipMessageProperty, value); }

        public MinLengthTextBox()
        {
            InitializeComponent();
            InnerTextBox.TextChanged += (s, e) => { TextValue = InnerTextBox.Text; Validate(); };
        }

        private static void OnTextValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (MinLengthTextBox)d;
            if (control.InnerTextBox.Text != (string)e.NewValue) control.InnerTextBox.Text = (string)e.NewValue;
            control.Validate();
        }

        public static bool IsValid(string text, int min) => text != null && text.Length >= min;

        private void Validate()
        {
            //if (InnerTextBox == null) return;

            if (string.IsNullOrEmpty(TextValue))
            {
                InnerTextBox.ClearValue(TextBox.BorderBrushProperty);
                InnerTextBox.ClearValue(TextBox.BorderThicknessProperty);
                TooltipMessage = "Aquest camp és obligatori";
            }
            else if (!IsValid(TextValue, MinLength))
            {
                InnerTextBox.BorderBrush = Brushes.Red;
                InnerTextBox.BorderThickness = new Thickness(2);
                TooltipMessage = $"Calen almenys {MinLength} caràcters.";
            }
            else
            {
                InnerTextBox.BorderBrush = Brushes.Green;
                InnerTextBox.BorderThickness = new Thickness(1);
                TooltipMessage = "Longitud correcta";
            }
        }
    }
}