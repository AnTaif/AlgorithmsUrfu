namespace Labs.Lab5;

public class HashTable<K, V>(int capacity, double loadFactor)
{
    private HashNode<K, V>?[] table = new HashNode<K, V>[capacity];
    private int capacity = capacity;

    public HashTable() : this(10, 0.75) { }

    public void Put(K key, V value)
    {
        var hash = Hash(key);
        var newNode = new HashNode<K, V>(key, value);

        if (table[hash] == null)
            table[hash] = newNode;
        else
        {
            var currentNode = table[hash];
            while (currentNode!.Next != null)
                currentNode = currentNode.Next;
            
            currentNode.Next = newNode;
        }

        if ((double)Size() / capacity > loadFactor)
            Resize(capacity * 2);
    }

    public V? Get(K key)
    {
        var hash = Hash(key);

        if (table[hash] == null)
            return default;

        var current = table[hash];
        while (current != null)
        {
            if (current.Key!.Equals(key))
            {
                return current.Value;
            }
            current = current.Next;
        }
        return default;
    }

    public bool ContainsKey(K key) => table[Hash(key)] != null;

    public V? Remove(K key)
    {
        var hash = Hash(key);
        var current = table[hash];
        HashNode<K, V>? previous = null;

        while (current != null)
        {
            if (current.Key!.Equals(key))
            {
                if (previous == null)
                    table[hash] = current.Next;
                else
                    previous.Next = current.Next;

                return current.Value;
            }

            previous = current;
            current = current.Next;
        }

        return default;
    }

    private void Resize(int newCapacity)
    {
        var temp = new HashTable<K, V>(newCapacity, loadFactor);
        foreach (var tableNode in table)
        {
            var node = tableNode;
            while (node != null)
            {
                temp.Put(node.Key, node.Value);
                node = node.Next;
            }
        }
        table = temp.table;
        capacity = temp.capacity;
    }

    private int Size()
    {
        var count = 0;
        foreach (var tableNode in table)
        {
            var node = tableNode;
            while (node != null)
            {
                count++;
                node = node.Next;
            }
        }
        return count;
    }

    private int Hash(K key) => key!.GetHashCode() % capacity;
}

public class HashNode<K, V>(K key, V value)
{
    public K Key { get; set; } = key;
    public V Value { get; set; } = value;

    public HashNode<K, V>? Next { get; set; }
}