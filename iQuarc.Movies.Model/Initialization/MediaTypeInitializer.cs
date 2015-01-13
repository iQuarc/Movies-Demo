using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace iQuarc.Movies.Model.Initialization
{
	internal static class MediaTypeInitializer
	{
		internal static void Initialize(DbContext context)
		{
			DbSet<MediaTypeEntity> entities = context.Set<MediaTypeEntity>();

			entities.Add(new MediaTypeEntity { Id = 1, Name = "Image" });
			entities.Add(new MediaTypeEntity { Id = 2, Name = "Audio" });
			entities.Add(new MediaTypeEntity { Id = 3, Name = "Video" });
		}
	}

	internal static class DataInitializer
	{
		internal static void Intialize(DbContext context)
		{
			DbSet<MovieEntity> movies = context.Set<MovieEntity>();

			List<MovieEntity> moviesData = new List<MovieEntity>
			                               {
				                               new MovieEntity
				                               {
					                               Name = "Interstellar",
					                               ReleaseDate = new DateTime(2014, 11, 7)
				                               },
											   new MovieEntity
											   {
												   Name = "Guardians of the Galaxy",
												   ReleaseDate = new DateTime(2014, 8, 1)
											   }
			                               };

			moviesData.ForEach(e => movies.Add(e));

			DbSet<MediaEntity> media = context.Set<MediaEntity>();

			List<MediaEntity> mediaData = new List<MediaEntity>
			                              {

				                              media.Add(new MediaEntity
				                                        {
					                                        MediaTypeId = 1,
					                                        MediaUrl = "http://ia.media-imdb.com/images/M/MV5BMjIxNTU4MzY4MF5BMl5BanBnXkFtZTgwMzM4ODI3MjE@._V1__SX1857_SY927_.jpg",
					                                        Name = "Interstellar (2014) Poster",
															Movie = moviesData[0]
				                                        })
			                              };

			mediaData.ForEach(e => media.Add(e));
		}
	}
}
