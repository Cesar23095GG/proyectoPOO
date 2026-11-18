using SistemadeGestionEscolar.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemadeGestionEscolar
{
    internal class Program
    {
        private static readonly CentroEscolar Centro = new CentroEscolar();
        private static readonly Reportes Reporte = new Reportes(Centro);

        static void Main(string[] args)
        {
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n===== SISTEMA DE GESTIÓN DE CENTRO ESCOLAR =====");
                Console.WriteLine("1.  Registrar estudiante");
                Console.WriteLine("2.  Registrar docente");
                Console.WriteLine("3.  Administrar grados y secciones");
                Console.WriteLine("4.  Registrar asignatura");
                Console.WriteLine("5.  Inscribir estudiante");
                Console.WriteLine("6.  Asignar docente a asignatura");
                Console.WriteLine("7.  Registrar calificación");
                Console.WriteLine("8.  Calcular promedios");
                Console.WriteLine("9.  Registrar asistencia");
                Console.WriteLine("10. Consultar expediente académico");
                Console.WriteLine("11. Reportes de estudiantes");
                Console.WriteLine("12. Aprobados y reprobados");
                Console.WriteLine("13. Registrar período académico");
                Console.WriteLine("14. Registrar personal administrativo");
                Console.WriteLine("0.  Salir");

                int opcion = LeerEntero("Opción: ");

                switch (opcion)
                {
                    case 1: RegistrarEstudiante(); break;
                    case 2: RegistrarDocente(); break;
                    case 3: MenuGradosSecciones(); break;
                    case 4: RegistrarAsignatura(); break;
                    case 5: InscribirEstudiante(); break;
                    case 6: AsignarDocente(); break;
                    case 7: RegistrarCalificacion(); break;
                    case 8: CalcularPromedios(); break;
                    case 9: RegistrarAsistencia(); break;
                    case 10: ConsultarExpediente(); break;
                    case 11: MenuReportes(); break;
                    case 12: AprobadosReprobados(); break;
                    case 13: RegistrarPeriodo(); break;
                    case 14: RegistrarAdministrativo(); break;
                    case 0: salir = true; break;
                    default: Console.WriteLine("Opción no válida."); break;
                }
            }
        }

        private static void RegistrarEstudiante()
        {
            Console.WriteLine("\n--- Registrar estudiante ---");
            string nombres = LeerTexto("Nombres: ");
            string apellidos = LeerTexto("Apellidos: ");
            string documento = LeerTexto("NIE: ");
            DateTime nacimiento = LeerFecha("Fecha de nacimiento (Dia/Mes/Año): ");
            string telefono = LeerTexto("Teléfono: ");
            string correo = LeerTexto("Correo: ");
            string carnet = LeerTexto("Carnet: ");

            Centro.RegistrarEstudiante(nombres, apellidos, documento, nacimiento, telefono, correo, carnet, out string mensaje);
            Console.WriteLine(mensaje);

            if (Centro.Grados.Any(g => g.Secciones.Count > 0))
            {
                var estudiante = Centro.Estudiantes.LastOrDefault(e => e.Carnet == carnet);
                if (estudiante != null && estudiante.Seccion == null)
                {
                    Console.WriteLine("Seleccione la sección del estudiante:");
                    var seccion = SeleccionarSeccion();
                    if (seccion != null)
                    {
                        Centro.AsignarSeccion(estudiante, seccion, out string msg);
                        Console.WriteLine(msg);
                    }
                }
            }
            else
            {
                Console.WriteLine("Aún no hay secciones; asigne grado y sección desde la opción 3.");
            }
        }

        private static void RegistrarDocente()
        {
            Console.WriteLine("\n--- Registrar docente ---");
            string nombres = LeerTexto("Nombres: ");
            string apellidos = LeerTexto("Apellidos: ");
            string documento = LeerTexto("DUI: ");
            DateTime nacimiento = LeerFecha("Fecha de nacimiento (Dia/Mes/Año): ");
            string telefono = LeerTexto("Teléfono: ");
            string correo = LeerTexto("Correo: ");
            string especialidad = LeerTexto("Especialidad: ");
            DateTime contratacion = LeerFecha("Fecha de contratación (Dia/Mes/Año): ");

            Centro.RegistrarDocente(nombres, apellidos, documento, nacimiento, telefono, correo,
                                    especialidad, contratacion, out string mensaje);
            Console.WriteLine(mensaje);
        }

        private static void RegistrarAdministrativo()
        {
            Console.WriteLine("\n--- Registrar personal administrativo ---");
            string nombres = LeerTexto("Nombres: ");
            string apellidos = LeerTexto("Apellidos: ");
            string documento = LeerTexto("DUI: ");
            DateTime nacimiento = LeerFecha("Fecha de nacimiento (Dia/Mes/Año): ");
            string telefono = LeerTexto("Teléfono: ");
            string correo = LeerTexto("Correo: ");
            string cargo = LeerTexto("Cargo: ");
            string departamento = LeerTexto("Departamento: ");

            Centro.RegistrarAdministrativo(nombres, apellidos, documento, nacimiento, telefono, correo,
                                           cargo, departamento, out string mensaje);
            Console.WriteLine(mensaje);
        }

        private static void RegistrarPeriodo()
        {
            Console.WriteLine("\n--- Registrar período académico ---");
            string nombre = LeerTexto("Nombre (ej. Período 1 - 2026): ");
            DateTime inicio = LeerFecha("Fecha de inicio (Dia/Mes/Año): ");
            DateTime fin = LeerFecha("Fecha de fin (Dia/Mes/Año): ");

            Centro.RegistrarPeriodo(nombre, inicio, fin, out string mensaje);
            Console.WriteLine(mensaje);
        }

        private static void RegistrarAsignatura()
        {
            Console.WriteLine("\n--- Registrar asignatura ---");
            var grado = Seleccionar(Centro.Grados, "Grado de la asignatura:");
            if (grado == null) return;

            string codigo = LeerTexto("Código: ");
            string nombre = LeerTexto("Nombre: ");

            Centro.RegistrarAsignatura(codigo, nombre, grado, out string mensaje);
            Console.WriteLine(mensaje);
        }

        private static void MenuGradosSecciones()
        {
            Console.WriteLine("\n--- Grados y secciones ---");
            Console.WriteLine("1. Agregar grado");
            Console.WriteLine("2. Agregar sección a un grado");
            Console.WriteLine("3. Asignar estudiante a sección");
            Console.WriteLine("4. Ver grados y secciones");
            int opcion = LeerEntero("Opción: ");

            switch (opcion)
            {
                case 1:
                    Centro.RegistrarGrado(LeerTexto("Nombre del grado: "), out string m1);
                    Console.WriteLine(m1);
                    break;
                case 2:
                    var grado = Seleccionar(Centro.Grados, "Grado:");
                    if (grado == null) return;
                    string nombre = LeerTexto("Nombre de la sección: ");
                    int capacidad = LeerEntero("Capacidad: ");
                    Centro.RegistrarSeccion(grado, nombre, capacidad, out string m2);
                    Console.WriteLine(m2);
                    break;
                case 3:
                    var estudiante = Seleccionar(Centro.Estudiantes, "Estudiante:");
                    if (estudiante == null) return;
                    var seccion = SeleccionarSeccion();
                    if (seccion == null) return;
                    Centro.AsignarSeccion(estudiante, seccion, out string m3);
                    Console.WriteLine(m3);
                    break;
                case 4:
                    Reporte.EstudiantesPorSeccion();
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }

        private static void InscribirEstudiante()
        {
            Console.WriteLine("\n--- Inscribir estudiante ---");
            var estudiante = Seleccionar(Centro.Estudiantes, "Estudiante:");
            if (estudiante == null) return;
            var asignatura = Seleccionar(Centro.Asignaturas, "Asignatura:");
            if (asignatura == null) return;
            var periodo = Seleccionar(Centro.Periodos, "Período:");
            if (periodo == null) return;

            Centro.Matriculas.Inscribir(estudiante, asignatura, periodo, out string mensaje);
            Console.WriteLine(mensaje);
        }

        private static void AsignarDocente()
        {
            Console.WriteLine("\n--- Asignar docente a asignatura ---");
            var asignatura = Seleccionar(Centro.Asignaturas, "Asignatura:");
            if (asignatura == null) return;
            var docente = Seleccionar(Centro.Docentes, "Docente:");
            if (docente == null) return;

            asignatura.AsignarDocente(docente);
            Console.WriteLine("Docente asignado correctamente.");
        }

        private static void RegistrarCalificacion()
        {
            Console.WriteLine("\n--- Registrar calificación ---");
            var matricula = SeleccionarMatricula();
            if (matricula == null) return;

            string evaluacion = LeerTexto("Evaluación (ej. Examen 1): ");
            decimal porcentaje = LeerDecimal("Porcentaje de la evaluación (1-100): ");
            decimal nota = LeerDecimal($"Nota ({Calificacion.NotaMinima}-{Calificacion.NotaMaxima}): ");

            Centro.Academico.RegistrarCalificacion(matricula, evaluacion, porcentaje, nota, out string mensaje);
            Console.WriteLine(mensaje);
        }

        private static void RegistrarAsistencia()
        {
            Console.WriteLine("\n--- Registrar asistencia ---");
            var matricula = SeleccionarMatricula();
            if (matricula == null) return;

            DateTime fecha = LeerFecha("Fecha (Dia/Mes/Año): ");
            Console.WriteLine("Estado: 1. Presente  2. Ausente  3. Justificado  4. Tardanza");
            int opcion = LeerEntero("Opción: ");

            if (opcion < 1 || opcion > 4)
            {
                Console.WriteLine("Estado no válido.");
                return;
            }

            var estado = (EstadoAsistencia)(opcion - 1);
            Centro.Academico.RegistrarAsistencia(matricula, fecha, estado, out string mensaje);
            Console.WriteLine(mensaje);
        }

        private static void CalcularPromedios()
        {
            Console.WriteLine("\n--- Calcular promedios ---");
            var estudiante = Seleccionar(Centro.Estudiantes, "Estudiante:");
            if (estudiante == null) return;
            var periodo = Seleccionar(Centro.Periodos, "Período:");
            if (periodo == null) return;

            var resultado = Centro.Rendimiento.EvaluarEstudiante(estudiante, periodo);
            if (resultado.Asignaturas.Count == 0)
            {
                Console.WriteLine("El estudiante no tiene inscripciones en ese período.");
                return;
            }

            Console.WriteLine();
            foreach (var a in resultado.Asignaturas)
                Console.WriteLine(a);
            Console.WriteLine($"\nPromedio general: {resultado.PromedioGeneral:0.00} | Estado: {resultado.Estado}");
        }

        private static void ConsultarExpediente()
        {
            var estudiante = Seleccionar(Centro.Estudiantes, "Estudiante:");
            if (estudiante == null) return;
            Reporte.Expediente(estudiante);
        }

        private static void MenuReportes()
        {
            Console.WriteLine("\n--- Reportes de estudiantes ---");
            Console.WriteLine("1. Listado general de estudiantes");
            Console.WriteLine("2. Estudiantes por grado y sección");
            Console.WriteLine("3. Rendimiento por período");
            int opcion = LeerEntero("Opción: ");

            switch (opcion)
            {
                case 1: Reporte.ListadoEstudiantes(); break;
                case 2: Reporte.EstudiantesPorSeccion(); break;
                case 3:
                    var periodo = Seleccionar(Centro.Periodos, "Período:");
                    if (periodo != null) Reporte.RendimientoPeriodo(periodo);
                    break;
                default: Console.WriteLine("Opción no válida."); break;
            }
        }

        private static void AprobadosReprobados()
        {
            var periodo = Seleccionar(Centro.Periodos, "Período:");
            if (periodo == null) return;
            Reporte.AprobadosYReprobados(periodo);
        }

        private static T Seleccionar<T>(IList<T> lista, string titulo) where T : class
        {
            if (lista.Count == 0)
            {
                Console.WriteLine("No hay registros disponibles. Registre primero los datos necesarios.");
                return null;
            }

            Console.WriteLine(titulo);
            for (int i = 0; i < lista.Count; i++)
                Console.WriteLine($"  {i + 1}. {lista[i]}");

            int indice = LeerEntero("Seleccione (0 para cancelar): ");
            if (indice < 1 || indice > lista.Count)
                return null;

            return lista[indice - 1];
        }

        private static Seccion SeleccionarSeccion()
        {
            var grado = Seleccionar(Centro.Grados, "Grado:");
            if (grado == null) return null;
            return Seleccionar(grado.Secciones, "Sección:");
        }

        private static Matricula SeleccionarMatricula()
        {
            var estudiante = Seleccionar(Centro.Estudiantes, "Estudiante:");
            if (estudiante == null) return null;

            var matriculas = Centro.Matriculas.ObtenerPorEstudiante(estudiante);
            if (matriculas.Count == 0)
            {
                Console.WriteLine("El estudiante no tiene inscripciones.");
                return null;
            }

            return Seleccionar(matriculas, "Inscripción:");
        }

        private static string LeerTexto(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                string texto = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(texto))
                    return texto;
                Console.WriteLine("El valor no puede estar vacío.");
            }
        }

        private static int LeerEntero(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                if (int.TryParse(Console.ReadLine(), out int valor))
                    return valor;
                Console.WriteLine("Ingrese un número entero válido.");
            }
        }

        private static decimal LeerDecimal(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                if (decimal.TryParse(Console.ReadLine(), out decimal valor))
                    return valor;
                Console.WriteLine("Ingrese un número válido.");
            }
        }

        private static DateTime LeerFecha(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                if (DateTime.TryParseExact(Console.ReadLine(), "Dia/Mes/Año",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime fecha))
                    return fecha;
                Console.WriteLine("Formato inválido. Use Dia/Mes/Año.");
            }
        }
    }
}

//ENUMS
public enum EstadoAsistencia
{
    Presente,
    Ausente,
    Justificado,
    Tardanza
}

public enum EstadoAcademico
{
    EnCurso,
    Aprobado,
    Reprobado
}