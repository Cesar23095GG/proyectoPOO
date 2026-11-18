using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemadeGestionEscolar.Clases
{
    public class PersonalAdministrativo : Persona
    {
        public string Cargo { get; set; }
        public string Departamento { get; set; }

        public PersonalAdministrativo(int id, string nombres, string apellidos, string documento,
                                      DateTime fechaNacimiento, string telefono, string correo,
                                      string cargo, string departamento)
            : base(id, nombres, apellidos, documento, fechaNacimiento, telefono, correo)
        {
            Cargo = cargo;
            Departamento = departamento;
        }

        public override string ObtenerRol()
        {
            return "Personal administrativo";
        }
    }
}
