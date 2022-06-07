using System.Windows.Controls;

namespace Starter
{
    public partial class UserMainPage : BasePage<MenuButtonListViewModel>
    {
        public UserMainPage() : base()
        {
            InitializeComponent();
        }

        public UserMainPage(MenuButtonListViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }
    }
}
