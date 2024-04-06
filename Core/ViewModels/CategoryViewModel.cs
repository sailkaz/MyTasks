using MyTasks.Core.Models.Domains;

namespace MyTasks.Core.ViewModels
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }
        public string Heading { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
