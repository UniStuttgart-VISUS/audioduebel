// <copyright file="BmdSwitcherExtensions.cs" company="Visualisierungsinstitut der Universität Stuttgart">
// Copyright © 2025 Visualisierungsinstitut der Universität Stuttgart.
// Licensed under the MIT licence. See LICENCE file for details.
// </copyright>
// <author>Christoph Müller</author>

using BMDSwitcherAPI;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace Visus.AudioDübel {

    /// <summary>
    /// Extension methods for the <see cref="IBMDSwitcher"/> interface.
    /// </summary>
    internal static class BmdSwitcherExtensions {

        /// <summary>
        /// Creates an iterator of the specified type via its annotated IID.
        /// </summary>
        /// <typeparam name="TIterator"></typeparam>
        /// <param name="that"></param>
        /// <returns></returns>
        public static TIterator? CreateIterator<TIterator>(
                this IBMDSwitcher? that)
                where TIterator : class {
            if (that is null) {
                Debug.WriteLine("Switcher is invalid.");
                return null;
            }

            Guid iid = typeof(TIterator).GUID;
            that.CreateIterator(ref iid, out var unknown);
            if (unknown == nint.Zero) {
                Debug.WriteLine("Could not retrieve iterator.");
                return null;
            }

            return Marshal.GetObjectForIUnknown(unknown) as TIterator;
        }

        /// <summary>
        /// Creates an iterator of the specified type via its annotated IID.
        /// </summary>
        /// <typeparam name="TIterator"></typeparam>
        /// <param name="that"></param>
        /// <returns></returns>
        public static TIterator? CreateIterator<TIterator>(
                this IBMDSwitcherAudioMixer? that)
                where TIterator : class {
            if (that is null) {
                Debug.WriteLine("Mixer is invalid.");
                return null;
            }

            Guid iid = typeof(TIterator).GUID;
            that.CreateIterator(ref iid, out var unknown);
            if (unknown == nint.Zero) {
                Debug.WriteLine("Could not retrieve iterator.");
                return null;
            }

            return Marshal.GetObjectForIUnknown(unknown) as TIterator;
        }

        /// <summary>
        /// Creates an iterator of the specified type via its annotated IID.
        /// </summary>
        /// <typeparam name="TIterator"></typeparam>
        /// <param name="that"></param>
        /// <returns></returns>
        public static TIterator? CreateIterator<TIterator>(
                this IBMDSwitcherFairlightAudioMixer? that)
                where TIterator : class {
            if (that is null) {
                Debug.WriteLine("Mixer is invalid.");
                return null;
            }

            Guid iid = typeof(TIterator).GUID;
            that.CreateIterator(ref iid, out var unknown);
            if (unknown == nint.Zero) {
                Debug.WriteLine("Could not retrieve iterator.");
                return null;
            }

            return Marshal.GetObjectForIUnknown(unknown) as TIterator;
        }

        /// <summary>
        /// Creates an iterator of the specified type via its annotated IID.
        /// </summary>
        /// <typeparam name="TIterator"></typeparam>
        /// <param name="that"></param>
        /// <returns></returns>
        public static TIterator? CreateIterator<TIterator>(
                this IBMDSwitcherFairlightAudioInput? that)
                where TIterator : class {
            if (that is null) {
                Debug.WriteLine("Input is invalid.");
                return null;
            }

            Guid iid = typeof(TIterator).GUID;
            that.CreateIterator(ref iid, out var unknown);
            if (unknown == nint.Zero) {
                Debug.WriteLine("Could not retrieve iterator.");
                return null;
            }

            return Marshal.GetObjectForIUnknown(unknown) as TIterator;
        }

        /// <summary>
        /// Creates an iterator of the specified type via its annotated IID.
        /// </summary>
        /// <typeparam name="TIterator"></typeparam>
        /// <param name="that"></param>
        /// <returns></returns>
        public static TIterator? CreateIterator<TIterator>(
                this IBMDSwitcherFairlightAudioAuxOutput? that)
                where TIterator : class {
            if (that is null) {
                Debug.WriteLine("Auxiliary output is invalid.");
                return null;
            }

            Guid iid = typeof(TIterator).GUID;
            that.CreateIterator(ref iid, out var unknown);
            if (unknown == nint.Zero) {
                Debug.WriteLine("Could not retrieve iterator.");
                return null;
            }

            return Marshal.GetObjectForIUnknown(unknown) as TIterator;
        }
    }
}
