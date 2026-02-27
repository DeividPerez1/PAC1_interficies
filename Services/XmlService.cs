using System;
using System.Collections.ObjectModel; // Per treballar amb llistes dinàmiques
using System.IO;                      // Per poder llegir i escriure fitxers al disc
using System.Xml.Serialization;       // L'eina màgica que converteix C# a XML
using WPF_MVVM_SPA_Template.Models;   // Connectem amb la definició del "Client"

namespace WPF_MVVM_SPA_Template.Services
{
    // Posem 'public' perquè l'app el pugui veure i 'static' perquè no haguem 
    // d'estar creant "versions" del servei cada vegada
    public static class XmlService
    {
        // Nom del fitxer que es crearà a la carpeta del projecte
        private static string fitxerDades = "clients_guardats.xml";

        // --- MÈTODE PER GUARDAR ---
        // Aquesta funció agafa els clients i els escriu a l'XML
        public static void Guardar(ObservableCollection<Client> llista)
        {
            try
            {
                // Creem el traductor (Serializer)
                XmlSerializer xmlTrans = new XmlSerializer(typeof(ObservableCollection<Client>));

                // Obrim el canal d'escriptura cap al fitxer
                using (StreamWriter sw = new StreamWriter(fitxerDades))
                {
                    // Traduïm la llista a text XML i la guardem
                    xmlTrans.Serialize(sw, llista);
                }
            }
            catch (Exception ex)
            {
                // Si hi ha un error (ex: permisos de carpeta), ens avisa per pantalla
                System.Windows.MessageBox.Show("Error al guardar l'XML: " + ex.Message);
            }
        }

        // --- MÈTODE PER CARREGAR ---
        // Aquesta funció llegeix el fitxer XML i ens torna la llista de clients
        public static ObservableCollection<Client> Carregar()
        {
            // Si el fitxer no existeix (la primera vegada), tornem una llista buida
            if (!File.Exists(fitxerDades)) return new ObservableCollection<Client>();

            try
            {
                // Preparem el traductor per a la lectura
                XmlSerializer xmlTrans = new XmlSerializer(typeof(ObservableCollection<Client>));

                // Obrim el canal de lectura del fitxer
                using (StreamReader sr = new StreamReader(fitxerDades))
                {
                    // Convertim el text de l'XML de nou a objectes 'Client'
                    return (ObservableCollection<Client>)xmlTrans.Deserialize(sr);
                }
            }
            catch
            {
                // Si el fitxer està trencat, tornem una llista buida per no bloquejar l'app
                return new ObservableCollection<Client>();
            }
        }
    }
}