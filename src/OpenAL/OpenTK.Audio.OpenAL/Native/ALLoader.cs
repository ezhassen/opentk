﻿//
// ApiBase.cs
//
// Copyright (C) 2020 OpenTK
//
// This software may be modified and distributed under the terms
// of the MIT license. See the LICENSE file for details.
//

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace OpenTK.Audio.OpenAL
{
    /// <summary>
    /// Provides a base for ApiContext so that it can register dll intercepts.
    /// </summary>
    internal static class ALLoader
    {
        private static readonly OpenALLibraryNameContainer ALLibraryNameContainer = new OpenALLibraryNameContainer();

        private static bool RegisteredResolver = false;

        static ALLoader()
        {
            RegisterDllResolver();
        }

#if NETFRAMEWORK || NETSTANDARD

        internal static void RegisterDllResolver()
        {
            if (RegisteredResolver == false)
            {
                // For .NET Framework, we use AppDomain.CurrentDomain.AssemblyResolve
                AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
                {
                    if (args.Name.StartsWith(typeof(ALLoader).Assembly.GetName().Name))
                    {
                        string libName = ALLibraryNameContainer.GetLibraryName();
                        try
                        {
                            LoadLibrary(libName);
                        }
                        catch (DllNotFoundException)
                        {
                            throw new DllNotFoundException(
                                $"Could not load the dll '{libName}'.");
                        }
                    }
                    return null;
                };
                RegisteredResolver = true;
            }
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);
#endif

#if NET

        internal static void RegisterDllResolver()
        {
            if (RegisteredResolver == false)
            {
                NativeLibrary.SetDllImportResolver(typeof(ALLoader).Assembly, ImportResolver);
                RegisteredResolver = true;
            }
        }

        private static IntPtr ImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            if (libraryName == AL.Lib || libraryName == ALC.Lib)
            {
                string libName = ALLibraryNameContainer.GetLibraryName();

                if (NativeLibrary.TryLoad(libName, assembly, searchPath, out IntPtr libHandle) == false)
                {
                    throw new DllNotFoundException($"Could not load the dll '{libName}' (this load is intercepted, specified in DllImport as '{libraryName}'), this could mean that OpenAL is not installed on this system or that we can't find the dll.");
                }

                return libHandle;
            }
            else
            {
                return NativeLibrary.Load(libraryName, assembly, searchPath);
            }
        }
#endif

    }
}
