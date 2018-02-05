using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using PhoneNumbers;

namespace Common
{
    [PublicAPI]
    public static class StringUtils
    {
        /// <summary>
        /// Calculates string 64 bit hash
        /// </summary>
        public static long CalculateHash64(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var hashedValue = 3074457345618258791ul;

            foreach (var c in value)
            {
                unchecked
                {
                    hashedValue += c;
                    hashedValue *= 3074457345618258799ul;
                }
            }

            unchecked
            {
                return (long) hashedValue;
            }
        }

        /// <summary>
        /// Calculates string 32 bit hash
        /// </summary>
        public static int CalculateHash32(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var hashedValue = 618258791u;

            foreach (var c in value)
            {
                unchecked
                {
                    hashedValue += c;
                    hashedValue *= 618258799u;
                }
            }

            unchecked
            {
                return (int)hashedValue;
            }
        }

        /// <summary>
        /// Calculates string hash as hex string of the given <paramref name="length"/> up to 16 digits
        /// Default <paramref name="length"/> is 16
        /// </summary>
        public static string CalculateHexHash64(this string value, int length = 16)
        {
            if (length < 1 || length > 16)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length, "Length should be in the range [1..16]");
            }

            ulong mask = 0xFul;

            for (var i = 1; i < length; ++i)
            {
                // One hex digit - 4 bits, so multiplies i by 4
                mask |= 0xFul << (i * 4);
            }

