// <copyright file="MainWindow.xaml.cs" company="Visualisierungsinstitut der Universität Stuttgart">
// Copyright © 2025 Visualisierungsinstitut der Universität Stuttgart.
// Licensed under the MIT licence. See LICENCE file for details.
// </copyright>
// <author>Christoph Müller</author>

using BMDSwitcherAPI;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;


namespace Visus.AudioDübel {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        #region Public constructors
        /// <summary>
        /// Initialises a new instance.
        /// </summary>
        public MainWindow() {
            this.InitializeComponent();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Handles clicking the connect button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnect(object sender, RoutedEventArgs e) {
            var reason = _BMDSwitcherConnectToFailure.bmdSwitcherConnectToFailureNoResponse;

            try {
                this._switcherDiscovery.ConnectTo(this._tbAddress.Text,
                    out this._switcher,
                    out reason);
                this._fairlight = this._switcher as IBMDSwitcherFairlightAudioMixer;
                this._mixer = this._switcher as IBMDSwitcherAudioMixer;
            } catch {
                MessageBox.Show($"Failed to connect to switcher: {reason}",
                    Properties.Resources.ErrorConnection,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            {
                this._switcher.GetProductName(out var name);
                this.Title = string.Format(
                    Properties.Resources.AppTitleConnected,
                    name);
            }

            // Get all routable inputs.
            this._routerView.Router = new RouterViewModel(
                    this._switcher.CreateIterator<IBMDSwitcherAudioRoutingOutputIterator>()!,
                    this._switcher.CreateIterator<IBMDSwitcherAudioRoutingSourceIterator>()!);

            try {
                var it = this._switcher.CreateIterator<IBMDSwitcherAudioRoutingSourceIterator>();
                if (it is null) {
                    MessageBox.Show(Properties.Resources.ErrorInputIterator,
                        null,
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

                it.Next(out var input);
                while (input is not null) {
                    input.GetAudioInputId(out var id);
                    input.GetName(out var name);
                    input.GetChannelPair(out var pair);
                    this._lbInputs.Items.Add(new SourceViewModel(input));
                    it.Next(out input);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message,
                    null,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            // Get all routable outputs.
            try {
                var it = this._switcher.CreateIterator<IBMDSwitcherAudioRoutingOutputIterator>();
                if (it is null) {
                    MessageBox.Show(Properties.Resources.ErrorOutputIterator,
                        null,
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

                it.Next(out var output);
                while (output is not null) {
                    output.GetAudioOutputId(out var id);
                    output.GetName(out var name);
                    output.GetChannelPair(out var pair);
                    this._lbOutputs.Items.Add(new OutputViewModel(output));
                    it.Next(out output);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message,
                    null,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            if (this._fairlight is not null) {
                var it = this._fairlight.CreateIterator<IBMDSwitcherFairlightAudioInputIterator>();
                if (it is null) {
                    return;
                }

                it.Next(out var input);
                while (input is not null) {
                    var vm = new FairlightInputViewModel(input);
                    this._lbMixIn.Items.Add(vm);

                    foreach (var source in vm.Sources) {
                        this._lbMixIn.Items.Add(source);
                    }

                    it.Next(out input);
                }
            }

            if (this._fairlight is not null) {
                var it = this._fairlight.CreateIterator<IBMDSwitcherFairlightAudioAuxOutputIterator>();
                if (it is null) {
                    return;
                }

                it.Next(out var output);
                while (output is not null) {
                    var vm = new FairlightAuxOutputViewModel(output);
                    this._lbMixIn.Items.Add(vm);

                    foreach (var input in vm.Inputs) {
                        this._lbMixIn.Items.Add(input);
                    }

                    it.Next(out output);
                }
            }

            //if (this._fairlight is not null) {
            //    var it = this._fairlight.CreateIterator<IBMDSwitcherFairlightAudioAuxOutputIterator>();
            //    if (it is null) {
            //        return;
            //    }

            //    it.Next(out var output);
            //    while (output is not null) {
            //        //this._lbMixIn.Items.Add(new FairlightMonitorOutputViewModel(output));
            //        it.Next(out output);
            //    }
            //}
        }

        /// <summary>
        /// Handles clicking the exit menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnExit(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Highlights the outputs when the selected input changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnInputSelectionChanged(object sender, SelectionChangedEventArgs e) {
            Debug.Assert(sender == this._lbInputs);
            var source = this._lbInputs.SelectedItem as SourceViewModel;
            if (source is not null) {
                var outputs = this._lbOutputs.Items
                    .Cast<OutputViewModel>()
                    .Where(o => o.Source == source.ID);
                this._lbOutputs.SelectedItems.Clear();
                foreach (var output in outputs) {
                    this._lbOutputs.SelectedItems.Add(output);
                }
            }
        }

        /// <summary>
        /// Highlights the selected source when an output is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOutputSelectionChanged(object sender,
                SelectionChangedEventArgs e) {
            // That does not make sense, because we would be unable to connect anything at all.
            //Debug.Assert(sender == this._lbOutputs);
            //var output = this._lbInputs.SelectedItems
            //    .Cast<OutputViewModel>()
            //    .SingleOrDefault();

            //if (output is not null) {
            //    var source = this._lbInputs.Items
            //        .Cast<SourceViewModel>()
            //        .Where(s => s.ID == output.Source);
            //    this._lbInputs.SelectedItem = source;
            //}
        }
        #endregion

        #region Private fields
        private IBMDSwitcherFairlightAudioMixer? _fairlight;
        private IBMDSwitcherAudioMixer? _mixer;
        private IBMDSwitcher? _switcher;
        private readonly IBMDSwitcherDiscovery _switcherDiscovery = new CBMDSwitcherDiscovery();
        #endregion
    }
}
