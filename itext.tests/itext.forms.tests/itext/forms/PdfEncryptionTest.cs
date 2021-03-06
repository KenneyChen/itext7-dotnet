using System;
using System.IO;
using iText.Kernel.Pdf;

namespace iText.Forms {
    public class PdfEncryptionTest {
        public static readonly String sourceFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory + "/../../resources/itext/forms/PdfEncryptionTest/";

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void EncryptedDocumentWithFormFields() {
            PdfReader reader = new PdfReader(new FileStream(sourceFolder + "encryptedDocumentWithFormFields.pdf", FileMode.Open
                , FileAccess.Read), new ReaderProperties().SetPassword("12345".GetBytes()));
            PdfDocument pdfDocument = new PdfDocument(reader);
            PdfAcroForm acroForm = PdfAcroForm.GetAcroForm(pdfDocument, false);
            acroForm.GetField("personal.name").GetPdfObject();
            pdfDocument.Close();
        }
    }
}
