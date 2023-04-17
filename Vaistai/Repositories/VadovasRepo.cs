namespace Vaistai.Repositories;

using MySql.Data.MySqlClient;
using Vaistai.Models;


/// <summary>
/// Database operations related to 'Vadovas' entity.
/// </summary>
public class VadovasRepo
{
	public static List<Vadovas> List()
	{
		var query = $@"SELECT * FROM `vadovas` ORDER BY id ASC";
		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<Vadovas>(drc, (dre, t) =>
			{
				t.Id = dre.From<int>("id");
				t.Vardas = dre.From<string>("vardas");
				t.Pavarde = dre.From<string>("pavarde");
				t.Pastas = dre.From<string>("el__pastas");
				t.TelNumeris = dre.From<string>("tel__numeris");
			});

		return result;
	}

	public static Vadovas Find(int id)
	{
		var query = $@"SELECT * FROM `vadovas` WHERE id=?id";

		var drc =
			Sql.Query(query, args =>
			{
				args.Add("?id", id);
			});

		if (drc.Count > 0)
		{
			var result =
				Sql.MapOne<Vadovas>(drc, (dre, t) =>
				{
					t.Id = dre.From<int>("id");
					t.Vardas = dre.From<string>("vardas");
					t.Pavarde = dre.From<string>("pavarde");
					t.Pastas = dre.From<string>("el__pastas");
					t.TelNumeris = dre.From<string>("tel__numeris");
				});

			return result;
		}

		return null;
	}

	public static void Update(Vadovas vad)
	{
		var query =
			$@"UPDATE `vadovas`
			SET 
				vardas=?vardas, 
				pavarde=?pavarde, 
                el__pastas=?el__pastas,
                tel__numeris=?tel__numeris
			WHERE 
				id=?id";

		Sql.Update(query, args =>
		{
			args.Add("?vardas", vad.Vardas);
			args.Add("?pavarde", vad.Pavarde);
			args.Add("?el__pastas", vad.Pastas);
			args.Add("?tel__numeris", vad.TelNumeris);
			args.Add("?id", vad.Id);
		});
	}

	public static void Insert(Vadovas vad)
	{
		var query =
			$@"INSERT INTO `vadovas`
			(
                vardas,
                pavarde,
                el__pastas,
                tel__numeris	
			)
			VALUES(
                ?vardas, 
                ?pavarde, 
                ?el__pastas,
                ?tel__numeris				
			)";

		Sql.Insert(query, args =>
		{
			args.Add("?vardas", vad.Vardas);
			args.Add("?pavarde", vad.Pavarde);
			args.Add("?el__pastas", vad.Pastas);
			args.Add("?tel__numeris", vad.TelNumeris);
		});
	}

	public static void Delete(int id)
	{
		var query = $@"DELETE FROM `vadovas` WHERE id=?id";
		Sql.Delete(query, args =>
		{
			args.Add("?id", id);
		});
	}
}
