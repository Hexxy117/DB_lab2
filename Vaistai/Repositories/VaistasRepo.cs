namespace Vaistai.Repositories;

using MySql.Data.MySqlClient;

using Newtonsoft.Json;

using Vaistai.Models;
using Vaistai;


/// <summary>
/// Database operations related to 'Sutartis' entity.
/// </summary>
public class VaistasRepo
{
	public static List<Vaistas> ListVaistas()
	{
		var query =
			$@"SELECT
				v.id,
				v.formulavimo_buferis as buferis,
				v.procesas,
				CONCAT(d.rusis,' ', d.turis, 'ml') as talpa,
				v.klinikine_faze as faze,
				v.galiojimo_laikas
			FROM
				`{Config.TblPrefix}vaistas` v
				LEFT JOIN `{Config.TblPrefix}talpa` d ON v.fk_TALPAid=d.id
			ORDER BY v.id DESC";

		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<Vaistas>(drc, (dre, t) =>
			{
				t.Id = dre.From<int>("id");
				t.Buferis = dre.From<string>("buferis");
				t.Procesas = dre.From<string>("procesas");
				t.Talpa = dre.From<string>("talpa");
				t.Faze = dre.From<string>("faze");
				t.Galiojimas = dre.From<int>("galiojimo_laikas");
			});

		return result;
	}
	public static VaistasCE FindVaistasCE(int id)
	{
		var query = $@"SELECT * FROM `vaistas` WHERE id=?id";
		var drc =
			Sql.Query(query, args =>
			{
				args.Add("?id", id);
			});

		var result =
			Sql.MapOne<VaistasCE>(drc, (dre, t) =>
			{
				var sut = t.Vaistas;

				sut.Id = dre.From<int>("id");
				sut.Data = dre.From<DateTime?>("pagaminimo_data");
				sut.Buferis = dre.From<string>("formulavimo_buferis");
				sut.Doze = dre.From<int>("doze_ml");
				sut.Procesas = dre.From<string>("procesas");
				sut.Faze = dre.From<string>("klinikine_faze");
				sut.Galiojimas = dre.From<int>("galiojimo_laikas");
				sut.FkTalpa = dre.From<string>("fk_TALPAid");
				sut.FkUzsakymas = dre.From<string>("fk_UZSAKYMASid");
			});

		return result;
	}
	public static void InsertVaistas(VaistasCE vaistasCE)
	{
		var query =
			$@"INSERT INTO `vaistas`
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
        VALUES
        (
            ?pagaminimo_data,
            ?formulavimo_buferis,
            ?doze_ml,
            ?procesas,
            ?klinikine_faze,
            ?galiojimo_laikas,
            ?fk_TALPAid,
            -1
        )";

		Sql.Insert(query, args =>
		{
			//make a shortcut
			var sut = vaistasCE.Vaistas;

			//
			args.Add("?pagaminimo_data", sut.Data);
			args.Add("?formulavimo_buferis", sut.Buferis);
			args.Add("?doze_ml", sut.Doze);
			args.Add("?procesas", sut.Procesas);
			args.Add("?klinikine_faze", sut.Faze);
			args.Add("?galiojimo_laikas", sut.Galiojimas);
			args.Add("?fk_TALPAid", sut.FkTalpa);
			args.Add("?fk_UZSAKYMASid", sut.FkUzsakymas);
		});
	}
	public static void UpdateVaistas(VaistasCE vaistasCE)
	{
		var query =
			$@"UPDATE `vaistas`
			SET
				`pagaminimo_data` = ?pagaminimo_data,
				`formulavimo_buferis` = ?formulavimo_buferis,
				`doze_ml` = ?doze_ml,
				`procesas` = ?procesas,
				`klinikine_faze` = ?klinikine_faze,
				`galiojimo_laikas` = ?galiojimo_laikas,
				`fk_TALPAid` = ?talpa,
				`fk_UZSAKYMASid` = ?fk_UZSAKYMASid
			WHERE id=?id";

		Sql.Update(query, args =>
		{
			var sut = vaistasCE.Vaistas;

			args.Add("?pagaminimo_data", sut.Data);
			args.Add("?formulavimo_buferis", sut.Buferis);
			args.Add("?doze_ml", sut.Doze);
			args.Add("?procesas", sut.Procesas);
			args.Add("?klinikine_faze", sut.Faze);
			args.Add("?galiojimo_laikas", sut.Galiojimas);
			args.Add("?talpa", sut.FkTalpa);
			args.Add("?id", sut.Id);
			args.Add("?fk_UZSAKYMASid", sut.FkUzsakymas);
		});
	}
	public static void DeleteVaistas(int id)
	{
		var query = $@"DELETE FROM `vaistas` where id=?id";
		Sql.Delete(query, args =>
		{
			args.Add("?id", id);
		});
	}
}
