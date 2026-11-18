using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemadeGestionEscolar.Clases
{
    public class PeriodoAcademico
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public PeriodoAcademico(int id, string nombre, DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaFin <= fechaInicio)
                throw new ArgumentException("La fecha de fin debe ser posterior a la fecha de inicio.");

            Id = id;
            Nombre = nombre;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }

        public override string ToString()
        {
            return $"{Nombre} ({FechaInicio:dd/MM/yyyy} - {FechaFin:dd/MM/yyyy})";
        }
    }
}
