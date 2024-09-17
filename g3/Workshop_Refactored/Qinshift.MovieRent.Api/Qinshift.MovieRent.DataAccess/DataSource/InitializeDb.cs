using Microsoft.EntityFrameworkCore;
using Qinshift.MovieRent.DomainModels.Enums;
using Qinshift.MovieRent.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Qinshift.MovieRent.DataAccess.DataSource
{
    public static class InitializeDb
    {
        public static void InitDb(ModelBuilder builder)
        {
            builder.Entity<Movie>()
               .HasData(new List<Movie>
               {
                   new() {
                    Id = 1,
                    Title = "The Grand Budapest Hotel",
                    Plot = "A concierge teams up with one of his employees to prove his innocence after he is framed for murder.",
                    ReleaseDate = new DateTime(2014, 3, 28),
                    Genre = Genre.Comedy
                        },
            new() {
                Id = 2,
                Title = "Superbad",
                Plot = "Two high school friends attempt to make it to a party before they go off to college.",
                ReleaseDate = new DateTime(2007, 8, 17),
                Genre = Genre.Comedy
            },
            new() {
                Id = 3,
                Title = "Mad Max: Fury Road",
                Plot = "In a post-apocalyptic world, Max teams up with Furiosa to escape a tyrant and his army.",
                ReleaseDate = new DateTime(2015, 5, 15),
                Genre = Genre.Action
            },
            new() {
                Id = 4,
                Title = "Die Hard",
                Plot = "A NYPD officer tries to save his wife and others taken hostage by German terrorists during a Christmas party.",
                ReleaseDate = new DateTime(1988, 7, 20),
                Genre = Genre.Action
            },
            new() {
                Id = 5,
                Title = "Gone Girl",
                Plot = "A man becomes the focus of an intense media circus when his wife disappears and he is suspected of murder.",
                ReleaseDate = new DateTime(2014, 10, 3),
                Genre = Genre.Thriller
            },
            new() {
                Id = 6,
                Title = "Inception",
                Plot = "A thief who enters the dreams of others is given the chance to erase his criminal record by planting an idea into someone's subconscious.",
                ReleaseDate = new DateTime(2010, 7, 16),
                Genre = Genre.Thriller
            },
            new() {
                Id = 7,
                Title = "The Conjuring",
                Plot = "Paranormal investigators help a family terrorized by a dark presence in their farmhouse.",
                ReleaseDate = new DateTime(2013, 7, 19),
                Genre = Genre.Horror
            },
            new() {
                Id = 8,
                Title = "Get Out",
                Plot = "A young African-American man visits his white girlfriend's family estate, where he uncovers a disturbing secret.",
                ReleaseDate = new DateTime(2017, 2, 24),
                Genre = Genre.Horror
            },
            new() {
                Id = 9,
                Title = "The Shawshank Redemption",
                Plot = "Two imprisoned men bond over a number of ReleaseDates, finding solace and eventual redemption through acts of common decency.",
                ReleaseDate = new DateTime(1994, 9, 23),
                Genre = Genre.Drama
            },
            new() {
                Id = 10,
                Title = "Forrest Gump",
                Plot = "The story of a man with a low IQ, who achieves great things in life despite the odds.",
                ReleaseDate = new DateTime(1994, 7, 6),
                Genre = Genre.Drama
            }
               });

            builder.Entity<User>()
                .HasData(new List<User>
                {
                    new User
                    {
                        Id = 1,
                        FirstName = "Bob",
                        LastName = "Bobsky",
                        UserName = "bob-bobsky",
                        Password = "test123",
                        ConfirmPassword = "test123",
                        CreatedOn = DateTime.Now
                    }
                });
        }
    }
}
