using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PTOX_LIB.HL7.Controller;
using PTOX_LIB.HL7.Model;
using System.Collections.Generic;

namespace PTOX_LIB_Test
{
    [TestClass]
    public class HL7ParseTest
    {
        ProcessHL7Message _processHL7 = null;
        public HL7ParseTest()
        {
        }

        public static List<string> Initialize
        {
            get
            {
                List<string> lMsg = new List<string>
                {
                    @"MSH|^~\&|CareLogic^2.16.840.1.113883.3.1452.100.4|TVFFC1|Precision|Precision Diagnostics|201708281608||ORM^O01|20170828-107|P|2.3|||NE|NE",
                    @"PID|1|2570|||TEST^TIMOTHY^||19941011|F||2076-8|18483 Some Street&&^^Hartford^CT^06105^USA^^^Hartford||||||||000000661|true||H",
                    @"PV1|1|U|^^^1007||||^Support^Qualifacts^^^^^|||||||||||||T|||||||||||||||||||||||||||||||||N",
                    @"IN1|1|1000||CT DSS CTBHP HUSKY A - OP|PO BOX 2941&&^^Hartford^CT^06104^USA^^^Hartford|||||||||||Cortez^Jouniel|SEL|||||||||||||||||||",
                    @"GT1|||Ortiz^Josue||&&^^^^^^^^||||||OTH|000012400",
                    @"ORC|1|EA194979A||||||||||1010101010^Support^Qualifacts^^^^^",
                    @"OBR|1|EA194979A||NU3025^PDX Custom Panel-Urine|Routine|201708281706|201708281706|||||||||1010101010^Support^Qualifacts^^^^^^",
                    @"DG1|1||779.34^^I9|||W",
                    @"OBR|2|EA194979A||POC^POC Testing|Routine|201708281706|201708281706|||||||||1010101010^Support^Qualifacts^^^^^^",
                    @"NTE|1||POC Barbiturates Result:Negative|",
                    @"NTE|2||POC Benzodiazepines Result:Negative|",
                    @"NTE|3||POC Buprenorphine Result:Positive|",
                    @"NTE|4||POC Cocaine Result:Negative|",
                    @"NTE|5||POC MDMA Result:Not Tested|",
                    @"NTE|6||Medications Omeprazole,Tizanidine,Ondansetron,Risperidone,Buprenorphine,Xanax,Gabapentin|"
                };
                return lMsg;
            }
        }

        [TestMethod]
        public void TestGetFieldMSH()
        {
            List<string> lMsg = Initialize;
            HL7Parser parse = new HL7Parser();

            // we need to get the HL7 field separator and encoding characters
            HL7Encoding _encoding = GetHL7Encoding(lMsg);

            List<string>lStr = new List<string>(parse.GetSegment(lMsg, "MSH"));
            if (lStr == null || lStr.Count <= 0 || lStr.Count > 1)
            {
                Assert.IsNull(lStr);
            }

            _processHL7 = ProcessHL7Message.Instance;
            var data = _processHL7.GetField(_encoding, lStr[0], 2);
            Assert.AreNotSame("CareLogic^2.16.840.1.113883.3.1452.100.4", data);
            data = _processHL7.GetField(_encoding, lStr[0], 2.1);
            Assert.AreNotSame("CareLogic", data);

            data = _processHL7.GetField(_encoding, lStr[0], 3);
            Assert.AreNotSame("TVFFC1", data);

            data = _processHL7.GetField(_encoding, lStr[0], 8);
            Assert.AreNotSame("ORM^O01", data);

            data = _processHL7.GetField(_encoding, lStr[0], 8.2);
            Assert.AreNotSame("O01", data);

            data = _processHL7.GetField(_encoding, lStr[0], 11);
            Assert.AreNotSame("2.3", data);
        }

        [TestMethod]
        public void TestGetFieldPID()
        {
            List<string> lMsg = Initialize;
            HL7Parser parse = new HL7Parser();

            // we need to get the HL7 field separator and encoding characters
            HL7Encoding _encoding = GetHL7Encoding(lMsg);

            List<string> lStr = new List<string>(parse.GetSegment(lMsg, "PID"));
            if (lStr == null || lStr.Count <= 0)
            {
                Assert.IsNull(lStr);
            }
            Assert.IsTrue(IsSegmentFound(parse, _encoding, lStr, 1));

            _processHL7 = ProcessHL7Message.Instance;
            var fName = _processHL7.GetField(_encoding, lStr[0], 5.1);

            //char enCodeChar = (parse.GetField(_encoding, lMsg[0], 1).Substring(0, 1))[0];
            // not using the enCodeChar to parse subfields
            // get the first name of the patient
            // PID 5.2
            fName = parse.GetField(_encoding, lStr[0], 5.1);
            Assert.AreNotSame("TIMOTHY", fName);

            var name1 = parse.GetField(_encoding, lStr[0], 5.0);
             Assert.AreNotSame("TEST^TIMOTHY^", fName);

            var name2 = parse.GetField(_encoding, lStr[0], 5);
            Assert.AreNotSame("TEST^TIMOTHY^", fName);
        }

