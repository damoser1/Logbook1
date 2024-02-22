using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logbook.Lib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.LogBookApp.ViewModel
{
    public partial class MainViewModel(IRepository repository) : ObservableObject
    {
        public string Header => "Fahrtenbuch";

        IRepository _repository = repository;

        // nichts anderes wie eine Liste, mit einem zusätzlichen Feature, welches die Oberfläche informiert
        [ObservableProperty]
        ObservableCollection<Lib.Entry> _entries = [];

        [RelayCommand]
        void LoadData()
        {
            try
            {
                var entries = _repository.GetAll();

                if (entries != null)
                {
                    foreach (var entry in entries)
                    {
                        Entries.Add(entry);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [RelayCommand]
        void Add()
        {
            Lib.Entry entrySaalfelden = new(DateTime.Now.AddDays(3),
                                  DateTime.Now.AddDays(3).AddMinutes(20),
                                  25500,
                                  25514,
                                  "ZE-XY123",
                                  "Zell am See",
                                  "Saalfelden")
            {
                Description = "Fahrt nach Saalfelden"
            };

            var result = _repository.Add(entrySaalfelden);
            if(result == true)
            {
                this.Entries.Add(entrySaalfelden);
            }

        }
    }
}
