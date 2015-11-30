using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HomeManager.Web.Common.Validators
{
    public class EnumValidatorProvider : ModelValidatorProvider
    {
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            if (typeof (Enum).IsAssignableFrom(metadata.ModelType))
                yield return new EnumValidator(metadata, context);
        }
    }
}