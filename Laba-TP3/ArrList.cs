using Lab3;
using System;
using System.Reflection;

namespace Laba3;

class ArrList<T> : List<T> where T : IComparable<T>
{
    private T[] items;
    private int Len = 4;








    /*
    public delegate void ListEvent(T newElement);

    public event ListEvent Event;

    public void OnAddElement()
    {
        Event += AddElement;
        Console.WriteLine("Элемент добавлен в ArrList");
    }
    */





    public ArrList()
    {
        items = new T[Len];  // начальный размер массива
        count = 0;
    }

    public override void AddElement(T newElement)
    {
        if (count == items.Length)
        {
            Len *= 2;
            T[] newItems = new T[Len];
            Array.Copy(items, newItems, count);
            items = newItems;
        }
        items[count] = newElement;
        count++;
        OnItemChanged();
    }

    public override void InsertElement(T newElement, int pos)
    {
        
        if (pos < 0 || pos > count)
        {
            throw new BadIndexException();
        }
        
        if (count == items.Length)
        {
            Len *= 2;
            T[] newItems = new T[Len];
            Array.Copy(items, newItems, count);
            items = newItems;
        }
        for (int i = count - 1; i >= pos; i--)
        {
            items[i + 1] = items[i];
        }
        items[pos] = newElement;
        count++;
        //OnListChanged(EventArgs.Empty);
    }


    public override void RemoveElement(int pos)
    {
        if (pos < 0 || pos >= count)
        {
            return;
        }
        for (int i = pos; i < count - 1; i++)
        {
            items[i] = items[i + 1];
        }
        count--;
        //OnListChanged(EventArgs.Empty);
    }

    public override void ClearList()
    {
        items = new T[4]; // Возвращаем начальный размер массива
        count = 0; // Сбрасываем счетчик элементов
        //OnListChanged(EventArgs.Empty);
    }


    protected override ArrList<T> EmptyClone()
    {
        return new ArrList<T>();
    }

    public override T this[int index]
    {
        get
        {
            if (index < 0 || index >= count)
            {
                return default(T);
            }
            return items[index];
        }
        set
        {
            if (index < 0 || index >= count)
            {
                return;
            }
            items[index] = value;
        }
    }
}
