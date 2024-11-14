using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public partial class TaskSolver
{
    // Метод для создания файла с текстом
    private static void CreateFileWithText(string filePath, string text)
    {
        try
        {
            File.WriteAllText(filePath, text);
            Console.WriteLine($"Файл успешно создан: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании файла: {ex.Message}");
        }
    }

    // Метод для нахождения глухих согласных букв, которые входят в каждое нечетное слово и не входят хотя бы в одно четное слово
    public static void FindVoicelessConsonants(string filePath)
    {
        HashSet<char> voicelessConsonants = new HashSet<char> { 'к', 'п', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ' };

        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл создается файла...");
                CreateFileWithText(filePath, "Сутки чай сто храм стол мошка стая погода");
            }

            string[] words = File.ReadAllText(filePath).Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            HashSet<char> oddWordsConsonants = new HashSet<char>(voicelessConsonants);
            HashSet<char> evenWordsConsonants = new HashSet<char>();

            for (int i = 0; i < words.Length; i++)
            {
                HashSet<char> wordConsonants = new HashSet<char>(words[i].ToLower().Where(c => voicelessConsonants.Contains(c)));

                if (i % 2 == 0) 
                {
                    oddWordsConsonants.IntersectWith(wordConsonants);
                }
                else 
                {
                    evenWordsConsonants.UnionWith(wordConsonants);
                }
            }

            HashSet<char> result = new HashSet<char>(oddWordsConsonants);
            result.ExceptWith(evenWordsConsonants);

            Console.WriteLine("Глухие согласные буквы, которые входят в каждое нечетное слово и не входят хотя бы в одно четное слово:");
            if (result.Count > 0)
            {
                foreach (var consonant in result.OrderBy(c => c))
                {
                    Console.WriteLine(consonant);
                }
            }
            else
            {
                Console.WriteLine("Таких букв не найдено.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
        }
    }

    public static void MainForTask4(string[] args)
    {
        string filePath = "task4.txt"; 
        FindVoicelessConsonants(filePath);
    }
}
