using System;
using HomeIncClient.Core;
using HomeIncClient.Core.UI;

namespace HomeIncClient
{
    public class RouteChangeArgs : EventArgs
    {
        public RouteChangeArgs(View view)
        {
            RouteView = view;
        }

        public View RouteView { get; set; }
    }

    public class Routing
    {
        public RoutingList List { get; set; }
        public event EventHandler<RouteChangeArgs> OnRoute;

        public void RouteRoot()
        {
            Route(RoutePaths.Root);
        }

        public void RaiseRoteChange(View routeView)
        {
            if (OnRoute != null)
            {
                OnRoute(this, new RouteChangeArgs(routeView));
            }
        }

        public void Route(string id, ViewModel customViewModel = null)
        {
            var newView = List.GetRouteView(id);
            if (newView == null)
            {
                return;
            }

            if (customViewModel != null && customViewModel is ViewModel)
            {
                newView.DataContext = customViewModel;
            }

            if (newView.DataContext != null && newView.DataContext is ViewModel)
            {
                ((ViewModel) newView.DataContext).Prepare();
            }

            RaiseRoteChange(newView);
        }

        #region Singleton

        private static Routing _instance;
        private static readonly object Locker = new object();

        public static Routing Instance
        {
            get
            {
                lock (Locker)
                {
                    return _instance ?? (_instance = new Routing());
                }
            }
        }

        private Routing()
        {
            List = new RoutingList();
        }

        #endregion
    }
}