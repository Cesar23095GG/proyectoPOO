using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemadeGestionEscolar.Clases
{
    public class ResultadoEstudiante
    {
        public Estudiante Estudiante { get; }
        public PeriodoAcademico Periodo { get; }
        public List<ResultadoAsignatura> Asignaturas { get; }
        public decimal PromedioGeneral { get; }
        public EstadoAcademico Estado { get; }

        public ResultadoEstudiante(Estudiante estudiante, PeriodoAcademico periodo,
                                   List<ResultadoAsignatura> asignaturas,
                                   decimal promedioGeneral, EstadoAcademico estado)
        {
            Estudiante = estudiante;
            Periodo = periodo;
            Asignaturas = asignaturas;
            PromedioGeneral = promedioGeneral;
            Estado = estado;
        }

        public override string ToString()
        {
            return $"{Estudiante.NombreCompleto} | Promedio general: {PromedioGeneral:0.00} | {Estado}";
        }
    }
}
