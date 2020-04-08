using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiccionarioTablasGUM.Models
{
    public class clsCamposGUM
    {

        public string nombreTabla { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string notas { get; set; }
        public Int16 orden { get; set; }
        public Int16 ordenPk { get; set; }
        public Int16 indIdentity { get; set; }
        public Int16 longitud { get; set; }
        public Int16 presicion { get; set; }
        public Int16 ordenIdentificador { get; set; }
        public Int16 ordenCampoDesc { get; set; }
        public string tipoDatoSql { get; set; }
        public Int16 indNulo { get; set; }
        public Int16 indGumConfigurable { get; set; }
        public Int16 indGumSincronizado { get; set; }
        public Int16 indGumSugerir { get; set; }
        public Int16 indCambioEnDb { get; set; }
        public Int16 indCambio { get; set; }
        public Int16 indEsNuevo { get; set; }
    }
}
