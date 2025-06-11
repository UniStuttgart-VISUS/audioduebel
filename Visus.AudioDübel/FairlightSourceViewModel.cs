// <copyright file="FairlightSourceViewModel.cs" company="Visualisierungsinstitut der Universität Stuttgart">
// Copyright © 2025 Visualisierungsinstitut der Universität Stuttgart.
// Licensed under the MIT licence. See LICENCE file for details.
// </copyright>
// <author>Christoph Müller</author>

using BMDSwitcherAPI;


namespace Visus.AudioDübel {

    /// <summary>
    /// A view model for mixer source.
    /// </summary>
    /// <param name="source"></param>
    internal sealed class FairlightSourceViewModel(
            IBMDSwitcherFairlightAudioSource source) {

        #region Public properties
        public long ID {
            get {
                this._source.GetId(out var retval);
                return retval;
            }
        }

        public _BMDSwitcherFairlightAudioSourceType Type {
            get {
                this._source.GetSourceType(out var retval);
                return retval;
            }
        }
        #endregion

        #region Public methods
        public override string ToString() => $"SRC {this.Type}: {this.ID}";
        #endregion

        #region Private fields
        private readonly IBMDSwitcherFairlightAudioSource _source = source
            ?? throw new ArgumentNullException(nameof(source));
        #endregion
    }
}
