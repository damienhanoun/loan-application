namespace Acquisition.Api.Scaffolding;

public interface IEndPoint
{
    string Url { get; }
    Delegate Handler { get; }
}