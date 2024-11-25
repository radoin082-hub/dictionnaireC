using System;
using System.IO;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        string directoryPath = @"C:\Users\radoi\source\repos\dictionnaireC#\dictionnaireC#";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Define random data for films
        string[] titles = { "Parasite", "Inception", "Interstellar", "Avatar", "The Godfather", "Gladiator", "The Dark Knight" };
        string[] descriptions = {
            "A young woman must outsmart a ruthless corporation to protect her family and future.",
            "An unlikely hero rises in a time of war, proving that courage comes in many forms.",
            "A mysterious disappearance forces a detective to uncover dark family secrets.",
            "A poor family’s rise as they infiltrate the lives of the rich and powerful.",
            "An epic journey of survival and revenge in a harsh wilderness.",
            "A tale of love and loss set against the backdrop of a futuristic world."
        };
        string[] images = { 
            "/image/asd.jpg",
            "/image/avatar.jpg",
            "/image/avengers.jpg",
            "/image/dark_knight.jpg",
            "/image/dark_knight_rises.jpg",
            "/image/django_unchained.jpg",
            "/image/fight_club.jpg",
            "/image/forrest_gump.jpg",
            "/image/gladiator.jpg",
            "/image/godfather.jpg ",                                                                            
            "/image/inception.jpg",
            "/image/interstellar.jpg",
            "/image/lion_king.jpg",
            "/image/matrix.jpg",
            "/image/MV5B_.jpg",
            "/image/parasite.jpg",
            "/image/prestige.jpg",
            "/image/pulp_fiction.jpg",
            "/image/schindlers_list.jpg",
            "/image/shawshank_redemption.jpg",
            "/image/silence_of_the_lambs.jpg",
            "/image/titanic.jpg",
            "/image/2264086.jpg-c_310_420_x-f_jpg-q_x-xxyxx.jpg",
            "/image/vbG0zu0lIVDZZaUVOZuBIE9kno3.jpg",
        };
        string[] categories = { "Historical", "Animation", "Western", "Drama", "Action", "Sci-Fi" };

        Random random = new Random();

        for (int i = 1; i <= 100; i++)
        {
            string fileName = $"file{i}.xml";
            string filePath = Path.Combine(directoryPath, fileName);

            // Generate random film data
            var xmlContent = new XDocument(
                new XElement("films",
                    new XElement("film",
                        new XElement("title", titles[random.Next(titles.Length)]),
                        new XElement("description", descriptions[random.Next(descriptions.Length)]),
                        new XElement("image", images[random.Next(images.Length)]),
                        new XElement("category", categories[random.Next(categories.Length)])
                    )
                )
            );

            try
            {
                xmlContent.Save(filePath);
                Console.WriteLine($"Created: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating file {filePath}: {ex.Message}");
            }
        }

        Console.WriteLine("XML file creation completed!");
    }
}
