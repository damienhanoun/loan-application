using Generator.Equals;

namespace Acquisition.Api.Domain.ValueObjects;

[Equatable]
public class ValueObject<T>
{
    public T Value { get; protected set; } = default!;
}