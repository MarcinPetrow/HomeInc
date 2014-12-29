using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace HomeIncClient.Assets.Styles.ControlTemplates
{
    /// <summary>
    /// Interaction logic for Window.xaml
    /// </summary>
    public partial class Window
    {

        public static System.Windows.Window GetParentWindow(DependencyObject child)
        {
            var parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
            {
                return null;
            }

            var parent = parentObject as System.Windows.Window;
            return parent ?? GetParentWindow(parentObject);
        }

        private void WindowDragAction(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left) return;
            var element = sender as FrameworkElement;
            if (element == null) return;
            var rootElement = GetParentWindow(element);
            if (rootElement != null)
            {
                rootElement.DragMove();
            }
        }
    }
}
