using Genbox.SimpleS3.Cli.Core.Enums;

namespace Genbox.SimpleS3.Cli.Core.Results;

public struct MoveResult
{
    public MoveResult(ActionType actionType, OperationStatus operationStatus, string source, string destination)
    {
        ActionType = actionType;
        OperationStatus = operationStatus;
        Source = source;
        Destination = destination;
    }

    public ActionType ActionType { get; }
    public OperationStatus OperationStatus { get; internal set; }
    public string Source { get; }
    public string Destination { get; }
}