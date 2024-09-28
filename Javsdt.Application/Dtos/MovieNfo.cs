using Javsdt.Shared.Configuration;
using Javsdt.Application.Helpers.Base;
using Javsdt.Domain.Entitys;
using Javsdt.Shared.Constants;
using System.Xml.Serialization;

namespace Javsdt.Application.Dtos
{
    [XmlRoot("movie")]
    public class MovieNfo
    {
        public MovieNfo() { }

        public MovieNfo(Movie movie, AssembleDto dto)
        {
            Num = movie.Car;
            Plot = $"{(SettingsHolder.Standard.Nfo.NeedZhPlot && !string.IsNullOrEmpty(movie.ZhPlot) ? movie.ZhPlot : movie.Plot)}{movie.Review}";
            Title = AssembleHelper.AssembleNfoTitleFunction(dto);
            OriginalTitle = $"{movie.Car} {movie.Title}";
            Rating = (movie.Score / 10.0).ToString("0.0");
            CriticRating = movie.Score;
            Year = movie.Release?.Year;
            Premiered = Release = movie.Release?.ToString("yyyy-MM-dd") ?? MediaConstant.DEFAULT_RELEASE;
            Runtime = movie.Runtime;
            Mpaa = "NC-17";
            CustomRating = "NC-17";
            Country = "日本";
            CountryCode = "JP";
            Studios = movie.Makers.Union(movie.Publishers).ToList();
            if (movie.Series != null)
            {
                Set = new Set() { Name = movie.Series };
            }
            List<string> tags = movie.Tags;
            List<string> extraTags = AssembleHelper.CollectPropertiesFunction(dto).ToList();
            Tags = [.. tags, .. extraTags];
            Actors = movie.Actors.Select(a => new Actor() { Name = a, Type = "Actor" }).ToList();
            Directors = movie.Directors.Select(d => d).ToList();
        }

        [XmlElement("id")]
        public string? Id { get; set; }

        [XmlElement("num")]
        public string Num { get; set; } = default!;

        [XmlElement("title")]
        public string Title { get; set; } = default!;

        [XmlElement("originaltitle")]
        public string OriginalTitle { get; set; } = default!;

        /// <summary>
        /// 如果没有翻译但用户在ini里还要PlotZh，则给Plot
        /// </summary>
        [XmlElement("plot")]
        public string? Plot { get; set; }

        [XmlElement("rating")]
        public string Rating { get; set; } = default!;

        [XmlElement("criticrating")]
        public int CriticRating { get; set; }

        [XmlElement("year")]
        public int? Year { get; set; }

        [XmlElement("premiered")]
        public string? Premiered { get; set; }

        [XmlElement("release")]
        public string? Release { get; set; }

        [XmlElement("runtime")]
        public int Runtime { get; set; }

        [XmlElement("country")]
        public string Country { get; set; } = default!;

        [XmlElement("countrycode")]
        public string CountryCode { get; set; } = default!;

        [XmlElement("mpaa")]
        public string Mpaa { get; set; } = default!;

        [XmlElement("customrating")]
        public string CustomRating { get; set; } = default!;

        [XmlElement("studio")]
        public List<string> Studios { get; set; } = [];

        [XmlElement("set")]
        public Set? Set { get; set; }

        [XmlElement("tag")]
        public List<string> Tags { get; set; } = [];

        [XmlElement("actor")]
        public List<Actor> Actors { get; set; } = [];

        [XmlElement("director")]
        public List<string> Directors { get; set; } = [];
    }

    public class Set
    {
        [XmlElement("name")]
        public string Name { get; set; } = default!;
    }

    public class Actor
    {
        [XmlElement("name")]
        public string Name { get; set; } = default!;

        [XmlElement("type")]
        public string Type { get; set; } = default!;
    }
}
