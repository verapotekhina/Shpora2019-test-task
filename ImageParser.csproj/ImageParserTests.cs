using System;
using NUnit.Framework;

namespace ImageParser
{
    [TestFixture]
    public class ImageParserTests
    {
        private ImageParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new ImageParser();
        }

        [Test]
        public void YouTried()
        {
            byte[] bytes = new byte[4] {128, 2, 0, 0};
            Console.WriteLine(BitConverter.ToInt32(bytes, 0));
        }
   }
}