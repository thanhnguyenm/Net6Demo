using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALTIELTS.Extensions
{
    public static class ModelStateExtensions
    {
        public static List<string> GetErrors(this ModelStateDictionary modelState)
        {
            var errors = modelState.Where(entry => entry.Value.Errors.Any())
                                .Select(entry => entry.Value.Errors).ToList();

            List<string> errorMessages = new List<string>();
            errors.ForEach(entry => {
                errorMessages.AddRange(GetErrors(entry));
            });

            return errorMessages;
        }

        public static string GetError(this ModelStateDictionary modelState)
        {
            var errors = modelState.Where(entry => entry.Value.Errors.Any())
                                .Select(entry => entry.Value.Errors).ToList();

            List<string> errorMessages = new List<string>();
            errors.ForEach(entry => {
                errorMessages.AddRange(GetErrors(entry));
            });

            return string.Join(". ", errorMessages);
        }

        public static List<string> GetErrors(ModelErrorCollection errors)
        {
            return errors.Select(entry => $"{entry.ErrorMessage}").ToList();

        }
    }
}
