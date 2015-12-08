using System;
using System.Web.Mvc;

namespace CompositeUI.Web.Common.Attributes
{
    public class SelectAttribute : Attribute, IMetadataAware
    {
        public bool Multiple { get; set; }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.TemplateHint = Multiple ? "MultiSelect" : "SingleSelect";
        }
    }
}
