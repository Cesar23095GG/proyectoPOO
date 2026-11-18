using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemadeGestionEscolar.Clases
{
    public class ResultadoAsignatura
    {
        public Matricula Matricula { get; }
        public decimal Promedio { get; }
        public decimal PorcentajeEvaluado { get; }
        public decimal PorcentajeAsistencia { get; }
        public EstadoAcademico Estado { get; }

        public ResultadoAsignatura(Matricula matricula, decimal promedio, decimal porcentajeEvaluado,
                                   decimal porcentajeAsistencia, EstadoAcademico estado)
        {
            Matricula = matricula;
            Promedio = promedio;
            PorcentajeEvaluado = porcentajeEvaluado;
            PorcentajeAsistencia = porcentajeAsistencia;
            Estado = estado;
        }

        public override string ToString()
        {
            return $"{Matricula.Asignatura.Nombre} | Promedio: {Promedio:0.00} | " +
                   $"Evaluado: {PorcentajeEvaluado}% | Asistencia: {PorcentajeAsistencia:0.0}% | {Estado}";
        }
    }
}
