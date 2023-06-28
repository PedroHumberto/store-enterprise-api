using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreEnterprise.WebAPI.CORE.Identity
{
    public class CustomAuthorization
    {
        //a partir do contexto da requisição e com base no valor e nome da claim primeiro valida o usuario, depois verifica se ele possui realmente uma clam passada e se contem o valor esperado.
        public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                   context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }

    }
}
