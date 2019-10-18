using DemoCRUD.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoCRUD.Infra
{
    public class ParametrosPaginacaoModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;

            ParametrosPaginacao paramPaginacao = new ParametrosPaginacao(request.Form);

            return paramPaginacao;
            /*
            int current = int.Parse(request.Form["current"]);
            int rowCount = int.Parse(request.Form["rowCount"]);
            string searchPhrase = request.Form["searchPhrase"];*/
        }
    }
}