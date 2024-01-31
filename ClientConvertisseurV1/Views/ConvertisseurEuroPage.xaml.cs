using ClientConvertisseurV1.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Services.Maps;
using WSConvertisseur.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClientConvertisseurV1.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConvertisseurEuroPage : Page, INotifyPropertyChanged
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

        public Devise SelectedDevise { 
            get { return selectedDevise; }
            set { 
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


        public ConvertisseurEuroPage()
        {
            this.InitializeComponent();
            Devises = new ObservableCollection<Devise>();
            this.DataContext = this;
            
            GetDataOnLoadAsync();
        }

        private async void GetDataOnLoadAsync()
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

        private void BtnConvertir_Click(object sender, RoutedEventArgs e)
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

        // Méthode honteusement volée à Dylan Miftary car la méthode MessageAsync ne fonctionne pas
        private async void ShowErrorMessage(string title, string message)
        {

            ContentDialog contentDialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "Ok"
            };

            contentDialog.XamlRoot = this.Content.XamlRoot;


            ContentDialogResult result = await contentDialog.ShowAsync();
        }


        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
