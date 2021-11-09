using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CepModel : BaseModel
    {
        private string cep;
        public string Cep {  
            get { return cep;} 
            set { cep=value;}
        }
        private string logradouro;
        public string Logradouro 
        {
            get { return logradouro;}
            set {  logradouro=value;}
        }
        private string numero;
        public string Numero 
        {
            get {  return numero;}
            set { numero= string.IsNullOrEmpty(value) ? "S/N" : value;}
        }
    }
}
