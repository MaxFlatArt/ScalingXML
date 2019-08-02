using System;
using System.Xml;
using System.IO;

namespace ScalingXML
{
    class Program
    {
        static XmlDocument Cloning(XmlNode element, XmlDocument xDoc)
        {
            XmlNode shallow = element.CloneNode(true);
            XmlNode root = xDoc.DocumentElement;
            root.AppendChild(shallow);

            return xDoc;
        }

        static long Quantity(XmlNode element, XmlDocument xDoc, int sizeAnswer)
        {
            FileInfo file = new FileInfo("ForClone.xml");
            var size = file.Length;

            Cloning(element, xDoc);
            xDoc.Save("ForClone.xml");

            file = new FileInfo("ForClone.xml");
            var newSize = file.Length;

            return (sizeAnswer - newSize) / (newSize - size);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите размер в MВ");

            var input = Console.ReadLine();
            int sizeAnswer = int.Parse(input);
            sizeAnswer = sizeAnswer * 1024 * 1024;

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("ForClone.xml");
            XmlNodeList element = xDoc.GetElementsByTagName("Employee");

            long countOfRepeat = Quantity(element[0], xDoc, sizeAnswer);

            double percentNew;
            var percentLast = 0;
            for (int i = 0; i < countOfRepeat; i++)
            {
                Cloning(element[0], xDoc);
                percentNew = ((i * 100) / countOfRepeat);
                percentNew = Math.Round(percentNew);
                if (percentNew != percentLast)
                {
                    Console.Write("\rЗавершено на: " + percentNew + "%");
                    percentLast = (int)percentNew;
                }
            }
            xDoc.Save("Cloning.xml");
            Console.Write("\rЗавершено на: 100%\n");
        }
    }
}

