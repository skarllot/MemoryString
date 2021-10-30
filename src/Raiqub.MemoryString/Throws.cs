using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Validation;

namespace MemoryString
{
    internal static class Throws
    {
        [DoesNotReturn]
        public static void InvalidEnum<T>(string? parameterName, T enumValue)
            where T : struct, Enum
        {
            Ensure32BitEnum<T>();
            throw new InvalidEnumArgumentException(parameterName, Unsafe.As<T, int>(ref enumValue), typeof(T));
        }

        [Conditional("DEBUG")]
        private static void Ensure32BitEnum<T>()
            where T : struct, Enum
        {
            Report.If(Unsafe.SizeOf<T>() != 4);
        }
    }
}