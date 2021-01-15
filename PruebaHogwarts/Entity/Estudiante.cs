using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaHogwarts.Entity
{
    public class Estudiante
    {
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public double identificacion { get; set; }
        public int edad { get; set; }
        public string casa { get; set; }


        public static string validate(Estudiante estudiante)
        {
            if (estudiante.nombre == "" || estudiante.nombre == null)
                return "Nombre inválido.";
            else if (estudiante.nombre.Length > 20)
                return "Nombre muy largo, máximo 20 caracteres.";

            if (estudiante.apellidos == "" || estudiante.apellidos == null)
                return "Apellidos inválidos.";
            else if (estudiante.apellidos.Length > 20)
                return "Apellidos muy largo, máximo 20 caracteres.";

            if (estudiante.identificacion == 0)
                return "Identificación inválida.";
            else if (estudiante.identificacion.ToString().Length > 10)
                return "Identificación muy larga, máximo 10 dígitos.";

            if (estudiante.edad == 0)
                return "Edad inválida.";
            else if (estudiante.edad.ToString().Length > 2)
                return "Edad muy larga, máximo 2 dígitos.";

            if (estudiante.casa == "" || estudiante.casa == null)
                return "Casa inválida.";
            else
            {
                string[] casas = { "Gryffindor", "Hufflepuff", "Ravenclaw", "Slytherin" };
                if(!Array.Exists(casas, element => element == estudiante.casa))
                    return "Casa inválida, sólo se perimiten lo siguientes valores Gryffindor, Hufflepuff, Ravenclaw, Slytherin.";
            }

            return "";
        }

    }
}
