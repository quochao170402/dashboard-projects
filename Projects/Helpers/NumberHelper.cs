using Projects.Enums;

namespace Projects.Helpers;

public static class NumberHelper
{
    public static ProjectStatus CastToProjectStatus(this int number)
    {
        if (number is < (int)ProjectStatus.NotStarted or > (int)ProjectStatus.Cancelled)
            throw new InvalidCastException($"Status: {number} invalid");
        return (ProjectStatus)number;
    }
}
