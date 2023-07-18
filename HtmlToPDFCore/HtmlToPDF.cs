﻿using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Wkhtmltopdf.NetCore;
using Wkhtmltopdf.NetCore.Options;

namespace HtmlToPDFCore
{
    public class HtmlToPDF
    {
        public PageMargins Margins { get; set; }
        public Size Size { get; set; } = Size.A4;
        public bool DisableSmartShrinking { get; set; }

        public byte[] ReturnPDF(string html)
        {
            var dir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            dir = Path.Combine(dir, "rotativa");
            WkhtmltopdfConfiguration.RotativaPath = dir;
            var pdf = new GeneratePdf(null);
            var convertOptions = new ConvertOptionsExtended();
            if (Margins != null)
            {
                convertOptions.PageMargins = new Margins(Margins.top, Margins.right, Margins.bottom, Margins.left);
            }
            convertOptions.PageSize = Size;

            convertOptions.DisableSmartShrinking = DisableSmartShrinking;
            pdf.SetConvertOptions(convertOptions);
            var buffer = pdf.GetPDF(html);
            return buffer;
        }
    }
}