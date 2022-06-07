using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Starter
{
    public class BasePage<VM> : Page where VM : BaseViewModel, new()
    {
        private VM mViewModel;

        public EnumPageAnimation PageLoadAnimation { get; set; } = EnumPageAnimation.SlideAndFadeInFromRight;
        public EnumPageAnimation PageUnloadAnimation { get; set; } = EnumPageAnimation.SlideAndFadeOutToLeft;
        public double SlideSeconds { get; set; } = 0.3;

        public VM ViewModel
        {
            get { return mViewModel; }
            set
            {
                if(mViewModel == value)
                {
                    return;
                }

                mViewModel = value;
                this.DataContext = mViewModel;
            }
        }

        public BasePage()
        {
            this.ViewModel = new VM();
        }

        public BasePage(VM specificViewModel = null) : base()
        {
            if(specificViewModel != null)
            {
                ViewModel = specificViewModel;
            }
            else
            {
                if (DesignerProperties.GetIsInDesignMode(this))
                {
                    ViewModel = new VM();
                }
            }
        }

        private async void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            await AnimateIn();
        }

        public async Task AnimateIn()
        {
            if (this.PageLoadAnimation == EnumPageAnimation.None)
                return;

            switch (this.PageLoadAnimation)
            {
                case EnumPageAnimation.SlideAndFadeInFromRight:
                    await this.SlideAndFadeInFromRight(this.SlideSeconds);
                    break;
            }
        }

        public async Task AnimateOut()
        {
            if (this.PageUnloadAnimation == EnumPageAnimation.None)
                return;

            if(this.PageUnloadAnimation == EnumPageAnimation.SlideAndFadeOutToLeft)
                await this.SlideAndFadeOutToLeft(this.SlideSeconds);
        }
    }
}
