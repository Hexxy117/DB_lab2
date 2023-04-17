namespace Vaistai.Controllers;

using Microsoft.AspNetCore.Mvc;

using Vaistai.Repositories;
using Vaistai.Models;


/// <summary>
/// Controller for working with 'Vadovas' entity.
/// </summary>
public class VadovasController : Controller
{
    /// <summary>
    /// This is invoked when either 'Index' action is requested or no action is provided.
    /// </summary>
    /// <returns>Entity list view.</returns>
    [HttpGet]
    public ActionResult Index()
    {
        //gražinamas darbuotoju sarašo vaizdas
        return View(VadovasRepo.List());
    }

    /// <summary>
    /// This is invoked when creation form is first opened in browser.
    /// </summary>
    /// <returns>Creation form view.</returns>
    [HttpGet]
    public ActionResult Create()
    {
        var vad = new Vadovas();
        return View(vad);
    }

    /// <summary>
    /// This is invoked when buttons are pressed in the creation form.
    /// </summary>
    /// <param name="darb">Entity model filled with latest data.</param>
    /// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
    [HttpPost]
    public ActionResult Create(Vadovas vad)
    {
        //do not allow creation of entity with 'tabelis' field matching existing one
        var match = VadovasRepo.Find(vad.Id);

        if (match != null)
            ModelState.AddModelError("id", "Field value already exists in database.");

        //form field validation passed?
        if (ModelState.IsValid)
        {
            //NOTE: insert will fail if someone managed to add different 'darbuotojas' with same 'tabelis' after the check
            VadovasRepo.Insert(vad);

            //save success, go back to the entity list
            return RedirectToAction("Index");
        }

        //form field validation failed, go back to the form
        return View(vad);
    }

    /// <summary>
    /// This is invoked when editing form is first opened in browser.
    /// </summary>
    /// <param name="id">ID of the entity to edit.</param>
    /// <returns>Editing form view.</returns>
    [HttpGet]
    public ActionResult Edit(int id)
    {
        return View(VadovasRepo.Find(id));
    }

    /// <summary>
    /// This is invoked when buttons are pressed in the editing form.
    /// </summary>
    /// <param name="id">ID of the entity being edited</param>		
    /// <param name="vad">Entity model filled with latest data.</param>
    /// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
    [HttpPost]
    public ActionResult Edit(string id, Vadovas vad)
    {
        //form field validation passed?
        if (ModelState.IsValid)
        {
            VadovasRepo.Update(vad);

            //save success, go back to the entity list
            return RedirectToAction("Index");
        }

        //form field validation failed, go back to the form
        return View(vad);
    }

    /// </summary>
    /// <param name="id">ID of the entity to delete.</param>
    /// <returns>Deletion form view.</returns>
    [HttpGet]
    public ActionResult Delete(int id)
    {
        var vad = VadovasRepo.Find(id);
        return View(vad);
    }

    /// <summary>
    /// This is invoked when deletion is confirmed in deletion form
    /// </summary>
    /// <param name="id">ID of the entity to delete.</param>
    /// <returns>Deletion form view on error, redirects to Index on success.</returns>
    [HttpPost]
    public ActionResult DeleteConfirm(int id)
    {
        //try deleting, this will fail if foreign key constraint fails
        try
        {
            VadovasRepo.Delete(id);

            //deletion success, redired to list form
            return RedirectToAction("Index");
        }
        //entity in use, deletion not permitted
        catch (MySql.Data.MySqlClient.MySqlException)
        {
            //enable explanatory message and show delete form
            ViewData["deletionNotPermitted"] = true;

            var vad = VadovasRepo.Find(id);
            return View("Delete", vad);
        }
    }
}
