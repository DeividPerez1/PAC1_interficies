using System;
using System.Collections.ObjectModel; 
using System.IO;                      
using System.Xml.Serialization;      
using Clients_Managment.Models;

namespace Clients_Managment.Services
{
    public static class XmlService
    {
        // --- NOU MÈTODE PER CALCULAR LA RUTA ---
        // Calcula la ruta cap a l'arrel del projecte, fora de la carpeta 'bin'
        private static string ObtenirRutaFitxer()
        {
            // Agafem la ruta d'on s'està executant (.exe)
            string directoriExe = AppDomain.CurrentDomain.BaseDirectory;

            // Pugem 3 nivells: \net8.0-windows\ -> \Debug\ -> \bin\ -> Arrel del projecte
            string directoriProjecte = Path.GetFullPath(Path.Combine(directoriExe, @"..\..\..\"));

            // Unim la ruta del projecte amb el nom de l'arxiu
            return Path.Combine(directoriProjecte, "clients_guardats.xml");
        }

        // --- MÈTODE PER GUARDAR ---
        public static void Guardar(ObservableCollection<Client> llista)
        {
            string fitxerDades = ObtenirRutaFitxer(); // Cridem la ruta bona

            try
            {
                XmlSerializer xmlTrans = new XmlSerializer(typeof(ObservableCollection<Client>));

                using (StreamWriter sw = new StreamWriter(fitxerDades))
                {
                    xmlTrans.Serialize(sw, llista);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error al guardar l'XML: " + ex.Message);
            }
        }

        // --- MÈTODE PER CARREGAR ---
        public static ObservableCollection<Client> Carregar()
        {
            string fitxerDades = ObtenirRutaFitxer(); // Cridem la ruta bona

            if (!File.Exists(fitxerDades)) return new ObservableCollection<Client>();

            try
            {
                XmlSerializer xmlTrans = new XmlSerializer(typeof(ObservableCollection<Client>));

                using (StreamReader sr = new StreamReader(fitxerDades))
                {
                    return (ObservableCollection<Client>)xmlTrans.Deserialize(sr);
                }
            }
            catch
            {
                return new ObservableCollection<Client>();
            }
        }
    }
}