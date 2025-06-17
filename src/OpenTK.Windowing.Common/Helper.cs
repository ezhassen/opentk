using System;
#if NET || NETSTANDARD
using System.Runtime.InteropServices;
#endif

namespace OpenTK.Windowing.Common
{
    public static class Helper
    {
        /// <summary>
        /// Converts a UTF-8 encoded byte array pointed to by a pointer to a managed String.
        /// </summary>
        /// <param name="ptr">Pointer to the UTF-8 encoded string.</param>
        /// <returns>A managed string containing the converted bytes, or null if ptr is null.</returns>
        public static unsafe string PtrToStringUTF8(IntPtr ptr)
        {
#if NETFRAMEWORK
            return NETFRAMEWORK.Net4Helpers.PtrToStringUTF8(ptr);
#else
            return Marshal.PtrToStringUTF8(ptr);
#endif
        }

        /// <summary>
        /// Converts a managed String to a UTF-8 encoded byte array and copies it to unmanaged memory.
        /// </summary>
        /// <param name="str">The string to convert.</param>
        /// <returns>A pointer to the allocated and filled memory, or IntPtr.Zero if str is null.</returns>
        public static unsafe IntPtr StringToCoTaskMemUTF8(string str)
        {
#if NETFRAMEWORK
            return NETFRAMEWORK.Net4Helpers.StringToCoTaskMemUTF8(str);
#else
            return Marshal.StringToCoTaskMemUTF8(str);
#endif
        }
    }
}
