using System.Windows.Media;

namespace HomeIncClient.Views.Controls
{
    public partial class IconButton
    {
        public IconButton()
        {
            InitializeComponent();
            DataContext = this;
        }

        public int IconSize { get; set; }
        public int Size { get; set; }
        public SolidColorBrush IconColor { get; set; }
    }
}