using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EmpresaNexer.Extensions
{
    public static class ModelStateExtemsion
    {
        public static List<string> GetErrors(this ModelStateDictionary modelstate)
        {
            var result = new List<string>();
            foreach (var item in modelstate.Values)
            {
                foreach (var error in item.Errors)
                {
                    result.Add(error.ErrorMessage);
                }
            }
            return result;
        }
    }
}