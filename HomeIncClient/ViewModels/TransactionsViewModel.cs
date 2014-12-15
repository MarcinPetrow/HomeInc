﻿using HomeIncClient.Core;
using HomeIncClient.Helpers;
using HomeIncClient.Models;
using HomeIncClient.Repositories;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HomeIncClient.ViewModels
{
    public class TransactionsViewModel : ViewModel
    {
        private Transaction _current;
        private ICommand _routeNewCommand;
        private ICommand _routeEditCommand;
        private ICommand _deleteItemCommand;
        private ICommand _saveCurrentCommand;
        private ObservableCollection<Transaction> _transactions;

        public ICommand RouteNewCommand
        {
            get { return _routeNewCommand ?? (_routeNewCommand = new RouteCommand(RoutePaths.NewTransactionPath)); }
        }

        public ICommand RouteEditCommand
        {
            get { return _routeEditCommand ?? (_routeEditCommand = new DelegateCommand<Transaction>(RouteEditCommandExecute)); }
        }

        public ICommand DeleteItemCommand
        {
            get { return _deleteItemCommand ?? (_deleteItemCommand = new DelegateCommand<Transaction>(DeleteItemCommandExecute)); }
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
            get { return _current ?? (Current = new Transaction()); }
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
            using (var repository = new TransactionsRepository())
            {
                Transactions = new ObservableCollection<Transaction>(repository.All());
            }
        }

        #region Commands implementations

        public void RouteEditCommandExecute(Transaction item)
        {
            var viewModel = new TransactionsViewModel
            {
                Current = item
            };
            Routing.Instance.Route(RoutePaths.EditTransactionPath, viewModel);
        }

        private void DeleteItem(Transaction item)
        {
            using (var repository = new TransactionsRepository())
            {
                var existingItem = repository.Read(item.Id);
                if (existingItem != null)
                {
                    repository.Delete(existingItem);
                }

            }
            if (Transactions.Contains(item))
            {
                Transactions.Remove(item);
            }
        }

        public void DeleteItemCommandExecute(Transaction item)
        {
            DeleteItem(item);
        }
        #endregion
    }
}