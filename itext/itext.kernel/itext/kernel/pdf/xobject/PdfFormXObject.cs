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
using iText.Kernel;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Wmf;

namespace iText.Kernel.Pdf.Xobject {
    public class PdfFormXObject : PdfXObject {
        private PdfResources resources = null;

        public PdfFormXObject(Rectangle bBox)
            : base(new PdfStream()) {
            GetPdfObject().Put(PdfName.Type, PdfName.XObject);
            GetPdfObject().Put(PdfName.Subtype, PdfName.Form);
            if (bBox != null) {
                GetPdfObject().Put(PdfName.BBox, new PdfArray(bBox));
            }
        }

        public PdfFormXObject(PdfStream pdfObject)
            : base(pdfObject) {
        }

        /// <summary>Creates form XObject from page content.</summary>
        /// <remarks>
        /// Creates form XObject from page content.
        /// The page shall be from the document, to which FormXObject will be added.
        /// </remarks>
        /// <param name="page"/>
        public PdfFormXObject(PdfPage page)
            : this(page.GetCropBox()) {
            GetPdfObject().GetOutputStream().WriteBytes(page.GetContentBytes());
            resources = new PdfResources((PdfDictionary)page.GetResources().GetPdfObject().Clone());
            GetPdfObject().Put(PdfName.Resources, resources.GetPdfObject());
        }

        /// <summary>
        /// Creates a form XObject from
        /// <see cref="iText.Kernel.Pdf.Canvas.Wmf.WmfImageData"/>
        /// .
        /// Unlike other images,
        /// <see cref="iText.Kernel.Pdf.Canvas.Wmf.WmfImageData"/>
        /// images are represented as
        /// <see cref="PdfFormXObject"/>
        /// , not as
        /// <see cref="PdfImageXObject"/>
        /// .
        /// </summary>
        /// <param name="image">image to create form object from</param>
        /// <param name="pdfDocument">document instance which is needed for writing form stream contents</param>
        public PdfFormXObject(WmfImageData image, PdfDocument pdfDocument)
            : this(new WmfImageHelper(image).CreatePdfForm(pdfDocument).GetPdfObject()) {
        }

        public virtual PdfResources GetResources() {
            if (this.resources == null) {
                PdfDictionary resourcesDict = GetPdfObject().GetAsDictionary(PdfName.Resources);
                if (resourcesDict == null) {
                    resourcesDict = new PdfDictionary();
                    GetPdfObject().Put(PdfName.Resources, resourcesDict);
                }
                this.resources = new PdfResources(resourcesDict);
            }
            return resources;
        }

        public override void Flush() {
            resources = null;
            if (GetPdfObject().Get(PdfName.BBox) == null) {
                throw new PdfException(PdfException.FormXObjectMustHaveBbox);
            }
            base.Flush();
        }

        //Additional entries in form dictionary for Trap Network annotation
        public virtual iText.Kernel.Pdf.Xobject.PdfFormXObject SetProcessColorModel(PdfName model) {
            return Put(PdfName.PCM, model);
        }

        public virtual PdfName GetProcessColorModel() {
            return GetPdfObject().GetAsName(PdfName.PCM);
        }

        public virtual iText.Kernel.Pdf.Xobject.PdfFormXObject SetSeparationColorNames(PdfArray colorNames) {
            return Put(PdfName.SeparationColorNames, colorNames);
        }

        public virtual PdfArray GetSeparationColorNames() {
            return GetPdfObject().GetAsArray(PdfName.SeparationColorNames);
        }

        public virtual iText.Kernel.Pdf.Xobject.PdfFormXObject SetTrapRegions(PdfArray regions) {
            return Put(PdfName.TrapRegions, regions);
        }

        public virtual PdfArray GetTrapRegions() {
            return GetPdfObject().GetAsArray(PdfName.TrapRegions);
        }

        public virtual iText.Kernel.Pdf.Xobject.PdfFormXObject SetTrapStyles(PdfString trapStyles) {
            return Put(PdfName.TrapStyles, trapStyles);
        }

        public virtual PdfString GetTrapStyles() {
            return GetPdfObject().GetAsString(PdfName.TrapStyles);
        }

        //Additional entries in form dictionary for Printer Mark annotation
        public virtual iText.Kernel.Pdf.Xobject.PdfFormXObject SetMarkStyle(PdfString markStyle) {
            return Put(PdfName.MarkStyle, markStyle);
        }

        public virtual PdfString GetMarkStyle() {
            return GetPdfObject().GetAsString(PdfName.MarkStyle);
        }

        public virtual PdfArray GetBBox() {
            return GetPdfObject().GetAsArray(PdfName.BBox);
        }

        public virtual iText.Kernel.Pdf.Xobject.PdfFormXObject SetBBox(PdfArray bBox) {
            return Put(PdfName.BBox, bBox);
        }

        public virtual iText.Kernel.Pdf.Xobject.PdfFormXObject SetGroup(PdfTransparencyGroup transparency) {
            return Put(PdfName.Group, transparency.GetPdfObject());
        }

        public override float GetWidth() {
            return GetBBox() == null ? 0 : GetBBox().GetAsNumber(2).FloatValue();
        }

        public override float GetHeight() {
            return GetBBox() == null ? 0 : GetBBox().GetAsNumber(3).FloatValue();
        }

        public virtual iText.Kernel.Pdf.Xobject.PdfFormXObject Put(PdfName key, PdfObject value) {
            GetPdfObject().Put(key, value);
            return this;
        }
    }
}
