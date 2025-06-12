// <copyright file="RouterViewModel.cs" company="Visualisierungsinstitut der Universität Stuttgart">
// Copyright © 2025 Visualisierungsinstitut der Universität Stuttgart.
// Licensed under the MIT licence. See LICENCE file for details.
// </copyright>
// <author>Christoph Müller</author>

using BMDSwitcherAPI;


namespace Visus.AudioDübel {

    /// <summary>
    /// The view model for an audio router.
    /// </summary>
    public sealed class RouterViewModel {

        #region Public constructors
        /// <summary>
        /// Initialises a new instance.
        /// </summary>
        /// <param name="outputs"></param>
        /// <param name="inputs"></param>
        public RouterViewModel(
                IBMDSwitcherAudioRoutingOutputIterator outputs,
                IBMDSwitcherAudioRoutingSourceIterator inputs) {
            if (inputs is not null) {
                int r = 1;

                inputs.Next(out var input);
                while (input is not null) {
                    this._sources.Add(new(input) {
                        Row = r++
                    });
                    inputs.Next(out input);
                }
            }

            if (outputs is not null) {
                int c = 1;

                outputs.Next(out var output);
                while (output is not null) {
                    int r = 1;
                    var o = new OutputViewModel(output) {
                        Column = c
                    };
                    this._outputs.Add(o);

                    foreach (var s in this._sources) {
                        this._routes.Add(new(r++, c, s, o));
                    }

                    ++c;
                    outputs.Next(out output);
                }
            }

            this.Outputs = this._routes.Select(r => r.Output).Distinct();
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets all audio outputs.
        /// </summary>
        public IEnumerable<OutputViewModel> Outputs { get; }

        /// <summary>
        /// Gets the audio routes.
        /// </summary>
        public IEnumerable<RouteViewModel> Routes => this._routes;

        /// <summary>
        /// Gets all audio sources.
        /// </summary>
        public IEnumerable<SourceViewModel> Sources => this._sources;
        #endregion

        #region Private fields
        private readonly List<OutputViewModel> _outputs = new();
        private readonly List<RouteViewModel> _routes = new();
        private readonly List<SourceViewModel> _sources = new();
        #endregion
    }
}