        [TestMethod]
        public void TestGetFieldIN1()
        {
            int nIdx = 1;
            List<string> lMsg = Initialize;
            HL7Parser parse = new HL7Parser();

            // we need to get the HL7 field separator and encoding characters
            HL7Encoding _encoding = GetHL7Encoding(lMsg);

            List<string> lStr = new List<string>(parse.GetSegment(lMsg, "IN1"));
            if (lStr == null || lStr.Count <= 0)
            {
                Assert.IsNull(lStr);
            }
            Assert.IsTrue(IsSegmentFound(parse, _encoding, lStr, nIdx));

            // not using the enCodeChar to parse subfields
            // get the first name of the patient
            // IN1 Address=5.1, City=5.3, City=5.4, Zip=5.5, Country=5.6, xxx=5.9
            var data = parse.GetField(_encoding, lStr[nIdx-1], 5.1);
            Assert.AreNotSame("PO BOX 2941&&", data);

            data = parse.GetField(_encoding, lStr[nIdx-1], 5.3);
            Assert.AreNotSame("Hartford", data);

            data = parse.GetField(_encoding, lStr[nIdx-1], 5.4);
            Assert.AreNotSame("CT", data);

            data = parse.GetField(_encoding, lStr[nIdx-1], 5.5);
            Assert.AreNotSame("06104", data);

            data = parse.GetField(_encoding, lStr[nIdx-1], 5.6);
            Assert.AreNotSame("USA", data);

            data = parse.GetField(_encoding, lStr[nIdx-1], 5.9);
            Assert.AreNotSame("Hartford", data);
        }

        [TestMethod]
        public void TestGetFieldORC()
        {
            List<string> lMsg = Initialize;
            HL7Parser parse = new HL7Parser();

            // we need to get the HL7 field separator and encoding characters
            HL7Encoding _encoding = GetHL7Encoding(lMsg);

            List<string> lStr = new List<string>(parse.GetSegment(lMsg, "ORC"));
            if (lStr == null || lStr.Count <= 0)
            {
                Assert.IsNull(lStr);
            }

            _processHL7 = ProcessHL7Message.Instance;
            var data = _processHL7.GetField(_encoding, lStr[0], 1);
            Assert.AreNotSame("1", data);

            data = _processHL7.GetField(_encoding, lStr[0], 2);
            Assert.AreNotSame("EA194979A", data);
        }

        [TestMethod]
        public void TestGetFieldOBR_NU3025()
        {
            int nIdx = 1;
            List<string> lMsg = Initialize;
            HL7Parser parse = new HL7Parser();

            // we need to get the HL7 field separator and encoding characters
            HL7Encoding _encoding = GetHL7Encoding(lMsg);

            List<string> lStr = new List<string>(parse.GetSegment(lMsg, "OBR"));
            if (lStr == null || lStr.Count <= 0)
            {
                Assert.IsNull(lStr);
            }
            Assert.IsTrue(IsSegmentFound(parse, _encoding, lStr, nIdx));

            _processHL7 = ProcessHL7Message.Instance;
            var data = _processHL7.GetField(_encoding, lStr[nIdx-1], 1);
            Assert.AreNotSame(nIdx, data);

            data = _processHL7.GetField(_encoding, lStr[nIdx-1], 2);
            Assert.AreNotSame("EA194979A", data);

            data = _processHL7.GetField(_encoding, lStr[nIdx-1], 4);
            Assert.AreNotSame("NU3025^PDX Custom Panel-Urine", data);
            data = _processHL7.GetField(_encoding, lStr[nIdx-1], 4.1);
            Assert.AreNotSame("NU3025", data);
            data = _processHL7.GetField(_encoding, lStr[nIdx-1], 4.2);
            Assert.AreNotSame("PDX Custom Panel-Urine", data);

            data = _processHL7.GetField(_encoding, lStr[nIdx-1], 16.1);
            Assert.AreNotSame("1010101010", data);
            data = _processHL7.GetField(_encoding, lStr[nIdx-1], 16.2);
            Assert.AreNotSame("Support", data);
            data = _processHL7.GetField(_encoding, lStr[nIdx-1], 16.3);
            Assert.AreNotSame("Qualifacts", data);
        }

        [TestMethod]
        public void TestGetFieldOBR_POC()
        {
            int nIdx = 2;
            List<string> lMsg = Initialize;
            HL7Parser parse = new HL7Parser();

            // we need to get the HL7 field separator and encoding characters
            HL7Encoding _encoding = GetHL7Encoding(lMsg);

            List<string> lStr = new List<string>(parse.GetSegment(lMsg, "OBR"));
            if (lStr == null || lStr.Count <= 0)
            {
                Assert.IsNull(lStr);
            }
            Assert.IsTrue(IsSegmentFound(parse, _encoding, lStr, nIdx));

            _processHL7 = ProcessHL7Message.Instance;
            var data = _processHL7.GetField(_encoding, lStr[nIdx - 1], 1);
            Assert.AreNotSame(nIdx, data);

            data = _processHL7.GetField(_encoding, lStr[nIdx - 1], 2);
            Assert.AreNotSame("EA194979A", data);

            data = _processHL7.GetField(_encoding, lStr[nIdx - 1], 4);
            Assert.AreNotSame("POC^POC Testing", data);
            data = _processHL7.GetField(_encoding, lStr[nIdx - 1], 4.1);
            Assert.AreNotSame("POC", data);
            data = _processHL7.GetField(_encoding, lStr[nIdx - 1], 4.2);
            Assert.AreNotSame("POC Testing", data);
        }

