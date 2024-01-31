using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;

namespace ClientConvertisseurV2.ViewModels
{
    public class ConvertisseurEuroViewModel:ObservableObject
    {
        private ObservableCollection<Devise> devises;

        public ObservableCollection<Devise> Devises
        {
            get { return devises; }
            set
            {
                devises = value;
            }
        }

        private Devise selectedDevise;

        public Devise SelectedDevise
        {
            get { return selectedDevise; }
            set
            {
                selectedDevise = value;
                this.OnPropertyChanged();
            }
        }


        private double montantEuro;

        public double MontantEuro
        {
            get { return montantEuro; }
            set
            {
                montantEuro = Math.Round(value, 2);
                this.OnPropertyChanged();
            }
        }

        private double montantDevise;
        public double MontantDevise
        {
            get { return montantDevise; }
            set
            {
                montantDevise = value;
                this.OnPropertyChanged();
            }
        }

        public RelayCommand BtnSetConversion { get; }

        public ConvertisseurEuroViewModel()
        {
            Devises = new ObservableCollection<Devise>();
            GetDataOnLoadAsync();
            BtnSetConversion = new RelayCommand(ActionSetConversion);
        }

        public void ActionSetConversion()
        {
            if (SelectedDevise == null)
            {
                ShowErrorMessage("Erreur de devise", "Vous devez séléctionner une dévise pour effectuer la conversion");
                return;
            }
            if (MontantEuro < 0)
            {
                ShowErrorMessage("Erreur de montant", "Vous ne pouvez convertir que des montants positifs");
                return;
            }
            MontantDevise = Math.Round(MontantEuro * SelectedDevise.Taux, 2);
        }

        public async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("https://localhost:44389/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");
            if (result == null)
            {
                ShowErrorMessage("API non disponible !", "Erreur");
            }
            else
            {
                foreach (Devise dev in result)
                {
                    Devises.Add(dev);
                }
            }
        }

        // Méthode honteusement volée à Dylan Miftary car la méthode MessageAsync ne fonctionne pas
        private async void ShowErrorMessage(string title, string message)
        {

            ContentDialog contentDialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "Ok"
            };

            contentDialog.XamlRoot = App.MainRoot.XamlRoot;


            ContentDialogResult result = await contentDialog.ShowAsync();
        }
    }
}
