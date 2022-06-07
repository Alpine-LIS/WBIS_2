using System;
    //using System.Collections.Generic;
    //using System.Data;
    using System.Linq;
    //using System.Text;
    //using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace WBIS_2.Modules.Tools
{
    public class WordHelper
    {
        public int AddIntroDescriptiveText(string field, string values, Word.Document doc, int runningTab, bool first = false)
        {
            if (!first) doc.Paragraphs.Add();
            doc.Paragraphs.Last.Range.Text = field + "  " + values;
            object start = runningTab;
            object filedEnd = runningTab + field.Length + 2;
            object textEnd = runningTab + field.Length + 2 + values.Length;

            doc.Range(ref start, ref filedEnd).Bold = 1;
            doc.Range(ref filedEnd, ref textEnd).Bold = 0;
            //doc.Range(ref start, ref textEnd).Cut();
            //doc.Paragraphs.Last.Range.Paste();
            //doc.Paragraphs.Add();
            return (int)textEnd + 2;
        }
        public int AddIntroDescriptiveTextWithTabs(string field1, string values1, string field2, string values2, Word.Document doc, int runningTab, bool first = false)
        {
            //if (!first) doc.Paragraphs.Add();
            doc.Paragraphs.Last.Range.Text = field1 + "  " + values1 + "\t\t" + field2 + "  " + values2;
            object start = runningTab;
            object filedEnd1 = runningTab + field1.Length + 2;
            object textEnd1 = runningTab + field1.Length + 2 + values1.Length + 2;

            object filedEnd2 = runningTab + field1.Length + 2 + values1.Length + 2 + field2.Length + 2;
            object textEnd2 = runningTab + field1.Length + 2 + values1.Length + 2 + field2.Length + 2 + values2.Length;

            doc.Range(ref start, ref filedEnd1).Bold = 1;
            doc.Range(ref filedEnd1, ref textEnd1).Bold = 0;
            doc.Range(ref textEnd1, ref filedEnd2).Bold = 1;
            doc.Range(ref filedEnd2, ref textEnd2).Bold = 0;
            //doc.Range(ref start, ref textEnd2).Cut();
            //doc.Paragraphs.Last.Range.Paste();
            doc.Paragraphs.Add();
            return (int)textEnd2;
        }

        public void FillCell(Word.Cell c, string field, string txt, Word.Document doc)
        {
            doc.Paragraphs.First.Range.Text = field + "  " + txt;
            object start = 0;
            object filedEnd = field.Length + 2;
            object textEnd = field.Length + 2 + txt.Length;

            doc.Range(ref start, ref filedEnd).Bold = 1;
            doc.Range(ref filedEnd, ref textEnd).Bold = 0;
            doc.Range(ref start, ref textEnd).Cut();
            c.Range.Paste();
        }


        public void FillCell(Word.Cell c, string field, string txt, Word.Range sectionRange)
        {
            sectionRange.Paragraphs.First.Range.Text = field + "  " + txt;
            object start = 0;
            object filedEnd = field.Length + 2;
            object textEnd = field.Length + 2 + txt.Length;

            sectionRange.Document.Range(ref start, ref filedEnd).Bold = 1;
            sectionRange.Document.Range(ref filedEnd, ref textEnd).Bold = 0;
            string val = sectionRange.Document.Range(ref start, ref textEnd).Text;
            sectionRange.Document.Range(ref start, ref textEnd).Cut();
            // c.Range.Select();
            c.Range.MoveStart();
            //c.Range.Paste();
            c.Range.Text = val;
        }
        public void WriteTextField(Word.Range sectionRange, string field, string txt)
        {
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Format.TabHangingIndent(-1);

            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = field + ":";
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
            sectionRange.Paragraphs.Last.Range.Text = txt;
        }
        public void WriteReferencesField(Word.Range sectionRange, string txt)
        {
            sectionRange.Paragraphs.Add();
            sectionRange.Paragraphs.Last.Format.TabHangingIndent(-1);
            sectionRange.Paragraphs.Last.Range.Font.Bold = 1;
            sectionRange.Paragraphs.Last.Range.Text = "References:";

            var refs = txt.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < refs.Count(); i++)
            {
                sectionRange.Paragraphs.Add();
                if (i == 0) sectionRange.Paragraphs.Last.Format.TabHangingIndent(1);
                else sectionRange.Paragraphs.Add();
                sectionRange.Paragraphs.Last.Range.Font.Bold = 0;
                sectionRange.Paragraphs.Last.Range.Text = refs[i];
            }

            //foreach (string re in refs)
            //{
            //    doc.Paragraphs.Add();
            //    if (re == refs[0])doc.Paragraphs.Last.Format.TabHangingIndent(1);
            //    doc.Paragraphs.Last.Range.Font.Bold = 0;
            //    doc.Paragraphs.Last.Range.Text = re;
            //}
        }
        public void WordCellItalicSpecies(Word.Cell cell, string species)
        {
            cell.Range.Italic = 1;
            cell.Range.Text = species;

            //Species with text such as var. or ssp. should have this text not be italicized
            if (species.Contains("."))
            {
                string nuance = species.Split(new char[] { ' ' }).First(_ => _.Contains("."));

                //Microsoft word indexes start at 1 not 0
                int index = species.Split(new char[] { ' ' }).ToList().IndexOf(nuance) + 1;
                var q = cell.Range.Paragraphs.First.Range;
                q.Words[index].Italic = 0;
            }
        }
    }
}
