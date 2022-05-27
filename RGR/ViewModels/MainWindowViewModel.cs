using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;
using ReactiveUI;
using RGR.Models;
using System.Collections.ObjectModel;

namespace RGR.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private DataSet tables;
        private DBContext context;

        private ObservableCollection<DataTable> tablesList;
        public ObservableCollection<DataTable> Tables
        {
            get => tablesList;
            private set => this.RaiseAndSetIfChanged(ref tablesList, value);
        }

        
        public MainWindowViewModel()
        {
            context = DBContext.getInstance();
            tables = context.getDataSet();
            Tables = new ObservableCollection<DataTable>();
            foreach(DataTable t in tables.Tables)
            {
                Tables.Add(t);
            }
        }

        public void AddTable(DataTable table)
        {
            if(!Tables.Contains(table))
                Tables.Add(table);
        }
        
        public void OnClick()
        {
            context.Save(tables);
        }
        public void deleteQuery(MyQuery quer)
        {
            Tables.Remove(quer);
            this.RaisePropertyChanged("Tables");
        }
    }
}
