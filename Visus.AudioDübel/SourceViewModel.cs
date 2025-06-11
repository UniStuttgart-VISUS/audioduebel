// <copyright file="SourceViewModel.cs" company="Visualisierungsinstitut der Universität Stuttgart">
// Copyright © 2025 Visualisierungsinstitut der Universität Stuttgart.
// Licensed under the MIT licence. See LICENCE file for details.
// </copyright>
// <author>Christoph Müller</author>

using BMDSwitcherAPI;
using System.Text;


namespace Visus.AudioDübel {

    /// <summary>
    /// A view model for routing source.
    /// </summary>
    /// <param name="source"></param>
    internal sealed class SourceViewModel(
            IBMDSwitcherAudioRoutingSource source) {

        #region Public properties
        public _BMDSwitcherAudioChannelPair ChannelPair {
            get {
                this._source.GetChannelPair(out var retval);
                return retval;
            }
        }

        public _BMDSwitcherExternalPortType ExternalPortType {
            get {
                this._source.GetExternalPortType(out var retval);
                return retval;
            }
        }

        public long ID {
            get {
                this._source.GetAudioInputId(out var retval);
                return retval;
            }
        }

        public _BMDSwitcherAudioInternalPortType InternalPortType {
            get {
                this._source.GetInternalPortType(out var retval);
                return retval;
            }
        }

        public bool IsDefaultName {
            get {
                this._source.GetIsNameDefault(out var retval);
                return (retval != 0);
            }
        }

        public string Name {
            get {
                this._source.GetName(out var retval);
                return retval;
            }
            set {
                this._source.SetName(value);
            }
        }

        public string Tooltip {
            get {
                var sb = new StringBuilder();
                sb.AppendLine(this.ID.ToString());
                sb.Append(this.Name).Append(" (").Append(this.IsDefaultName).AppendLine(")");
                sb.AppendLine(this.ChannelPair.ToString());
                sb.AppendLine(this.ExternalPortType.ToString());
                sb.AppendLine(this.InternalPortType.ToString());
                return sb.ToString();
            }
        }
        #endregion

        #region Public methods
        public override string ToString() => this.Name;
        #endregion

        #region Private fields
        private readonly IBMDSwitcherAudioRoutingSource _source = source
            ?? throw new ArgumentNullException(nameof(source));
        #endregion
    }
}
