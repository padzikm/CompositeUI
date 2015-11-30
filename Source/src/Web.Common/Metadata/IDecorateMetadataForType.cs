using System;
using System.Web.Mvc;

namespace HomeManager.Web.Common.Metadata
{
    public interface IDecorateMetadataForType
    {
        void DecorateMetadata(ModelMetadata modelMetadata, Type modelType);
    }
}