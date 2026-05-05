namespace Projects.Domain.Exceptions
{
    public class ResourceNotFoundException(string resourceName, object? key) :
        Exception(key is null ?
            $"{resourceName} not found"
            : $"{resourceName} with key {key} was not found.")
    {
    }
}
