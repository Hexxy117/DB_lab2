namespace Vaistai.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Newtonsoft.Json;

using Vaistai.Repositories;
using Vaistai.Models;


/// <summary>
/// Controller for working with 'Vaistas' entity. Implementation of F2 version.
/// </summary>
public class VaistasController : Controller
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		return View(VaistasRepo.ListVaistas());
	}

	/// <summary>
	/// This is invoked when creation form is first opened in a browser.
	/// </summary>
	/// <returns>Entity creation form.</returns>
	[HttpGet]
	public ActionResult Create()
	{
		var vaistasCE = new VaistasCE();
		PopulateSelections(vaistasCE);

		return View(vaistasCE);
	}


	public ActionResult Create(VaistasCE vaistasCE)
	{
		//form field validation passed?
		if (ModelState.IsValid)
		{
			VaistasRepo.InsertVaistas(vaistasCE);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}
		//form field validation failed, go back to the form
		PopulateSelections(vaistasCE);
		return View(vaistasCE);
	}

	/// <summary>
	/// This is invoked when editing form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to edit.</param>
	/// <returns>Editing form view.</returns>
	[HttpGet]
	public ActionResult Edit(int id)
	{
		var vaistrasCE = VaistasRepo.FindVaistasCE(id);
		PopulateSelections(vaistrasCE);

		return View(vaistrasCE);
	}
	[HttpPost]
	public ActionResult Edit(int id, VaistasCE vaistasCE)
	{
		//form field validation passed?
		if (ModelState.IsValid)
		{
			VaistasRepo.UpdateVaistas(vaistasCE);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}

		//form field validation failed, go back to the form
		PopulateSelections(vaistasCE);
		return View(vaistasCE);
	}


	/// <summary>
	/// This is invoked when deletion form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(int id)
	{
		var vaistasCE = VaistasRepo.FindVaistasCE(id);
		return View(vaistasCE);
	}

	/// <summary>
	/// This is invoked when deletion is confirmed in deletion form
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view on error, redirects to Index on success.</returns>
	[HttpPost]
	public ActionResult DeleteConfirm(int id)
	{
		try
		{
			VaistasRepo.DeleteVaistas(id);

			//deletion success, redired to list form
			return RedirectToAction("Index");
		}
		//entity in use, deletion not permitted
		catch (MySql.Data.MySqlClient.MySqlException)
		{
			//enable explanatory message and show delete form
			ViewData["deletionNotPermitted"] = true;

			var vaistrasCE = VaistasRepo.FindVaistasCE(id);
			PopulateSelections(vaistrasCE);

			return View("Delete", vaistrasCE);
		}
	}

	/// <summary>
	/// Populates select lists used to render drop down controls.
	/// </summary>
	/// <param name="vaistasCE">'Vaistas' view model to append to.</param>
	private void PopulateSelections(VaistasCE vaistasCE)
	{
		//load entities for the select lists
		var talpa = TalpaRepo.List();

		//build select lists
		vaistasCE.Lists.Talpa =
			talpa
				.Select(it =>
				{
					return
						new SelectListItem
						{
							Value = Convert.ToString(it.Id),
							Text = $"{it.Rusis} {it.Turis}"
						};
				})
				.ToList();
	}
}
