using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
            static void Main(string[] args)
            {
                Console.WriteLine("Пожалуйста, введите путь к .txt файлу");
                var path = Console.ReadLine();
                CountWords(path);
            }
            public static void CountWords(string url)
            {
                var text = File.ReadAllText(url, Encoding.Unicode);
                var words = Regex.Match(text, @"\w[\w'-]*");
                var dic = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
                while (words.Success)
                {
                    var word = words.Value;
                    if (dic.ContainsKey(word))
                    {
                        dic[word] += 1;
                    }
                    else
                    {
                        dic.Add(word, 1);
                    }
                    words = words.NextMatch();
                }
                CreateCSV(dic);
            }

            public static void CreateCSV(Dictionary<string, int> dic)
            {
                Console.WriteLine("Пожалуйста, введите путь к папке, в которой вы хотите сохранить файл .csv");
                var path = Console.ReadLine();

                Console.WriteLine("Пожалуйста, введите название для файла");
                var title = Console.ReadLine();

                var filePath = $"{path}\\{title}.csv";

                var delimiter = ",";

                var builder = new StringBuilder();

                foreach (var line in dic)
                {
                    builder.AppendLine(string.Join(delimiter, $"{line.Key}, {line.Value}"));
                }

                File.WriteAllText(filePath, builder.ToString());
                Console.WriteLine($"Создан файл {title}.csv");
                Console.ReadKey();
            }
        }
    }

