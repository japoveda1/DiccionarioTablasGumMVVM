using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiccionarioTablasGUM.Models
{
    public class TablasGUM
    {
        public string nombre { get; set; }
		public string descripcion { get; set; }
		public string notas { get; set; }
		public Int16 indProcesoGum { get; set; }
		public Int16 indCambio { get; set; }
		public Int16 indCambioEnDB { get; set; }
		public Int16 IndEsNuevo { get; set; }
		

	}
}