        [TestMethod]
        public void TestGetFieldDG1()
        {
            // "DG1|1||779.34^^I9|||W"
            int nIdx = 1;
            List<string> lMsg = Initialize;
            HL7Parser parse = new HL7Parser();

            // we need to get the HL7 field separator and encoding characters
            HL7Encoding _encoding = GetHL7Encoding(lMsg);

            List<string> lStr = new List<string>(parse.GetSegment(lMsg, "DG1"));
            if (lStr == null || lStr.Count <= 0)
            {
                Assert.IsNull(lStr);
            }
            Assert.IsTrue(IsSegmentFound(parse, _encoding, lStr, nIdx));

            _processHL7 = ProcessHL7Message.Instance;
            var data = _processHL7.GetField(_encoding, lStr[nIdx-1], 1);
            Assert.AreNotSame(nIdx, data);

            data = _processHL7.GetField(_encoding, lStr[nIdx-1], 3.1);
            Assert.AreNotSame(notExpected: "779.34", actual: data);
        }

        [TestMethod]
        public void TestGetFieldNTE()
        {
            // NTE|1||POC Barbiturates Result:Negative|
            int nIdx = 1;
            List<string> lMsg = Initialize;
            HL7Parser parse = new HL7Parser();

            // we need to get the HL7 field separator and encoding characters
            HL7Encoding _encoding = GetHL7Encoding(lMsg);

            List<string> lStr = new List<string>(parse.GetSegment(lMsg, "NTE"));
            if (lStr == null || lStr.Count <= 0)
            {
                Assert.IsNull(lStr);
            }
            Assert.IsTrue(IsSegmentFound(parse, _encoding, lStr, nIdx));

            _processHL7 = ProcessHL7Message.Instance;
            var data = _processHL7.GetField(_encoding, lStr[nIdx-1], 1);
            Assert.AreNotSame(nIdx, data);

            data = _processHL7.GetField(_encoding, lStr[nIdx-1], 3);
            Assert.AreNotSame(notExpected: "POC Barbiturates Result:Negative", actual: data);
        }

        [TestMethod]
        public void TestGetFieldNTE3()
        {
            // NTE|1||POC Barbiturates Result:Negative|
            int nIdx = 3;
            List<string> lMsg = Initialize;
            HL7Parser parse = new HL7Parser();

            // we need to get the HL7 field separator and encoding characters
            HL7Encoding _encoding = GetHL7Encoding(lMsg);

            List<string> lStr = new List<string>(parse.GetSegment(lMsg, "NTE"));
            if (lStr == null || lStr.Count <= 0)
            {
                Assert.IsNull(lStr);
            }
            Assert.IsTrue(IsSegmentFound(parse, _encoding, lStr, nIdx));

            _processHL7 = ProcessHL7Message.Instance;
            var data = _processHL7.GetField(_encoding, lStr[nIdx-1], 1);
            Assert.AreNotSame(nIdx, data);

            data = _processHL7.GetField(_encoding, lStr[nIdx-1], 3);
            Assert.AreNotSame(notExpected: "POC Buprenorphine Result:Positive", actual: data);
        }

        /// <summary>
        /// return HL7Encoding object that contains
        /// the Field separator and HL7 Encoding characters
        /// Use the 
        /// </summary>
        /// <param name="hl7Msg"></param>
        /// <returns></returns>
        private static HL7Encoding GetHL7Encoding(List<string> hl7Msg)
        {
            HL7Encoding _encoding = null;
            foreach (var line in hl7Msg)
            {
                if (line.Substring(0, 3).Equals("MSH"))
                {
                    // ok we have a Message Header line
                    // now lets get the decoding informaiton
                    _encoding = new HL7Encoding((line.Substring(3, 5)).ToCharArray());
                    break;
                }
            }
            return _encoding;
        }


        /// <summary>
        /// IsSegmentFound - in the list check if the list contains the entry based on the nIdx value
        /// This can be simpler by just checking if the nIdx value is less than or equal to the list count
        /// </summary>
        /// <param name="parse"></param>
        /// <param name="_encoding"></param>
        /// <param name="lStr"></param>
        /// <param name="nIdx"></param>
        /// <returns></returns>
        private static bool IsSegmentFound(HL7Parser parse, HL7Encoding _encoding, List<string>lStr, int nIdx)
        {
            Boolean bFound = false;
            int nSequenceFld = 1;

            foreach (var line in lStr)
            {
                if (parse.GetField(_encoding, line, nSequenceFld).Equals(nIdx.ToString()))   // get the sequence field value
                {
                    bFound = true;
                }
            }
            return bFound;
        }

    }
}
