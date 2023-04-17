namespace Vaistai.Repositories;

using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;

using Newtonsoft.Json;

using Vaistai.Models;


/// <summary>
/// Database operations related to 'Uzsakymas' entity.
/// </summary>
public class UzsakymasRepo
{
	public static List<Uzsakymas> ListUzsakymas()
	{
		var query =
			$@"SELECT
				s.id,
				s.kaina,
				s.busena as busena,
				s.pagaminimo_data as data,
				CONCAT(d.salis,', ',d.pavadinimas) as uzsakovas
			FROM
				`{Config.TblPrefix}uzsakymas` s
				LEFT JOIN `{Config.TblPrefix}uzsakovas` d ON s.fk_UZSAKOVASpavadinimas=d.pavadinimas
			ORDER BY s.id DESC";

		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<Uzsakymas>(drc, (dre, t) =>
			{
				t.Id = dre.From<int>("id");
				t.Kaina = dre.From<double>("kaina");
				t.Uzsakovas = dre.From<string>("uzsakovas");
				t.Data = dre.From<DateTime>("data");
				t.Busena = dre.From<string>("busena");
			});

		return result;
	}

	public static UzsakymasCE FindUzsakymasCE(int nr)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}uzsakymas` WHERE id=?id";
		var drc =
			Sql.Query(query, args =>
			{
				args.Add("?id", nr);
			});

		var result =
			Sql.MapOne<UzsakymasCE>(drc, (dre, t) =>
			{
				//make a shortcut
				var sut = t.Uzsakymas;

				//
				sut.Id = dre.From<int>("id");
				sut.Data = dre.From<DateTime>("pagaminimo_data");
				sut.Terminas = dre.From<DateTime>("terminas");
				sut.Kaina = dre.From<int>("kaina");
				sut.FkUzsakovas = dre.From<string>("fk_UZSAKOVASpavadinimas");
				sut.FkVadovas = dre.From<string>("fk_VADOVASid");
				sut.Busena = dre.From<string>("busena");
			});

		return result;
	}

	public static int InsertUzsakymas(UzsakymasCE uzsakCE)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}uzsakymas`
			(
				`id`,
				`pagaminimo_data`,
				`terminas`,
				`kaina`,
				`fk_UZSAKOVASpavadinimas`,
				`fk_VADOVASid`,
				`busena`
			)
			VALUES(
				?id,
				?pagaminimo_data,
				?terminas,
				?kaina,
				?fk_UZSAKOVASpavadinimas,
				?fk_VADOVASid,
				?busena
			)";

		var nr =
		Sql.Insert(query, args =>
		{
			//make a shortcut
			var sut = uzsakCE.Uzsakymas;

			//
			args.Add("?id", sut.Id);
			args.Add("?pagaminimo_data", sut.Data);
			args.Add("?terminas", sut.Terminas);
			args.Add("?kaina", sut.Kaina);
			args.Add("?fk_UZSAKOVASpavadinimas", sut.FkUzsakovas);
			args.Add("?fk_VADOVASid", sut.FkVadovas);
			args.Add("?busena", sut.Busena);
		});

		return (int)nr;
	}

	public static void UpdateUzsakymas(UzsakymasCE sutCE)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}uzsakymas`
			SET
				`id` = ?id,
				`pagaminimo_data` =	?pagaminimo_data,
				`terminas` = ?terminas,
				`kaina` = ?kaina,
				`fk_UZSAKOVASpavadinimas` =	?fk_UZSAKOVASpavadinimas,
				`fk_VADOVASid`=	?fk_VADOVASid,
				`busena`=?busena
			WHERE id=?id";

		Sql.Update(query, args =>
		{
			//make a shortcut
			var sut = sutCE.Uzsakymas;

			//
			args.Add("?id", sut.Id);
			args.Add("?pagaminimo_data", sut.Data);
			args.Add("?terminas", sut.Terminas);
			args.Add("?kaina", sut.Kaina);
			args.Add("?fk_UZSAKOVASpavadinimas", sut.FkUzsakovas);
			args.Add("?fk_VADOVASid", sut.FkVadovas);
			args.Add("?busena", sut.Busena);
		});
	}

	public static void DeleteUzsakymas(int nr)
	{
		UzsakymasRepo.RemoveVaistas(nr);
		var query = $@"DELETE FROM `{Config.TblPrefix}uzsakymas` where id=?id";
		Sql.Delete(query, args =>
		{
			args.Add("?id", nr);
		});
	}
	public static UzsakymasCE ListBusena(UzsakymasCE uzsakCE)
	{
		uzsakCE.Lists.Busena = new List<SelectListItem>();
		uzsakCE.Lists.Busena.Add(new SelectListItem("priimtas", "priimtas"));
		uzsakCE.Lists.Busena.Add(new SelectListItem("gaminamas", "gaminamas"));
		uzsakCE.Lists.Busena.Add(new SelectListItem("paruostas", "paruostas"));

		return uzsakCE;
	}
	public static List<UzsakymasCE.VaistasM> ListVaistas(int UzsakymasId)
	{
		var query =
			$@"SELECT *
			FROM `{Config.TblPrefix}vaistas`
			WHERE fk_UZSAKYMASid = ?UzsakymasId
			ORDER BY fk_UZSAKYMASid ASC";

		var drc =
			Sql.Query(query, args =>
			{
				args.Add("?UzsakymasId", UzsakymasId);
			});

		var result =
			Sql.MapAll<UzsakymasCE.VaistasM>(drc, (dre, t) =>
			{
				t.Id = dre.From<int>("id");
				t.Doze = dre.From<int>("doze_ml");
				t.Buferis = dre.From<string>("formulavimo_buferis");
				t.Procesas = dre.From<string>("procesas");
				t.FkTalpa = dre.From<string>("fk_TALPAid");
				t.Faze = dre.From<string>("klinikine_faze");
				t.Galiojimas = dre.From<int>("galiojimo_laikas");
				t.Data = dre.From<DateTime?>("pagaminimo_data");
			});

		for (int i = 0; i < result.Count; i++)
			result[i].InListId = i;

		return result;
	}

	public static void InsertVaistas(int UzsakymasId, UzsakymasCE.VaistasM up)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}vaistas`
				(
					`pagaminimo_data`,
					`formulavimo_buferis`,
					`doze_ml`,
					`procesas`,
					`klinikine_faze`,
					`galiojimo_laikas`,
					`fk_TALPAid`,
					`fk_UZSAKYMASid`
				)
				VALUES(
					?pagaminimo_data,
					?formulavimo_buferis,
					?doze_ml,
					?procesas,
					?klinikine_faze,
					?galiojimo_laikas,
					?fk_TALPAid,
					?fk_UZSAKYMASid
				)";

		Sql.Insert(query, args =>
		{
			args.Add("?pagaminimo_data", up.Data);
			args.Add("?formulavimo_buferis", up.Buferis);
			args.Add("?doze_ml", up.Doze);
			args.Add("?procesas", up.Procesas);
			args.Add("?klinikine_faze", up.Faze);
			args.Add("?galiojimo_laikas", up.Galiojimas);
			args.Add("?fk_TALPAid", up.FkTalpa);
			args.Add("?fk_UZSAKYMASid", UzsakymasId);
		});
	}

	public static void RemoveVaistas(int UzsakymasId)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}vaistas`
            SET `fk_UZSAKYMASid` = -1
            WHERE `fk_UZSAKYMASid` = ?fkid";

		Sql.Update(query, args =>
		{
			args.Add("?fkid", UzsakymasId);
		});
	}
	public static void DeleteVaistas(int sutartis)
	{
		var query =
			$@"DELETE FROM a
			USING `{Config.TblPrefix}vaistas` as a
			WHERE a.fk_UZSAKYMASid=?fkid";

		Sql.Delete(query, args =>
		{
			args.Add("?fkid", sutartis);
		});
	}
}