namespace VRSimulator.Api.Models;

public sealed record CompleteTrainingRequest(
    int Score,
    int DurationMinutes);

