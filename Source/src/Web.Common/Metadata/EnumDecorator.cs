using System;
using System.ComponentModel;
using System.Web.Mvc;

namespace CompositeUI.Web.Common.Metadata
{
    public class EnumDecorator : IDecorateMetadataForType, IDecorateMetadataForProperty
    {
        public void DecorateMetadata(ModelMetadata modelMetadata, Type modelType)
        {
            if (typeof (Enum).IsAssignableFrom(modelType))
                modelMetadata.TemplateHint = "Enum";
        }

        public void DecorateMetadata(ModelMetadata modelMetadata, Type containerType, PropertyDescriptor propertyDescriptor)
        {
            if (typeof(Enum).IsAssignableFrom(modelMetadata.ModelType))
                modelMetadata.TemplateHint = "Enum";
        }
    }
}