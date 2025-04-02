namespace Aras.StringTools;
public static class Collections
{
    /// <summary>
    /// Compare the first elements of two integer arrays and return the smaller one.
    /// </summary>
    /// <param name="arr1"></param>
    /// <param name="arr2"></param>
    /// <returns> </returns>
    public static int CompareFirst(this int[] arr1, int[] arr2)
    {
        if (arr1.Length == 0 && arr2.Length == 0)
            return -1;
        if (arr1.Length == 0)
            return arr2[0];
        if (arr2.Length == 0)
            return arr1[0];

        return arr1[0] <= arr2[0] ? arr1[0] : arr2[0];
    }

    public static void AddToFrontAndBack<T>(LinkedList<T> linkedList, T value)
    {
        linkedList.AddLast(value);
        linkedList.AddFirst(value);
    }

    public static (T, T) RemoveFromFrontAndBack<T>(LinkedList<T> linkedList)
    {
        T value1 = default;
        T value2 = default;

        if (linkedList.Count == 0)
            return (value1, value2);

        else if (linkedList.Count == 1)
        {
            value1 = linkedList.First.Value;
            value2 = linkedList.First.Value;
        }
        else if (linkedList.Count > 1)
        {
            value1 = linkedList.First.Value;
            value2 = linkedList.Last.Value;
        }

        linkedList.RemoveFirst();
        linkedList.RemoveLast();

        return (value1, value2);
    }

    /// <summary>
    /// Get the values of a dictionary by a list of keys
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="keys"></param>
    /// <returns></returns>
    public static List<TValue> GetValues<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, List<TKey> keys)
    {
        List<TValue> values = [];

        foreach (TKey key in keys)
            if (dictionary.TryGetValue(key, out TValue value))
                values.Add(value);

        return values;
    }

    /// <summary>
    /// Remove the keys from a dictionary
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="keys"></param>
    public static void RemoveKeys<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, List<TKey> keys)
    {
        foreach (TKey key in keys)
            dictionary.Remove(key);
    }

    /// <summary>
    /// Combine two lists into a HashSet
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list1"></param>
    /// <param name="list2"></param>
    /// <returns></returns>
    public static HashSet<T> UnionList<T>(this List<T> list1, List<T> list2)
    {
        HashSet<T> union = new([.. list1, .. list2]);
        return union;
    }

    /// <summary>
    /// Find the common elements of two lists
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list1"></param>
    /// <param name="list2"></param>
    /// <returns></returns>
    public static HashSet<T> IntersectionList<T>(this List<T> list1, List<T> list2)
    {
        var set1 = new HashSet<T>(list1);
        var set2 = new HashSet<T>(list2);
        set1.IntersectWith(set2);
        return set1;
    }

    /// <summary>
    ///  Remove the elements of a list from a Queue
    /// </summary>
    /// <param name="queue"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    public static int RemoveUntil(this Queue<int> queue, int element)
    {
        int totalRemoved = 0;

        while (queue.Count > 0)
        {
            if (queue.Peek() == element)
                break;
            queue.Dequeue();
            totalRemoved++;
        }
        return totalRemoved;
    }

    /// <summary>
    /// Remove the elements of a list from a Stack
    /// </summary>
    /// <param name="stack"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    public static bool PopIf(Stack<int> stack, int element)
    {
        if (stack.Count > 0 && stack.Peek() == element)
        {
            stack.Pop();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Combine two SortedSets into one
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="set1"></param>
    /// <param name="set2"></param>
    /// <returns></returns>
    public static SortedSet<T> CombineSets<T>(this SortedSet<T> set1, SortedSet<T> set2)
    {
        return new SortedSet<T>([.. set1, .. set2]);
    }

    /// <summary>
    /// Find the first n values of a SortedDictionary
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static List<TValue> FirstValues<TKey, TValue>(this SortedDictionary<TKey, TValue> dictionary, int n)
    {
        List<TValue> result = [];
        int counter = 0;
        if (dictionary.Count >= 0)
        {
            foreach (var item in dictionary)
            {
                result.Add(item.Value);
                counter++;
                if (counter == n)
                    break;
            }
        }
        return result;
    }
}
