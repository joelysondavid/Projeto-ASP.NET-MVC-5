using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoCRUD.ViewModel
{
    public class DadosFiltro
    {
        public DadosFiltro(ParametrosPaginacao parametrosPaginacao)
        {
            rowCount = parametrosPaginacao.RowCount;
            current = parametrosPaginacao.Current;
        }
        // estas propriedades serão renderizadas com este nome por isso não está seguindo os padrões
        public dynamic rows { get; set; }
        public int current { get; set; }
        public int rowCount { get; set; }
        public int total { get; set; }
    }
}