using System.Collections.ObjectModel;

namespace Starter
{
    public class CategoryListViewModel : BaseViewModel
    {
        public string CategoryName { get; set; }

        protected ObservableCollection<MenuButtonListItemViewModel> menuButtons;

        public ObservableCollection<MenuButtonListItemViewModel> FilteredMenuButtonsCollection { get; set; }

        public ObservableCollection<MenuButtonListItemViewModel> MenuButtonsCollection
        {
            get => menuButtons;
            set
            {
                if (menuButtons == value)
                    return;

                menuButtons = value;
                FilteredMenuButtonsCollection = new ObservableCollection<MenuButtonListItemViewModel>(menuButtons);
            }
        }
    }
}
