namespace Vaistai.Controllers;

using Microsoft.AspNetCore.Mvc;

using Vaistai.Repositories;
using Vaistai.Models;


/// <summary>
/// Controller for working with 'Uzsakovas' entity.
/// </summary>
public class UzsakovasController : Controller
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		//gražinamas darbuotoju sarašo vaizdas
		return View(UzsakovasRepo.List());
	}

	/// <summary>
	/// This is invoked when creation form is first opened in browser.
	/// </summary>
	/// <returns>Creation form view.</returns>
	[HttpGet]
	public ActionResult Create()
	{
		var vad = new Uzsakovas();
		return View(vad);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="darb">Entity model filled with latest data.</param>
	/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Create(Uzsakovas vad)
	{
		//do not allow creation of entity with 'tabelis' field matching existing one
		var match = UzsakovasRepo.Find(vad.Pavadinimas);

		if (match != null)
			ModelState.AddModelError("pavadinimas", "Field value already exists in database.");

		//form field validation passed?
		if (ModelState.IsValid)
		{
			//NOTE: insert will fail if someone managed to add different 'darbuotojas' with same 'tabelis' after the check
			UzsakovasRepo.Insert(vad);

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
	public ActionResult Edit(string id)
	{
		return View(UzsakovasRepo.Find(id));
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the editing form.
	/// </summary>
	/// <param name="id">ID of the entity being edited</param>		
	/// <param name="vad">Entity model filled with latest data.</param>
	/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Edit(string id, Uzsakovas vad)
	{
		//form field validation passed?
		if (ModelState.IsValid)
		{
			UzsakovasRepo.Update(vad);

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
	public ActionResult Delete(string id)
	{
		var vad = UzsakovasRepo.Find(id);
		return View(vad);
	}

	/// <summary>
	/// This is invoked when deletion is confirmed in deletion form
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view on error, redirects to Index on success.</returns>
	[HttpPost]
	public ActionResult DeleteConfirm(string id)
	{
		//try deleting, this will fail if foreign key constraint fails
		try
		{
			UzsakovasRepo.Delete(id);

			//deletion success, redired to list form
			return RedirectToAction("Index");
		}
		//entity in use, deletion not permitted
		catch (MySql.Data.MySqlClient.MySqlException)
		{
			//enable explanatory message and show delete form
			ViewData["deletionNotPermitted"] = true;

			var vad = UzsakovasRepo.Find(id);
			return View("Delete", vad);
		}
	}
}
