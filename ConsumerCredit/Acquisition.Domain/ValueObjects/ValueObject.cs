using Generator.Equals;

namespace Acquisition.Domain.ValueObjects;

[Equatable]
public class ValueObject<T>
{
    public T Value { get; protected set; } = default!;
}