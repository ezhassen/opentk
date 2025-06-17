#if NETFRAMEWORK

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenTK.Windowing.Common.NETFRAMEWORK
{
    public static unsafe class Net4Helpers
    {
        /// <summary>
        /// Converts a UTF-8 encoded byte array pointed to by a pointer to a managed String.
        /// </summary>
        /// <param name="ptr">Pointer to the UTF-8 encoded string.</param>
        /// <returns>A managed string containing the converted bytes, or null if ptr is null.</returns>
        public static unsafe string PtrToStringUTF8(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
            {
                return null;
            }

            int length = 0;
            while (Marshal.ReadByte(ptr, length) != 0)
            {
                length++;
            }

            byte[] buffer = new byte[length];
            Marshal.Copy(ptr, buffer, 0, length);
            return Encoding.UTF8.GetString(buffer);
        }

        /// <summary>
        /// Converts a managed String to a UTF-8 encoded byte array and copies it to unmanaged memory.
        /// </summary>
        /// <param name="str">The string to convert.</param>
        /// <returns>A pointer to the allocated and filled memory, or IntPtr.Zero if str is null.</returns>
        public static unsafe IntPtr StringToCoTaskMemUTF8(string str)
        {
            if (str == null)
            {
                return IntPtr.Zero;
            }

            byte[] bytes = Encoding.UTF8.GetBytes(str);
            IntPtr ptr = Marshal.AllocCoTaskMem(bytes.Length + 1); // +1 for null terminator
            Marshal.Copy(bytes, 0, ptr, bytes.Length);
            Marshal.WriteByte(ptr, bytes.Length, 0); // Add null terminator
            return ptr;
        }
    }
}

#endif
