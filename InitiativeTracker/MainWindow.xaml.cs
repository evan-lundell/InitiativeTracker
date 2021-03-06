﻿using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitiativeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            var viewModel = new MainWindowViewModel();
            viewModel.SaveTriggered += ViewModel_SaveTriggered;
            viewModel.LoadTriggered += ViewModel_LoadTriggered;
            viewModel.InitiativeTied += ViewModel_InitiativeTied;
            DataContext = viewModel;
        }

        private void ViewModel_InitiativeTied(object sender, TieInitiativeEventArgs e)
        {
            TieBreakerWindow tieBreakerWindow = new TieBreakerWindow();
            tieBreakerWindow.DataContext = new TieBreakerWindowViewModel(e.Combatants);
            tieBreakerWindow.ShowDialog();
        }

        private void ViewModel_LoadTriggered(object sender, SaveEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.DefaultExt = ".json";
                dialog.Filter = "JSON File|*.json";
                var result = dialog.ShowDialog();
                if (result.HasValue && result.Value && File.Exists(dialog.FileName))
                {
                    e.SaveData = JsonConvert.DeserializeObject<IEnumerable<Combatant>>(File.ReadAllText(dialog.FileName));
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ViewModel_SaveTriggered(object sender, SaveEventArgs e)
        {
            try
            {
                string serializedSaveData = JsonConvert.SerializeObject(e.SaveData);
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.DefaultExt = ".json";
                dialog.Filter = "JSON File|*.json";
                var result = dialog.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    File.WriteAllText(dialog.FileName, serializedSaveData);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
