using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PropertyChanged;
using System.ComponentModel;

namespace Tabi.ViewModels
{
    [ImplementPropertyChanged]
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<string> ID { get; set; }
        public string capID { get; set; }
        public string input { get; set; }
        public string center { get; set; }
        public string level { get; set; }
        public string capitol { get; set; }
        public List<string> populars { get; set; }
    }
}
