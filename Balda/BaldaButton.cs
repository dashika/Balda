using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Balda
{
    class BaldaButton:Button
    {
        static string litera;

        public static String GetLitera { get { return litera; } }
        public static string NameButton { get; set; }

        protected override void OnTap(System.Windows.Input.GestureEventArgs e)
        {
            base.OnTap(e);
            litera = this.Content.ToString();
        }
    }
}
