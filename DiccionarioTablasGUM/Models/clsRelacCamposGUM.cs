using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiccionarioTablasGUM.Models
{
    public class clsRelacCamposGUM
    {
        public string nombreTablaRef { get; set; }
        public string nombreTabla { get; set; }
        public string nombreRelacion { get; set; }
        public string nombreCampo { get; set; }
        public string nombreCampoRef { get; set; }
        public Int16 indInner { get; set; }
        public Int16 indOrden { get; set; }
        public Int16 indCreado { get; set; }
    }
}
