using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Balda
{
    class BaldaWord : Button
    {
        /// <summary>
        /// конструктор
        /// </summary>
       public BaldaWord()
        {
           this.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;          
           this.VerticalAlignment = System.Windows.VerticalAlignment.Center;
           this.Width = 105;
           this.Height = 100;
           this.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Yellow);
           this.Opacity = 0.8;         
       }

       //protected override void OnDoubleTap(System.Windows.Input.GestureEventArgs e)
       //{
       //   // base.OnDoubleTap(e);
           
       //    int selectLitera = Convert.ToInt32(this.Name);
       //    foreach(var c in Selected)
       //    {
       //        //если буква уже выделена
       //        if (selectLitera == c) return;
       //    }

       //    //проверяем на пересечение/ если текущая буква получена не как предыдущая +-1 +-5 то буква выделена неверно
       //    if (Selected.Count >= 1 )
       //        if (selectLitera != (Selected[(Selected.Count - 1)] + 1) && selectLitera != (Selected[(Selected.Count - 1)] - 1) &&
       //        selectLitera != (Selected[(Selected.Count - 1)] - 5) && selectLitera != (Selected[(Selected.Count - 1)] + 5)) 
       //        return;
          
       //    //иначе добавляем букву в перечень
       //    Selected.Add( selectLitera);
       //    //формируем слово
       ////    word += this.Text;
       //   // this.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
       //}


       // /// <summary>
       // /// ввод буквы, если это не буква - заменяем на пустоту. если пусто - сделать все поля редактируемыми
       // /// </summary>
       //protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
       //{
       //    // проверка на букву или на удаление
       //    base.OnKeyDown(e);
       //    if (e.PlatformKeyCode >= 65 & e.PlatformKeyCode <= 90)
       //    {
       //        WrittingWordPos = this.Name;
       //    }
       //        // все действия с нуля (текущего пололжения)
       //    else
       //    {
       //        this.Text = "";
       //        MustReadOnlyDo = false;
       //        Rewritting = true;
       //        word = "";
       //        Selected = new List<int>();
       //    }
       //}


        /// <summary>
        /// пользователь отпустил бокс, если поле не пусто - сделать все нередактируемым (кроме введенной буквы)
       /// </summary>
       //protected override void OnLostFocus(System.Windows.RoutedEventArgs e)
       //{
       //   // base.OnLostFocus(e);
       // //   if (this.Text != "")
       //  //  {
       //        MustReadOnlyDo = true;
       //        Rewritting = false;
       //  //  }
       //}
    }
}
