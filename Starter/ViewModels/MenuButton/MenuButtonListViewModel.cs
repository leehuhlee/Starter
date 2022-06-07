using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace Starter
{
    public class MenuButtonListViewModel : BaseViewModel
    {
        public List<CategoryListViewModel> CategoryList { get; set; } = new List<CategoryListViewModel>();

        public string AppVersion { get; set; }

        public ICommand SearchMenuCommand { get; set; }
        public ICommand ClearSearchBoxCommand { get; set; }

        protected string userSearchText = string.Empty;
        protected string lastSearchText = string.Empty;

        public string UserSearchText
        {
            get => userSearchText;
            set
            {
                if (userSearchText == value)
                    return;

                userSearchText = value;

                SearchMenu();
            }
        }

        public MenuButtonListViewModel()
        {
            SearchMenuCommand = new RelayCommand(SearchMenu);
            ClearSearchBoxCommand = new RelayCommand(ClearSearchBox);

            CreateCategory();
            CreateCommand();
            AppVersion = "ver. " + Assembly.GetCallingAssembly().GetName().Version.ToString();
        }

        private void CreateCategory()
        {
            CategoryList.Add(new CategoryListViewModel
            {
                CategoryName = "Website",
                MenuButtonsCollection = new ObservableCollection<MenuButtonListItemViewModel>
                {
                    new MenuButtonListItemViewModel
                    {
                        Name = "Portfolio",
                        SourcePath = "https://leehuhlee.github.io/"
                    },
                    new MenuButtonListItemViewModel
                    {
                        Name = "Github",
                        SourcePath = "https://github.com/leehuhlee"
                    },
                    new MenuButtonListItemViewModel
                    {
                        Name = "LinkedIn",
                        SourcePath = "https://www.linkedin.com/in/jihyun-lee-6234b41a4"
                    }
                }
            });

            CategoryList.Add(new CategoryListViewModel
            {
                CategoryName = "Study",
                MenuButtonsCollection = new ObservableCollection<MenuButtonListItemViewModel>
                {
                    new MenuButtonListItemViewModel
                    {
                        Name = "Youtube",
                        SourcePath = "https://www.youtube.com/"
                    },
                    new MenuButtonListItemViewModel
                    {
                        Name = "Udemy",
                        SourcePath = "https://www.udemy.com/"
                    },
                    new MenuButtonListItemViewModel
                    {
                        Name = "Inflrean",
                        SourcePath = "https://www.inflearn.com/"
                    }
                }
            });

            CategoryList.Add(new CategoryListViewModel
            {
                CategoryName = "Code",
                MenuButtonsCollection = new ObservableCollection<MenuButtonListItemViewModel>
                {
                    new MenuButtonListItemViewModel
                    {
                        Name = "Stack Overflow",
                        SourcePath = "https://stackoverflow.com/"
                    },
                    new MenuButtonListItemViewModel
                    {
                        Name = "Codewars",
                        SourcePath = "https://www.codewars.com/"
                    },
                    new MenuButtonListItemViewModel
                    {
                        Name = "MSDN",
                        SourcePath = "https://docs.microsoft.com/en-us/dotnet/csharp/"
                    }
                }
            });
        }

        private void CreateCommand()
        {
            foreach(var category in CategoryList)
            {
                foreach(var button in category.MenuButtonsCollection)
                {
                    button.ButtonCommand = new RelayCommand(() => ExecuteButton(button.SourcePath));
                }
            }
        }

        private void ExecuteButton(string sourcePath)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = sourcePath,
                UseShellExecute = true
            });
        }

        private void SearchMenu()
        {
            if ((string.IsNullOrEmpty(lastSearchText) && string.IsNullOrEmpty(UserSearchText))
                    || string.Equals(lastSearchText, UserSearchText))
            {
                return;
            }

            bool noSearchText = false;

            foreach (var category in CategoryList)
            {
                if (string.IsNullOrEmpty(UserSearchText) || !category.MenuButtonsCollection.Any())
                {
                    category.FilteredMenuButtonsCollection = new ObservableCollection<MenuButtonListItemViewModel>(
                        category.MenuButtonsCollection ?? Enumerable.Empty<MenuButtonListItemViewModel>());

                    lastSearchText = UserSearchText;
                    noSearchText = true;
                }
            }

            if (noSearchText)
                return;

            string lowerCaseUserSearchText = UserSearchText.ToLower();

            foreach (var category in CategoryList)
            {
                category.FilteredMenuButtonsCollection =
                    new ObservableCollection<MenuButtonListItemViewModel>
                    (
                        category.MenuButtonsCollection
                            .Where(item => item.Name.ToLower().Contains(lowerCaseUserSearchText))
                    );
            }

            lastSearchText = UserSearchText;
        }

        private void ClearSearchBox()
        {
            this.UserSearchText = string.Empty;
        }
    }
}
