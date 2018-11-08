using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PTOX_LIB.HL7.Model
{
    public enum encodingCharacters
    {
        FieldSeparator = 0
        , ComponentSeparator
        , SubComponentSeparator
        , RepeititionSeparator
        , EscapeCharacter
    }

    /// <summary>
    /// HL7Encoding class
    /// user to use this class to obtain the HL7 message encoding characters as well as the field separator
    /// use the enum encodingCharacters to pull the correct encoding element value
    /// </summary>
    public class HL7Encoding
    {
        /// <summary>
        /// Constructor
        /// parameter is the HL7 encoding string as well as the field separator character as the first element
        /// </summary>
        /// <param name="encodingStr"></param>
        public HL7Encoding(char[] _encode)
        {
            Encoding = _encode;
        }

        private char[] mEncoding;
        public char[] Encoding
        {
            get { return mEncoding; }
            set
            {
                mEncoding = value;
                mFieldSeparator = value[0];
            }
        }

        public string GetEncoding()
        {
            return new string(mEncoding);
        }

        private char mFieldSeparator;
        public char FieldSeparator { get => mFieldSeparator; set => mFieldSeparator = value; }

    }
}
