namespace HomeManager.Infrastructure
{
    public class ImportantClass
    {
        public OptionalClass OptionalDependency { get; set; }

        public string GenerateText(string value)
        {
            return (OptionalDependency == null ? "No" : "Is") + " optional dependency in " + value;
        }
    }
}
