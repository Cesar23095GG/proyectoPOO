using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemadeGestionEscolar.Clases
{
    public class Asignatura
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public Grado Grado { get; set; }
        public Docente DocenteResponsable { get; private set; }

        public Asignatura(int id, string codigo, string nombre, Grado grado)
        {
            Id = id;
            Codigo = codigo;
            Nombre = nombre;
            Grado = grado;
        }

        public void AsignarDocente(Docente docente)
        {
            DocenteResponsable = docente;
        }

        public override string ToString()
        {
            string docente = DocenteResponsable != null ? DocenteResponsable.NombreCompleto : "Sin docente";
            return $"{Codigo} - {Nombre} ({Grado.Nombre}) | Docente: {docente}";
        }
    }
}
