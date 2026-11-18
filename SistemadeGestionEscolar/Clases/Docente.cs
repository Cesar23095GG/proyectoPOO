using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemadeGestionEscolar.Clases
{
    public class Docente : Persona
    {
        public string Especialidad { get; set; }
        public DateTime FechaContratacion { get; set; }

        public Docente(int id, string nombres, string apellidos, string documento,
                       DateTime fechaNacimiento, string telefono, string correo,
                       string especialidad, DateTime fechaContratacion)
            : base(id, nombres, apellidos, documento, fechaNacimiento, telefono, correo)
        {
            Especialidad = especialidad;
            FechaContratacion = fechaContratacion;
        }

        public override string ObtenerRol()
        {
            return "Docente";
        }
    }
}
