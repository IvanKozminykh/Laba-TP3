using Laba3;
using System;
namespace Lab3;

using Laba3;
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;


public class BadIndexException : Exception
{
    public BadIndexException() : base("Неверный индекс") { }
}

public class Program
{
    static int badIndexExceptionsOnArrList = 0;
    static int badIndexExceptionsOnChainList = 0;
    static int badFileExceptionsOnArrList = 0;
    static int badFileExceptionsOnChainList = 0;

    static void Main()
    {
        ArrList<int> array = new ArrList<int>();
        ChainList<int> chain = new ChainList<int>();

        // Подписываемся на события изменения списков
        array.AddArrayListEventHandlers();
        chain.AddChainListEventHandlers();

        Random rnd = new Random();
        for (int i = 0; i < 5000; i++)
        {
           
                int operation = rnd.Next(0, 4); // rnd.Next(0, 3);
                int item = rnd.Next(100);
                int pos = rnd.Next(100);
                switch (operation)
                {
                    case 0:
                        array.AddElement(item);
                        chain.AddElement(item);
                        break;
                    case 1:
                        array.RemoveElement(pos);
                        chain.RemoveElement(pos);
                        break;
                    
                    case 2:
                        try
                        {
                            array.InsertElement(item, pos);
                        }
                        catch (BadIndexException)
                        {
                            badIndexExceptionsOnArrList++;
                            //Console.WriteLine("Обнаружено недопустимое использование индекса.");
                        }
                        try
                        {
                            chain.InsertElement(item, pos);
                        }
                        
                        catch (BadIndexException)
                        {
                            badIndexExceptionsOnChainList++;
                            //Console.WriteLine("Обнаружено недопустимое использование индекса.");
                        }
                        
                        break;
                    
                    case 3:
                        array[pos] = item;
                        chain[pos] = item;
                        break;
                }
            
            
        }

        // Тестирование методов SaveToFile и LoadFromFile
        /*
        array.SaveToFile("array.txt");
        chain.SaveToFile("chain.txt");

        try
        {
            array.LoadFromFile("array565858.txt");         
        }
        catch (Exception)
        {
            badFileExceptionsOnArrList++;
            Console.WriteLine("Ошибка при считывании данных из файла для ArrList");
        }
        try
        {
            chain.LoadFromFile("chain.txt");
        }
        catch (Exception)
        {
            badFileExceptionsOnChainList++;
            Console.WriteLine("Ошибка при считывании данных из файла для ChainList");
        }
        
        */
        foreach (int item in array)
        {
            Console.WriteLine(item);
        }
        
        // Вывод счетчиков исключений
        Console.WriteLine("Количество исключений BadIndexExceptionOnArrList: " + badIndexExceptionsOnArrList);
        Console.WriteLine("Количество исключений BadIndexExceptionOnChainList: " + badIndexExceptionsOnChainList);
        Console.WriteLine("Количество исключений BadFileExceptionOnArrList: " + badFileExceptionsOnArrList);
        Console.WriteLine("Количество исключений BadFileExceptionOnChainList: " + badFileExceptionsOnChainList);
    }

}

