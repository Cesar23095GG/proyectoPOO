using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemadeGestionEscolar.Clases
{
    public class GestorAcademico
    {
        private readonly List<Calificacion> _calificaciones = new List<Calificacion>();
        private readonly List<Asistencia> _asistencias = new List<Asistencia>();
        private int _siguienteIdCalificacion = 1;
        private int _siguienteIdAsistencia = 1;

        public IReadOnlyList<Calificacion> Calificaciones => _calificaciones;
        public IReadOnlyList<Asistencia> Asistencias => _asistencias;

        public bool RegistrarCalificacion(Matricula matricula, string evaluacion, decimal porcentaje,
                                          decimal nota, out string mensaje)
        {
            if (!Calificacion.EsNotaValida(nota))
            {
                mensaje = $"La nota debe estar entre {Calificacion.NotaMinima} y {Calificacion.NotaMaxima}.";
                return false;
            }

            if (porcentaje <= 0 || porcentaje > 100)
            {
                mensaje = "El porcentaje debe ser mayor que 0 y menor o igual a 100.";
                return false;
            }

            decimal acumulado = _calificaciones
                .Where(c => c.Matricula == matricula)
                .Sum(c => c.Porcentaje);

            if (acumulado + porcentaje > 100)
            {
                mensaje = $"Los porcentajes superan el 100% (acumulado actual: {acumulado}%).";
                return false;
            }

            _calificaciones.Add(new Calificacion(_siguienteIdCalificacion++, matricula, evaluacion, porcentaje, nota));
            mensaje = "Calificación registrada correctamente.";
            return true;
        }

        public bool RegistrarAsistencia(Matricula matricula, DateTime fecha, EstadoAsistencia estado, out string mensaje)
        {
            if (_asistencias.Any(a => a.Matricula == matricula && a.Fecha == fecha.Date))
            {
                mensaje = "Ya existe un registro de asistencia para ese estudiante en esa fecha.";
                return false;
            }

            _asistencias.Add(new Asistencia(_siguienteIdAsistencia++, matricula, fecha, estado));
            mensaje = "Asistencia registrada correctamente.";
            return true;
        }

        public List<Calificacion> ObtenerCalificaciones(Matricula matricula)
        {
            return _calificaciones.Where(c => c.Matricula == matricula).ToList();
        }

        public List<Asistencia> ObtenerAsistencias(Matricula matricula)
        {
            return _asistencias.Where(a => a.Matricula == matricula).ToList();
        }
    }
}
