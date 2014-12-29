using HomeIncClient.Core;
using HomeIncClient.Models;
using HomeIncClient.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace HomeIncClient.ViewModels
{
    internal class CategoriesViewModel : ViewModel
    {
        private Category _current;
        private Category _rootCategory;

        public Category RootCategory
        {
            get { return _rootCategory ?? (RootCategory = new Category()); }
            set
            {
                _rootCategory = value;
                OnPropertyChanged();
            }
        }

        public Category Current
        {
            get { return _current ?? (Current = new Category()); }
            set
            {
                _current = value;
                OnPropertyChanged();
            }
        }

        public override void Prepare()
        {
            using (var repository = new CategoriesRepository())
            {
                RootCategory = repository.GetSet().Include("SubCategories").First(x => x.Name == "Root");
                RootCategory.SubCategories.First().SubCategories = new List<Category>
                {
                    new Category()
                    {
                        Name = "A",
                        SubCategories = new List<Category>
                        {
                            new Category
                            {
                                Name = "1"
                            }
                        }
                    },
                    new Category()
                    {
                        Name = "B"
                    },
                    new Category()
                    {
                        Name = "C"
                    },
                };
                OnPropertyChanged("RootCategory");
            }
        }

        #region Commands implementations

        #endregion
    }
}