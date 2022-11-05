using System;

namespace CustomString
{
    public class CustomString
    {
        private readonly int _endIndex;
        private readonly int _startIndex = 1;
        private readonly char[] _textArray;

        public CustomString(char[] chars, int counter)
        {
            var effectiveLength = counter;
            _textArray = new char[chars.Length + 1];
            chars.CopyTo(_textArray, 1);
            _textArray[0] = (char)effectiveLength;
            Lenght = effectiveLength;
            _endIndex = Lenght + 1;
        }

        public int Lenght { get; }

        public char this[int index] => _textArray[index + _startIndex];


        public int Find(char value)
        {
            for (var i = _startIndex; i < _endIndex; i++)
                if (_textArray[i] == value)
                    return i - _startIndex;
            return -1;
        }

        public int[] FindAll(char value)
        {
            var foundIndex = new int[Lenght];
            var foundCount = 0;
            for (var i = _startIndex; i < _endIndex; i++)
                if (_textArray[i] == value)
                {
                    foundIndex[foundCount] = i - _startIndex;
                    foundCount++;
                }

            Array.Resize(ref foundIndex, foundCount);
            return foundIndex;
        }

        public void ToLower()
        {
            for (var i = _startIndex; i < _endIndex; i++)
                if (_textArray[i] >= 65 && _textArray[i] <= 90)
                    _textArray[i] += ' ';
        }

        public void ToUpper()
        {
            for (var i = _startIndex; i < _endIndex; i++)
                if (_textArray[i] >= 97 && _textArray[i] <= 122)
                    _textArray[i] -= ' ';
        }

        public void Replace(char oldChar, char newChar)
        {
            for (var i = _startIndex; i < _endIndex; i++)
                if (_textArray[i] == oldChar)
                    _textArray[i] = newChar;
        }

        public CustomString[] Split(char separator)
        {
            var separatorIndexes = FindAll(separator);
            var splStrings = new CustomString[separatorIndexes.Length + 1];
            for (var i = 0; i < separatorIndexes.Length + 1; i++)
            {
                int from;
                int to;
                // If its first time, we should separate from 0 to first separator (separatorIndexes[0] + _startIndex)
                if (i == 0)
                {
                    from = 0 + _startIndex;
                    to = separatorIndexes[0] + _startIndex;
                }
                // If its last time, we should separate from last separator to last effective index
                else if (i == separatorIndexes.Length)
                {
                    //Minus by 1, cuz 'i' counter increased by 1 from 0 index to first separator.
                    from = separatorIndexes[i - 1] + i;
                    to = _endIndex;
                }
                else
                {
                    from = separatorIndexes[i - 1] +
                           _startIndex +
                           i; //Minus by 1, cuz 'i' counter increased by 1 from 0 index to first separator. And plus i cuz of ignoring separator index. 
                    to = separatorIndexes[i] + _startIndex;
                }

                var chars = new char[to - from];
                var addedChar = 0;
                for (var j = from; j < to; j++)
                {
                    chars[addedChar] = _textArray[j];
                    addedChar++;
                }

                splStrings[i] = new CustomString(chars, chars.Length);
            }

            return splStrings;
        }

        public CustomString Substring(int index)
        {
            index += _startIndex;
            var charList = new char[Lenght];
            for (var i = index; i < _endIndex; i++)
                charList[i - index] = _textArray[i];
            return new CustomString(charList, Lenght - index + 1);
        }

        public CustomString Substring(int index, int length)
        {
            index += _startIndex;
            var charList = new char[Lenght];
            for (var i = index; i < index + length; i++)
                charList[i - index] = _textArray[i];
            return new CustomString(charList, length);
        }

        public static CustomString operator +(CustomString a, CustomString b)
        {
            var chars = new char[a.Lenght + b.Lenght];
            var indexCounter = 0;
            for (var i = a._startIndex; i < a._endIndex; i++)
            {
                chars[indexCounter] = a._textArray[i];
                indexCounter++;
            }

            for (var i = b._startIndex; i < b._endIndex; i++)
            {
                chars[indexCounter] = b._textArray[i];
                indexCounter++;
            }

            return new CustomString(chars, a.Lenght + b.Lenght);
        }

        public static implicit operator CustomString(char[] chars)
        {
            // While not technically a requirement; see below why this is done.
            if (chars == null)
                return null;

            return new CustomString(chars, chars.Length);
        }

        public override string ToString()
        {
            var result = "";
            for (var i = _startIndex; i < _endIndex; i++) result += _textArray[i];

            return result;
        }

        public void CopyTo(int sourceIndex, ref CustomString destinationString, int destinationIndex, int count)
        {
            var valueToCopy = new char[count];
            for (var i = 0; i < count; i++) valueToCopy[i] = _textArray[i + sourceIndex + _startIndex];
            var valueToCopyString = new CustomString(valueToCopy, valueToCopy.Length);
            var newArrayBeforeText = destinationString.Substring(0, destinationIndex);
            var newArrayAfterText = destinationString.Substring(destinationIndex);


            destinationString = newArrayBeforeText + valueToCopyString + newArrayAfterText;
        }

        public bool CompareTo(CustomString value)
        {
            for (var i = 0; i < Lenght; i++)
            {
                if (this[i] == value[i])
                    continue;
                return false;
            }

            return true;
        }
    }
}