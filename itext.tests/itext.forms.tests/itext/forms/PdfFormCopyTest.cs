using System;
using System.IO;
using iText.IO;
using iText.IO.Source;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Forms {
    public class PdfFormCopyTest : ExtendedITextTest {
        public static readonly String sourceFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory + "/../../resources/itext/forms/PdfFormFieldsCopyTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/forms/PdfFormFieldsCopyTest/";

        [NUnit.Framework.TestFixtureSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(LogMessageConstant.DOCUMENT_ALREADY_HAS_FIELD, Count = 13)]
        public virtual void CopyFieldsTest01() {
            String srcFilename1 = sourceFolder + "appearances1.pdf";
            String srcFilename2 = sourceFolder + "fieldsOn2-sPage.pdf";
            String srcFilename3 = sourceFolder + "fieldsOn3-sPage.pdf";
            String filename = destinationFolder + "copyFields01.pdf";
            PdfDocument doc1 = new PdfDocument(new PdfReader(new FileStream(srcFilename1, FileMode.Open, FileAccess.Read
                )));
            PdfDocument doc2 = new PdfDocument(new PdfReader(new FileStream(srcFilename2, FileMode.Open, FileAccess.Read
                )));
            PdfDocument doc3 = new PdfDocument(new PdfReader(new FileStream(srcFilename3, FileMode.Open, FileAccess.Read
                )));
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(new FileStream(filename, FileMode.Create)));
            pdfDoc.InitializeOutlines();
            doc3.CopyPagesTo(1, doc3.GetNumberOfPages(), pdfDoc, new PdfPageFormCopier());
            doc2.CopyPagesTo(1, doc2.GetNumberOfPages(), pdfDoc, new PdfPageFormCopier());
            doc1.CopyPagesTo(1, doc1.GetNumberOfPages(), pdfDoc, new PdfPageFormCopier());
            pdfDoc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(filename, sourceFolder + "cmp_copyFields01.pdf"
                , destinationFolder, "diff_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CopyFieldsTest02() {
            String srcFilename = sourceFolder + "hello_with_comments.pdf";
            String filename = destinationFolder + "copyFields02.pdf";
            PdfDocument doc1 = new PdfDocument(new PdfReader(new FileStream(srcFilename, FileMode.Open, FileAccess.Read
                )));
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(new FileStream(filename, FileMode.Create)));
            pdfDoc.InitializeOutlines();
            doc1.CopyPagesTo(1, doc1.GetNumberOfPages(), pdfDoc, new PdfPageFormCopier());
            pdfDoc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(filename, sourceFolder + "cmp_copyFields02.pdf"
                , destinationFolder, "diff_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CopyFieldsTest03() {
            String srcFilename = sourceFolder + "hello2_with_comments.pdf";
            String filename = destinationFolder + "copyFields03.pdf";
            PdfDocument doc1 = new PdfDocument(new PdfReader(new FileStream(srcFilename, FileMode.Open, FileAccess.Read
                )));
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(new FileStream(filename, FileMode.Create)));
            pdfDoc.InitializeOutlines();
            doc1.CopyPagesTo(1, doc1.GetNumberOfPages(), pdfDoc, new PdfPageFormCopier());
            pdfDoc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(filename, sourceFolder + "cmp_copyFields03.pdf"
                , destinationFolder, "diff_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Timeout(60000)]
        [NUnit.Framework.Test]
        public virtual void LargeFilePerformanceTest() {
            String srcFilename1 = sourceFolder + "frontpage.pdf";
            String srcFilename2 = sourceFolder + "largeFile.pdf";
            String filename = destinationFolder + "copyLargeFile.pdf";
            long timeStart = System.DateTime.Now.Ticks;
            PdfDocument doc1 = new PdfDocument(new PdfReader(new FileStream(srcFilename1, FileMode.Open, FileAccess.Read
                )));
            PdfDocument doc2 = new PdfDocument(new PdfReader(new FileStream(srcFilename2, FileMode.Open, FileAccess.Read
                )));
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(new FileStream(filename, FileMode.Create)));
            pdfDoc.InitializeOutlines();
            doc1.CopyPagesTo(1, doc1.GetNumberOfPages(), pdfDoc, new PdfPageFormCopier());
            doc2.CopyPagesTo(1, doc2.GetNumberOfPages(), pdfDoc, new PdfPageFormCopier());
            pdfDoc.Close();
            System.Console.Out.WriteLine(((System.DateTime.Now.Ticks - timeStart) / 1000 / 1000));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(filename, sourceFolder + "cmp_copyLargeFile.pdf"
                , destinationFolder, "diff_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(LogMessageConstant.DOCUMENT_ALREADY_HAS_FIELD)]
        public virtual void CopyFieldsTest04() {
            String srcFilename = sourceFolder + "srcFile1.pdf";
            PdfDocument srcDoc = new PdfDocument(new PdfReader(new FileStream(srcFilename, FileMode.Open, FileAccess.Read
                )));
            PdfDocument destDoc = new PdfDocument(new PdfWriter(new ByteArrayOutputStream()));
            srcDoc.CopyPagesTo(1, srcDoc.GetNumberOfPages(), destDoc, new PdfPageFormCopier());
            srcDoc.CopyPagesTo(1, srcDoc.GetNumberOfPages(), destDoc, new PdfPageFormCopier());
            PdfAcroForm form = PdfAcroForm.GetAcroForm(destDoc, false);
            NUnit.Framework.Assert.AreEqual(1, form.GetFields().Size());
            NUnit.Framework.Assert.IsNotNull(form.GetField("Name1"));
            NUnit.Framework.Assert.IsNotNull(form.GetField("Name1.1"));
            destDoc.Close();
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CopyFieldsTest05() {
            String srcFilename = sourceFolder + "srcFile1.pdf";
            String destFilename = destinationFolder + "copyFields05.pdf";
            PdfDocument srcDoc = new PdfDocument(new PdfReader(new FileStream(srcFilename, FileMode.Open, FileAccess.Read
                )));
            PdfDocument destDoc = new PdfDocument(new PdfWriter(new FileStream(destFilename, FileMode.Create)));
            destDoc.AddPage(srcDoc.GetFirstPage().CopyTo(destDoc, new PdfPageFormCopier()));
            destDoc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destFilename, sourceFolder + "cmp_copyFields05.pdf"
                , destinationFolder, "diff_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CopyPagesWithInheritedResources() {
            String sourceFile = sourceFolder + "AnnotationSampleStandard.pdf";
            String destFile = destinationFolder + "AnnotationSampleStandard_copy.pdf";
            PdfReader reader = new PdfReader(new FileStream(sourceFile, FileMode.Open, FileAccess.Read));
            PdfWriter writer = new PdfWriter(destFile);
            PdfDocument source = new PdfDocument(reader);
            PdfDocument target = new PdfDocument(writer);
            target.InitializeOutlines();
            source.CopyPagesTo(1, source.GetNumberOfPages(), target, new PdfPageFormCopier());
            target.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destFile, sourceFolder + "cmp_AnnotationSampleStandard_copy.pdf"
                , destinationFolder, "diff_"));
        }
    }
}
