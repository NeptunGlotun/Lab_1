using System;
using System.IO;
using System.Linq;

class FileManager
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nОберіть операцію:");
            Console.WriteLine("1 - Створити файл");
            Console.WriteLine("2 - Записати у файл");
            Console.WriteLine("3 - Прочитати файл");
            Console.WriteLine("4 - Редагувати файл");
            Console.WriteLine("5 - Шифрувати текст (Цезар)");
            Console.WriteLine("6 - Дешифрувати текст (Цезар)");
            Console.WriteLine("7 - Вийти");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    CreateFile();
                    break;
                case "2":
                    WriteToFile();
                    break;
                case "3":
                    ReadFromFile();
                    break;
                case "4":
                    EditFile();
                    break;
                case "5":
                    EncryptFile();
                    break;
                case "6":
                    DecryptFile();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Невірний вибір, спробуйте ще раз.");
                    break;
            }
        }
    }

    static void CreateFile()
    {
        Console.Write("Введіть ім'я файлу: ");
        string fileName = Console.ReadLine();

        if (File.Exists(fileName))
        {
            Console.WriteLine("Файл вже існує.");
        }
        else
        {
            File.Create(fileName).Close();
            Console.WriteLine("Файл успішно створено.");
        }
    }

    static void WriteToFile()
    {
        Console.Write("Введіть ім'я файлу: ");
        string fileName = Console.ReadLine();
        Console.Write("Введіть текст для запису: ");
        string content = Console.ReadLine();

        if (!File.Exists(fileName))
        {
            Console.WriteLine("Файл не існує.");
        }
        else
        {
            File.WriteAllText(fileName, content);
            Console.WriteLine("Текст успішно записаний у файл.");
        }
    }

    static void ReadFromFile()
    {
        Console.Write("Введіть ім'я файлу: ");
        string fileName = Console.ReadLine();

        if (!File.Exists(fileName))
        {
            Console.WriteLine("Файл не існує.");
        }
        else
        {
            string content = File.ReadAllText(fileName);
            Console.WriteLine("Вміст файлу:");
            Console.WriteLine(content);
        }
    }

    static void EditFile()
    {
        Console.Write("Введіть ім'я файлу: ");
        string fileName = Console.ReadLine();

        if (!File.Exists(fileName))
        {
            Console.WriteLine("Файл не існує.");
        }
        else
        {
            Console.Write("Введіть новий вміст файлу: ");
            string newContent = Console.ReadLine();
            File.WriteAllText(fileName, newContent);
            Console.WriteLine("Файл успішно відредаговано.");
        }
    }

    static void EncryptFile()
    {
        Console.Write("Введіть ім'я файлу: ");
        string fileName = Console.ReadLine();

        if (!File.Exists(fileName))
        {
            Console.WriteLine("Файл не існує.");
            return;
        }

        Console.Write("Введіть ключ для шифрування: ");
        if (!int.TryParse(Console.ReadLine(), out int shift))
        {
            Console.WriteLine("Некоректний ключ.");
            return;
        }
        Console.Write("Введіть мову тексту (ua - українська, en - англійська): ");
        string language = Console.ReadLine().ToLower();

        string content = File.ReadAllText(fileName);
        string encryptedContent = CaesarCipher(content, shift, language);

        string encryptedFileName = Path.GetFileNameWithoutExtension(fileName) + "(encrypted)" + Path.GetExtension(fileName);
        File.WriteAllText(encryptedFileName, encryptedContent);
        Console.WriteLine($"Файл успішно зашифровано. Зашифрований текст збережено у файлі: {encryptedFileName}");
    }

    static void DecryptFile()
    {
        Console.Write("Введіть ім'я файлу: ");
        string fileName = Console.ReadLine();

        if (!File.Exists(fileName))
        {
            Console.WriteLine("Файл не існує.");
            return;
        }

        Console.Write("Введіть ключ для дешифрування: ");
        if (!int.TryParse(Console.ReadLine(), out int shift))
        {
            Console.WriteLine("Некоректний ключ.");
            return;
        }

        Console.Write("Введіть мову тексту (ua - українська, en - англійська): ");
        string language = Console.ReadLine().ToLower();

        string content = File.ReadAllText(fileName);
        string decryptedContent = CaesarCipher(content, -shift, language);

        Console.WriteLine("Дешифрований текст:");
        Console.WriteLine(decryptedContent);
    }

    static string CaesarCipher(string text, int shift, string language)
    {
        string alphabetUa = "абвгдеєжзиіїйклмнопрстуфхцчшщьюя";
        string alphabetEn = "abcdefghijklmnopqrstuvwxyz";
        string alphabet = language == "ua" ? alphabetUa : alphabetEn;

        shift %= alphabet.Length;

        string result = "";
        foreach (char c in text)
        {
            if (alphabet.Contains(c))
            {
                int index = alphabet.IndexOf(c);
                result += alphabet[(index + shift + alphabet.Length) % alphabet.Length];
            }
            else
            {
                result += c;
            }
        }
        return result;
    }
}