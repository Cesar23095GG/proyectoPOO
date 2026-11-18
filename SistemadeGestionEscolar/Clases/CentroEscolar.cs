using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemadeGestionEscolar.Clases
{
    public class CentroEscolar
    {
        public List<Estudiante> Estudiantes { get; } = new List<Estudiante>();
        public List<Docente> Docentes { get; } = new List<Docente>();
        public List<PersonalAdministrativo> Administrativos { get; } = new List<PersonalAdministrativo>();
        public List<Grado> Grados { get; } = new List<Grado>();
        public List<Asignatura> Asignaturas { get; } = new List<Asignatura>();
        public List<PeriodoAcademico> Periodos { get; } = new List<PeriodoAcademico>();

        public GestorMatriculas Matriculas { get; } = new GestorMatriculas();
        public GestorAcademico Academico { get; } = new GestorAcademico();
        public CalculadorRendimiento Rendimiento { get; }

        private int _idPersona = 1;
        private int _idGrado = 1;
        private int _idSeccion = 1;
        private int _idAsignatura = 1;
        private int _idPeriodo = 1;

        public CentroEscolar()
        {
            Rendimiento = new CalculadorRendimiento(Matriculas, Academico);
        }

        private bool DocumentoExiste(string documento)
        {
            return Estudiantes.Cast<Persona>()
                .Concat(Docentes).Concat(Administrativos)
                .Any(p => p.Documento.Equals(documento, StringComparison.OrdinalIgnoreCase));
        }

        public bool RegistrarEstudiante(string nombres, string apellidos, string documento, DateTime nacimiento,
                                        string telefono, string correo, string carnet, out string mensaje)
        {
            if (DocumentoExiste(documento))
            {
                mensaje = "Ya existe una persona registrada con ese documento.";
                return false;
            }
            if (Estudiantes.Any(e => e.Carnet.Equals(carnet, StringComparison.OrdinalIgnoreCase)))
            {
                mensaje = "Ya existe un estudiante con ese carnet.";
                return false;
            }

            Estudiantes.Add(new Estudiante(_idPersona++, nombres, apellidos, documento, nacimiento, telefono, correo, carnet));
            mensaje = "Estudiante registrado correctamente.";
            return true;
        }

        public bool RegistrarDocente(string nombres, string apellidos, string documento, DateTime nacimiento,
                                     string telefono, string correo, string especialidad, DateTime contratacion,
                                     out string mensaje)
        {
            if (DocumentoExiste(documento))
            {
                mensaje = "Ya existe una persona registrada con ese documento.";
                return false;
            }

            Docentes.Add(new Docente(_idPersona++, nombres, apellidos, documento, nacimiento, telefono, correo,
                                     especialidad, contratacion));
            mensaje = "Docente registrado correctamente.";
            return true;
        }

        public bool RegistrarAdministrativo(string nombres, string apellidos, string documento, DateTime nacimiento,
                                            string telefono, string correo, string cargo, string departamento,
                                            out string mensaje)
        {
            if (DocumentoExiste(documento))
            {
                mensaje = "Ya existe una persona registrada con ese documento.";
                return false;
            }

            Administrativos.Add(new PersonalAdministrativo(_idPersona++, nombres, apellidos, documento, nacimiento,
                                                           telefono, correo, cargo, departamento));
            mensaje = "Personal administrativo registrado correctamente.";
            return true;
        }

        public bool RegistrarGrado(string nombre, out string mensaje)
        {
            if (Grados.Any(g => g.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase)))
            {
                mensaje = "Ya existe un grado con ese nombre.";
                return false;
            }

            Grados.Add(new Grado(_idGrado++, nombre));
            mensaje = "Grado registrado correctamente.";
            return true;
        }

        public bool RegistrarSeccion(Grado grado, string nombre, int capacidad, out string mensaje)
        {
            if (capacidad <= 0)
            {
                mensaje = "La capacidad debe ser mayor que cero.";
                return false;
            }

            if (!grado.AgregarSeccion(new Seccion(_idSeccion, nombre, capacidad, grado)))
            {
                mensaje = "Ya existe una sección con ese nombre en el grado.";
                return false;
            }

            _idSeccion++;
            mensaje = "Sección registrada correctamente.";
            return true;
        }

        public bool AsignarSeccion(Estudiante estudiante, Seccion seccion, out string mensaje)
        {
            if (estudiante.Seccion == seccion)
            {
                mensaje = "El estudiante ya pertenece a esa sección.";
                return false;
            }

            if (seccion.Estudiantes.Count >= seccion.Capacidad)
            {
                mensaje = "La sección no tiene cupo disponible.";
                return false;
            }

            estudiante.Seccion?.Estudiantes.Remove(estudiante);
            seccion.AgregarEstudiante(estudiante);
            mensaje = "Estudiante asignado a la sección correctamente.";
            return true;
        }

        public bool RegistrarAsignatura(string codigo, string nombre, Grado grado, out string mensaje)
        {
            if (Asignaturas.Any(a => a.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase)))
            {
                mensaje = "Ya existe una asignatura con ese código.";
                return false;
            }

            Asignaturas.Add(new Asignatura(_idAsignatura++, codigo, nombre, grado));
            mensaje = "Asignatura registrada correctamente.";
            return true;
        }

        public bool RegistrarPeriodo(string nombre, DateTime inicio, DateTime fin, out string mensaje)
        {
            if (fin <= inicio)
            {
                mensaje = "La fecha de fin debe ser posterior a la fecha de inicio.";
                return false;
            }
            if (Periodos.Any(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase)))
            {
                mensaje = "Ya existe un período con ese nombre.";
                return false;
            }

            Periodos.Add(new PeriodoAcademico(_idPeriodo++, nombre, inicio, fin));
            mensaje = "Período registrado correctamente.";
            return true;
        }
    }
}
