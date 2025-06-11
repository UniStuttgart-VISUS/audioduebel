// <copyright file="OutputViewModel.cs" company="Visualisierungsinstitut der Universität Stuttgart">
// Copyright © 2025 Visualisierungsinstitut der Universität Stuttgart.
// Licensed under the MIT licence. See LICENCE file for details.
// </copyright>
// <author>Christoph Müller</author>

using BMDSwitcherAPI;
using System.Text;


namespace Visus.AudioDübel {

    /// <summary>
    /// A view model for routing sink.
    /// </summary>
    /// <param name="output"></param>
    internal sealed class OutputViewModel(
            IBMDSwitcherAudioRoutingOutput output) {

        #region Public properties
        public _BMDSwitcherAudioChannelPair ChannelPair {
            get {
                this._output.GetChannelPair(out var retval);
                return retval;
            }
        }

        public _BMDSwitcherExternalPortType ExternalPortType {
            get {
                this._output.GetExternalPortType(out var retval);
                return retval;
            }
        }

        public uint ID {
            get {
                this._output.GetId(out var retval);
                return retval;
            }
        }

        public _BMDSwitcherAudioInternalPortType InternalPortType {
            get {
                this._output.GetInternalPortType(out var retval);
                return retval;
            }
        }

        public bool IsDefaultName {
            get {
                this._output.GetIsNameDefault(out var retval);
                return (retval != 0);
            }
        }

        public string Name {
            get {
                this._output.GetName(out var retval);
                return retval;
            }
            set {
                this._output.SetName(value);
            }
        }

        public uint Source {
            get {
                this._output.GetSource(out var retval);
                return retval;
            }
            set {
                this._output.SetSource(value);
            }
        }

        public string Tooltip {
            get {
                var sb = new StringBuilder();
                sb.Append("0x").AppendLine(this.ID.ToString("X"));
                sb.Append(this.Name).Append(" (").Append(this.IsDefaultName).AppendLine(")");
                sb.AppendLine(this.ChannelPair.ToString());
                sb.AppendLine(this.ExternalPortType.ToString());
                sb.AppendLine(this.InternalPortType.ToString());
                sb.Append(Properties.Resources.Source).Append(": 0x").AppendLine(this.Source.ToString("X"));
                return sb.ToString();
            }
        }
        #endregion

        #region Public methods
        public override string ToString() => this.Name;
        #endregion

        #region Private fields
        private readonly IBMDSwitcherAudioRoutingOutput _output = output
            ?? throw new ArgumentNullException(nameof(output));
        #endregion
    }
}
