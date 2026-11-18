using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace SistemadeGestionEscolar.Clases
{
    public class Grado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Seccion> Secciones { get; } = new List<Seccion>();

        public Grado(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public bool AgregarSeccion(Seccion seccion)
        {
            if (Secciones.Any(s => s.Nombre.Equals(seccion.Nombre, System.StringComparison.OrdinalIgnoreCase)))
                return false;

            Secciones.Add(seccion);
            return true;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
