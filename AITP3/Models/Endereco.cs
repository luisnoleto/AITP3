using System;
using System.Collections.Generic;
using System.Web.Services.Description;


namespace AITP3.Models
{
    public class Endereco
    {
        public int ID { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }

    }
}