using System.Collections.Generic;
using Tasks.Core.Models;
using Tasks.Core.Models.Domains;

namespace Tasks.Core.ViewModels
{
    public class TasksViewModel
    {
        public IEnumerable<Task> Tasks { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public FilterTasks FilterTasks { get; set; }
    }
}
