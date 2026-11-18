using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemadeGestionEscolar.Clases
{
    public class Asistencia
    {
        public int Id { get; set; }
        public Matricula Matricula { get; }
        public DateTime Fecha { get; }
        public EstadoAsistencia Estado { get; set; }

        public Asistencia(int id, Matricula matricula, DateTime fecha, EstadoAsistencia estado)
        {
            Id = id;
            Matricula = matricula;
            Fecha = fecha.Date;
            Estado = estado;
        }

        public override string ToString()
        {
            return $"{Fecha:dd/MM/yyyy} | {Matricula.Estudiante.NombreCompleto} | {Matricula.Asignatura.Nombre}: {Estado}";
        }
    }
}
