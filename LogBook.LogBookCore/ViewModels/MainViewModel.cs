﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logbook.Lib;
using LogBook.LogBookCore.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using LogBook.LogBookCore.Messages;

namespace Logbook.LogBookCore.ViewModel
{
    public partial class MainViewModel(IRepository repository, IAlertService alertService) : ObservableObject
    {
   
        // public string Header => "Fahrtenbuch";
        

        public object Entries { get; private set; }

        IRepository _repository = repository;

        IAlertService _alertService = alertService;

        private bool _isLoaded = false;

        [ObservableProperty]
        ObservableCollection<Logbook.Lib.Entry> _ent = [];

        [ObservableProperty]
        Lib.Entry? _selectedEntry = null;


        #region Properties

        [ObservableProperty]
        DateTime _start = DateTime.Now;

        [ObservableProperty]
        DateTime _end = DateTime.Now;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddCommand))]
        string _description = string.Empty;


        [ObservableProperty]
        string _numberPlate = string.Empty;

        [ObservableProperty]
        int _startKM = 0;

        [ObservableProperty]
        int _endKM = 0;

        [ObservableProperty]
        string _from = string.Empty;

        [ObservableProperty]
        string _to = string.Empty;

        #endregion

        void ToggleFavorite(Lib.Entry entry)
        {
            entry.Favourite = !entry.Favourite;

            var result = _repository.Update(entry);

            if (result == true)
            {
                int pos = this.Ent.IndexOf(entry);

                if(pos != -1)
                {
                    this.Ent[pos] = entry;
                    _alertService.ShowAlert("Fehler", "Der Status konnte verändert werden");
                }
            }
            else
            {
                _alertService.ShowAlert("Fehler", "Der Status konnte nicht verändert werden");
            }
        }

        [RelayCommand]
        void Delete(Lib.Entry entry)
        {
            Lib.Entry entryToDelete = _repository.find(entry.Id);

            if (entryToDelete != null)
            {
                var res = _repository.Delete(entryToDelete);

                if (res)
                {
                    this.SelectedEntry = null;
                    this.Ent.Remove(entry);
                    _alertService.ShowAlert("Erfolg", "Der Eintrag wurde gelöscht .");
                }
                else
                {
                    _alertService.ShowAlert("Fehler", "Der Eintrag konnte nicht gelöscht werden.");
                }
            }
            else
            {
                _alertService.ShowAlert("Fehler", "Der Eintrag konnte nicht gelöscht werden.");
            }
        }

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

        
        private bool CanAdd => this.Description.Length > 0;

        [RelayCommand(CanExecute = nameof(CanAdd))] // nameof ist eine Hilfe für den Programmierer
        void Add()
        {
            /*
            Lib.Entry entrySaalfelden = new(

            DateTime.Now.AddDays(3),
            DateTime.Now.AddDays(3).AddMinutes(20),
            25500, 25514,
            "ZE-XY123",
            "Zell am See",
            "Saalfelden");
            */

            Lib.Entry entry = new(this.Start, this.End, this.StartKM, this.EndKM, this.NumberPlate, this.From, this.To, false);

            if (this.Description.Length > 0)
            {
                entry.Description = this.Description;
            }

            var result = _repository.Add(entry);

            if (result)
            {
                this.Ent.Add(entry);
                this.Description = string.Empty;
                this.From = "";
                this.To = "";
                this.StartKM = this.EndKM;
                this.EndKM = 0;

                WeakReferenceMessenger.Default.Send(new AddMessage(entry));
            }
        }
    }
}
