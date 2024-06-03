using Laba3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Laba3;

abstract class List<T> : IEnumerable<T> where T : IComparable<T>
{


    public delegate void ListChangedEventHandler();

    // События
    public event ListChangedEventHandler ItemChanged;

    protected virtual void OnItemChanged()
    {
        ItemChanged?.Invoke();
    }


    public void AddArrayListEventHandlers()
    {
        int EventArrayList = 0;
        ItemChanged += () =>
        {
            Console.WriteLine("ArrayList был изменён.");
            EventArrayList++;
            Console.WriteLine($"Event Array: {EventArrayList}", EventArrayList);
        };
    }
    public void AddChainListEventHandlers()
    {
        int EventChainList = 0;
        ItemChanged += () =>
        {
            Console.WriteLine("ChainList был изменён.");
            EventChainList++;
            Console.WriteLine($"Event Chain: {EventChainList}", EventChainList);
        };
    }
















    public T Value { get; set; }
    protected int count = 0;
    public int Count { get { return count; } }
    public abstract void AddElement(T newElement);
    public abstract void InsertElement(T newElement, int pos);
    public abstract void RemoveElement(int pos);
    public abstract void ClearList();

    public void Print()
    {
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine(this[i]);
        }
    }
    public abstract T this[int index] { get; set; }
    public void Assign(List<T> sourse)
    {
        ClearList();
        for (int i = 0; i < sourse.Count; i++)
        {
            AddElement(sourse[i]);
        }
    }
    public void AssigneTo(List<T> dest)
    {
        dest.Assign(this);
    }
    protected abstract List<T> EmptyClone();

    public List<T> Clone()
    {
        List<T> clone = EmptyClone();
        clone.Assign(this);
        return clone;
    }

    public virtual void Sort()
    {
        T temp;
        for (int i = 0; i < Count; i++)
        {
            int element = i;
            for (int j = i + 1; j < Count; j++)
            {
                if (this[j].CompareTo(this[element]) < 0)
                {
                    element = j;
                }
            }
            temp = this[i];
            this[i] = this[element];
            this[element] = temp;
        }
    }

    public virtual void Return()
    {

        for (int i = count - 1; i >= 0; i--)
        {
            Console.WriteLine(this[i]);
        }
    }

    public bool IsEqual(List<T> array)
    {
        if (this.count != array.count)
        {
            return false;
        }
        for (int i = 0; i < this.count; i++)
        {
            if (this[i].CompareTo((array[i])) > 0 || this[i].CompareTo((array[i])) < 0)
            {
                return false;
            }
        }
        return true;
    }

    public void SaveToFile(string fileName)
    {

        using (StreamWriter writer = new StreamWriter(fileName))
        {
            for (int i = 0; i < count; i++)
            {
                writer.WriteLine(this[i]);
            }
        }
        Console.WriteLine("Данные успешно записаны в файл: " + fileName);
    }

    public void LoadFromFile(string fileName)
    {
        ClearList(); // Очищаем список перед загрузкой данных из файла
        using (StreamReader reader = new StreamReader(fileName))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // Предполагая, что каждая строка файла содержит один элемент типа T
                // Здесь можно добавить обработку формата строки и парсинга данных
                AddElement((T)Convert.ChangeType(line, typeof(T)));
            }
        }
        Console.WriteLine("Данные успешно загружены из файла: " + fileName);
    }

    public static bool operator ==(List<T> first, List<T> second)
    {
        return first.IsEqual(second);
    }

    public static bool operator !=(List<T> first, List<T> second)
    {
        return !first.IsEqual(second);
    }

    public static List<T> operator +(List<T> first, List<T> second)
    {
        List<T> list = first.Clone();
        for (int i = 0; i < second.Count; i++)
        {
            list.AddElement(second[i]);
        }
        return list;
    }


    public IEnumerator<T> GetEnumerator()
    {
        return new BaseListEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private class BaseListEnumerator : IEnumerator<T>
    {
        private List<T> list;
        private int currentIndex = -1;

        public BaseListEnumerator(List<T> list)
        {
            this.list = list;
        }

        public T Current => list[currentIndex];

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            currentIndex++;
            return currentIndex < list.Count;
        }

        public void Reset()
        {
            currentIndex = -1;
        }

        public void Dispose()
        {
            // Метод Dispose не требуется в этом примере, но интерфейс IDisposable реализуется для соответствия
        }
    }
}
