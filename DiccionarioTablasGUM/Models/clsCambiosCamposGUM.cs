using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiccionarioTablasGUM.Models
{
    public class clsCambiosCamposGUM
    {
        public string nombreTabla { get; set; }
        public string campoModificados { get; set; }
        public string valorAnterior { get; set; }
        public string valorNuevo { get; set; }
        public string propiedad { get; set; }

    }
}
