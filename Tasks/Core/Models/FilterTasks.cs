using System.ComponentModel.DataAnnotations;

namespace Tasks.Core.Models
{
    public class FilterTasks
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }

        [Display(Name ="Tylko zrealizowane")]
        public bool isExecuted { get; set; }
    }
}
