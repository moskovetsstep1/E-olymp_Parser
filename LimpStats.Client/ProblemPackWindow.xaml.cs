﻿using System;
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
using LimpStats.Client.CustomControls;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Client
{
    /// <summary>
    /// Логика взаимодействия для ProblemPackWindow.xaml
    /// </summary>
    public partial class ProblemPackWindow : Window
    {
        public ProblemPackWindow()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var f = new InitializationCardWindow(new StudyGroup());
            f.ShowDialog();
        }

        public void OnClick_UpdatePanel(object sender, RoutedEventArgs e)
        {
           // Panel.Children.Add(new StudentGroupPreview(this, FilePath.Text));
            FilePath.Text = string.Empty;
            PanelViewer.ScrollToRightEnd();
        }
        private void LoadFromFile_ButtonClick(string title)
        {
           // var preview = new StudentGroupPreview(this, title);
           // preview.Update();
           // Panel.Children.Add(preview);
            PanelViewer.ScrollToRightEnd();

        }
        private void TextBox1_OnGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox1_OnGotFocus;
        }

        private void FilePath_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            AddList.IsEnabled = tb.Text != string.Empty;
        }

        private void FilePath_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OnClick_UpdatePanel(new object(), new RoutedEventArgs());
            }
        }

        private void PanelViewer_OnLoaded(object sender, RoutedEventArgs e)
        {
            var s = JsonBackupManager.LoadCardName();
            foreach (var title in s)
            {
                LoadFromFile_ButtonClick(title);
            }
        }
    }
}