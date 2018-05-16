using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HamburgerMenuTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<MenuItem> items;
        public bool UseNavigationView { get; set; } = true;

        public MainPage()
        {
            this.InitializeComponent();
            items = new ObservableCollection<MenuItem>(MenuItem.GetMainItems());
            hamburgerMenuControl.ItemsSource = items;
        }

        private void OnMenuItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = e.ClickedItem as MenuItem;
            System.Diagnostics.Debug.WriteLine($"Clicked {menuItem.Name}");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            items.Add(new MenuItem() { Icon = Symbol.Add, Name = $"MenuItem{items.Count + 1}" });
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UseNavigationView = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UseNavigationView = false;
        }
    }

    public class MenuItem
    {
        public Symbol Icon { get; set; }
        public string Name { get; set; }

        public static List<MenuItem> GetMainItems()
        {
            var items = new List<MenuItem>();
            items.Add(new MenuItem() { Icon = Symbol.Accept, Name = "MenuItem1" });
            items.Add(new MenuItem() { Icon = Symbol.Send, Name = "MenuItem2" });
            items.Add(new MenuItem() { Icon = Symbol.Shop, Name = "MenuItem3" });
            return items;
        }

        public static List<MenuItem> GetOptionsItems()
        {
            var items = new List<MenuItem>();
            items.Add(new MenuItem() { Icon = Symbol.Setting, Name = "OptionItem1" });
            return items;
        }
    }
}
