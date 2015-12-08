using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace CompositeUI.Web.Common.Metadata
{
    public class DataAnnotationsModelMetadataProviderDecorator : DataAnnotationsModelMetadataProvider
    {
        private readonly IEnumerable<IDecorateMetadataForType> _typeDecorators;
        private readonly IEnumerable<IDecorateMetadataForProperty> _propertyDecorators;

        public DataAnnotationsModelMetadataProviderDecorator(IEnumerable<IDecorateMetadataForType> typeDecorators, IEnumerable<IDecorateMetadataForProperty> propertyDecorators)
        {
            _typeDecorators = typeDecorators;
            _propertyDecorators = propertyDecorators;
        }

        protected override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, PropertyDescriptor propertyDescriptor)
        {
            var metadata = base.GetMetadataForProperty(modelAccessor, containerType, propertyDescriptor);

            foreach (var decorator in _propertyDecorators)
                decorator.DecorateMetadata(metadata, containerType, propertyDescriptor);

            return metadata;
        }

        public override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
        {
            var metadata = base.GetMetadataForType(modelAccessor, modelType);

            foreach (var decorator in _typeDecorators)
                decorator.DecorateMetadata(metadata, modelType);

            return metadata;
        }
    }
}