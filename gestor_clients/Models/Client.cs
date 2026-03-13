using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM_SPA_Template.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string DNI { get; set; }
        public string Name { get; set; }
        public string last_name { get; set; }
        public string Email { get; set; }
        public int Tlf { get; set; }
        public string date { get; set; }
        public double[] ChartValues { get; set; }
        public string [] ChartLabels { get; set; }

        public string Premi { get; set; }

    }
}