            unchecked
            {
                return ((ulong) CalculateHash64(value) & mask).ToString($"X{length}");
            }
        }

        /// <summary>
        /// Calculates string hash as hex string of the given <paramref name="length"/> up to 8 digits.
        /// Default <paramref name="length"/> is 8
        /// </summary>
        public static string CalculateHexHash32(this string value, int length = 8)
        {
            if (length < 1 || length > 8)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length, "Length should be in the range [1..8]");
            }

            uint mask = 0xF;

            for (var i = 1; i < length; ++i)
            {
                // One hex digit - 4 bits, so multiplies i by 4
                mask |= 0xFu << (i * 4);
            }

            unchecked
            {
                return ((uint)CalculateHash32(value) & mask).ToString($"X{length}");
            }
        }

        /// <summary>
        /// Проверить на валидность строки Email
        /// </summary>
        /// <param name="email">строка, содержащая Email</param>
        /// <returns>true - да в строке валидный Email</returns>
        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            //according to https://tools.ietf.org/html/rfc5321#section-4.5.3.1.3
            if (email.Length > 256)
                return false;

            email = email.Trim();

            var lines = email.Split('@');

            if (lines.Length != 2)
                return false;

            if (lines[0].Trim() == "" || lines[1].Trim() == "")
                return false;

            if (lines[0].Contains(' ') || lines[1].Contains(' '))
                return false;

            var lines2 = lines[1].Split('.');

            return lines2.Length >= 2;
        }

        public static int IndexOfFromEnd(this string src, char c, int? from = null)
        {
            if (from == null)
                from = src.Length - 1;

            for (var i = from.Value; i >= 0; i--)
                if (src[i] == c)
                    return i;

            return -1;
        }

        public static int FindIndexBeforeTheStatement(this string src, string statement, char c)
        {
            for (var i = src.Length - 1 - statement.Length; i >= 0; i--)
            {
                var index = src.IndexOf(statement, i, StringComparison.Ordinal);
                if (index >= 0)
                    return src.IndexOfFromEnd(c, i);
            }


            return -1;

        }


        public static bool IsOnlyDigits(this string data)
        {
            return data.All(char.IsDigit);
        }

        public static bool IsDigit(this char c)
        {
            return char.IsDigit(c);
        }

        public static string GetDigitsAndSymbols(this string s)
        {
            return new string(s.Where(char.IsLetterOrDigit).ToArray());
        }


        private static readonly Regex IsGuidRegex =
           new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);

        /// <summary>
        /// проверить на то что строка в фрмате GUID
        /// </summary>
        /// <param name="src">исходная строка</param>
        /// <returns>Строка соответствует формату GUID</returns>
        public static bool IsGuid(this string src)
        {
            return !string.IsNullOrEmpty(src) && IsGuidRegex.IsMatch(src);
        }

        public static bool ContainsHtml(this string src)
        {
            return Regex.IsMatch(src, "<(.|\n)*?>");
        }

        public static string ToE164Number(this string src)
        {
            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
            string decodedNumber = null;
            try
            {
                var number = phoneNumberUtil.Parse(src, null);
                decodedNumber = phoneNumberUtil.Format(number, PhoneNumberFormat.E164);
            }
            catch (NumberParseException)
            {
            }
            return decodedNumber;
        }

        public static bool IsUSCanadaNumber(this string src)
        {
            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();

            try
            {
                var number = phoneNumberUtil.Parse(src, null);
                return number.CountryCode == 1;
            }
            catch (NumberParseException)
            {
            }

            return false;
        }

        public static string RemoveCountryPhonePrefix(this string src)
        {
            if (src == null)
                return null;

            if (src.Length < 5)
                return src;


            if (src[0] == '+')
                return src.Substring(2, src.Length - 2);

            if (src.StartsWith("00"))
                return src.Substring(3, src.Length - 3);

            if (src[0] == '8' && src.Length>=11)
                return src.Substring(1, src.Length - 1);

            return src;

        }
        public static string AddFirstSymbolIfNotExists(this string src, char symbol)
        {
            if (string.IsNullOrEmpty(src))
                return symbol+"";

            return src[0] == symbol ? src : symbol+src;
        }

        public static string AddLastSymbolIfNotExists(this string src, char symbol)
        {
            if (string.IsNullOrEmpty(src))
                return ""+symbol;

            return src[src.Length - 1] == symbol ? src : src+symbol;
        }

        public static string RemoveLastSymbolIfExists(this string src, char symbol)
        {
            if (string.IsNullOrEmpty(src))
                return src;

            return src[src.Length - 1] == symbol ? src.Substring(0, src.Length - 1) : src;
        }

        public static string RemoveFirstSymbolIfExists(this string src, char symbol)
        {
            if (String.IsNullOrEmpty(src))
                return src;

            return src[0] == symbol ? src.Substring(1, src.Length - 1) : src;
        }


        public static string SubstringTillSymbol(this string src, int from, char c)
        {
            var indexOf = src.IndexOf(c);

            if (indexOf < 0)
                return @from == 0 ? src : src.Substring(@from, src.Length - @from);


            return src.Substring(@from, indexOf - @from);
        }

        public static string OneLineViaSeparator(this IEnumerable<string> src, char separator)
        {
            var result = new StringBuilder();

            foreach (var s in src)
            {
                if (result.Length > 0)
                    result.Append(separator);

                result.Append(s);
            }

            return result.ToString();
        }

        public static string SubstringExt(this string src, int from, int to)
        {
            return src.Substring(@from, to - @from+1);
        }





        /// <summary>
        /// Get Substring between chars
        /// </summary>
        /// <param name="src">source string</param>
        /// <param name="from">from char</param>
        /// <param name="to">to chat</param>
        /// <param name="skipFrames"></param>
        /// <returns></returns>
        public static string SubstringBetween(this string src, char from, char to, int skipFrames = 0)
        {
            var fromIndex = 0;
            var toIndex = -1;

            for (var i = 0; i <= skipFrames; i++)
            {
                toIndex++;
                fromIndex = src.IndexOf(@from, toIndex) + 1;

                if (fromIndex == 0)
                    return null;

                toIndex = src.IndexOf(to, fromIndex);
            }

            if (toIndex == 0)
                toIndex = src.Length;

            return SubstringExt(src, fromIndex, toIndex-1);
        }

        /// <summary>
        /// Get substring right after the char from
        /// </summary>
        /// <param name="src">source string</param>
        /// <param name="from">from char</param>
        /// <param name="skipCount">how many to skip chars first</param>
        /// <returns></returns>
        public static string SubstringFromChar(this string src, char from, int skipCount = 0)
        {
            var fromIndex = 0;

            for (var i = 0; i <= skipCount; i++)
            {
                fromIndex = src.IndexOf(@from, fromIndex) + 1;

                if (fromIndex == 0)
                    return null;
            }


            return SubstringExt(src, fromIndex, src.Length - 1);
        }

        public static string SubstringFromString(this string src, string from, int skipCount = 0)
        {
            var fromIndex = 0;

            for (var i = 0; i <= skipCount; i++)
            {
                fromIndex = src.IndexOf(@from, fromIndex, StringComparison.Ordinal) + @from.Length;

                if (fromIndex == 0)
                    return null;
            }


            return SubstringExt(src, fromIndex, src.Length - 1);
        }

        public static string ToLowCase(this string src)
        {
            return src?.ToLower();
        }

        public static Tuple<string, string> GetFirstNameAndLastName(this string src)
        {

            if (string.IsNullOrEmpty(src))
                return new Tuple<string, string>(null, null);

            var fl = src.Split(' ');

            if (fl.Length == 1)
                return new Tuple<string, string>(fl[0], null);

            return new Tuple<string, string>(fl[0], fl[1]);

        }

        public static string GenerateId()
        {
            return Guid.NewGuid().ToString().ToLower();
        }



        public static string FirstLetterLowCase(this string src)
        {

            if (string.IsNullOrEmpty(src))
                return src;

            var firstLetter = char.ToLower(src[0]);

            if (firstLetter == src[0])
                return src;

            return firstLetter + src.Substring(1, src.Length - 1);

        }

        public static string ToStringViaSeparator<T>(this IEnumerable<T> str, string separator)
        {
            if (str == null)
                return null;

            var result = new StringBuilder();
            foreach (var s in str)
            {
                if (result.Length > 0)
                    result.Append(separator);
                result.Append(s);
            }

            return result.ToString();
        }

        public static IEnumerable<string> FromStringViaSeparator(this string str, char separator)
        {
            if (string.IsNullOrEmpty(str)) yield break;

            foreach (var s in str.Split(separator))
                yield return s;
        }


        public static string ExtractWebSiteAndPath(this string src)
        {
            if (src == null)
                return null;

            var qIndex = src.IndexOf('?');

            return qIndex < 0 ? src : src.Substring(0, qIndex);
        }

        public static string ExtractWebSiteDomain(this string src)
        {
            if (src == null)
                return null;

            var indexFrom = src.IndexOf(@"//", StringComparison.Ordinal);
            if (indexFrom < 0)
                return null;

            var indexTo = src.IndexOf(@"/", indexFrom+2, StringComparison.Ordinal);

            if (indexTo < 0)
                return src.Substring(indexFrom + 2, src.Length - indexFrom - 2);


            return src.Substring(indexFrom + 2, indexTo - indexFrom - 2);
        }


        public static string ExtractWebSiteRoot(this string src)
        {
            if (src == null)
                return null;

            var indexFrom = src.IndexOf(@"//", StringComparison.Ordinal);
            if (indexFrom < 0)
                return null;

            var indexTo = src.IndexOf(@"/", indexFrom + 2, StringComparison.Ordinal);

            if (indexTo < 0)
                return src.Substring(indexFrom + 2, src.Length - indexFrom - 2);


            return src.Substring(0, indexTo);
        }

        public static int FindFirstNonSpaceSymbolIndex(this string src, int from = 0)
        {
            if (src == null)
                return -1;

            for(var i= from; i<src.Length; i++)
                if (src[i] > ' ')
                    return i;

            return -1;
        }

        public static int IndexOfNotAny(this string src, int startIndex, params char[] symbols)
        {
            for(var i=startIndex; i< src.Length; i++)
            {
                if (!symbols.Any(c => c==src[i]))
                    return i;
            }

            return -1;
        }

        public static string RightSubstring(this string src, int length)
        {
            if (src == null)
                return null;

            if (src.Length < length)
                return src;


            return src.Substring(src.Length - length, length);

        }

        public static string PreparePhoneNum(this string phoneNum)
        {
            phoneNum = new string(phoneNum.Where(char.IsDigit).ToArray());
            return $"+{phoneNum}";
        }

        public static byte[] GetHexStringToBytes(this string value)
        {
            int numberChars = value.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);
            return bytes;
        }

        public static string GetBytesToHexString(byte[] value)
        {
            var hex = BitConverter.ToString(value);
            return hex.Replace("-", "");
        }

        public static bool IsHexString(this string test)
        {
            return Regex.IsMatch(test, @"\A\b[0-9a-fA-F]+\b\Z");
        }

        public static string SanitizeEmail(this string email)
        {
            if (string.IsNullOrEmpty(email))
                return string.Empty;

            var hash = SHA256.Create().ComputeHash(email.ToUtf8Bytes());
            return hash.ToHexString().ToLower();
        }
        
        public static string SanitizePhone(this string phone)
        {
            const int sanitizeCount = 5;

            int length = phone.Length;

            return length > sanitizeCount 
                ? phone.Substring(0, sanitizeCount).PadRight(length, '*') 
                : phone;
        }
    }

    public static class IdGenerator
    {
        public static string GenerateDateTimeId(DateTime creationDateTime)
        {
            return $"{creationDateTime.Ticks.ToString("d19")}_{Guid.NewGuid().ToString("N")}";
        }

        public static string GenerateDateTimeIdNewFirst(DateTime creationDateTime)
        {
            return $"{(DateTime.MaxValue.Ticks - creationDateTime.Ticks).ToString("d19")}_{Guid.NewGuid().ToString("N")}";
        }
    }
}
