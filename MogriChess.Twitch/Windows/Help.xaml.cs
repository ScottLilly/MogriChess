﻿using System.Windows;

namespace MogriChess.Twitch.Windows
{
    public partial class Help : Window
    {
        public Help()
        {
            InitializeComponent();
        }

        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}