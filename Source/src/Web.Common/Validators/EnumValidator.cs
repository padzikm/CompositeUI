using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CompositeUI.Web.Common.Validators
{
    public class EnumValidator : ModelValidator
    {
        public EnumValidator(ModelMetadata metadata, ControllerContext controllerContext)
            : base(metadata, controllerContext)
        {
        }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            if (!Enum.IsDefined(Metadata.ModelType, Metadata.Model))
                yield return new ModelValidationResult();
        }
    }
}