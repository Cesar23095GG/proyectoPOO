using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemadeGestionEscolar.Clases
{
    public class GestorMatriculas
    {
        private readonly List<Matricula> _matriculas = new List<Matricula>();
        private int _siguienteId = 1;

        public IReadOnlyList<Matricula> Matriculas => _matriculas;

        public bool ExisteMatricula(Estudiante estudiante, Asignatura asignatura, PeriodoAcademico periodo)
        {
            return _matriculas.Any(m => m.Estudiante == estudiante
                                     && m.Asignatura == asignatura
                                     && m.Periodo == periodo);
        }

        public bool Inscribir(Estudiante estudiante, Asignatura asignatura, PeriodoAcademico periodo, out string mensaje)
        {
            if (estudiante.Seccion == null)
            {
                mensaje = "El estudiante debe estar asociado a un grado y sección.";
                return false;
            }

            if (asignatura.DocenteResponsable == null)
            {
                mensaje = "La asignatura no tiene docente responsable.";
                return false;
            }

            if (ExisteMatricula(estudiante, asignatura, periodo))
            {
                mensaje = "El estudiante ya está inscrito en esta asignatura en este período.";
                return false;
            }

            _matriculas.Add(new Matricula(_siguienteId++, estudiante, asignatura, periodo));
            mensaje = "Inscripción realizada correctamente.";
            return true;
        }

        public List<Matricula> ObtenerPorEstudiante(Estudiante estudiante)
        {
            return _matriculas.Where(m => m.Estudiante == estudiante).ToList();
        }
    }
}
