using Microsoft.VisualStudio.TestTools.UnitTesting;
using PTOX_LIB.HL7.Controller;
using PTOX_LIB.HL7.Model;
using System;
using System.Collections.Generic;

namespace PTOX_LIB_Test
{
    [TestClass]
    public class BuildHeaderTest
    {
        static public List<string> Initialize
        {
            get
            {
                var lMsg = new List<string>
                {
                    @"MSH|^~\&|CareLogic^2.16.840.1.113883.3.1452.100.4|TVFFC1|Precision|Precision Diagnostics|201708281608||ORM^O01|20170828-107|P|2.3|||NE|NE"
                };
                return lMsg;
            }
        }

        static public List<string> InitializeNullMSH10
        {
            get
            {
                var lMsg = new List<string>
                {
                    @"MSH|^~\&|CareLogic^2.16.840.1.113883.3.1452.100.4|TVFFC1|Precision|Precision Diagnostics|201708281608||ORM^O01||P|2.3|||NE|NE"
                };
                return lMsg;
            }
        }

        static public List<string> InitializeDifferentSeparatorMSH
        {
            get
            {
                var lMsg = new List<string>
                {
                    @"MSH:^~\&:CareLogic^2.16.840.1.113883.3.1452.100.4:TVFFC1:Precision:Precision Diagnostics:201708281608::ORM^O01:20170828-107:P:2.3:::NE:NE"
                };
                return lMsg;
            }
        }

        [TestMethod]
        public void TestMSHHeader()
        {
            var sTmp = string.Empty;
            // HL7Parser parse = new HL7Parser();
            try
            {
                HL7Header header = new BuildHeader().GetHeader(Initialize);
                Assert.AreEqual('|', header.HL7Encoding.FieldSeparator);
                Assert.AreEqual("|^~\\&", header.HL7Encoding.GetEncoding());

                Assert.AreEqual("^~\\&", header.MSHSegment.Encoding);
                Assert.AreEqual("CareLogic", header.MSHSegment.SendingApp, true);
                Assert.AreEqual("TVFFC1", header.MSHSegment.SendingFacility, true);
                Assert.AreEqual("201708281608", header.MSHSegment.TimeOfMessage, true);
                Assert.AreEqual("ORM^O01", header.MSHSegment.MessageType, true);
                Assert.AreEqual("20170828-107", header.MSHSegment.MessageControlId, true);
                Assert.AreEqual("2.3", header.MSHSegment.Version, true);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.ToString());
            }
        }

        [TestMethod]
        public void TestMSHHeaderMissingfield10()
        {
            var sTmp = string.Empty;
            HL7Parser parse = new HL7Parser();
            try
            {
                HL7Header header = new BuildHeader().GetHeader(InitializeNullMSH10);
                // Assert.IsNull(header.MSHSegment.MessageControlId);
                Assert.IsTrue(string.IsNullOrEmpty(header.MSHSegment.MessageControlId));
                if (header.MSHSegment.Errors.Count < 0)
                {
                    Assert.Fail("No error count value.   field tested for null, should be an error");
                }
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.ToString());
            }
        }

        [TestMethod]
        public void TestDifferentSeparatorMSH()
        {
            var sTmp = string.Empty;
            HL7Parser parse = new HL7Parser();
            try
            {
                HL7Header header = new BuildHeader().GetHeader(InitializeDifferentSeparatorMSH);
                Assert.AreEqual(':', header.HL7Encoding.FieldSeparator);
                Assert.AreEqual(":^~\\&", header.HL7Encoding.GetEncoding());

                Assert.AreEqual("^~\\&", header.MSHSegment.Encoding);
                Assert.AreEqual("CareLogic", header.MSHSegment.SendingApp, true);
                Assert.AreEqual("TVFFC1", header.MSHSegment.SendingFacility, true);
                Assert.AreEqual("201708281608", header.MSHSegment.TimeOfMessage, true);
                Assert.AreEqual("ORM^O01", header.MSHSegment.MessageType, true);
                Assert.AreEqual("20170828-107", header.MSHSegment.MessageControlId, true);
                Assert.AreEqual("2.3", header.MSHSegment.Version, true);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.ToString());
            }
        }
    }
}
