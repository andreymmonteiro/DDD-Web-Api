using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UfModel : BaseModel
    {
        private string sigla;
        public string Sigla
        {
            get {  return sigla; }
            set { sigla = value; }
        }
        private string name;
        public string Name 
        {
            get {  return name; }
            set {  name = value; }
        }
    }
}
