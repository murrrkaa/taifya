namespace Execution;

public interface IEnvironment
{
    decimal ReadNumber();

    void AddResult(decimal result);
}