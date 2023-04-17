namespace Vaistai.Repositories;

using MySql.Data.MySqlClient;
using Vaistai.Models;


/// <summary>
/// Database operations related to 'Uzsakovas' entity.
/// </summary>
public class UzsakovasRepo
{
	public static List<Uzsakovas> List()
	{
		var query = $@"SELECT * FROM `uzsakovas` ORDER BY pavadinimas ASC";
		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<Uzsakovas>(drc, (dre, t) =>
			{
				t.Kryptis = dre.From<string>("kryptis");
				t.Pavadinimas = dre.From<string>("pavadinimas");
				t.Salis = dre.From<string>("salis");
			});

		return result;
	}

	public static Uzsakovas Find(string key)
	{
		var query = $@"SELECT * FROM `uzsakovas` WHERE pavadinimas=?pavadinimas";

		var drc =
			Sql.Query(query, args =>
			{
				args.Add("pavadinimas", key);
			});

		if (drc.Count > 0)
		{
			var result =
				Sql.MapOne<Uzsakovas>(drc, (dre, t) =>
				{
					t.Kryptis = dre.From<string>("kryptis");
					t.Pavadinimas = dre.From<string>("pavadinimas");
					t.Salis = dre.From<string>("salis");
				});

			return result;
		}

		return null;
	}

	public static void Update(Uzsakovas vad)
	{
		var query =
			$@"UPDATE `uzsakovas`
			SET 
				kryptis=?kryptis,
				pavadinimas=?pavadinimas,
				salis=?salis
			WHERE 
				pavadinimas=?pavadinimas";

		Sql.Update(query, args =>
		{
			args.Add("?kryptis", vad.Kryptis);
			args.Add("?pavadinimas", vad.Pavadinimas);
			args.Add("?salis", vad.Salis);
		});
	}

	public static void Insert(Uzsakovas vad)
	{
		var query =
			$@"INSERT INTO `uzsakovas`
			(
				kryptis,
				pavadinimas,
				salis
			)
			VALUES(
                ?kryptis,
				?pavadinimas,
				?salis			
			)";

		Sql.Insert(query, args =>
		{
			args.Add("?kryptis", vad.Kryptis);
			args.Add("?pavadinimas", vad.Pavadinimas);
			args.Add("?salis", vad.Salis);
		});
	}

	public static void Delete(string pavadinimas)
	{
		var query = $@"DELETE FROM `uzsakovas` WHERE pavadinimas=?pavadinimas";
		Sql.Delete(query, args =>
		{
			args.Add("?pavadinimas", pavadinimas);
		});
	}
}
