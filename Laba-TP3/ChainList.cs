using System;
using System.Collections.Generic;
using System.Collections;
using Lab3;

namespace Laba3;

class ChainList<T> : List<T> where T : IComparable<T>
{






    public class Node //
    {
        public T Data { get; set; }
        public Node Next { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
        }
    }

    private Node head;  // Голова списка

    public ChainList()
    {
        head = null;  // Изначально список пуст
        count = 0;
    }
    public Node NodeFind(int pos)
    {
        if (pos >= count) return null;
        int i = 0;
        Node P = head;
        while (P != null && i < pos)
        {
            P = P.Next;
            i++;
        }
        if (i == pos) return P;
        else return null;
    }
    public override void AddElement(T newElement)
    {

        Node newNode = new Node(newElement);

        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node lastNode = NodeFind(count - 1);
            lastNode.Next = newNode;
        }
        count++;
        OnItemChanged();
    }
    public override void InsertElement(T newElement, int pos)
    {
        
        if (pos < 0 || pos > count)
        {
            throw new BadIndexException();
        }
        
        Node newNode = new Node(newElement);

        if (pos == 0)
        {
            newNode.Next = head;
            head = newNode;
        }
        else
        {
            Node prevNode = NodeFind(pos - 1);
            newNode.Next = prevNode.Next;
            prevNode.Next = newNode;
        }
        count++;
        //OnListChanged(EventArgs.Empty);
    }
    public override void RemoveElement(int pos)
    {
        if (pos < 0 || pos >= count)
        {
            return;
        }
        if (pos == 0)
        {
            head = head.Next;
        }
        else
        {
            Node prevNode = NodeFind(pos - 1);
            prevNode.Next = prevNode.Next.Next;
        }
        count--;
        //OnListChanged(EventArgs.Empty);
    }
    public override void ClearList()
    {
        head = null;  // Очищаем список, просто убирая ссылку на голову
        count = 0;
        //OnListChanged(EventArgs.Empty);
    }

    protected override ChainList<T> EmptyClone()
    {
        return new ChainList<T>();
    }
    public override void Sort()
    {
        if (count <= 1)
        {
            return;
        }

        Node current;
        T temp;

        for (int i = 0; i < count; i++)
        {

            current = head;

            while (current != null & current.Next != null)
            {
                if (current.Data.CompareTo(current.Next.Data)>0)
                {
                    temp = current.Data;
                    current.Data = current.Next.Data;
                    current.Next.Data = temp;
                }
                current = current.Next;
            }
        }
    }
    public override void Return()
    {
        if (head == null)
        {
            return;
        }
        PrintReturn(null, head);
        void PrintReturn(Node node1, Node node2)
        {
            if (node2.Next != null)
            {
                PrintReturn(node2, node2.Next);
            }
            else
            {
                head = node2;
            }
            node2.Next = node1;
        }
    }
    public override T this[int index]
    {
        get
        {
            if (index < 0 || index >= count)
            {
                return default(T);
            }
            return NodeFind(index).Data;
        }
        set
        {
            if (index < 0 || index >= count)
                return;

            NodeFind(index).Data = value;
        }
    }
}
