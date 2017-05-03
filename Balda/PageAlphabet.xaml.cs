using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Balda
{
    public partial class PageAlphabet : PhoneApplicationPage
    {
       BaldaButton[] BB;
        string [] str = { "А", "Б", "В", "Г", "Д", "Е", "Ё","Ж","З","И","Й","К","Л","М","Н","О","П","Р","С","Т","У","Ф","Х","Ц","Ч","Ш","Щ","Ъ","Ы","Ь","Э","Ю","Я", " " };

        public PageAlphabet()
        {
            InitializeComponent();
            BB = new BaldaButton[str.Length];
            for (int i = 0, j = 0, u = 1; i < str.Length; i++, j++)
            {
                BB[i] = new BaldaButton();
                if (i / (6 * u) == 1) { j = 0; u += 1; }
                int o = ((i / 6) + 1);
                Grid.SetRow(BB[i], o);
                Grid.SetColumn(BB[i], (j));
                BB[i].Content =str[i];
                this.LayoutRoot.Children.Add(BB[i]);
            }
            
        }

        private void LayoutRoot_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}