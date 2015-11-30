using System;
using System.ComponentModel;
using System.Web.Mvc;

namespace HomeManager.Web.Common.Metadata
{
    public interface IDecorateMetadataForProperty
    {
        void DecorateMetadata(ModelMetadata modelMetadata, Type containerType, PropertyDescriptor propertyDescriptor);
    }
}