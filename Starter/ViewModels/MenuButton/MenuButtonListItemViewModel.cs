using System.Windows.Input;

namespace Starter
{
    public class MenuButtonListItemViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string SourcePath { get; set; }

        public ICommand ButtonCommand { get; set; }
    }
}
