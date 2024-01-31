using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientConvertisseurV2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientConvertisseurV2.Models;
using System.Collections.ObjectModel;

namespace ClientConvertisseurV2.ViewModels.Tests
{
    [TestClass()]
    public class ConvertisseurEuroViewModelTests
    {
        /// <summary>
        /// Test constructeur.
        /// </summary>
        [TestMethod()]
        public void ConvertisseurEuroViewModelTest()
        {
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            Assert.IsNotNull(convertisseurEuro);
        }

        /// Test GetDataOnLoadAsyncTest OK
        /// </summary>
        [TestMethod()]
        public void GetDataOnLoadAsyncTest_OK()
        {
            //Arrange
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            //Act
            List<Devise> devise = new List<Devise>();
            devise.Add(new Devise(1, "Dollar", 1.08));
            devise.Add(new Devise(2, "Franc Suisse", 1.07));
            devise.Add(new Devise(3, "Yen", 120));        
            Thread.Sleep(1000);
            ObservableCollection<Devise> result = convertisseurEuro.Devises;
            //Assert
            Assert.IsNotNull(convertisseurEuro.Devises);
            CollectionAssert.AreEqual(result, devise, "Liste non égale");
        }

        [TestMethod()]
        public void GetDataOnLoadAsyncTest_NonOK_WSnondemarre()
        {
            //Arrange
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();

            //Assert
            Assert.AreEqual(convertisseurEuro.Devises.Count(), 0, "WSnondemmarré, aucune devise récupérée");
        }

        /// <summary>
        /// Test conversion OK
        /// </summary>
        [TestMethod()]
        public void ActionSetConversionTest()
        {
            //Arrange
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            convertisseurEuro.MontantEuro = 100;
            Devise devise = new Devise(2, "Franc Suisse", 1.07);
            convertisseurEuro.SelectedDevise = devise;

            //Act
            convertisseurEuro.ActionSetConversion();

            //Assert
            Assert.AreEqual(convertisseurEuro.MontantDevise, 107, "Les valeurs ne sont pas égales");
        }
    }
}