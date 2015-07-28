using System.Web.Mvc;


namespace GEMA.FMK.Security
{
    public class PerfilFiltro : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            //Caso o usuário não for autorizado, ele será enviado para a página /Acesso/Negado 
            if (filterContext.Result is HttpUnauthorizedResult)
                filterContext.HttpContext.Response.Redirect("/Account/Denied");
        }
    }
}
