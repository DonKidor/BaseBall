using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using RGR.ViewModels;
using System.Data;

namespace RGR.Views
{
    public partial class QueryWindow : Window
    {
        public QueryWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }



        private async void CreateQuery(object? sender, RoutedEventArgs args)
        {
            var context = this.DataContext as QueryWindowViewModel;
            NewQueryWindow dialog = new NewQueryWindow() { DataContext = context };
            var rTable = await dialog.ShowDialog<DataTable?>(this.VisualRoot as Window);
            if(rTable != null)
            {
                var q = new Models.MyQuery("New Query");
                q.Items.Add(new Models.MyQueryItem(rTable));
                context.QueryItems.Add(q);
                context.setTarget(q);
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
