namespace EMS.Domain.Enums
{
    public sealed class PermissionDefinition
    {
        public string Key { get; init; } = string.Empty;
        public string Group { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
    }
}
