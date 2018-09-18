using App2.Entity;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<string> filter = new List<string>();
        public ObservableCollection<User> Users { get => Model.UserModel.GetUsers(filter); set => Model.UserModel.SetUsers(value); }

        public object Interaction { get; private set; }

        public MainPage()
        {
            this.InitializeComponent();
            
        }

        private void SaveUser (object sender, RoutedEventArgs e)
        {

            User user = new User
            {
                Name = this.name.Text,
                Email = this.Email.Text,
                Phone = this.Phone.Text,
                Address = this.Address.Text,
                Avatar = this.Avatar.Text
            };
            Model.UserModel.AddUser(user);
        }

     
        private async void Search(object sender, RoutedEventArgs e)
        {
            filter = new List<string>();
            filter = await InputTextDialogAsync("Search for customers:");
            Users.Clear();
            Users = Model.UserModel.GetUsers(filter);
        }

        private async Task<List<string>> InputTextDialogAsync(string title)
        {
            TextBox inputTextBox = new TextBox
            {
                AcceptsReturn = false,
                Height = 32
            };

            ComboBox cbx = new ComboBox
            {
                PlaceholderText = "slect field"
            };
            cbx.Items.Add("Name");
            cbx.Items.Add("Email");
            cbx.Items.Add("Phone");

            StackPanel sp = new StackPanel();
            sp.Children.Add(inputTextBox);
            sp.Children.Add(cbx);
            ContentDialog dialog = new ContentDialog
            {
                Content = sp,
                Title = title,
                IsSecondaryButtonEnabled = true,
                PrimaryButtonText = "Ok",
                SecondaryButtonText = "Cancel"
            };
            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                filter.Add(inputTextBox.Text);
                filter.Add(cbx.SelectedValue.ToString());
                return filter;
            }
            else
                return null;
            
        }
        
        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            e.ClickedItem.GetHashCode();
        }
    }
}
