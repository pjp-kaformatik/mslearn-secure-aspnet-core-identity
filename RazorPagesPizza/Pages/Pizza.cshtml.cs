using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesPizza.Models;
using RazorPagesPizza.Services;

// Turorial
using Microsoft.AspNetCore.Authorization;

namespace RazorPagesPizza.Pages
{
    // Turorial
    [Authorize]

    public class PizzaModel : PageModel
    {
        // Tutorial
        public bool IsAdmin => HttpContext.User.HasClaim("IsAdmin", bool.TrueString);
        
        public List<Pizza> pizzas = new();

        [BindProperty]
        public Pizza NewPizza { get; set; } = new();

        public void OnGet()
        {
            pizzas = PizzaService.GetAll();
        }

        public IActionResult OnPost()
        {
            // Tutorial
            if (!IsAdmin) return Forbid();

            if (!ModelState.IsValid)
            {
                return Page();
            }
            PizzaService.Add(NewPizza);
            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int id)
        {
            // Tutorial
            if (!IsAdmin) return Forbid();
            
            PizzaService.Delete(id);
            return RedirectToAction("Get");
        }

        public string GlutenFreeText(Pizza pizza)
        {
            if (pizza.IsGlutenFree)
                return "Gluten Free";
            return "Not Gluten Free";
        }
    }
}