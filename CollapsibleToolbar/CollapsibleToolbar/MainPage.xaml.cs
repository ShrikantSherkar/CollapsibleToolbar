using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CollapsibleToolbar
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        double _toolbarPosition;
        public MainPage()
        {
            InitializeComponent();
            TheScroll.PropertyChanged += OnScrollViewPropertyChanged;
            collapsibleContent.SizeChanged += CollapsibleContentSizeChanged;
            toolbarStack.SizeChanged += OnTitleTextSizeChanged;
            headerLbl.Opacity = 0;
        }

        private void OnTitleTextSizeChanged(object sender, System.EventArgs e)
        {
            toolbarStack.SizeChanged -= OnTitleTextSizeChanged;
            _toolbarPosition = toolbarStack.Y + 1;
        }

        private void CollapsibleContentSizeChanged(object sender, System.EventArgs e)
        {
            collapsibleContent.SizeChanged -= CollapsibleContentSizeChanged;
            toolbarStack.Margin = new Thickness(0, collapsibleContent.Height - 50, 0, 0);
        }

        private void OnScrollViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(ScrollView.ScrollYProperty.PropertyName))
            {
                var scrolled = ((ScrollView)sender).ScrollY;
                toolbarStack.TranslationY = scrolled < _toolbarPosition ? 0 - scrolled : 0 - _toolbarPosition;
                var opac = -Math.Round(100 * toolbarStack.TranslationY / 150) / 100;
                if (opac >= 0 || opac <= 1)
                    headerLbl.Opacity = opac;
            }
        }
    }
}
