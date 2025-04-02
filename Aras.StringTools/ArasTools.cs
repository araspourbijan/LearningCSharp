using System.Text;

namespace Aras.StringTools;

public static class ArasTools
{
    //    Encode a String Using Run-Length Encoding(RLE)
    //Implement Run-Length Encoding(RLE), a simple form of lossless data compression where consecutive characters are replaced with the character followed by its count.

    //🔹 Example:
    //Input: "aaabbcddd"
    //Output: "a3b2c1d3"

    /// <summary>
    /// Encode a String Using Run-Length Encoding(RLE)
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string EncodeString(this string text)
    {
        if (text.Length == 0)
            throw new ArgumentException("Text Can't be empty", nameof(text));
              
        StringBuilder sb = new();
        int num = 1;
        sb.Append(text[0]);

        for (int i = 1; i < text.Length; i++)
        {
            if (text[i] == text[i - 1])
                num++;
            else
            {
                sb.Append(num);
                sb.Append(text[i]);
                num = 1;
            }
        }
        sb.Append(num);
        return sb.ToString();
    }

    //    Decode a Run-Length Encoded String
    //Given a Run-Length Encoded(RLE) string, decode it back to its original form.

    //🔹 Example:
    //Input: "a3b2c1d3"
    //Output: "aaabbcddd"
    /// <summary>
    /// Decode a Run-Length Encoded String
    /// </summary>
    /// <param name="text"></param>
    /// <returns>string</returns>
    public static string DecodeString(this string text)
    {
        if (text.Length == 0)
            return string.Empty;

        if (text.Length % 2 != 0)
            throw new Exception("Length of string must be even");

        StringBuilder sb = new();

        for (int i = 0; i < text.Length; i++)
        {
            if (i % 2 == 0)
            {
                if (!char.IsDigit(text[i+1]))
                    throw new NotSupportedException("Invalid Input");

                for (int j = 0; j < int.Parse(text[i + 1].ToString()); j++)
                    sb.Append(text[i]);
            }
        }
        return sb.ToString();
    }


    //Find the Longest Substring Without Repeating Characters
    //Write a function that returns the length of the longest substring without repeating characters.

    //🔹 Example:
    //Input: "abcabcbb"
    //Output: 3 (because "abc" is the longest substring without duplicates)

    /// <summary>
    /// .Find the Longest Substring Without Repeating Characters
    /// </summary>
    /// <param name="text"></param>
    /// <returns>int</returns>
    public static int LargestSubString(this string text)
    {
        int maxNum = 0;
        int num = 0;
        for (int i = 1; i < text.Length; i++)
        {
            if (text[i] != text[i - 1])
            {
                num++;
                if (num > maxNum)
                    maxNum = num;
            }
            else
                num = 0;
        }
        return maxNum;
    }

    /// <summary>
    /// Create an email address from your full name and a domain address 
    /// </summary>
    /// <param name="fullName">Your full name</param>
    /// <param name="domain">Domain address</param>
    /// <returns>"Jahn Smith" + "example.com" => "j.smith@example.com</returns>
    public static string MakeEmail(this string fullName, string domain)
    {
        var words = fullName.Split(" ");
        var username = new StringBuilder();
        username.Append(words[0][0]);
        username.Append("." + words[words.Length - 1]);
        return string.Join("@", username.ToString().ToLower(), domain.ToLower());
    }

    /// <summary>
    /// Reverse a string 
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string ReverseString(this string text)
    {
        char[] charArray = text.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
    /// <summary>
    /// Print each character of a string as an array
    /// </summary>
    /// <param name="text"></param>
    public static void StringAsArray(this string text)
    {
        foreach (char c in text)
            Console.WriteLine(c);
    }

    /// <summary>
    /// Replace every nth character of a string with an asterisk
    /// </summary>
    /// <param name="text"></param>
    /// <param name="n"></param>
    public static string EscapeString(this string text, int n)
    {
        StringBuilder sb = new();
        for (int i = 0; i < text.Length; i++)
        {
            if (i % n == 0 && text[i] != ' ')
                sb.Append('*');
            else
                sb.Append(text[i]);
        }
        return sb.ToString().ToUpper();
    }
}
