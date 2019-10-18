using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace DemoCRUD.ViewModel
{
    public class ParametrosPaginacao
    {
        public ParametrosPaginacao(NameValueCollection dados)
        {
            // obterá: sort[Titulo] || sort[Autor] || sort[AnoEdicao] || sort[Valor]
            string chav = dados.AllKeys.Where(k => k.StartsWith("sort")).FirstOrDefault();
            // variavel que recebe a ordenação do formulario
            string ordenacao = dados[chav];
            string campo = chav.Replace("sort[", string.Empty).Replace("]", string.Empty);

            CampoOrdenado = String.Format("{0} {1}", campo, ordenacao);
            Current = int.Parse(dados["current"]);
            RowCount = int.Parse(dados["rowCount"]);
            SearchPhrase = dados["searchPhrase"];
            // Id = int.Parse(dados["id"]);
        }
        public int Current { get; set; }
        public int RowCount { get; set; }
        public string SearchPhrase { get; set; }
        // public int Id{ get; set; }
        public string CampoOrdenado { get; set; }
    }
}