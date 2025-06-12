// <copyright file="RouterView.cs" company="Visualisierungsinstitut der Universität Stuttgart">
// Copyright © 2025 Visualisierungsinstitut der Universität Stuttgart.
// Licensed under the MIT licence. See LICENCE file for details.
// </copyright>
// <author>Christoph Müller</author>

using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;


namespace Visus.AudioDübel {

    /// <summary>
    /// Interaction logic for RouterView.xaml.
    /// </summary>
    public partial class RouterView : UserControl, INotifyPropertyChanged {

        #region Dependency properties
        /// <summary>
        /// An attached property that allows for binding the column definitions
        /// of the data template to <see cref="Columns"/>.
        /// </summary>
        public static readonly DependencyProperty ColumnDefinitionsProperty
            = DependencyProperty.RegisterAttached(
                "ColumnDefinitions",
                typeof(IEnumerable<ColumnDefinition>),
                typeof(RouterView),
                new PropertyMetadata(OnColumnsChanged));

        /// <summary>
        /// An attached property that allows for binding the row definitions of
        /// the data template to <see cref="Rows"/>.
        /// </summary>
        public static readonly DependencyProperty RowDefinitionsProperty
            = DependencyProperty.RegisterAttached(
                "RowDefinitions",
                typeof(IEnumerable<RowDefinition>),
                typeof(RouterView),
                new PropertyMetadata(OnRowsChanged));

        public static IEnumerable<ColumnDefinition> GetColumnDefinitions(
                DependencyObject obj)
            => (IEnumerable<ColumnDefinition>) obj.GetValue(
                ColumnDefinitionsProperty);

        public static void SetColumnDefinitions(DependencyObject obj,
                IEnumerable<ColumnDefinition> value)
            => obj.SetValue(ColumnDefinitionsProperty, value);

        public static IEnumerable<RowDefinition> GetRowDefinitions(
                DependencyObject obj)
            => (IEnumerable<RowDefinition>) obj.GetValue(
                RowDefinitionsProperty);

        public static void SetRowDefinitions(DependencyObject obj,
                IEnumerable<RowDefinition> value)
            => obj.SetValue(RowDefinitionsProperty, value);
        #endregion

        #region Public constructors
        /// <summary>
        /// Initialises a new instance.
        /// </summary>
        public RouterView() {
            this.DataContext = this;
            this.InitializeComponent();
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets all cells being shown in the routing matrix.
        /// </summary>
        public IEnumerable<object> Cells {
            get {
                if (this.Router is null) {
                    return Enumerable.Empty<object>();
                }

                return this.Router.Outputs.Cast<object>()
                    .Concat(this.Router.Sources.Cast<object>())
                    .Concat(this.Router.Routes.Cast<object>());
            }
        }

        /// <summary>
        /// Gets the number of columns in the routing matrix.
        /// </summary>
        public IEnumerable<ColumnDefinition> Columns => this._columns;

        /// <summary>
        /// Gets or sets the router model that is being displayed.
        /// </summary>
        public RouterViewModel? Router {
            get => this._router;
            set {
                ArgumentNullException.ThrowIfNull(value);
                this._router = value;

                // Make sure that the values are actually new, because the
                // notifications will not trigger if we modify the existing
                // collections.
                this._columns = new();
                this._rows = new();

                // the first column is for the source names.
                this._columns.Add(new ColumnDefinition {
                    Width = new GridLength(1, GridUnitType.Auto)
                });

                // The first row is for the output names.
                this._rows.Add(new RowDefinition {
                    Height = new GridLength(1, GridUnitType.Auto)
                });

                // Add the cells for the routes.
                for (int i = 0; i < this._router.Outputs.Count(); ++i) {
                    this._columns.Add(new ColumnDefinition {
                        Width = new GridLength(1, GridUnitType.Star)
                    });
                }

                for (int i = 0; i < this._router.Sources.Count(); ++i) {
                    this._rows.Add(new RowDefinition {
                        Height = new GridLength(1, GridUnitType.Auto)
                    });
                }

                this.OnPropertyChanged(nameof(this.Columns));
                this.OnPropertyChanged(nameof(this.Rows));
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.Cells));
            }
        }

        /// <summary>
        /// Gets the number of rows in the routing matrix.
        /// </summary>
        public IEnumerable<RowDefinition> Rows => this._rows;
        #endregion

        #region Public events
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Private methods
        private static void OnColumnsChanged(DependencyObject d,
                DependencyPropertyChangedEventArgs e) {
            Debug.WriteLine(nameof(OnColumnsChanged));
            if ((d is Grid g) && (e.NewValue is IEnumerable<ColumnDefinition> cols)) {
                g.ColumnDefinitions.Clear();

                foreach (var c in cols) {
                    g.ColumnDefinitions.Add(c);
                }

                Debug.WriteLine($"{g.ColumnDefinitions.Count} columns");
                g.InvalidateMeasure();
            }
        }

        private void OnPropertyChanged(
                [CallerMemberName] string propertyName = null!)
            => this.PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));

        private static void OnRowsChanged(DependencyObject d,
                DependencyPropertyChangedEventArgs e) {
            Debug.WriteLine(nameof(OnRowsChanged));
            if ((d is Grid g) && (e.NewValue is IEnumerable<RowDefinition> rows)) {
                g.RowDefinitions.Clear();

                foreach (var r in rows) {
                    g.RowDefinitions.Add(r);
                }

                Debug.WriteLine($"{g.RowDefinitions.Count} rows");
                g.InvalidateMeasure();
            }
        }
        #endregion

        #region Private fields
        private RouterViewModel? _router;
        private List<ColumnDefinition> _columns = new();
        private List<RowDefinition> _rows = new();
        #endregion
    }
}
