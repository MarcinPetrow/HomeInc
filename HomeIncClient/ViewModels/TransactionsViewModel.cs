using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HomeIncClient.Core;
using HomeIncClient.Helpers;
using HomeIncClient.Models;
using HomeIncClient.Repositories;

namespace HomeIncClient.ViewModels
{
    public class TransactionsViewModel : ViewModel
    {
        private Transaction _current;
        private ICommand _routeNewCommand;
        private ICommand _saveCurrentCommand;
        private ObservableCollection<Transaction> _transactions;

        public ICommand RouteNewCommand
        {
            get { return _routeNewCommand ?? (_routeNewCommand = new RouteCommand(RoutePaths.NewTransactionPath)); }
        }

        public ICommand RouteListCommand
        {
            get { return _routeNewCommand ?? (_routeNewCommand = new RouteCommand(RoutePaths.TransactionsPath)); }
        }

        public ICommand SaveCurrentCommand
        {
            get
            {
                return _saveCurrentCommand ?? (_saveCurrentCommand = new DelegateCommand(SaveCurrentCommandExecute));
            }
        }

        public ObservableCollection<Transaction> Transactions
        {
            get { return _transactions ?? (Transactions = new ObservableCollection<Transaction>()); }
            set
            {
                _transactions = value;
                OnPropertyChanged();
            }
        }

        public Transaction Current
        {
            get { return _current; }
            set
            {
                _current = value;
                OnPropertyChanged();
            }
        }

        public void SaveCurrentCommandExecute()
        {
            using (var repository = new TransactionsRepository())
            {
                repository.UpdateOrCreate(Current);
                Current = new Transaction();
                Routing.Instance.Route(RoutePaths.TransactionsPath);

                MessageBox.Show("New transaction added properly", "Success", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        public override void Prepare()
        {
            Current = new Transaction();
            using (var repository = new TransactionsRepository())
            {
                Transactions = new ObservableCollection<Transaction>(repository.All());
            }
        }
    }
}