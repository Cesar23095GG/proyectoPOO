using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemadeGestionEscolar.Clases
{
    public class Calificacion
    {
        public const decimal NotaMinima = 0m;
        public const decimal NotaMaxima = 10m;

        public int Id { get; set; }
        public Matricula Matricula { get; }
        public string Evaluacion { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal Nota { get; private set; }
        public DateTime Fecha { get; }

        public Calificacion(int id, Matricula matricula, string evaluacion, decimal porcentaje, decimal nota)
        {
            if (!EsNotaValida(nota))
                throw new ArgumentOutOfRangeException(nameof(nota),
                    $"La nota debe estar entre {NotaMinima} y {NotaMaxima}.");

            if (porcentaje <= 0 || porcentaje > 100)
                throw new ArgumentOutOfRangeException(nameof(porcentaje),
                    "El porcentaje debe ser mayor que 0 y menor o igual a 100.");

            Id = id;
            Matricula = matricula;
            Evaluacion = evaluacion;
            Porcentaje = porcentaje;
            Nota = nota;
            Fecha = DateTime.Now;
        }

        public static bool EsNotaValida(decimal nota)
        {
            return nota >= NotaMinima && nota <= NotaMaxima;
        }

        public override string ToString()
        {
            return $"{Matricula.Asignatura.Nombre} | {Evaluacion} ({Porcentaje}%): {Nota:0.00}";
        }
    }
}
