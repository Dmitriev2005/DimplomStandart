using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Text =  DocumentFormat.OpenXml.Wordprocessing.Text;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimplomStandart.Classes
{
    public class WordHelper
    {
        public void Word(string path, Dictionary<string, string> items)
        {
            using (WordprocessingDocument document = WordprocessingDocument.Open(path, true))
            {
                MainDocumentPart mainPart = document.MainDocumentPart;

                Body body = mainPart.Document.Body;
                IEnumerable<Text> textElements = body.Descendants<Text>();//Wordprocessing

                foreach (var textElement in textElements)
                {
                    foreach (var item in items)
                    {
                        if (textElement.Text == item.Key)
                            textElement.Text = item.Value;
                        //else if (textElement.Text == "<DISCIPLINE>") ;

                    }
                }
                mainPart.Document.Save();
            }

        }

    }
}
