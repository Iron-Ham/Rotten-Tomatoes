﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace RT
{
	public class RTRepository
	{

		public RTRepository ()
		{
		}

		public async Task<TopBoxRootObject> RetrieveTopBox()
		{
			var client = new HttpClient ();
			var result = await client.GetStringAsync ("http://api.rottentomatoes.com/api/public/v1.0/lists/movies/box_office.json?apikey=mrfz362ume2tmxb7ugbm6p22");
			return JsonConvert.DeserializeObject<TopBoxRootObject> (result);
		}
		public async Task<InTheatersRootObject> RetrieveInTheaters()
		{
			var client = new HttpClient ();
			var result = await client.GetStringAsync (RTApiUrls.inTheaters);
			return JsonConvert.DeserializeObject<InTheatersRootObject> (result);
		}
		public async Task<OpeningRootObject> RetrieveOpeningMovies()
		{
			var client = new HttpClient ();
			var result = await client.GetStringAsync (RTApiUrls.openingMovies);
			return JsonConvert.DeserializeObject<OpeningRootObject> (result);
		}
	}


	public class ReleaseDates
	{
		public string theater { get; set; }
		public string dvd { get; set; }
	}

	public class Ratings
	{
		public int critics_score { get; set; }
		public int audience_score { get; set; }
		public string critics_rating { get; set; }
		public string audience_rating { get; set; }
	}

	public class Posters
	{
		public string thumbnail { get; set; }
		public string profile { get; set; }
		public string detailed { get; set; }
		public string original { get; set; }
	}

	public class AbridgedCast
	{
		public string name { get; set; }
		public string id { get; set; }
		public List<string> characters { get; set; }
	}

	public class AlternateIds
	{
		public string imdb { get; set; }
	}

	public class Links
	{
		public string self { get; set; }
		public string alternate { get; set; }
		public string cast { get; set; }
		public string reviews { get; set; }
		public string similar { get; set; }
	}

	public class Movie : IMovie
	{
		public string id { get; set; }
		public string title { get; set; }
		public int year { get; set; }
		public string mpaa_rating { get; set; }
		public int runtime { get; set; }
		public ReleaseDates release_dates { get; set; }
		public Ratings ratings { get; set; }
		public string synopsis { get; set; }
		public Posters posters { get; set; }
		public List<AbridgedCast> abridged_cast { get; set; }
		public AlternateIds alternate_ids { get; set; }
		public Links links { get; set; }
		public string critics_consensus { get; set; }
	}

	public class Links2
	{
		public string self { get; set; }
		public string alternate { get; set; }
	}

	public class OpeningRootObject
	{
		public List<Movie> movies { get; set; }
		public Links2 links { get; set; }
		public string link_template { get; set; }
		public OpeningRootObject(){
			movies = new List<Movie> ();
		}
	}
		
	public class Movie2 : IMovie
	{
		public string id { get; set; }
		public string title { get; set; }
		public int year { get; set; }
		public string mpaa_rating { get; set; }
		public int runtime { get; set; }
		public string critics_consensus { get; set; }
		public ReleaseDates release_dates { get; set; }
		public Ratings ratings { get; set; }
		public string synopsis { get; set; }
		public Posters posters { get; set; }
		public List<AbridgedCast> abridged_cast { get; set; }
		public AlternateIds alternate_ids { get; set; }
		public Links links { get; set; }
	}

	public class TopBoxRootObject
	{
		public List<Movie2> movies { get; set; }
		public Links2 links { get; set; }
		public string link_template { get; set; }
		public TopBoxRootObject() {
			movies = new List<Movie2> ();
		}
	}


	public class Movie3 : IMovie
	{
		public string id { get; set; }
		public string title { get; set; }
		public int year { get; set; }
		public string mpaa_rating { get; set; }
		public int runtime { get; set; }
		public string critics_consensus { get; set; }
		public ReleaseDates release_dates { get; set; }
		public Ratings ratings { get; set; }
		public string synopsis { get; set; }
		public Posters posters { get; set; }
		public List<AbridgedCast> abridged_cast { get; set; }
		public AlternateIds alternate_ids { get; set; }
		public Links links { get; set; }
	}

	public interface IMovie 
	{
		string title {get; set;}
		string mpaa_rating { get; set; }
		string critics_consensus { get; set; }
		int runtime { get; set; }
		Ratings ratings { get; set; }

		ReleaseDates release_dates { get; set; }
		Posters posters { get; set; }


	}

	public class Links3
	{
		public string self { get; set; }
		public string next { get; set; }
		public string alternate { get; set; }
	}

	public class InTheatersRootObject
	{
		public int total { get; set; }
		public List<Movie3> movies { get; set; }
		public Links3 links { get; set; }
		public string link_template { get; set; }
		public InTheatersRootObject () {
			movies = new List<Movie3> ();
		}
	}
}
