using System.Text;
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
                 new FrameworkPropertyMetadata(0, OnMinLengthChanged));

            public string TextValue
            {
                get => (string)GetValue(TextValueProperty);
                set => SetValue(TextValueProperty, value);
            }

            public int MinLength
            {
                get => (int)GetValue(MinLengthProperty);
                set => SetValue(MinLengthProperty, value);
            }

          
            public bool IsValid => TextValue != null && TextValue.Length >= MinLength;

            public MinLengthTextBox()
            {
                InitializeComponent();

                
                InternalTextBox.TextChanged += (s, e) => {
                    TextValue = InternalTextBox.Text;
                    Validate();
                };
            }

            
            private static void OnTextValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                var control = (MinLengthTextBox)d;
                var newValue = (string)e.NewValue;

                if (control != null)
                {
                   
                    if (control.InternalTextBox.Text != newValue)
                    {
                        control.InternalTextBox.Text = newValue;
                    }
                    control.Validate();
                }
            }

            private static void OnMinLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                var control = (MinLengthTextBox)d;
                control?.Validate();
            }

            //Validacio de si el text coincideix o no em les normatives
            //si no coincideix es posa en vermell el requadre si no no es
            //posa en vermell segueix en gris
            private void Validate()
            {
                if (!IsValid)
                {
                    
                    InternalTextBox.BorderBrush = Brushes.Red;
                    InternalTextBox.BorderThickness = new Thickness(2);


                   
            }
                else
                {
                    
                    InternalTextBox.BorderBrush = Brushes.Gray;
                    InternalTextBox.BorderThickness = new Thickness(1);
                    InternalTextBox.Background = Brushes.White;
            }
            }
        }
    }