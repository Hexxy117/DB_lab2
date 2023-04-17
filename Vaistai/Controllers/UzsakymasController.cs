namespace Vaistai.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Newtonsoft.Json;

using Vaistai.Repositories;
using Vaistai.Models;
using System.Security.Policy;


/// <summary>
/// Controller for working with 'Uzsakymas' entity. Implementation of F2 version.
/// </summary>
public class UzsakymasController : Controller
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		return View(UzsakymasRepo.ListUzsakymas());
	}

	/// <summary>
	/// This is invoked when creation form is first opened in a browser.
	/// </summary>
	/// <returns>Entity creation form.</returns>
	[HttpGet]
	public ActionResult Create()
	{
		var UzsakymasCE = new UzsakymasCE();
		PopulateSelections(UzsakymasCE);

		return View(UzsakymasCE);
	}


	[HttpPost]
	public ActionResult Create(int? save, int? add, int? remove, UzsakymasCE uzsakCE)
	{
		//addition of new 'Vaistai' record was requested?
		if (add != null)
		{
			//add entry for the new record
			var up =
				new UzsakymasCE.VaistasM
				{
					InListId =
							uzsakCE.Vaistai.Count > 0 ?
							uzsakCE.Vaistai.Max(it => it.InListId) + 1 :
										0
				};
			uzsakCE.Vaistai.Add(up);

			//make sure @Html helper is not reusing old model state containing the old list
			ModelState.Clear();

			//go back to the form
			PopulateSelections(uzsakCE);
			return View(uzsakCE);
		}

		//removal of existing 'Vaistai' record was requested?
		if (remove != null)
		{
			//filter out 'Vaistai' record having in-list-id the same as the given one
			uzsakCE.Vaistai =
				uzsakCE
					.Vaistai
					.Where(it => it.InListId != remove.Value)
					.ToList();

			//make sure @Html helper is not reusing old model state containing the old list
			ModelState.Clear();

			//go back to the form
			PopulateSelections(uzsakCE);
			return View(uzsakCE);
		}

		//save of the form data was requested?
		if (save != null)
		{
			//form field validation passed?
			if (ModelState.IsValid)
			{
				//create new 'Uzsakymas'
				uzsakCE.Uzsakymas.Id = UzsakymasRepo.InsertUzsakymas(uzsakCE);

				//create new 'Vaistai' records
				foreach (var upVm in uzsakCE.Vaistai)
					UzsakymasRepo.InsertVaistas(uzsakCE.Uzsakymas.Id, upVm);

				//save success, go back to the entity list
				return RedirectToAction("Index");
			}
			//form field validation failed, go back to the form
			else
			{
				PopulateSelections(uzsakCE);
				return View(uzsakCE);
			}
		}

		//should not reach here
		throw new Exception("Should not reach here.");
	}

	/// <summary>
	/// This is invoked when editing form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to edit.</param>
	/// <returns>Editing form view.</returns>
	[HttpGet]
	public ActionResult Edit(int id)
	{
		var sutCE = UzsakymasRepo.FindUzsakymasCE(id);

		sutCE.Vaistai = UzsakymasRepo.ListVaistas(id);
		PopulateSelections(sutCE);

		return View(sutCE);
	}
	[HttpPost]
	public ActionResult Edit(int id, int? save, int? add, int? remove, UzsakymasCE uzsakCE)
	{
		//addition of new 'UzsakytosPaslaugos' record was requested?
		if (add != null)
		{
			//add entry for the new record
			var up =
				new UzsakymasCE.VaistasM
				{
					InListId =
							uzsakCE.Vaistai.Count > 0 ?
							uzsakCE.Vaistai.Max(it => it.InListId) + 1 : 0,
					Doze = 1

				};
			uzsakCE.Vaistai.Add(up);

			//make sure @Html helper is not reusing old model state containing the old list
			ModelState.Clear();

			//go back to the form
			PopulateSelections(uzsakCE);
			return View(uzsakCE);
		}

		//removal of existing 'UzsakytosPaslaugos' record was requested?
		if (remove != null)
		{
			//filter out 'Vaistai' record having in-list-id the same as the given one
			uzsakCE.Vaistai =
				uzsakCE
					.Vaistai
					.Where(it => it.InListId != remove.Value)
					.ToList();

			//make sure @Html helper is not reusing old model state containing the old list
			ModelState.Clear();

			//go back to the form
			PopulateSelections(uzsakCE);
			return View(uzsakCE);
		}

		//save of the form data was requested?
		if (save != null)
		{

			//form field validation passed?
			if (ModelState.IsValid)
			{
				//update 'uzsakartis'
				UzsakymasRepo.UpdateUzsakymas(uzsakCE);

				//delete all old 'UzsakytosPaslaugos' records
				UzsakymasRepo.DeleteVaistas(uzsakCE.Uzsakymas.Id);

				//create new 'UzsakytosPaslaugos' records
				foreach (var upVm in uzsakCE.Vaistai)
					UzsakymasRepo.InsertVaistas(uzsakCE.Uzsakymas.Id, upVm);

				//save success, go back to the entity list
				return RedirectToAction("Index");
			}
			//form field validation failed, go back to the form
			else
			{
				PopulateSelections(uzsakCE);
				return View(uzsakCE);
			}
		}

		//should not reach here
		throw new Exception("Should not reach here.");
	}


	/// <summary>
	/// This is invoked when deletion form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(int id)
	{
		var UzsakymasCE = UzsakymasRepo.FindUzsakymasCE(id);
		return View(UzsakymasCE);
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
			UzsakymasRepo.DeleteUzsakymas(id);
			UzsakymasRepo.RemoveVaistas(id);
			//deletion success, redired to list form
			return RedirectToAction("Index");
		}
		//entity in use, deletion not permitted
		catch (MySql.Data.MySqlClient.MySqlException)
		{
			//enable explanatory message and show delete form
			ViewData["deletionNotPermitted"] = true;

			var uzsakymasCE = UzsakymasRepo.FindUzsakymasCE(id);
			PopulateSelections(uzsakymasCE);

			return View("Delete", uzsakymasCE);
		}
	}

	/// <summary>
	/// Populates select lists used to render drop down controls.
	/// </summary>
	/// <param name="UzsakymasCE">'Uzsakymas' view model to append to.</param>
	private void PopulateSelections(UzsakymasCE UzsakymasCE)
	{
		//load entities for the select lists
		var busena = UzsakymasRepo.ListBusena(UzsakymasCE);
		var vadovas = VadovasRepo.List();
		var uzsakovas = UzsakovasRepo.List();
		var talpa = TalpaRepo.List();
		UzsakymasCE.Lists.Vadovas =
			vadovas
				.Select(it =>
				{
					return
						new SelectListItem
						{
							Value = Convert.ToString(it.Id),
							Text = $"{it.Vardas} {it.Pavarde}"
						};
				})
				.ToList();
		UzsakymasCE.Lists.Uzsakovas =
		uzsakovas
		.Select(it =>
		{
			return
				new SelectListItem
				{
					Value = Convert.ToString(it.Pavadinimas),
					Text = $"{it.Kryptis}, {it.Pavadinimas}"
				};
		})
		.ToList();
		UzsakymasCE.Lists.Talpa =
			talpa
				.Select(it =>
				{
					return
						new SelectListItem
						{
							Value = Convert.ToString(it.Id),
							Text = $"{it.Rusis} {it.Turis} ml"
						};
				})
				.ToList();




	}
}
