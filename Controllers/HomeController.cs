using Microsoft.AspNetCore.Mvc;
using Tp06.Models;
using System.Collections.Generic;

namespace Tp06.Controllers
{
    public class HomeController : Controller
    {
        private BD bd = new BD();

    
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("ID") == null)
                return RedirectToAction("Login", "Account");

            int idUsuario = int.Parse(HttpContext.Session.GetString("ID"));
            List<Tarea> todasTareas = bd.TareasPorUsuario(idUsuario);

            return View(todasTareas);
        }

public IActionResult FinalizarTarea(int id)
{
    if (HttpContext.Session.GetString("ID") == null)
        return RedirectToAction("Login", "Account");

    bd.MarcarTareaComoFinalizada(id); 
    return RedirectToAction("Index");
}


        [HttpGet]
        public IActionResult CrearTarea()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CrearTarea(string Titulo, string Descripcion, System.DateTime Fecha)
        {
            if (HttpContext.Session.GetString("ID") == null)
                return RedirectToAction("Login", "Account");

            int idUsuario = int.Parse(HttpContext.Session.GetString("ID"));

            Tarea nuevaTarea = new Tarea();
            nuevaTarea.Titulo = Titulo;
            nuevaTarea.Descripcion = Descripcion;
            nuevaTarea.Fecha = Fecha;
            nuevaTarea.Finalizado = false;
            nuevaTarea.IdU = idUsuario;

            bd.AgregarTarea(nuevaTarea);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult EditarTarea(int id)
        {
            List<Tarea> todasTareas = bd.LevantarTareas();
            Tarea tarea = null;

            for (int i = 0; i < todasTareas.Count; i++)
            {
                if (todasTareas[i].ID == id)
                {
                    tarea = todasTareas[i];
                    break;
                }
            }

            if (tarea == null)
                return RedirectToAction("Index");

            return View(tarea);
        }

        [HttpPost]
        public IActionResult EditarTarea(int ID, string Titulo, string Descripcion, System.DateTime Fecha, bool Finalizado)
        {
            int idUsuario = int.Parse(HttpContext.Session.GetString("ID"));

            Tarea tarea = new Tarea();
            tarea.ID = ID;
            tarea.Titulo = Titulo;
            tarea.Descripcion = Descripcion;
            tarea.Fecha = Fecha;
            tarea.Finalizado = Finalizado;
            tarea.IdU = idUsuario;

            bd.EditarTarea(tarea);
            return RedirectToAction("Index");
        }


        public IActionResult EliminarTarea(int id)
{
    if (HttpContext.Session.GetString("ID") == null)
        return RedirectToAction("Login", "Account");

    bd.EliminarTarea(id);
    return RedirectToAction("Index");
}
    }
    
}
