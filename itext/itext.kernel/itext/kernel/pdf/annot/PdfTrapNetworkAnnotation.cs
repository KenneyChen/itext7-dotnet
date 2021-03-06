/*

This file is part of the iText (R) project.
Copyright (c) 1998-2016 iText Group NV
Authors: Bruno Lowagie, Paulo Soares, et al.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using System.Collections.Generic;
using iText.Kernel;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Xobject;

namespace iText.Kernel.Pdf.Annot {
    public class PdfTrapNetworkAnnotation : PdfAnnotation {
        public PdfTrapNetworkAnnotation(Rectangle rect, PdfFormXObject appearanceStream)
            : base(rect) {
            if (appearanceStream.GetProcessColorModel() == null) {
                throw new PdfException("Process color model must be set in appearance stream for Trap Network annotation!"
                    );
            }
            SetNormalAppearance(appearanceStream.GetPdfObject());
            SetFlags(PdfAnnotation.PRINT | PdfAnnotation.READ_ONLY);
        }

        public PdfTrapNetworkAnnotation(PdfDictionary pdfObject)
            : base(pdfObject) {
        }

        public override PdfName GetSubtype() {
            return PdfName.TrapNet;
        }

        public virtual iText.Kernel.Pdf.Annot.PdfTrapNetworkAnnotation SetLastModified(PdfDate lastModified) {
            return (iText.Kernel.Pdf.Annot.PdfTrapNetworkAnnotation)Put(PdfName.LastModified, lastModified.GetPdfObject
                ());
        }

        public virtual PdfString GetLastModified() {
            return GetPdfObject().GetAsString(PdfName.LastModified);
        }

        public virtual iText.Kernel.Pdf.Annot.PdfTrapNetworkAnnotation SetVersion(PdfArray version) {
            return (iText.Kernel.Pdf.Annot.PdfTrapNetworkAnnotation)Put(PdfName.Version, version);
        }

        public virtual PdfArray GetVersion() {
            return GetPdfObject().GetAsArray(PdfName.Version);
        }

        public virtual iText.Kernel.Pdf.Annot.PdfTrapNetworkAnnotation SetAnnotStates(PdfArray annotStates) {
            return (iText.Kernel.Pdf.Annot.PdfTrapNetworkAnnotation)Put(PdfName.AnnotStates, annotStates);
        }

        public virtual PdfArray GetAnnotStates() {
            return GetPdfObject().GetAsArray(PdfName.AnnotStates);
        }

        public virtual iText.Kernel.Pdf.Annot.PdfTrapNetworkAnnotation SetFauxedFonts(PdfArray fauxedFonts) {
            return (iText.Kernel.Pdf.Annot.PdfTrapNetworkAnnotation)Put(PdfName.FontFauxing, fauxedFonts);
        }

        public virtual iText.Kernel.Pdf.Annot.PdfTrapNetworkAnnotation SetFauxedFonts(IList<PdfFont> fauxedFonts) {
            PdfArray arr = new PdfArray();
            foreach (PdfFont f in fauxedFonts) {
                arr.Add(f.GetPdfObject());
            }
            return SetFauxedFonts(arr);
        }

        public virtual PdfArray GetFauxedFonts() {
            return GetPdfObject().GetAsArray(PdfName.FontFauxing);
        }
    }
}
