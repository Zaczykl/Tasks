using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using Tasks.Core.Models.Domains;

namespace Tasks.Core.ViewModels
{
    public class CategoriesViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
