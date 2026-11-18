using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemadeGestionEscolar.Clases
{
    public abstract class Persona
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Documento { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }

        public string NombreCompleto => $"{Nombres} {Apellidos}";

        protected Persona(int id, string nombres, string apellidos, string documento,
                          DateTime fechaNacimiento, string telefono, string correo)
        {
            Id = id;
            Nombres = nombres;
            Apellidos = apellidos;
            Documento = documento;
            FechaNacimiento = fechaNacimiento;
            Telefono = telefono;
            Correo = correo;
        }

        public abstract string ObtenerRol();

        public override string ToString()
        {
            return $"[{ObtenerRol()}] {Id} - {NombreCompleto} ({Documento})";
        }
    }
}
