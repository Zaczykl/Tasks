using System.Collections.Generic;
using Tasks.Core.Models.Domains;

namespace Tasks.Core.ViewModels
{
    public class TaskViewModel
    {
        public string Heading { get; set; }
        public Task Task { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
