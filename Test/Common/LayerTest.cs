using NUnit.Framework;
using Common;

namespace Test.Common
{
    public class LayerTest
    {
        [SetUp]
        public void Setup() {
            

        }

        [Test]
        public void Count()
        {
            Layer layer = new Layer(new int[] { 1, 2, 3, 1 }, 2, 2);

            Assert.AreEqual(2, layer.Count(1));
            Assert.AreEqual(1, layer.Count(2));
            Assert.AreEqual(1, layer.Count(3));
            Assert.AreEqual(0, layer.Count(0));
        }

        [Test]
        public void Apply()
        {
            Layer layer1 = new Layer(new int[] { (int)PixelColor.White, (int)PixelColor.Black, (int)PixelColor.White, (int)PixelColor.Black }, 2, 2);
            Layer layer2 = new Layer(new int[] { (int)PixelColor.Transparent, (int)PixelColor.White, (int)PixelColor.Black, (int)PixelColor.Transparent }, 2, 2);

            layer1.Apply(layer2);

            Assert.AreEqual((int)PixelColor.White, layer1.GetPixels()[0]);
            Assert.AreEqual((int)PixelColor.White, layer1.GetPixels()[1]);
            Assert.AreEqual((int)PixelColor.Black, layer1.GetPixels()[2]);
            Assert.AreEqual((int)PixelColor.Black, layer1.GetPixels()[3]);
        }

        [Test]
        public void ToString_test()
        {
            Layer layer = new Layer(new int[] { 1, 2, 3, 1 }, 2, 2);

            Assert.AreEqual("1\t2" + "\r\n" + "3\t1", layer.ToString());
               
        }
    }

}