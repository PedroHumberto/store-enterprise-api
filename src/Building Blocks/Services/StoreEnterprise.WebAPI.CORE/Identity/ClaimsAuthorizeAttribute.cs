using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace StoreEnterprise.WebAPI.CORE.Identity
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        //Atributo que vai decorar um metodo. Necessario um filtro
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(FilterClaimRequistion))
        {
            //FilterClaimRequistion -> faz a validação do usuario, se está autenticado.
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
}
