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
        public _BMDSwitcherFairlightAudioInputConfiguration Configuration {
            get {
                this._input.GetConfiguration(out var retval);
                return retval;
            }
            set {
                this._input.SetConfiguration(value);
            }
        }

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

        public IEnumerable<FairlightSourceViewModel> Sources {
            get {
                var it = this._input.CreateIterator<IBMDSwitcherFairlightAudioSourceIterator>();
                if (it is null) {
                    yield break;
                }

                it.Next(out var source);
                while (source is not null) {
                    yield return new FairlightSourceViewModel(source);
                    it.Next(out source);
                }
            }
        }
        #endregion

        #region Public methods
        public override string ToString() => $"{this.Type}: {this.ID}, {this.Configuration}";
        #endregion

        #region Private fields
        private readonly IBMDSwitcherFairlightAudioInput _input = input
            ?? throw new ArgumentNullException(nameof(input));
        #endregion
    }
}
