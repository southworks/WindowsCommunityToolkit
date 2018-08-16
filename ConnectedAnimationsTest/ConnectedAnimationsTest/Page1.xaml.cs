using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ConnectedAnimationsTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page1 : Page
    {
        public List<Item> SearchResults { get; set; }

        public Page1()
        {
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            this.InitializeComponent();

            if (SearchResults == null)
            {
                SearchResults = new List<Item>
                {
                    new Item{ Text = "a" }, new Item{ Text = "b" }, new Item{ Text = "c" }, new Item{ Text = "d" }, new Item{ Text = "e" }, new Item{ Text = "f" }, new Item{ Text = "g" }
                };
            }
            MyListView.ItemClick += (s, e) => 
            {
                var frame = Window.Current.Content as Frame;
                frame.Navigate(typeof(Page2), e.ClickedItem);
            };

            MyListView.IsItemClickEnabled = true;
        }
    }

    public class Item
    {
        public string Text { get; set; }
    }
}
