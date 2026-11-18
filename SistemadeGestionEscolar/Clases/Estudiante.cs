using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace SistemadeGestionEscolar.Clases
{
    public class Estudiante : Persona
    {
        public string Carnet { get; set; }
        public Seccion Seccion { get; private set; }
        public Grado Grado => Seccion?.Grado;

        public Estudiante(int id, string nombres, string apellidos, string documento,
                          DateTime fechaNacimiento, string telefono, string correo, string carnet)
            : base(id, nombres, apellidos, documento, fechaNacimiento, telefono, correo)
        {
            Carnet = carnet;
        }

        public void AsignarSeccion(Seccion seccion)
        {
            Seccion = seccion;
        }

        public override string ObtenerRol()
        {
            return "Estudiante";
        }
    }
}
