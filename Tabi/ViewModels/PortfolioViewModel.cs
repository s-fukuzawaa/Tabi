using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Tabi.ViewModels
{
    [ImplementPropertyChanged]
    public class PortfolioViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string query { get; set; }
        public string ID { get; set; }
        public string city { get; set; }
        public string map { get; set; }
        public List<string> placeName { get; set; }
        public List<string> imgUrl { get; set; }
        public List<string> rating { get; set; }
        public List<string> price { get; set; }
        public List<string> origin { get; set; }
        public List<string> destin { get; set; }
    }

}
