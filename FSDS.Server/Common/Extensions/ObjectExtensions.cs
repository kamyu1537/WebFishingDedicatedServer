﻿namespace FSDS.Server.Common.Extensions;

public static class ObjectExtensions
{
    public static long GetNumber(this object obj)
    {
        return obj switch
        {
            int intValue => intValue,
            long longValue => longValue,
            _ => 0
        };
    }
}