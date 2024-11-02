using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO.Compression;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Xml;


namespace FileManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> items = new List<int>() { 1, 2, 3, 4, 5, 6};
            
            List<string> invalidChars = new List<string>() { "\"", "<", ">", "|", "/", @"\", "?", "*", ":" , ","};
          
            string fileName = "";
            string path = "";

            string dirPath = @"D:\WorkWithFiles\"; //директория, в которую будут сохраняться созданные файлы
            
            
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }


            Menu();


            void CheckFileName(string expention = "")
            {

                if (expention == "json") Console.WriteLine("Введите имя файла с расширением 'json'");
                else if (expention == "xml") Console.WriteLine("Введите имя файла с расширением 'xml'");
                else if (expention == "zip") Console.WriteLine("Введите имя файла с расширением 'zip'");
                else Console.WriteLine("Введите имя файла с расширением");


                fileName = Console.ReadLine();

                path = dirPath + fileName;
              
                bool correctFileName = false;

                while (correctFileName != true)
                {
                    correctFileName = true;

                    for (int i = 0; i < fileName.Length; i++)
                    {
                        if (invalidChars.Contains(fileName[i].ToString()))
                        {
                            correctFileName = false;
                            break;
                        }
                    }
                    if (correctFileName == false)
                    {
                        Console.WriteLine("Ошибка! В названии файла недопустимые символы");
                        if (expention == "json") Console.WriteLine("Введите имя файла с расширением 'json'");
                        else if (expention == "xml") Console.WriteLine("Введите имя файла с расширением 'xml'");
                        else if (expention == "zip") Console.WriteLine("Введите имя файла с расширением 'zip'");
                        else Console.WriteLine("Введите имя файла с расширением");
                        fileName = Console.ReadLine();

                        path = dirPath + fileName;
                    }
                }
                while (File.Exists(path))
                {

                    Console.WriteLine("Файл с таким именем уже существует, введите другое имя файла");

                    fileName = Console.ReadLine();

                    path = dirPath + fileName;
                }

                
            }
            bool CheckFileExists(string expention = "")
            {
                if (expention == "json") Console.WriteLine("Введите имя файла с расширением 'json'");
                else if (expention == "xml") Console.WriteLine("Введите имя файла с расширением 'xml'");
                else if (expention == "zip") Console.WriteLine("Введите имя файла с расширением 'zip'");
                else Console.WriteLine("Введите имя файла с расширением");

                fileName = Console.ReadLine();

                path = dirPath + fileName;

                if (File.Exists(path))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }



            void Menu(int task = 0)
            {
                if (task == 0)
                {
                    Console.WriteLine("Выбирите действие из приведённого списка");

                    Console.WriteLine("1 - Посмотреть информацию о дисках \n" +
                                       "2 - Работа с файлами \n" +
                                        "3 - Работа с форматом JSON \n" +
                                        "4 - Работа с форматом XML \n" +
                                        "5 - Работа с архивами \n" +
                                        "6 - Выход");

                    try
                    {
                        task = Convert.ToInt32(Console.ReadLine());


                    }
                    catch
                    {
                        Console.WriteLine("Ошибка! Некорректный ввод");
                        Menu();
                        return;
                    }

                    if (!items.Contains(task))
                    {
                        Console.WriteLine("Введённый номер действия не существует, выберите действие повторно");

                        Menu();
                    }
                    else
                    {
                        Console.WriteLine();
                        Menu(task);
                    }

                }
                else if (task == 1)
                {
                    DriveInfo[] drives = DriveInfo.GetDrives();

                    foreach (DriveInfo drive in drives)
                    {
                        Console.WriteLine($"Название: {drive.Name}");
                        Console.WriteLine($"Тип: {drive.DriveType}");
                        if (drive.IsReady)
                        {
                            Console.WriteLine($"Объем диска: {drive.TotalSize}");
                            Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
                            Console.WriteLine($"Метка: {drive.VolumeLabel}");
                        }
                        Console.WriteLine();
                    }
                    Menu();
                }
                else if (task == 2)
                {
                    Console.WriteLine("Выбирите действие из приведённого списка");

                    Console.WriteLine("1 - Создать файл \n" +
                                        "2 - Записать в файл строку \n" +
                                        "3 - Прочитать файл в консоль \n" +
                                        "4 - Удалить файл \n" +
                                        "5 - Главное меню");


                    int fileTask;
                    try
                    {
                        fileTask = Convert.ToInt32(Console.ReadLine());

                    }
                    catch
                    {
                        Console.WriteLine("Ошибка! Некорректный ввод");
                        Menu(2);
                        return;
                    }

                    Console.WriteLine();

                    if (items.Contains(fileTask))
                    {
                        if (fileTask == 5) Menu();
                        else WorkWithFiles(fileTask);

                    }
                    else
                    {
                        Console.WriteLine("Введённый номер действия не существует, выберите действие повторно");
                        Menu(2);
                    }


                }
                else if (task == 3)
                {
                    Console.WriteLine("Выбирите действие из приведённого списка");

                    Console.WriteLine("1 - Создать файл в формате JSON\n" +
                                        "2 - Создать новый объект. Его серелизация в JSON и запись в файл \n" +
                                        "3 - Прочитать файл в консоль \n" +
                                        "4 - Удалить файл \n" +
                                        "5 - Главное меню");

                    int JSONTask;


                    try
                    {
                        JSONTask = Convert.ToInt32(Console.ReadLine());

                    }
                    catch
                    {
                        Console.WriteLine("Ошибка! Некорректный ввод");
                        Menu(3);
                        return;
                    }

                    Console.WriteLine();

                    if (items.Contains(JSONTask))
                    {
                        if (JSONTask == 5) Menu();
                        else WorkWithJSON(JSONTask);
                    }
                    else
                    {
                        Console.WriteLine("Введённый номер действия не существует, выберите действие повторно");
                        Menu(3);
                    }


                }
                else if (task == 4)
                {
                    Console.WriteLine("Выбирите действие из приведённого списка");

                    Console.WriteLine("1 - Создать файл в формате XML \n" +
                                        "2 - Записать в файл новые данные \n" +
                                        "3 - Прочитать файл в консоль \n" +
                                        "4 - Удалить файл \n" +
                                        "5 - Главное меню");

                    int XMLTask;
                    try
                    {
                        XMLTask = Convert.ToInt32(Console.ReadLine());

                    }
                    catch
                    {
                        Console.WriteLine("Ошибка! Некорректный ввод");
                        Menu(4);
                        return;
                    }


                    Console.WriteLine();

                    if (items.Contains(XMLTask))
                    {
                        if (XMLTask == 5) Menu();
                        else WorkWithXML(XMLTask);
                    }
                    else
                    {
                        Console.WriteLine("Введённый номер действия не существует, выберите действие повторно");
                        Menu(4);
                    }
                }
                else if (task == 5)
                {
                    Console.WriteLine("Выбирите действие из приведённого списка");

                    Console.WriteLine("1 - Создать архив в формате zip \n" +
                                        "2 - Добавить файл в архив \n" +
                                        "3 - Разархивировать файл и вывести данные о нём \n" +
                                        "4 - Удалить файл и архив \n" +
                                        "5 - Главное меню");

                    int ArchiveTask;

                    try
                    {
                        ArchiveTask = Convert.ToInt32(Console.ReadLine());

                    }
                    catch
                    {
                        Console.WriteLine("Ошибка! Некорректный ввод");
                        Menu(5);
                        return;
                    }



                    Console.WriteLine();

                    if (items.Contains(ArchiveTask))
                    {
                        if (ArchiveTask == 5) Menu();
                        else WorkWithArchives(ArchiveTask);
                    }
                    else
                    {
                        Console.WriteLine("Введённый номер действия не существует, выберите действие повторно");
                        Menu(5);
                    }
                }
                else if (task == 6)
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Неверный код задания");
                    Console.WriteLine();
                    Menu();
                }

            }



            void WorkWithFiles(int fileTask)
            {
                switch (fileTask)
                {
                    case 1:

                        CheckFileName();

                        using (FileStream fs = new FileStream(path, FileMode.CreateNew))
                        {

                        }

                        Console.WriteLine($"Файл {fileName} создан по следующему пути: {path}");

                        Console.WriteLine();
                        Menu(2);
                        break;
                    case 2:

                        while (!CheckFileExists())
                        {
                            Console.WriteLine("Файла с таким именем не существует");
                        }
                        
                        
                        Console.WriteLine($"Введите строку для записи в файл:");
                        string text = Console.ReadLine();

                        try
                        {
                            using (StreamWriter sw = new StreamWriter(path, true))
                            {
                                sw.WriteLine(text);
                            }
                            Console.WriteLine($"Текст записан в файл {fileName}");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        Console.WriteLine();

                        

                        Menu(2);
                        break;
                    case 3:

                        while (!CheckFileExists())
                        {
                            Console.WriteLine("Файла с таким именем не существует");
                        }

                        try
                        {
                            using (StreamReader sr = new StreamReader(path))
                            {
                                string line;
                                Console.WriteLine($"Текст из файла {fileName}: ");
                                while ((line = sr.ReadLine()) != null)
                                {
                                    Console.WriteLine(line);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        Console.WriteLine();
                        Menu(2);
                        break;
                    case 4:

                        while (!CheckFileExists())
                        {
                            Console.WriteLine("Файла с таким именем не существует");
                        }

                        FileInfo fileInf = new FileInfo(path);

                        if (fileInf.Exists)
                        {
                            fileInf.Delete();
                        }
                        Console.WriteLine($"Файл {fileName} был удалён по следующему пути: {path}");

                        Console.WriteLine();
                        Menu(2);
                        break;
                    default:
                        Menu();
                        break;
                }
            }
            void WorkWithJSON(int fileTask)
            {
                switch (fileTask)
                {
                    case 1:

                        string format = "";
                        while (format.ToLower() != "json")
                        {
                            CheckFileName("json");
                            format = "";
                            for (int i = fileName.Length - 4; i < fileName.Length; i++)
                            {
                                format += fileName[i];
                            }
                            if (format.ToLower() != "json")
                            {
                                Console.WriteLine("Файл имеет отличное расширение от JSON");

                            }

                        }
                        File.WriteAllText(path, "{}");
                        Console.WriteLine($"Пустой JSON-файл '{fileName}' успешно создан");
                        Console.WriteLine();

                        Menu(3);
                        break;
                    case 2:
                       
                        while (!CheckFileExists("json"))
                        {
                            Console.WriteLine("Файла с таким именем не существует");

                        }
                        format = "";
                        while (format.ToLower() != "json")
                        {

                            format = "";
                            for (int i = fileName.Length - 4; i < fileName.Length; i++)
                            {
                                format += fileName[i];
                            }
                            if (format.ToLower() != "json")
                            {
                                Console.WriteLine("Файл имеет отличное расширение от JSON");
                                while (!CheckFileExists("json"))
                                {
                                    Console.WriteLine("Файла с таким именем не существует");

                                }
                            }

                        }

                        

                        
                        List<Dictionary<string, string>> objectsList = new List<Dictionary<string, string>>();

                        while (true)
                        {
                            
                            Dictionary<string, string> obj = new Dictionary<string, string>();

                            Console.WriteLine("Создаём новый объект");

                            while (true)
                            {
                                
                                Console.Write("Введите ключ: ");
                                string key = Console.ReadLine();

                               
                                Console.Write("Введите значение: ");
                                string value = Console.ReadLine();

                                
                                obj[key] = value;

                                
                                Console.Write("Добавить еще ключ-значение в этот объект? (да/нет): ");
                                string addMore = Console.ReadLine().ToLower();

                                if (addMore != "да")
                                {
                                    break;
                                }
                            }

                            
                            objectsList.Add(obj);

                            
                            Console.Write("Создать новый объект? (да/нет): ");
                            string createAnother = Console.ReadLine().ToLower();

                            if (createAnother != "да")
                            {
                                break;
                            }
                        }

                        // Сериализуем список объектов в формат JSON
                        string jsonString = JsonSerializer.Serialize(objectsList, new JsonSerializerOptions { WriteIndented = true });

                       
                        File.WriteAllText(path, jsonString);

                        Console.WriteLine("Данные записаны в файл: " + fileName);

                        Console.WriteLine();

                        Menu(3);
                        break;

                    case 3:

                        while (!CheckFileExists("json"))
                        {
                            Console.WriteLine("Файла с таким именем не существует");
                        }

                        format = "";
                        while (format.ToLower() != "json")
                        {

                            format = "";
                            for (int i = fileName.Length - 4; i < fileName.Length; i++)
                            {
                                format += fileName[i];
                            }
                            if (format.ToLower() != "json")
                            {
                                Console.WriteLine("Файл имеет отличное расширение от JSON");
                                while (!CheckFileExists("json"))
                                {
                                    Console.WriteLine("Файла с таким именем не существует");

                                }
                            }

                        }

                        string readJson = File.ReadAllText(path);

                        List<Dictionary<string, string>> deserializedObjects = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(readJson);

                      
                        foreach (var obj in deserializedObjects)
                        {
                            Console.WriteLine("\nОбъект:");
                            foreach (var kv in obj)
                            {
                                Console.WriteLine($"{kv.Key}: {kv.Value}");
                            }
                        }



                        Console.WriteLine();
                        Menu(3);
                        break;
                    case 4:
                        
                        while (!CheckFileExists("json"))
                        {
                            Console.WriteLine("Файла с таким именем не существует");
                        }
                        format = "";
                        while (format.ToLower() != "json")
                        {

                            format = "";
                            for (int i = fileName.Length - 4; i < fileName.Length; i++)
                            {
                                format += fileName[i];
                            }
                            if (format.ToLower() != "json")
                            {
                                Console.WriteLine("Файл имеет отличное расширение от JSON");
                                while (!CheckFileExists("json"))
                                {
                                    Console.WriteLine("Файла с таким именем не существует");

                                }
                            }

                        }
                        File.Delete(path);
                        Console.WriteLine($"Файл '{fileName}' успешно удален");


                        Console.WriteLine();
                        Menu(3);
                        break;
                    default:
                        Menu();
                        break;
                }

            }

            void WorkWithXML(int fileTask)
            {
                switch (fileTask)
                {
                    case 1:
                        string format = "";
                        while (format.ToLower() != "xml")
                        {
                            CheckFileName("xml");
                            format = "";
                            for (int i = fileName.Length - 3; i < fileName.Length; i++)
                            {
                                format += fileName[i];
                            }
                            if (format.ToLower() != "xml")
                            {
                                Console.WriteLine("Файл имеет отличное расширение от XML");

                            }

                        }
                      
                        // Создаем XML документ и пишем базовую структуру
                        XmlWriterSettings settings = new XmlWriterSettings();
                        settings.Indent = true;

                        using (XmlWriter writer = XmlWriter.Create(path, settings))
                        {
                            writer.WriteStartDocument();
                            writer.WriteStartElement("Items"); 
                            writer.WriteEndElement();
                            writer.WriteEndDocument();
                        }

                        Console.WriteLine($"Файл {fileName} создан.");
                        


                        Console.WriteLine();
                        Menu(4);
                        break;
                    case 2:
                        while (!CheckFileExists("xml"))
                        {
                            Console.WriteLine("Файла с таким именем не существует");
                        }
                        format = "";
                        while (format.ToLower() != "xml")
                        {

                            format = "";
                            for (int i = fileName.Length - 3; i < fileName.Length; i++)
                            {
                                format += fileName[i];
                            }
                            if (format.ToLower() != "xml")
                            {
                                Console.WriteLine("Файл имеет отличное расширение от XML");

                                while (!CheckFileExists("xml"))
                                {
                                    Console.WriteLine("Файла с таким именем не существует");

                                }
                            }

                        }

                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(path);

                        XmlElement root = xmlDoc.DocumentElement;

                        if (root != null)
                        {
                            using (XmlWriter writer = root.CreateNavigator().AppendChild())
                            {
                                AddObjectsToXml(writer);
                            }

                            xmlDoc.Save(path);
                            Console.WriteLine($"Данные успешно записаны в файл '{fileName}'.");
                        }
                        else
                        {
                            Console.WriteLine("Ошибка в структуре файла");
                        }

                        Console.WriteLine();
                        Menu(4);
                        break;
                    case 3:
                        while (!CheckFileExists("xml"))
                        {
                            Console.WriteLine("Файла с таким именем не существует");
                        }
                        format = "";
                        while (format.ToLower() != "xml")
                        {

                            format = "";
                            for (int i = fileName.Length - 3; i < fileName.Length; i++)
                            {
                                format += fileName[i];
                            }
                            if (format.ToLower() != "xml")
                            {
                                Console.WriteLine("Файл имеет отличное расширение от XML");
                                while (!CheckFileExists("xml"))
                                {
                                    Console.WriteLine("Файла с таким именем не существует");

                                }
                            }

                        }
                        Console.WriteLine();

                        XmlDocument xDoc = new XmlDocument();
                        xDoc.Load(path);

                        XmlNode xRoot = xDoc.DocumentElement;
                        if (xRoot != null)
                        {
                            foreach (XmlNode xnode in xRoot.ChildNodes)
                            {
                                Console.WriteLine($"Объект: {xnode.Name}");
                                foreach (XmlNode field in xnode.ChildNodes)
                                {
                                    Console.WriteLine($"{field.Name}: {field.InnerText}");
                                }
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Файл пуст");
                        }

                        Console.WriteLine();
                        Menu(4);
                        break;
                    case 4:
                        while (!CheckFileExists("xml"))
                        {
                            Console.WriteLine("Файла с таким именем не существует"); 
                        }
                        format = "";
                        while (format.ToLower() != "xml")
                        {

                            format = "";
                            for (int i = fileName.Length - 3; i < fileName.Length; i++)
                            {
                                format += fileName[i];
                            }
                            if (format.ToLower() != "xml")
                            {
                                Console.WriteLine("Файл имеет отличное расширение от xml");
                                while (!CheckFileExists("xml"))
                                {
                                    Console.WriteLine("Файла с таким именем не существует");

                                }
                            }

                        }

                        if (File.Exists(path))
                        {
                            File.Delete(path);
                            Console.WriteLine($"Файл '{fileName}' удалён");
                        }
                        else
                        {
                            Console.WriteLine("Файл не найден");
                        }
                        Console.WriteLine();
                        Menu(4);
                        break;
                    default:
                        Menu();
                        break;
                }

            }

            void AddObjectsToXml(XmlWriter writer)
            {
                bool addMore = true;

                while (addMore)
                {
                    Console.Write("Введите название объекта: ");
                    string objectName = Console.ReadLine();

                    writer.WriteStartElement(objectName); 

                    bool addFields = true;
                    while (addFields)
                    {
                        Console.Write("Введите название поля: ");
                        string fieldName = Console.ReadLine();

                        Console.Write("Введите значение поля: ");
                        string fieldValue = Console.ReadLine();

                        writer.WriteElementString(fieldName, fieldValue);

                        Console.Write("Добавить еще одно поле в этот объект? (да/нет): ");
                        string addFieldResponse = Console.ReadLine().ToLower();

                        if (addFieldResponse != "да")
                        {
                            addFields = false;
                        }
                    }

                    writer.WriteEndElement();

                    Console.Write("Добавить еще один объект? (да/нет): ");
                    string addObjectResponse = Console.ReadLine().ToLower();

                    if (addObjectResponse != "да")
                    {
                        addMore = false;
                    }
                }
            }



            string zipPath = "";

            void WorkWithArchives(int fileTask)
            {
               
                switch (fileTask)
                {
                    case 1:
                        string format = "";
                        while (format.ToLower() != "zip")
                        {
                            CheckFileName("zip");
                            format = "";
                            for (int i = fileName.Length - 3; i < fileName.Length; i++)
                            {
                                format += fileName[i];
                            }
                            if (format.ToLower() != "zip")
                            {
                                Console.WriteLine("Файл имеет отличное расширение от zip");

                            }

                        }
                        using (ZipArchive zip = ZipFile.Open(path, ZipArchiveMode.Create))
                        {
                            
                            
                        }

                        Console.WriteLine($"Архив с именем '{fileName}' успешно создан");


                        Console.WriteLine();
                        Menu(5);
                        break;
                    case 2:
                        while (!CheckFileExists("zip"))
                        {
                            Console.WriteLine("Архива с таким именем не существует"); 
                        }
                        format = "";
                        while (format.ToLower() != "zip")
                        {

                            format = "";
                            for (int i = fileName.Length - 3; i < fileName.Length; i++)
                            {
                                format += fileName[i];
                            }
                            if (format.ToLower() != "zip")
                            {
                                Console.WriteLine("Файл имеет отличный формат от zip");
                                while (!CheckFileExists("zip"))
                                {
                                    Console.WriteLine("Архива с таким именем не существует");

                                }
                            }

                        }
                        zipPath = path;


                        Console.WriteLine("Введите имя файла, который нужно добавить в архив: ");

                        while (!CheckFileExists())
                        {
                            Console.WriteLine("Файла с таким именем не существует"); 
                        }
                        
                        // Открываем архив в режиме обновления и добавляем файл
                        using (ZipArchive zip = ZipFile.Open(zipPath, ZipArchiveMode.Update))
                        {
                            zip.CreateEntryFromFile(path, Path.GetFileName(path));
                            Console.WriteLine($"Файл {path} добавлен в архив {zipPath}.");
                        }
                       
                        Console.WriteLine();
                        Menu(5);
                        break;
                    case 3:
                        while (!CheckFileExists("zip"))
                        {
                            Console.WriteLine("Архива с таким именем не существует"); 
                        }
                        format = "";
                        while (format.ToLower() != "zip")
                        {

                            format = "";
                            for (int i = fileName.Length - 3; i < fileName.Length; i++)
                            {
                                format += fileName[i];
                            }
                            if (format.ToLower() != "zip")
                            {
                                Console.WriteLine("Файл имеет отличное расширение от zip");
                                while (!CheckFileExists("zip"))
                                {
                                    Console.WriteLine("Архива с таким именем не существует");

                                }
                            }

                        }
                        zipPath = path;

                        string extractPath = dirPath + Path.GetFileNameWithoutExtension(zipPath);

                       
                        ZipFile.ExtractToDirectory(zipPath, extractPath);
                        Console.WriteLine($"Архив {zipPath} извлечён в папку по следующему пути: {extractPath}.");

                        // Вывод информации о каждом файле
                        string[] files = Directory.GetFiles(extractPath);
                        foreach (var file in files)
                        {
                            FileInfo fileInfo = new FileInfo(file);
                            Console.WriteLine($"Файл: {fileInfo.Name}, Размер: {fileInfo.Length} байт, Создан: {fileInfo.CreationTime}");
                        }
                        
                        Console.WriteLine();
                        Menu(5);
                        break;
                    case 4:
                        while (!CheckFileExists("zip"))
                        {
                            Console.WriteLine("Архива с таким именем не существует"); 
                        }
                        format = "";
                        while (format.ToLower() != "zip")
                        {

                            format = "";
                            for (int i = fileName.Length - 3; i < fileName.Length; i++)
                            {
                                format += fileName[i];
                            }
                            if (format.ToLower() != "zip")
                            {
                                Console.WriteLine("Файл имеет отличное расширение от zip");
                                while (!CheckFileExists("zip"))
                                {
                                    Console.WriteLine("Архива с таким именем не существует");

                                }
                            }

                        }

                        if (File.Exists(path))
                        {
                            File.Delete(path);
                            Console.WriteLine($"Архив {fileName} удалён");
                        }
                        else
                        {
                            Console.WriteLine("Архив не найден");
                        }
                        Console.WriteLine();
                        Menu(5);
                        break;
                    default:
                        Menu();
                        break;
                }

            }

        }
    }
}
