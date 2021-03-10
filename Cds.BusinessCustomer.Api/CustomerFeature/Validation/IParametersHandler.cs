using System;
using System.Collections.Generic;
using System.Text;

namespace Cds.BusinessCustomer.Api.CustomerFeature.Validation
{
    /// <summary>
    /// Validation of input data
    /// </summary>
    public interface IParametersHandler
    {
        /// <summary>
        /// Validation for parameter : siret
        /// </summary>
        /// <param name="siret"></param>
        /// <returns>(bool, string) => (siret valide ou pas, message d'erreur au cas non valide)</returns>
        public (bool, string) Validate(string siret);
        /// <summary>
        /// Validation for parameters : socialreason and zipcode
        /// </summary>
        /// <param name="socialreason"></param>
        /// <param name="zipcode"></param>
        /// <returns>(bool, string) => (params valides ou pas, message d'erreur au cas non valides)</returns>
        public (bool, string) Validate(string socialreason, string zipcode);

    }
}
