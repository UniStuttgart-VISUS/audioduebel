// <copyright file="FairlightInputViewModel.cs" company="Visualisierungsinstitut der Universität Stuttgart">
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
    internal sealed class FairlightInputViewModel(
            IBMDSwitcherFairlightAudioInput input) {

        #region Public properties
        public long ID {
            get {
                this._input.GetId(out var retval);
                return retval;
            }
        }

        public _BMDSwitcherFairlightAudioInputType Type {
            get {
                this._input.GetType(out var retval);
                return retval;
            }
        }
        #endregion

        #region Public methods
        public override string ToString() => $"{this.Type}: {this.ID}";
        #endregion

        #region Private fields
        private readonly IBMDSwitcherFairlightAudioInput _input = input
            ?? throw new ArgumentNullException(nameof(input));
        #endregion
    }
}
