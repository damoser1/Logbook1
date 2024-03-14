using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logbook.Lib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogBook.LogBookCore.ViewModels
{
    public partial class ReportViewModel : ObservableObject
    {
        IRepository _repository;
        public ReportViewModel(IRepository repository)
        {
            _repository = repository;
        }

        [ObservableProperty]
        ObservableCollection<Logbook.Lib.Entry> _ent = [];

        private bool _isLoaded = false;

        [RelayCommand]
        void LoadData()
        {
            // naja
            // Ent.Clear(); // ist für dafür da, das wenn wir vom Bericht zurück wechseln, dass die Einträge zurückgesetzt werden

            // besser so
            if (!_isLoaded)
            {
                var entries = _repository.GetAll();

                foreach (var entry in entries)
                {
                    Ent.Add(entry);
                }

                _isLoaded = true;
            }
        }

    }
}
