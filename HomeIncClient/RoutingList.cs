using System;
using System.Collections.Generic;
using System.Linq;
using HomeIncClient.Core;
using HomeIncClient.Core.UI;

namespace HomeIncClient
{
    public class Route
    {
        public string Id { get; set; }
        public Type View { get; set; }
        public Type ViewModel { get; set; }
    }

    public class RoutingList
    {
        public List<Route> Routes;

        public RoutingList()
        {
            Routes = new List<Route>();
        }

        public void AddRoute(string id, Type view, Type viewModel = null)
        {
            Routes.Add(new Route
            {
                Id = id,
                View = view,
                ViewModel = viewModel
            });
        }

        public View GetRouteView(string id, ViewModel customViewModel = null)
        {
            var route = Routes.FirstOrDefault(x => x.Id == id);

            if (route == null)
            {
                return null;
            }

            var viewInstance = Activator.CreateInstance(route.View) as View;
            if (viewInstance == null)
            {
                return null;
            }

            var newContext = customViewModel;
            if (newContext == null && route.ViewModel != null)
            {
                var viewModelInstance = Activator.CreateInstance(route.ViewModel) as ViewModel;
                newContext = viewModelInstance;
            }

            viewInstance.DataContext = newContext;

            return viewInstance;
        }
    }
}