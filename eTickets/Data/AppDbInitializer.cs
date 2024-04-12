using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Cinema
                if (!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>()
                    {
                        new Cinema()
                        {
                            Name = "B&B Theatres",
                            Logo = "data:image/png;base64,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",
                            Description ="This is a great movie theater. It's a nice big parking lot. The theaters are well maintained.",
                            City = "OverLand Park",
                            ZipCode = 66212,
                            NumberOfSeats = 75
                        },
                        new Cinema()
                        {
                            Name = "AMC Town Center",
                            Logo = "https://towncenterplaza.com/images/default-source/store-logos/store-logos/amc-theatres48cf467cad8d66169deeff000082b112.tmb-t-400x400.png?sfvrsn=1a422078_7",
                            Description = "This is the description of the first cinema",
                            City = "Missouri",
                            ZipCode = 75071,
                            NumberOfSeats = 40
                        },
                        new Cinema()
                        {
                            Name = "Cinema XD",
                            Logo = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTPmb_YRyZVu7Z-9iHnlP9T_Ih6SpnOJpYvdtXEqQYn&s",
                            Description = "still hasn't lost its touch.The activities and atmosphere of this movie theater makes you feel...",
                            City = "Overland Park",
                            ZipCode = 66230,
                            NumberOfSeats = 40
                        },
                        new Cinema()
                        {
                            Name = "CineTopia Overland Park",
                            Logo = "https://www.thriftynorthwestmom.com/wp-content/uploads/2013/10/Screen-Shot-2013-10-23-at-5.12.53-PM.webp",
                            Description = "Cinetopia launched in 2005, with luxury accommodations including leather seating, state-of-the-art digital projectors and food and alcohol service.",
                            City = "OverLand Park",
                            ZipCode = 66215,
                            NumberOfSeats = 60
                        },
                        new Cinema()
                        {
                            Name = "Boulevard Theatres",
                            Logo = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAoHCBIWEhUUFhUYFBgYGRgZHBgaHB4cEh0ZGRkZGh4YGBgcIS4lHB4sIRgaJjgmKy8xNTU1GiQ7QDszPy40NTUBDAwMEA8QHxISHzorJSs9NDQ2MTQ2MTQ9MTY0MTs2NDQ0NDQ0NDQ0NDQ2NDQ2NDQ0NDQ0NDQ0NDQ0NDQ0NDQ0NP/AABEIAKsBJgMBIgACEQEDEQH/xAAcAAEAAgIDAQAAAAAAAAAAAAAABgcBBQIECAP/xABDEAACAQIEAwYDAwoCCwEAAAABAgADEQQFEiEGBzETIkFRYXEygbGRobIUIzVCYnJzs8HRM5IVJCU0UlOCg8LS8Bf/xAAZAQEAAwEBAAAAAAAAAAAAAAAAAQIDBAX/xAAoEQADAAICAgEDAwUAAAAAAAAAAQIDERIxIUFhBCIyEyOBM1GhscH/2gAMAwEAAhEDEQA/AINERPWO4REQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEAbgRMp8Q9x9YBYWH5WVioLYhASL2CE/fedTMeWeMRS1NkrW/VF1f5Btj9suOl8K+w+k5zz/ANe9nL+rR5lr4d0dkdWRlNirAhgfUGfOX1xdwxTxlIiwWqouj+IPkfNTKJxNBkd0caWRirDyINiJ148qtfJvFqkfOIialxERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBMp8Q9x9ZiZT4h7j6wD03S+FfYfSc5wpfCvsPpOc8k4TEpTmngVp4/Wu3aorkftLdCfmFX75dZlNc2cUGxqIN+zpi/oXJa32AH5zf6f8zTF2QaIid51ADoPOTMctcwO/5r/Of/WQ1eo9x9Z6MzbEVUw1R6KdpUVLonXU3gNphmuoaU+zPJTWtFR1eW+YgEgU2t4B9z7XAkTxOGem7I6lHU2ZTsQZdnCuc5hWqOuJwvYIFuHsRdr/AA2PXaQnm2Kf5ZSK219l37dfiOi/rbV8rSMeSnfGv8ERb3pkEiTLhPgOrikFWoexpH4dru3qoOwX1PWSjFcrcMV7laoreBazC/qLD7pd55T02WeSU9FSxNpnGR1sNiBQrCxJGll3VlJtqU/08JL+JeAqGHwb4harsyBSAwXSbso8B6yzyStfJLpLXyV5Ptg8M1SolNbanZUW+wuxsLn5ze8EZBTxld6TsyhU1XW173A8fCbbNuHaeCzLAojMwd0YlrXBFQDawkVkSfH2Q7W9Gnz3g/FYSkKtXs9JcL3WJa7AkbWHlI9Ll5tf7gv8ZPwvIRwjwPVxY7V27KjcgNa9RrbHSOgHqfslIy7jdETfjbIjEtyryuwpUha1QN5nSftW0r3iXhytgqoWpZkbdXX4WA6jfow8pacs09Isrl+EaWJZmN5dYc4Vq9CrUdtGtA2mzbarbDykR4OyD8txPZMWVFVmZl6gCwAF/Ekj75Kyy03/AGCtNNmhiS/jThehhHo0qLPUqVCe42npsBaw6kmSDJuWCFA2IqsHIuVS2kemojcyHmhLbI5zrZWES135W0e0Fqz9nY3BtrDbW0m1iOvWRjiThWlh8bhcOruVradRa2oan07WFonNFPSCuX0Q+JbL8rMPddNeoBfvAhSSLdAbbbyKcecM0sG9BaJZu0DXDG5uCALfbInNNPSE3LekRGd/JcpqYmstCmVDsCRqNlsu53AMnmRcstSK2JqMrML9mlu6PJmPU+03+TcCU8Li0r0qjFVDqUfc94bFSP6yKzytpPyVrKvRV3EXDlfBMi1ihLhiuhifhte9wP8AiE00sjnJ/i4T92r9acreXx06lNl5e5TYiImhYREQBMp8Q9x9ZiZU2IPtAPTVL4V9h9JzmsyLN6OJorUpNqXoR+srDqrDwM71UNpOkgNbYkXF/UAi4+c8prT0cTOhn+dUsLQarUNgNgv6zN4Ko8TPP2ZY569apXfdnYsfoB7AAD5SacacM5qzmtUb8qUX/wAMW0D0p9be1zIER8p24JmVtPbOjGklsREToNTknUe4+s9GZpj1oYZ67AstNNRA+IgeU85L1HuPrPRmIqYapTNN2psjLZlLLYjyO85fqe0YZfRHMk5g4bEV0oLTqIzkhSbFbgFt7HbYGR/mbw5TWpTxSkr2tRKdQEki7bBxfptsR7SYYXLsqw7dqq4ekyg9+6gjbexJ2lfcyOKaeJZKFBtVNCWZx8LPaw0+YAPXzPpKY1u1xTS9lZX3eCf8ZY18JlzNQGkqERSB8CkhdVvQSreFuJMYuMpfnalQPUVGRmLBgxAOx8Re9x5Se8M8X4XFYbscQyq4TS61LBXUDdlv12G463kT5bYjCrjK71FRAFZ6bMbLTAfdQSeull3/AGfWTC4zSpeS0rSaaJXzUw6HD0KhHeWuig+NmvcfcJsuYA/2VX/dT8aSvuYPFa4p0p0TelTOoPb4n6agPIeHvLCyTOsJj8IEZkuyaalFiA4NrEW6kbXBHpKuamZb9EOXKTZBuUY/1yp/CP4hNxx9+lcu90/miSDJsNlmCqGjSdFdwWOp9T2XwLE7DfYeO8jHHGKptmmXsrqVUpdgwKj86DuQdpKrnk5Jev8AgT5Vs2/N39Hj+Mv4Xkoy/DImEp01bQq0lUMLAgaQNVztfxkQ5qYyk+AUK6Me2Q2VlY2s+9gZ8+CeMcPUw64XEsquqhAalgjqBYAk7XttY9ZTjTxrXpleLcm0y7hbD0ay1lxdYsDc6qoKtvuGB6gz48z+yfL2N1ZldCtiC1ybG3yM5Lwlk1FjXZaencgVKl6I9lY2+tvCQDjfNMC7LSwdGmqobtVRApY9Aqm17Dz8ZeFytPz4+CZW6ROeVecdrhWw7G7UCAL+KNcqfkQw+Qm14U4dXCNimtbtKrMD5UxcqvsNTSq+A83/ACfGozGyP+ba/SzHYn2a0s7jrPko4Koabqz1LU10sCbuDc7HwUExlhq9Lpk1LVaXsheV5guLz9ah3VWcIPDTTR9J+0FvnN9zXzOvSpUUpsyK5bUymxOkCy3HTrf5Srspx74evTrp8SMGA8D4EH3BI+cuyhmeXZlQCsyODYmmxAqK3tcEH1EtknhSetpE0uNJ+iIcqc3xDVqlFmapT0a+8S2lrgbE9Ab9J2uP/wBLZf7p/NkpytctwbdhRNKkzAsRqu5C7XZmJP2mRDjrFU2zTAMHRlBS5DAqLVb7kdJRPlk2loqnutokPMzMatDA3pMUZ6ioWXZgpVmNj4X02+crHhKoauY4UVGZx2l++xbcKzDr6gSe81sXTfBUwrox7dDZWBNtFTewPrKpwWJelUSqvxIyuvupmuGf23/JfGvtLf5pZlXo4WmKTMmt9LMuzaQCdII6XMjXK3NsScWaBd6lNkZiGJYKVtZgT0veTTLs6wGY4cI5QlgNVFyA4YeQvc+hE+mWUcrwT9lSNKk732L3qELv3mYk29zMU9Q5a8lE9S015Ifzk/xcL+7V/FTlbyw+bmIR6mFKsrWWrfSQ1rmn1t0leTqwfgjbH+KERE1LiIiAIiIBMuV+aNTxopX7lYFSPDUoLKffqPnLslFcuME1TMaTDpS1Ox8B3So+0tL1nB9Trmc2XXIwZB+NuCUxKtWogJXAvYbLUt4N5N5H7ZOYmU05e0ZzTl7R5jdCpKsCpBIIOxBBsQZxlhc1siCVVxSiy1O648NY6N8x9JXs9LHaudo65rktiY0DyE5KNwPUS7U5eZbYHsm6f8b/AN5W8sx2KpT2UeEHlMy8P/zvLP8AlN/nf+81OecEYCm2GCIwFSulNu+xurBiQLnbpKL6iG/ZRZZZU1pgiWBzE4VwuEw9J6KsrNV0HUxYadDt4+qiV+HHmJrFq1tF5pUtozMETMwTLFhpHlAWAwPQgzMAwFEzME/KAw8xAMaR5TlMBpMMr4Br18MuJFamqMhcAglrAE2NhbwlatT2Q6S7IgZgKPKFa4vGoecsSZmCJgMD0IMyTaANI8hAAgMD0N5mAYCiZiIBgiNImYgGABMxEkCIiQBERAE7mU5c+IrLRp6dTEgajZdhc3PsJ053Moxho4mjWH6lRGPsGGofMXHzlXvXgh9F4cJcNU8FS0g6naxd7WLEeAHgBJFOCMCARuCL/IznPMptvbONtt7ZmIiQQRjmDhQ+XV/NVDj3Qg/S8oeegeNKgXL8UT/ynHzYaR9Z5+nb9N0zow9CbTJcVVOJoA1HI7RNtbW+MbdZq53sk/3qh/Fp/jE3rpmr6Ll5ksRllYgkG9PcGx/xF8RKp4QrO2YYQM7MO1XYsSPHzMtXmZ+jK3vT/mLKn4N/SOF/ir/Wc+H+m/5/0Y4/xZd+eYfCtTV8SEKUm19/4A1ityD16nb1nTwT5djaRCLSrIO6RpAI28iAV95p+bB/1AetVP6maHk2353Fjw00j9jVP7zGY/bdb6KKft5EazLhtlzNsFTN9TjSTvZGGu5/dF/8ssz/AEdlmWUA9RU8BrdQ1Zm8l8fkJpcTWReJULWGqmFB/aNNgP7fOTLiDNFoUw7UKlZb2OhQxX1IJ6estkuq4r4Jqm9I1GGGVZnTbSqOV2J06ay36G9r2+6VbnHDdSjjhg1OouyhGPirnYt7b39pamXcVJUUtRwOKI6Eimij73F5Fs0zpHzvCM1N6OgBGWoFDXfXpPdJFu8PGTjqk2kTDabJbhMmy/L8PrdaY0gBqrqC7MfLqdz0AmMtrZXjqgq01p1Hp36ppezC3eVgLjfxnR5o5VXr4WmaSl+zqamQbsVKldQHiRf7CZGOWOSYlcZ27I1OmqOpLArrJ2AAPXz/AOkSqScOm/JCW5bb8m05hUcNSxWXsyU0p9oS9lGkqCl9QA7wtfaTrL6+HbDK9IL2JUlQq6U0WN7Jbp12tK+5ydcL/wBz/wAZKuDxfKaAG5NEj52ItIpbxyxS+1Mr3jXM8BXqYT8kVAFZtemnoB1NT037o1dG/wDjLRx2W4FE7SpRoqqHWWKKACAdztv1nn/CIy1KYYFSGQEEWNww2l1czyf9GVf3qX81JrlnTmUy1r8UiI8bZlhcc+FoYRgzmoV2UqO9YA3IFx1krw2S5dlmH7WqqErbVVddVQsfBRbb0AlYcD1kTMcMzbDWRfwuyMo+8iXZnuYLQpdo1GpXUHcIoZh+1pJ6e0rlTlqV0L+3Uro02CxOVZmjqio5UbgppqqD0INrgeolVcXZC2DxLUrlkYa0Y9SpvsfUEWlqZZxYlQFqOCxLAbEimq/K5YXkD5mZp21eiDRqUmRGutQKGOoggjSTtsftk4eSrXoRtVr0QyIidhuIiIAiIgCIiAIiIAmDMxAL/wCCcf22AoPe5CaW/eTun6TfytuT2PvTxFAn4GV19nFj96g/9UsmeZknjTRx2tU0IidLNcxpYek9aqwVVFyfEnwAHiSdgJRLfgqQ3mxmoTCrQB71VrkeOhdyfa9hKgm14kzlsXiWrtsD3UXwVBew99yT6kzVT0cUcZ0dkTxnQnYy+uErU3NyEdGIHWysDt9k68TRlixeLeOsNisHUoIlQMxQgsBp7rqx6HyEhWQ45aGKoVmBKo4YgfEQPK818Ss45U8UVUJLSJ7xrxph8ZhhRRHVg6tdrWsL+R9ZrOA+JKWCes1RWYOqKNNv1SxN7n9qRWJCxyp4+gpXHib7i3O1xGMOIpa07qWvs4ZPEWkuyPmcAiriaZZhtrS3e9Sh6H2+6VnEPFNSk/QcS1otPNeaFMIVw9Ji1tmewQeukG5lZYvEvUdqjsWZjqZj1J/pPjEmMcz0JhLosvh/mYFpqmJRnZQB2iWJYDxZTbve079bmjh+0UJScpvqY2DdDYKt/O3WVLEo8EN70VeKdkv484oo400ezVl7PXfVb9bT0t7Tt8F8dDC0uwrIzIpJVltqUMblSD1F7ke8gsS36U8ePotwXHRNuOOKMNivyc0VcGm5ZtShbg6eljue7O7xjxzhsVg3oIjqzMhBYDT3HVj0PkJXkSFila+COC8fAUkG4NiNwR1uPESx+H+ZhRAmJRnK7dolrkftKfH1HWVxEtUTa1RNSq7LWzHmhRCkUKLs3gXsqA+ZANzKxx+OqVqjVajFnc3J/oB4AeAnXiRGOY6Eyp6ERE0LCIiAIiIAiIgCIiAIiIBLeWmP7PMEW9hVVkPvbUPwy7XqgC5IAHUk2H2meZ0cqQykqRuCCQwPmCNwZyr1nf43d/32LfiMwyYOdb2ZVj5PZd2eceYKgCFcVnHRENxf1boBKn4j4kxGMfVUIVB8NNfhX19T6zSxJjDM+V2WmFIiImxcREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQBERAEREAREQD/2Q==",
                            Description = "Boulevard theatre consists mostly of comedies but also dramas. In general, the characters are simply drawn, ordinary or easily understandable. There is a strong tendency to avoid touchy subjects, such as politics and religion. The style is not designed to challenge preconceived ideas or offend.",
                            City = "Missourri",
                            ZipCode = 75271,
                            NumberOfSeats = 50
                        },
                        new Cinema()
                        {
                            Name = "Glen Wood Theatre",
                            Logo = "https://live.staticflickr.com/2089/2348999965_eee2a013ee_b.jpg",
                            Description = "Glenwood arts theater is my favorite movie theater in Kansas City! The two brothers who own this....",
                            City = "Kansas City",
                            ZipCode = 66645,
                            NumberOfSeats = 60
                        },
                        new Cinema()
                        {
                            Name = "DickinSon Theatres",
                            Logo = "https://upload.wikimedia.org/wikipedia/en/0/07/Dickinson_Theatres_Logo.jpg",
                            Description = "Dickinson Theatres was an American movie theater chain based in Overland Park, Kansas. It was founded on November 1920 in Manhattan Kansas by Glen W. Dickinson after leaving his family business to purchase a small theater, renaming it to The Dickinson Marshall Theatre.",
                            City = "OverLand Park",
                            ZipCode = 66794,
                            NumberOfSeats = 60
                        },
                        new Cinema()
                        {
                            Name = "Pharaoh Theatre",
                            Logo = "https://img.freepik.com/free-vector/flat-design-cleopatra-logo-template_23-2150132592.jpg",
                            Description = "The Pharaoh is a nice little theater on the Square, it only has 4 screens, but they show the current top movies, including in 3D. The ticket prices are dollars below the competition and the concession prices are far below what you pay at other theaters.",
                            City = "OverLand Park",
                            ZipCode = 66895,
                            NumberOfSeats = 60
                        },
                        new Cinema()
                        {
                            Name = "Regal South Wind",
                            Logo = "data:image/png;base64,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",
                            Description = "It's overpriced like every other movie theater and has all of the same concession stand offerings. It only has 12 auditoriums so, while they have most of the big movies",
                            City = "Missouri",
                            ZipCode = 75471,
                            NumberOfSeats = 60
                        },
                        new Cinema()
                        {
                            Name = "Regnier Screen Theatre",
                            Logo = "data:image/png;base64,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",
                            Description = "Ideal for grand galas, receptions, weddings, and corporate events. This state-of-the-art, 400 seat venue boasts the largest movie screen in the Midwest, a massive stage, and a versatile new lobby that makes for a magnificent event space.",
                            City = "Missouri",
                            ZipCode = 75356,
                            NumberOfSeats = 90
                        },
                    });
                    context.SaveChanges();
                }
                //Actors
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {
                            FullName = "Actor 1",
                            Bio = "This is the Bio of the first actor",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-1.jpeg"

                        },
                        new Actor()
                        {
                            FullName = "Actor 2",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-2.jpeg"
                        },
                        new Actor()
                        {
                            FullName = "Actor 3",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-3.jpeg"
                        },
                        new Actor()
                        {
                            FullName = "Actor 4",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-4.jpeg"
                        },
                        new Actor()
                        {
                            FullName = "Actor 5",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-5.jpeg"
                        }
                    });
                    context.SaveChanges();
                }
                //Producers
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer()
                        {
                            FullName = "Producer 1",
                            Bio = "This is the Bio of the first actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-1.jpeg"

                        },
                        new Producer()
                        {
                            FullName = "Producer 2",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-2.jpeg"
                        },
                        new Producer()
                        {
                            FullName = "Producer 3",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-3.jpeg"
                        },
                        new Producer()
                        {
                            FullName = "Producer 4",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-4.jpeg"
                        },
                        new Producer()
                        {
                            FullName = "Producer 5",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-5.jpeg"
                        }
                    });
                    context.SaveChanges();
                }
                //Movies
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {
                            Name = "Life",
                            Description = "This is the Life movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-3.jpeg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(10),
                            //CinemaId = 3,
                            ProducerId = 3,
                            MovieCategory = MovieCategory.Documentary
                        },
                        new Movie()
                        {
                            Name = "The Shawshank Redemption",
                            Description = "This is the Shawshank Redemption description",
                            Price = 29.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-1.jpeg",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(3),
                            //CinemaId = 1,
                            ProducerId = 1,
                            MovieCategory = MovieCategory.Action
                        },
                        new Movie()
                        {
                            Name = "Ghost",
                            Description = "This is the Ghost movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-4.jpeg",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(7),
                            //CinemaId = 4,
                            ProducerId = 4,
                            MovieCategory = MovieCategory.Horror
                        },
                        new Movie()
                        {
                            Name = "Race",
                            Description = "This is the Race movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-6.jpeg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-5),
                            //CinemaId = 1,
                            ProducerId = 2,
                            MovieCategory = MovieCategory.Documentary
                        },
                        new Movie()
                        {
                            Name = "Scoob",
                            Description = "This is the Scoob movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-7.jpeg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-2),
                           // CinemaId = 1,
                            ProducerId = 3,
                            MovieCategory = MovieCategory.Cartoon
                        },
                        new Movie()
                        {
                            Name = "Cold Soles",
                            Description = "This is the Cold Soles movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-8.jpeg",
                            StartDate = DateTime.Now.AddDays(3),
                            EndDate = DateTime.Now.AddDays(20),
                            //CinemaId = 1,
                            ProducerId = 5,
                            MovieCategory = MovieCategory.Drama
                        }
                    });
                    context.SaveChanges();
                }
                //Cinema IDs
                if (!context.MovieCinemas.Any())
                {
                    context.MovieCinemas.AddRange(new List<Movie_Cinema>()
                    {
                        new Movie_Cinema()
                        {
                            MovieId = 1,
                            CinemaId = 1
                        },
                        new Movie_Cinema()
                        {
                            MovieId = 2,
                            CinemaId = 1
                        },
                        new Movie_Cinema()
                        {
                            MovieId = 3,
                            CinemaId = 1
                        },
                        new Movie_Cinema()
                        {
                            MovieId = 1,
                            CinemaId = 2
                        },
                        new Movie_Cinema()
                        {
                            MovieId = 1,
                            CinemaId = 3
                        },
                        new Movie_Cinema()
                        {
                            MovieId = 3,
                            CinemaId = 2
                        },
                        new Movie_Cinema()
                        {
                            MovieId = 3,
                            CinemaId = 3
                        },new Movie_Cinema()
                        {
                            MovieId = 4,
                            CinemaId = 4
                        },
                        new Movie_Cinema()
                        {
                            MovieId = 5,
                            CinemaId = 3
                        },
                        new Movie_Cinema()
                        {
                            MovieId = 5,
                            CinemaId = 2
                        },


                    });
                    
                }
                //Actors & Movies
                if (!context.Actors_Movies.Any())
                {
                    context.Actors_Movies.AddRange(new List<Actor_Movie>()
                    {
                        new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 1
                        },
                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 1
                        },

                         new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 2
                        },
                         new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 2
                        },

                        new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 3
                        },
                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 3
                        },
                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 3
                        },


                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 4
                        },
                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 4
                        },
                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 4
                        },


                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 5
                        },
                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 5
                        },
                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 5
                        },
                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 5
                        },


                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 6
                        },
                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 6
                        },
                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 6
                        },
                    });
                    context.SaveChanges();
                }
            }

        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@tickets.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if(adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin@1234");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Admin@1234");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
