using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemadeGestionEscolar.Clases
{
    public class CalculadorRendimiento
    {
        public const decimal NotaAprobacion = 6m;
        public const decimal AsistenciaMinima = 75m;

        private readonly GestorMatriculas _matriculas;
        private readonly GestorAcademico _academico;

        public CalculadorRendimiento(GestorMatriculas matriculas, GestorAcademico academico)
        {
            _matriculas = matriculas;
            _academico = academico;
        }

        public decimal CalcularPromedio(Matricula matricula)
        {
            var notas = _academico.ObtenerCalificaciones(matricula);
            return notas.Sum(c => c.Nota * c.Porcentaje / 100m);
        }

        public decimal CalcularPorcentajeAsistencia(Matricula matricula)
        {
            var registros = _academico.ObtenerAsistencias(matricula);
            if (registros.Count == 0)
                return 100m;

            int asistidas = registros.Count(a => a.Estado != EstadoAsistencia.Ausente);
            return asistidas * 100m / registros.Count;
        }

        public ResultadoAsignatura EvaluarAsignatura(Matricula matricula)
        {
            decimal promedio = CalcularPromedio(matricula);
            decimal evaluado = _academico.ObtenerCalificaciones(matricula).Sum(c => c.Porcentaje);
            decimal asistencia = CalcularPorcentajeAsistencia(matricula);

            EstadoAcademico estado;
            if (evaluado < 100m)
                estado = EstadoAcademico.EnCurso;
            else if (promedio >= NotaAprobacion && asistencia >= AsistenciaMinima)
                estado = EstadoAcademico.Aprobado;
            else
                estado = EstadoAcademico.Reprobado;

            return new ResultadoAsignatura(matricula, promedio, evaluado, asistencia, estado);
        }

        public ResultadoEstudiante EvaluarEstudiante(Estudiante estudiante, PeriodoAcademico periodo)
        {
            var resultados = _matriculas.ObtenerPorEstudiante(estudiante)
                .Where(m => m.Periodo == periodo)
                .Select(EvaluarAsignatura)
                .ToList();

            decimal promedioGeneral = resultados.Count > 0 ? resultados.Average(r => r.Promedio) : 0m;

            EstadoAcademico estado;
            if (resultados.Count == 0 || resultados.Any(r => r.Estado == EstadoAcademico.EnCurso))
                estado = EstadoAcademico.EnCurso;
            else if (resultados.All(r => r.Estado == EstadoAcademico.Aprobado))
                estado = EstadoAcademico.Aprobado;
            else
                estado = EstadoAcademico.Reprobado;

            return new ResultadoEstudiante(estudiante, periodo, resultados, promedioGeneral, estado);
        }

        public List<ResultadoEstudiante> EvaluarTodos(IEnumerable<Estudiante> estudiantes, PeriodoAcademico periodo)
        {
            return estudiantes.Select(e => EvaluarEstudiante(e, periodo)).ToList();
        }

        public List<ResultadoEstudiante> ObtenerAprobados(IEnumerable<Estudiante> estudiantes, PeriodoAcademico periodo)
        {
            return EvaluarTodos(estudiantes, periodo)
                .Where(r => r.Estado == EstadoAcademico.Aprobado).ToList();
        }

        public List<ResultadoEstudiante> ObtenerReprobados(IEnumerable<Estudiante> estudiantes, PeriodoAcademico periodo)
        {
            return EvaluarTodos(estudiantes, periodo)
                .Where(r => r.Estado == EstadoAcademico.Reprobado).ToList();
        }
    }
}
