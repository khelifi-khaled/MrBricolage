using MrBricolage.ViewModels;
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

namespace MrBricolage.Views
{
   
    public partial class AddClientWindow : Window
    {
        AddClientVM AddClientVM { get; set; }

        public AddClientWindow()
        {
            this.AddClientVM = new AddClientVM();
            DataContext = this.AddClientVM;
            InitializeComponent();
        }
    }
}
