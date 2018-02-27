using org.apache.pdfbox.pdmodel.font;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.WEB.Util
{
    class PdfBoxParrafo
    {
        /** position X */
        private float x;

        /** position Y */
        private float y;

        /** width of this paragraph */
        private int width;

        /** text to write */
        private String text;

        /** font to use */
        private PDType1Font font = PDType1Font.HELVETICA;

        /** font size to use */
        private int fontSize = 10;

        private int color = 0;

        public PdfBoxParrafo(float x, float y, int width, String text)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.text = text;
        }

        /**
         * Break the text in lines
         * @return
         * @throws java.io.IOException
         */
        public List<String> getLines() 
        {
            List<String> result = new List<String>();

            String[] split = text.Split('(','?','<','=','\\','W',')');
            //int[] possibleWrapPoints = new int[split.Length];
            //possibleWrapPoints[0] = split[0].Length;
            //for (int i = 1; i < split.Length; i++)
            //{
            //    possibleWrapPoints[i] = possibleWrapPoints[i - 1] + split[i].Length;
            //}

            //int start = 0;
            //int end = 0;
            //foreach (int i in possibleWrapPoints)
            //{
            //    float width = font.getStringWidth(text.Substring(start, i- start)) / 1000 * fontSize;
            //    if (start < end && width > this.width)
            //    {
            //        result.Add(text.Substring(start, i - start));
            //        start = end;
            //    }
            //    end = i;
            //}
            //// Last piece of text
            //result.Add(text.Substring(start));
            //return result;

            ////int iIdx = 0;
            ////int iIdxRenIni = 0;
            ////int iIdxRenLen = 1;
            ////while (iIdx < text.Length)
            ////{
            ////    float fWidthLocal = font.getStringWidth( text.Substring(iIdxRenIni, iIdxRenLen) ) / 1000 * fontSize;

            ////    if (fWidthLocal < this.width)
            ////        iIdxRenLen++;
            ////    else
            ////    {
            ////        result.Add(text.Substring(iIdxRenIni, iIdxRenLen ) );
            ////        iIdxRenIni = iIdxRenIni + iIdxRenLen;
            ////        iIdxRenLen = 1;
            ////    }

            ////    iIdx++;
            ////}
            ////result.Add(text.Substring(iIdxRenIni));
            ////return result;

            split = text.Split(' ');
            int iIdx = 0;
            int iIdxRenIni = 0;
            int iIdxRenLen = 0;
            float fWidthLocal = 0;
            while (iIdx < split.Length)
            {
                fWidthLocal = font.getStringWidth(text.Substring(iIdxRenIni, iIdxRenLen)) / 1000 * fontSize;

                if (fWidthLocal < this.width)
                {
                    iIdxRenLen = iIdxRenLen + split[iIdx].Length + 1;
                    iIdx++;
                }
                else
                {
                    result.Add(text.Substring(iIdxRenIni, iIdxRenLen));

                    iIdxRenIni = iIdxRenIni + iIdxRenLen;
                    iIdxRenLen = 0;
                }                
            }

            fWidthLocal = font.getStringWidth(text.Substring(iIdxRenIni)) / 1000 * fontSize;

            if (fWidthLocal > this.width)
            {
                iIdxRenLen = iIdxRenLen - split[iIdx-1].Length ;
                result.Add(text.Substring(iIdxRenIni, iIdxRenLen));
                result.Add(text.Substring(iIdxRenIni+ iIdxRenLen +1 ));
            }
            else
            {
                result.Add(text.Substring(iIdxRenIni));
            }
            
            return result;

        }

        public float getFontHeight()
        {
            return font.getFontDescriptor().getFontBoundingBox().getHeight() / 1000 * fontSize;
        }

        public PdfBoxParrafo withWidth(int width)
        {
            this.width = width;
            return this;
        }

        public PdfBoxParrafo withFont(PDType1Font font, int fontSize)
        {
            this.font = font;
            this.fontSize = fontSize;
            return this;
        }

        public PdfBoxParrafo withColor(int color)
        {
            this.color = color;
            return this;
        }

        public int getColor()
        {
            return color;
        }

        public float getX()
        {
            return x;
        }

        public float getY()
        {
            return y;
        }

        public int getWidth()
        {
            return width;
        }

        public String getText()
        {
            return text;
        }

        public PDType1Font getFont()
        {
            return font;
        }

        public int getFontSize()
        {
            return fontSize;
        }    
    }
}
