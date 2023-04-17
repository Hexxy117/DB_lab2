namespace Vaistai.Repositories;

using MySql.Data.MySqlClient;
using Vaistai.Models;


/// <summary>
/// Database operations related to 'Talpa' entity.
/// </summary>
public class TalpaRepo
{
	public static List<Talpa> List()
	{
		var query = $@"SELECT * FROM `talpa` ORDER BY id ASC";
		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<Talpa>(drc, (dre, t) =>
			{
				t.Rusis = dre.From<string>("rusis");
				t.Turis = dre.From<string>("turis");
				t.Id = dre.From<int>("id");
			});

		return result;
	}

	public static Talpa Find(int key)
	{
		var query = $@"SELECT * FROM `Talpa` WHERE id=?id";

		var drc =
			Sql.Query(query, args =>
			{
				args.Add("id", key);
			});

		if (drc.Count > 0)
		{
			var result =
				Sql.MapOne<Talpa>(drc, (dre, t) =>
				{
					t.Rusis = dre.From<string>("rusis");
					t.Turis = dre.From<string>("turis");
					t.Id = dre.From<int>("id");
				});

			return result;
		}

		return null;
	}

	public static void Update(Talpa vad)
	{
		var query =
			$@"UPDATE `Talpa`
			SET 
				rusis=?rusis,
				turis=?turis
			WHERE 
				id=?id";

		Sql.Update(query, args =>
		{
			args.Add("?rusis", vad.Rusis);
			args.Add("?turis", vad.Turis);
			args.Add("?id", vad.Id);
		});
	}

	public static void Insert(Talpa vad)
	{
		var query =
			$@"INSERT INTO `Talpa`
			(
				turis,
				rusis
			)
			VALUES(
                ?turis,
				?rusis		
			)";

		Sql.Insert(query, args =>
		{
			args.Add("?rusis", vad.Turis);
			args.Add("?turis", vad.Rusis);
		});
	}

	public static void Delete(int id)
	{
		var query = $@"DELETE FROM `Talpa` WHERE id=?id";
		Sql.Delete(query, args =>
		{
			args.Add("?id", id);
		});
	}
}
