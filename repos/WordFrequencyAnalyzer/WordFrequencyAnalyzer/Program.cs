using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Анализатор частоты слов ===");
        Console.WriteLine("Введите текст для анализа (для завершения ввода нажмите Enter дважды):");

        string input = ReadMultilineText();
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Текст не введён!");
            return;
        }

        string noPunctuationText = new string(input.Where(c => !char.IsPunctuation(c)).ToArray());

        string[] words = noPunctuationText.ToLower().Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        Dictionary<string, int> wordCount = new Dictionary<string, int>();
        foreach (string word in words)
        {
            if (wordCount.ContainsKey(word))
                wordCount[word]++;
            else
                wordCount[word] = 1;
        }

        var topWords = wordCount.OrderByDescending(pair => pair.Value).Take(10).ToList();

        Console.WriteLine("\n" + new string('=', 40));
        Console.WriteLine("Результаты анализа");
        Console.WriteLine(new string('=', 40));

        if (topWords.Count == 0)
        {
            Console.WriteLine("В тексте не найдено слов.");
            return;
        }

        Console.WriteLine($"Всего уникальных слов: {wordCount.Count}");
        Console.WriteLine($"Общее количество слов: {words.Length}");
        Console.WriteLine("\nТоп-10 самых частых слов:");
        Console.WriteLine(new string('-', 40));

        for (int i = 0; i < topWords.Count; i++)
        {
            var word = topWords[i];
            double percentage = (double)word.Value / words.Length * 100; 
            Console.WriteLine($"{i + 1,2}. {word.Key,-15} - {word.Value,4} раз ({percentage:0.00}%)");
        }

        Console.WriteLine("\n" + new string('=', 40));
        Console.WriteLine("Дополнительная статистика");
        Console.WriteLine(new string('=', 40));

        if (wordCount.Count > 10) 
        {
            var lessFrequent = wordCount.OrderBy(pair => pair.Value).ThenBy(pair => pair.Key).Take(5).ToList(); 

            Console.WriteLine("\n5 самых редких слов:");
            foreach (var item in lessFrequent)
            {
                Console.WriteLine($"  {item.Key} - {item.Value} раз");
            }
        }
        else
        {
            Console.WriteLine("\nНедостаточно уникальных слов для анализа редких.");
        }
    }

    static string ReadMultilineText()
    {
        List<string> lines = new List<string>();
        while (true)
        {
            string line = Console.ReadLine();
            if (string.IsNullOrEmpty(line))
            {
                if (lines.Count > 0) break;
            }
            else
            {
                lines.Add(line);
            }
        }
        return string.Join("\n", lines);
    }
}
