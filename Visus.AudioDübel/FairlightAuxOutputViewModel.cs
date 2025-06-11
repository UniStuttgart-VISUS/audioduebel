// <copyright file="FairlightAuxIOutputViewModel.cs" company="Visualisierungsinstitut der Universität Stuttgart">
// Copyright © 2025 Visualisierungsinstitut der Universität Stuttgart.
// Licensed under the MIT licence. See LICENCE file for details.
// </copyright>
// <author>Christoph Müller</author>

using BMDSwitcherAPI;


namespace Visus.AudioDübel {

    /// <summary>
    /// A view model for an axuiliary output.
    /// </summary>
    /// <param name="output"></param>
    internal sealed class FairlightAuxOutputViewModel(
            IBMDSwitcherFairlightAudioAuxOutput output) {

        #region Public properties
        public _BMDSwitcherFairlightAudioAuxOutputId ID {
            get {
                this._output.GetOutputId(out var retval);
                return retval;
            }
        }
        #endregion

        #region Private fields
        private readonly IBMDSwitcherFairlightAudioAuxOutput _output = output
            ?? throw new ArgumentNullException(nameof(output));
        #endregion
    }
}
