using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemadeGestionEscolar.Clases
{
    public class Seccion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Capacidad { get; set; }
        public Grado Grado { get; }
        public List<Estudiante> Estudiantes { get; } = new List<Estudiante>();

        public Seccion(int id, string nombre, int capacidad, Grado grado)
        {
            Id = id;
            Nombre = nombre;
            Capacidad = capacidad;
            Grado = grado;
        }

        public bool AgregarEstudiante(Estudiante estudiante)
        {
            if (Estudiantes.Count >= Capacidad || Estudiantes.Contains(estudiante))
                return false;

            Estudiantes.Add(estudiante);
            estudiante.AsignarSeccion(this);
            return true;
        }

        public override string ToString()
        {
            return $"{Grado.Nombre} - Sección {Nombre}";
        }
    }
}
