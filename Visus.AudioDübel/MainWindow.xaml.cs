// <copyright file="MainWindow.xaml.cs" company="Visualisierungsinstitut der Universität Stuttgart">
// Copyright © 2025 Visualisierungsinstitut der Universität Stuttgart.
// Licensed under the MIT licence. See LICENCE file for details.
// </copyright>
// <author>Christoph Müller</author>

using BMDSwitcherAPI;
using System.Windows;


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

            //try {
            //    var it = this._mixer!.CreateIterator<IBMDSwitcherAudioInputIterator>();
            //    if (it is null) {
            //        MessageBox.Show(Properties.Resources.ErrorIterator,
            //            null,
            //            MessageBoxButton.OK,
            //            MessageBoxImage.Error);
            //        return;
            //    }

            //    it.Next(out var input);
            //    while (input is not null) {
            //        input.GetAudioInputId(out var id);
            //        input.GetType(out var type);
            //        input.GetCurrentExternalPortType(out var portType);
            //        input.GetMixOption(out var mixOption);
            //        this._lbInputs.Items.Add($"{id}: {type}, {portType}, {mixOption}");
            //        it.Next(out input);
            //    }
            //} catch (Exception ex) {
            //    MessageBox.Show(ex.Message,
            //        null,
            //        MessageBoxButton.OK,
            //        MessageBoxImage.Error);
            //    return;
            //}

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

        }

        /// <summary>
        /// Handles clicking the exit menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnExit(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
        #endregion

        #region Private fields
        private IBMDSwitcherAudioMixer? _mixer;
        private IBMDSwitcher? _switcher;
        private readonly IBMDSwitcherDiscovery _switcherDiscovery = new CBMDSwitcherDiscovery();
        #endregion
    }
}
