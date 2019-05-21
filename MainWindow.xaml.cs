using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace RegistryEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Dictionary<string, bool> state = new Dictionary<string, bool>();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void SubKeyErrorLabel_Loaded(object sender, RoutedEventArgs e)
        {
            SubKeyErrorLabel.Content = "";
            RegistryKeyErrorLabel.Content = "";
            ValueErrorLabel.Content = "";
        }

        private void SubKeyTextBox_TextChanged(object sender, TextChangedEventArgs e)
       {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(SubKeyTextBox.Text);

            if(key == null)
            {
                state["SubKey"] = false;
                SubKeyErrorLabel.Content = "Sub Key Does not exitst";
            } else
            {
                state["SubKey"] =true;
                SubKeyErrorLabel.Content = "";
                key.Close();
            }

        }

        private void RegistryKeyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(SubKeyTextBox.Text);

            if (key != null)
            {
                string keyValue = (string)key.GetValue(RegistryKeyTextBox.Text);

                if(keyValue == null)
                {
                    state["RegistryKey"] = false;
                    state["Value"] = false;
                    ValueTextBox.Clear();
                    RegistryKeyErrorLabel.Content = "Registry Key Does not exitst";
                } else
                {
                    state["RegistryKey"] = true;
                    state["Value"] = true;
                    RegistryKeyErrorLabel.Content = "";
                    ValueTextBox.Text = keyValue.ToString();
                }

                key.Close();
            }
        }

        private void SetValueButton_Click(object sender, RoutedEventArgs e)
        {
            RegistryKey key;

            if (state["SubKey"])
            {
                key = Registry.LocalMachine.OpenSubKey(SubKeyTextBox.Text, true);
            } else
            {
                key = Registry.LocalMachine.CreateSubKey(SubKeyTextBox.Text);
            }

            key.SetValue(RegistryKeyTextBox.Text, ValueTextBox.Text);
            key.Close();
        }
    }
}
