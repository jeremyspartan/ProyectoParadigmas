using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParadigmas.Clases.Simbolos
{
    internal class TipoSimbolo : Simbolo
    {
        public static readonly TipoSimbolo Error = new TipoSimbolo("?");
        public static readonly TipoSimbolo Bool = new TipoSimbolo("bool");
        public static readonly TipoSimbolo Int = new TipoSimbolo("int");
        public static readonly TipoSimbolo String = new TipoSimbolo("string");
        
        private TipoSimbolo(string nombre):base(nombre){}

        public override ClaseSimbolo Clase => ClaseSimbolo.TIPO;
    }
   
}
