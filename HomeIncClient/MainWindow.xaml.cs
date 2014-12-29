using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HomeIncClient.Annotations;
using HomeIncClient.Core.UI;
using HomeIncClient.Helpers;
using HomeIncClient.ViewModels;
using HomeIncClient.Views;
using HomeIncClient.Views.Categories;
using HomeIncClient.Views.Transactions;

namespace HomeIncClient
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private View _currentView;
        private ICommand _routeAboutCommand;

        public MainWindow()
        {
            AddRoutingPaths();
            Routing.Instance.OnRoute += HandleRouteChange;
            InitializeComponent();
            DataContext = this;

            Routing.Instance.Route(RoutePaths.Root);
        }

        public View CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public ICommand RouteAboutCommand
        {
            get { return _routeAboutCommand ?? (_routeAboutCommand = new RouteCommand(RoutePaths.AboutPath)); }
        }

        private void HandleRouteChange(object sender, RouteChangeArgs e)
        {
            if (e.RouteView == null)
            {
                return;
            }
            CurrentView = e.RouteView;
        }

        public void AddRoutingPaths()
        {
            var routingList = Routing.Instance.List;
            routingList.AddRoute(RoutePaths.Root, typeof (TransactionsList), typeof (TransactionsViewModel));
            routingList.AddRoute(RoutePaths.NewTransactionPath, typeof (TransactionsNew), typeof (TransactionsViewModel));
            routingList.AddRoute(RoutePaths.EditTransactionPath, typeof (TransactionsEdit),
                typeof (TransactionsViewModel));

            routingList.AddRoute(RoutePaths.CategoriesPath, typeof (CategoriesList), typeof (CategoriesViewModel));
            routingList.AddRoute(RoutePaths.NewCategoryPath, typeof (CategoriesNew), typeof (CategoriesViewModel));
            routingList.AddRoute(RoutePaths.EditCategoryPath, typeof (CategoriesEdit), typeof (CategoriesViewModel));

            routingList.AddRoute(RoutePaths.AboutPath, typeof (About), null);
        }

        #region Daraggable

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        #endregion

        #region Property changed

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}