using Microsoft.AspNetCore.Mvc;
using Gantu_Diego_T1.Models;
using Gantu_Diego_T1.Services;

namespace Gantu_Diego_T1.Controllers;

public class LibroController : Controller
{
    private readonly ILibroServices _libroServices;

    public LibroController(ILibroServices libroServices)
    {
        _libroServices = libroServices;
    }

    public IActionResult Index(string? autor)
    {
        ViewBag.Mensaje = TempData["Mensaje"] ?? ViewBag.Mensaje;
        ViewBag.FiltroAutor = autor;

        if (!string.IsNullOrWhiteSpace(autor))
        {
            IEnumerable<Libro> filtrados = _libroServices.ListarLibrosPorAutor(autor.Trim());
            return View(filtrados);
        }

        IEnumerable<Libro> libros = _libroServices.ListarLibros();
        return View(libros);
    }

    [HttpGet]
    public IActionResult BuscarPorAutor(string? autor)
    {
        ViewBag.FiltroAutor = autor;
        IEnumerable<Libro> resultados = string.IsNullOrWhiteSpace(autor)
            ? _libroServices.ListarLibros()
            : _libroServices.ListarLibrosPorAutor(autor.Trim());

        ViewBag.Mensaje = string.IsNullOrWhiteSpace(autor)
            ? "Mostrando todos los libros."
            : $"Resultados encontrados para el autor: {autor.Trim()}";

        return View("Index", resultados);
    }

    [HttpGet]
    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Crear(Libro libro)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Mensaje = "Complete todos los campos obligatorios antes de registrar.";
            return View(libro);
        }

        try
        {
            bool registrado = _libroServices.RegistrarLibro(libro);
            if (registrado)
            {
                ViewBag.Mensaje = "El libro se registró correctamente.";
                TempData["Mensaje"] = ViewBag.Mensaje;
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Mensaje = "No se pudo registrar el libro.";
            return View(libro);
        }
        catch (Exception ex)
        {
            ViewBag.Mensaje = $"Error al registrar el libro: {ex.Message}";
            return View(libro);
        }
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {
        try
        {
            Libro? libro = _libroServices.ObtenerLibroPorId(id);
            if (libro == null)
            {
                ViewBag.Mensaje = "No se encontró el libro solicitado.";
                TempData["Mensaje"] = ViewBag.Mensaje;
                return RedirectToAction(nameof(Index));
            }

            return View(libro);
        }
        catch (Exception ex)
        {
            ViewBag.Mensaje = $"Error al cargar el libro: {ex.Message}";
            TempData["Mensaje"] = ViewBag.Mensaje;
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public IActionResult Editar(Libro libro)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Mensaje = "Complete todos los campos obligatorios para actualizar.";
            return View(libro);
        }

        try
        {
            bool actualizado = _libroServices.ActualizarLibro(libro);
            if (actualizado)
            {
                ViewBag.Mensaje = "Los datos del libro se actualizaron correctamente.";
                TempData["Mensaje"] = ViewBag.Mensaje;
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Mensaje = "No se pudo actualizar el libro.";
            return View(libro);
        }
        catch (Exception ex)
        {
            ViewBag.Mensaje = $"Error al actualizar el libro: {ex.Message}";
            return View(libro);
        }
    }

    [HttpPost]
    public IActionResult Eliminar(int id)
    {
        try
        {
            bool eliminado = _libroServices.EliminarLibro(id);
            ViewBag.Mensaje = eliminado
                ? "El libro se eliminó correctamente."
                : "No se pudo eliminar el libro.";
        }
        catch (Exception ex)
        {
            ViewBag.Mensaje = $"Error al eliminar el libro: {ex.Message}";
        }

        TempData["Mensaje"] = ViewBag.Mensaje;
        return RedirectToAction(nameof(Index));
    }
}