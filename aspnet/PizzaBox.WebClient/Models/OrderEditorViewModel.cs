using System.ComponentModel.DataAnnotations;

namespace PizzaBox.WebClient.Models
{
    public class OrderEditorViewModel
    {
        public OrderViewModel OrderVM { get; set; }

        [Required]
        public int SelectedPizzaIndex { get; set; }
    }
}