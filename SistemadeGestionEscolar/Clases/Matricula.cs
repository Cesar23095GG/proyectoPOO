using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemadeGestionEscolar.Clases
{
    public class Matricula
    {
        public int Id { get; set; }
        public Estudiante Estudiante { get; }
        public Asignatura Asignatura { get; }
        public PeriodoAcademico Periodo { get; }
        public DateTime FechaInscripcion { get; }

        public Matricula(int id, Estudiante estudiante, Asignatura asignatura, PeriodoAcademico periodo)
        {
            Id = id;
            Estudiante = estudiante;
            Asignatura = asignatura;
            Periodo = periodo;
            FechaInscripcion = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Matrícula {Id}: {Estudiante.NombreCompleto} -> {Asignatura.Nombre} | {Periodo.Nombre}";
        }
    }
}
