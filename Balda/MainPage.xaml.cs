using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Balda.Resources;
using Microsoft.Phone.Info;


namespace Balda
{
    public partial class MainPage : PhoneApplicationPage
    {
        String Player;


        // Constructor
        public MainPage()
        {
            InitializeComponent();

            byte[] id = (byte[])DeviceExtendedProperties.GetValue("DeviceUniqueId");
            Player = Convert.ToBase64String(id);
            var SRB = new ServiceReferenceBalda.ServiceBaldaClient();
         //  SRB.RegistrationCompleted += new EventHandler<ServiceReferenceBalda.RegistrationCompletedEventArgs>(CallRegistration);
        //    SRB.RegistrationAsync(Player);
        }

        private void CallRegistration(object sender, ServiceReferenceBalda.RegistrationCompletedEventArgs e)
        {
            var player = e.Result;
        }


        /// <summary>
        /// Выход
        /// </summary>
        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            App.Current.Terminate();
        }


       /// <summary>
       /// Начало игры
       /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // если напарник найдем - начало игры, иначе сообщение что все парнеры заняты
            NavigationService.Navigate(new Uri("/GamePage.xaml", UriKind.Relative));
        }
    }
}