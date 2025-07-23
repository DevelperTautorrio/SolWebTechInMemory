using DOMAIN.ENTITIES;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebTechInMemory.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<E_User> usuarios = new List<E_User>();

            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:5265");

                    
                    var respuesta = await cliente.GetAsync("/api/Values/users");

                    if (respuesta.IsSuccessStatusCode)
                    {
                        // Leer el contenido de la respuesta de forma asíncrona
                        string json = await respuesta.Content.ReadAsStringAsync();

                        // Deserializar el JSON a una lista de usuarios
                        usuarios = JsonConvert.DeserializeObject<List<E_User>>(json) ?? new List<E_User>();
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                
                ViewBag.ErrorMessage = "No se pudo obtener la lista de usuarios.";
            }

            return View("Start", usuarios);
        }
    }
}
