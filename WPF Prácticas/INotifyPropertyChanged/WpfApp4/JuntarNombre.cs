using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4
{
    public class JuntarNombre : INotifyPropertyChanged
    {

        private string nombre, apellido, nombre_completo;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value;
                OnPropertyChanged("Nombre_completo");
            }
        }

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value;
                // Si tiene cambio la caja de texto se aplica al textBox3 Nombre Completo.
                OnPropertyChanged("Nombre_completo");    
            }
        }

        public string Nombre_completo
        {
            get { nombre_completo = Nombre + " " + Apellido;
                return nombre_completo;
            }
            set { }
        }
    }
}
