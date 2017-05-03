using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;

namespace Balda
{
    public partial class GamePage : PhoneApplicationPage
    {
        /// <summary>
        /// 
        /// </summary>
        Int16 count = 25;
        /// <summary>
        /// 
        /// </summary>
        BaldaWord[] BTB;
        /// <summary>
        /// 
        /// </summary>
        System.Windows.Threading.DispatcherTimer BaldaTimer;
        /// <summary>
        /// 
        /// </summary>
        bool navigateToAlphabet = false;
        /// <summary>
        /// 
        /// </summary>
        DateTime DT;

        /// <summary>
        /// список из букв, которые уже изменению не подлежат
        /// </summary>
        List<String> Literas;
        /// <summary>
        /// буква, которая активна сейчас
        /// </summary>
        String Litera = "";
        /// <summary>
        /// позиция активной сейчас буквы
        /// </summary>
        int PosLitera = -1 ;
        /// <summary>
        /// набор выделяемых букв
        /// </summary>
        List<string> select;


        /// <summary>
        /// конструктор
        /// </summary>
        public GamePage()
        {
            InitializeComponent();

            //создание полей для букв
            BTB = new BaldaWord[count];
            Literas = new List<string>();
            for(int i = 0, j = 1, u = 1; i < count; i++, j++)
            {
                BTB[i] = new BaldaWord();
                if (i > 9 && i < 15) 
                Literas.Add(i.ToString());
                if (i / (5 * u) == 1) { j = 1; u += 1; }
                int o = ((i / 5) + 1) ;
                BTB[i].Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(OnTap);
                BTB[i].Hold += new EventHandler<System.Windows.Input.GestureEventArgs>(OnHold);
                Grid.SetRow(BTB[i], o);
                Grid.SetColumn(BTB[i], (j));
                BTB[i].Name = i.ToString();
                this.LayoutRoot.Children.Add(BTB[i]);              
            }

            //инициализация таймера 
            BaldaTimer = new System.Windows.Threading.DispatcherTimer();
            BaldaTimer.Tick += new EventHandler(Timer_Tick);
            BaldaTimer.Interval = new TimeSpan(0, 0, 1);
            BaldaTimer.Start();
            DT = new DateTime();
            DT = DateTime.Now;

            select = new List<string>();

        }

       

        /// <summary>
        /// выделение букв
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (PosLitera != -1)
            {
                Button but = sender as Button;
                int pos = Convert.ToInt16(but.Name);
                bool flag = false;

                //повторы недопустимы
                foreach (var c in select)
                {
                    //найден повтор - вон (убрав выделения до текущец позиции)
                    if (c == but.Name)
                    {
                        int ii = 1;
                        while( select[select.Count - ii] != c )
                        {
                            BTB[Convert.ToInt16(select[select.Count - ii])].Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Yellow);
                            select.Remove((select.Count - ii).ToString());
                            ++ii;
                        }
                        BTB[Convert.ToInt16(select[select.Count - ii])].Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Yellow);
                        select.Remove((select.Count - ii).ToString());
                        
                            return;
                    }
                }


                // выделять можно только буквы из списка неизменяемых и текущее
                if (PosLitera != Convert.ToInt16(but.Name))
                {
                    foreach (var c in Literas)
                    {
                        //буква в списке найдена
                        if (c == (but.Name))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                else flag = true;
                if (!flag) return;
                flag = false;

                // выделения должны быть соседскими  и не повторяться
                // буква должна быть соседкой к последней выделенной
                if (select.Count > 1)
                {
                    var c = select[select.Count - 1];
                   
                        if (pos == (Convert.ToInt16(c) + 1) || pos == (Convert.ToInt16(c) - 1) ||
                            pos == (Convert.ToInt16(c) - 5) || pos == (Convert.ToInt16(c) + 5))
                        {
                            flag = true;
                            // найден сосед
                            
                        }
                    
                    if (!flag) return;
                }

                

                //все супер
                but.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                select.Add(but.Name);
            }
        }


        /// <summary>
        /// выбор буквы
        /// </summary>
        private void OnTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            // -1 указывает на то, что поле поддается изменению
            //если позиция активной и sennder совпадают - вызвать алфавит и убрать все выделения
            if (PosLitera == Convert.ToInt16((sender as Button).Name))
            {
                NavigationService.Navigate(new Uri("/PageAlphabet.xaml", UriKind.Relative));
                foreach(var c in select)
                {
                    //убрать выделения
                    foreach (var b in select)
                    {
                        BTB[Convert.ToInt16(b)].Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Yellow);
                    }
                    select = new List<string>();
                }
                return;
            }
            if (PosLitera == -1 )
            {
                PosLitera = Convert.ToInt16((sender as Button).Name);
                //новая буква должна быть соседкой остальных
                foreach (var c in Literas)
                {
                    if (PosLitera == (Convert.ToInt16(c) + 1) || PosLitera == (Convert.ToInt16(c) - 1) ||
                        PosLitera == (Convert.ToInt16(c) - 5) || PosLitera == (Convert.ToInt16(c) + 5))
                    {
                        BaldaButton.NameButton = (sender as Button).Name;
                        // Litera = (sender as Button).Name;
                        NavigationService.Navigate(new Uri("/PageAlphabet.xaml", UriKind.Relative));
                        return;
                    }
                }
                PosLitera = -1;
            }
        }


        /// <summary>
        /// работа таймера
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = (DateTime.Now - DT);
            Time.Text = String.Format("{0}:{1}:{2}", ts.Hours, ts.Minutes, ts.Seconds) ;
        }

        
        /// <summary>
        /// переход в алфавит
        /// </summary>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if(e.Uri.ToString().Contains("/PageAlphabet.xaml"))
            {
                navigateToAlphabet = true;
            }
        }


        /// <summary>
        /// очистка хода
        /// </summary>
        void CleanLast() 
        {
            // затираем активную позицию
            Litera = "";
            PosLitera = -1;
            //убрать выделения
            foreach (var b in select)
            {
                BTB[Convert.ToInt16(b)].Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Yellow);
            }
            select = new List<string>();
        }


        /// <summary>
        /// назначение content 
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (navigateToAlphabet)
            {
                foreach (var c in BTB)
                {
                    if (c.Name == BaldaButton.NameButton)
                    {
                        if (BaldaButton.GetLitera.Contains(" "))
                        {
                            CleanLast();
                        }
                        else
                        {
                            c.Content = BaldaButton.GetLitera;
                            Litera = c.Content.ToString();
                        }
                    }
                }
                navigateToAlphabet = false;
            }
        }


        /// <summary>
        /// отправить слово на сервер, на проверку
        /// </summary>
        private void Send_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            // если проверка слова прошла успешно - добавить букву в список неизменяемых
            Literas.Add(BTB[PosLitera].Content.ToString());
        }

        /// <summary>
        /// пропустить ход
        /// </summary>
        private void Skip_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

    }
}