using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemadeGestionEscolar.Clases
{
    public class Reportes
    {
        private readonly CentroEscolar _centro;

        public Reportes(CentroEscolar centro)
        {
            _centro = centro;
        }

        public void ListadoEstudiantes()
        {
            Console.WriteLine("\n=== LISTADO GENERAL DE ESTUDIANTES ===");
            if (_centro.Estudiantes.Count == 0)
            {
                Console.WriteLine("No hay estudiantes registrados.");
                return;
            }

            foreach (var e in _centro.Estudiantes.OrderBy(x => x.Apellidos))
            {
                string ubicacion = e.Seccion != null ? e.Seccion.ToString() : "Sin sección";
                Console.WriteLine($"{e.Carnet} | {e.NombreCompleto} | {ubicacion}");
            }
            Console.WriteLine($"Total: {_centro.Estudiantes.Count}");
        }

        public void EstudiantesPorSeccion()
        {
            Console.WriteLine("\n=== ESTUDIANTES POR GRADO Y SECCIÓN ===");
            bool hayDatos = false;

            foreach (var grado in _centro.Grados)
            {
                foreach (var seccion in grado.Secciones)
                {
                    hayDatos = true;
                    Console.WriteLine($"\n{seccion} (Cupo: {seccion.Estudiantes.Count}/{seccion.Capacidad})");
                    if (seccion.Estudiantes.Count == 0)
                        Console.WriteLine("   (sin estudiantes)");
                    foreach (var e in seccion.Estudiantes.OrderBy(x => x.Apellidos))
                        Console.WriteLine($"   - {e.Carnet} | {e.NombreCompleto}");
                }
            }

            if (!hayDatos)
                Console.WriteLine("No hay secciones registradas.");
        }

        public void RendimientoPeriodo(PeriodoAcademico periodo)
        {
            Console.WriteLine($"\n=== RENDIMIENTO - {periodo} ===");
            var resultados = _centro.Rendimiento.EvaluarTodos(_centro.Estudiantes, periodo)
                .Where(r => r.Asignaturas.Count > 0)
                .OrderByDescending(r => r.PromedioGeneral)
                .ToList();

            if (resultados.Count == 0)
            {
                Console.WriteLine("No hay estudiantes inscritos en este período.");
                return;
            }

            foreach (var r in resultados)
                Console.WriteLine(r);
        }

        public void AprobadosYReprobados(PeriodoAcademico periodo)
        {
            Console.WriteLine($"\n=== APROBADOS Y REPROBADOS - {periodo} ===");

            var aprobados = _centro.Rendimiento.ObtenerAprobados(_centro.Estudiantes, periodo);
            var reprobados = _centro.Rendimiento.ObtenerReprobados(_centro.Estudiantes, periodo);

            Console.WriteLine($"\nAPROBADOS ({aprobados.Count})");
            foreach (var r in aprobados)
                Console.WriteLine($"   {r}");

            Console.WriteLine($"\nREPROBADOS ({reprobados.Count})");
            foreach (var r in reprobados)
            {
                Console.WriteLine($"   {r}");
                foreach (var a in r.Asignaturas.Where(x => x.Estado == EstadoAcademico.Reprobado))
                    Console.WriteLine($"      * {a}");
            }
        }

        public void Expediente(Estudiante estudiante)
        {
            Console.WriteLine("\n=== EXPEDIENTE ACADÉMICO ===");
            Console.WriteLine($"Estudiante : {estudiante.NombreCompleto}");
            Console.WriteLine($"Carnet     : {estudiante.Carnet}");
            Console.WriteLine($"Documento  : {estudiante.Documento}");
            Console.WriteLine($"Nacimiento : {estudiante.FechaNacimiento:dd/MM/yyyy}");
            Console.WriteLine($"Contacto   : {estudiante.Telefono} | {estudiante.Correo}");
            Console.WriteLine($"Grado/Secc.: {(estudiante.Seccion != null ? estudiante.Seccion.ToString() : "Sin asignar")}");

            var matriculas = _centro.Matriculas.ObtenerPorEstudiante(estudiante);
            if (matriculas.Count == 0)
            {
                Console.WriteLine("\nEl estudiante no tiene inscripciones.");
                return;
            }

            foreach (var periodo in matriculas.Select(m => m.Periodo).Distinct())
            {
                var resultado = _centro.Rendimiento.EvaluarEstudiante(estudiante, periodo);
                Console.WriteLine($"\n--- {periodo} ---");

                foreach (var asig in resultado.Asignaturas)
                {
                    Console.WriteLine(asig);
                    foreach (var c in _centro.Academico.ObtenerCalificaciones(asig.Matricula))
                        Console.WriteLine($"      Nota: {c.Evaluacion} ({c.Porcentaje}%) = {c.Nota:0.00}");

                    var asistencias = _centro.Academico.ObtenerAsistencias(asig.Matricula);
                    Console.WriteLine($"      Asistencia: {asistencias.Count(a => a.Estado == EstadoAsistencia.Presente)} presente(s), " +
                                      $"{asistencias.Count(a => a.Estado == EstadoAsistencia.Ausente)} ausente(s), " +
                                      $"{asistencias.Count(a => a.Estado == EstadoAsistencia.Justificado)} justificado(s), " +
                                      $"{asistencias.Count(a => a.Estado == EstadoAsistencia.Tardanza)} tardanza(s)");
                }

                Console.WriteLine($"Promedio general: {resultado.PromedioGeneral:0.00} | Estado: {resultado.Estado}");
            }
        }
    }
}
