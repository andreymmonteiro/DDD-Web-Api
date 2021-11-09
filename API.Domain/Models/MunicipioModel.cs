using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MunicipioModel : BaseModel
    {
        private string name;
        public string Name 
        {
            get {  return name; }
            set { name = value; }
        }
        private int codigoIbge;
        public int CodigoIbge 
        {
            get { return codigoIbge; }
            set { codigoIbge = value;}
        }
        private Guid ufId;
        public Guid UfId 
        {
            get { return ufId; }
            set {  ufId = value; }
        }
    }
}
