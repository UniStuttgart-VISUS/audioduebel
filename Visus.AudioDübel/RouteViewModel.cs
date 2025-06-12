// <copyright file="RouteViewModel.cs" company="Visualisierungsinstitut der Universität Stuttgart">
// Copyright © 2025 Visualisierungsinstitut der Universität Stuttgart.
// Licensed under the MIT licence. See LICENCE file for details.
// </copyright>
// <author>Christoph Müller</author>

using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Visus.AudioDübel {

    /// <summary>
    /// Represents a route betwen a <see cref="SourceViewModel"/> and an
    /// <see cref="OutputViewModel"/>.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <param name="source"></param>
    /// <param name="output"></param>
    [DebuggerDisplay($"{{{nameof(Row)}}}, {{{nameof(Column)}}}: {{{nameof(Output)}}}")]
    public sealed class RouteViewModel(
            int row,
            int column,
            SourceViewModel source,
            OutputViewModel output)
            : INotifyPropertyChanged {

        #region Public properties
        /// <summary>
        /// Gets the column of the route in the matrix.
        /// </summary>
        public int Column { get; } = column;

        /// <summary>
        /// Gets the group name, which is dependent on the output that can have
        /// exactly one source selected.
        /// </summary>
        public string Group => this.Output.ID.ToString();

        /// <summary>
        /// Gets whether the route is the selected one for the
        /// <see cref="Output"/>.
        /// </summary>
        public bool IsSelected {
            get => (this.Output.Source == this.Source.ID);
            set {
                if (value) {
                    this.Output.Source = this.Source.ID;
                }
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the output the route is the destination for.
        /// </summary>
        public OutputViewModel Output {
            get;
        } = output ?? throw new ArgumentNullException(nameof(output));

        /// <summary>
        /// Gets the row of the route in the matrix.
        /// </summary>
        public int Row { get; } = row;

        /// <summary>
        /// Gets the source of the route.
        /// </summary>
        public SourceViewModel Source {
            get;
        } = source ?? throw new ArgumentNullException(nameof(source));
        #endregion

        #region Public events
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Private methods
        private void OnPropertyChanged(
                [CallerMemberName] string propertyName = null!)
            => this.PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
