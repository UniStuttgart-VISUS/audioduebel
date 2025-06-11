// <copyright file="FairlightAuxInputViewModel.cs" company="Visualisierungsinstitut der Universität Stuttgart">
// Copyright © 2025 Visualisierungsinstitut der Universität Stuttgart.
// Licensed under the MIT licence. See LICENCE file for details.
// </copyright>
// <author>Christoph Müller</author>

using BMDSwitcherAPI;


namespace Visus.AudioDübel {

    /// <summary>
    /// A view model for mixer input.
    /// </summary>
    /// <param name="input"></param>
    internal sealed class FairlightAuxInputViewModel(
            IBMDSwitcherFairlightAudioAuxOutputInput input) {

        #region Public properties
        public _BMDSwitcherFairlightAudioAuxOutputInputId ID {
            get {
                this._input.GetInputId(out var retval);
                return retval;
            }
        }
        #endregion

        #region Public methods
        public override string ToString() => $"AUX {this.ID}";
        #endregion

        #region Private fields
        private readonly IBMDSwitcherFairlightAudioAuxOutputInput _input = input
            ?? throw new ArgumentNullException(nameof(input));
        #endregion
    }
}
