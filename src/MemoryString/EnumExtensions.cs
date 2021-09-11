using System;
using System.Runtime.CompilerServices;

namespace MemoryString
{
    internal static class EnumExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasRemoveEmptyEntries(this StringSplitOptions value)
        {
            return (value & StringSplitOptions.RemoveEmptyEntries) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasTrimEntries(this StringSplitOptions value)
        {
#if NET5_0_OR_GREATER
            return (value & StringSplitOptions.TrimEntries) != 0;
#else
            return false;
#endif
        }
    }
}