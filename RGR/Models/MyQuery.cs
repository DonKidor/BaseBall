using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections.ObjectModel;
using ReactiveUI;
using System.ComponentModel;

namespace RGR.Models
{
    public class MyQuery: DataTable, IReactiveObject
    {

        private string queryString;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        public string QueryString
        {
            get => queryString;
            private set => queryString=value;
        }

        private void GenerateQuery()
        {
            var sel = Items[0].getSelected();
            string res = "SELECT ";
            for(int i=0; i<sel.Count; i++)
            {
                res += sel[i];
                if (i != sel.Count - 1) res += ", ";
            }
            res += " FROM " + Items[0].getString();
            res += ";";
            queryString = res;
        }
        public void Run()
        {
            GenerateQuery();
            DBContext.getInstance().GetQuery(QueryString, this);
        }

        
        public string QueryName
        {
            get => TableName;
            set
            {
                if(TableName != value)
                {
                    TableName = value;
                    this.RaisePropertyChanged(new PropertyChangedEventArgs("QueryName"));
                } else
                {
                    TableName = value;
                }
            }
        }
        
        public void RaisePropertyChanging(PropertyChangingEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }

        public ObservableCollection<MyQueryItem> Items
        {
            get;
            set;
        }
        public MyQuery(string name) :base()
        {
            TableName = name;
            Items = new ObservableCollection<MyQueryItem>();
        }
    }
}
