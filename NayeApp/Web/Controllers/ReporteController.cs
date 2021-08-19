using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Core.Services;
using Infraestructure.Models;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO;
using System.Reflection;
using Core.Interfaces;
using iText.Kernel.Geom;
using System.Linq;

namespace Web.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReportesSalidas()
        {
            return View();
        }

        public ActionResult CreatePdfEntradas(DateTime? date1, DateTime? date2)
        {
            //Ejemplos IText7 https://kb.itextpdf.com/home/it7kb/examples
            IEnumerable<Movimientos> lista = null;
            try
            {
                // Extraer informacion
                IServiceMovimientos _ServiceMovimientos = new ServiceMovimientos();

                if (date1 != null && date2 != null)
                {
                    lista = _ServiceMovimientos.GetMovimientosEntradaFechas(date1.Value, date2.Value);
                }
                else if (date1 != null && date2 == null)
                {
                    lista = _ServiceMovimientos.GetMovimientosEntradaFechas(date1.Value, null);
                }

                // Crear stream para almacenar en memoria el reporte 
                MemoryStream ms = new MemoryStream();
                //Initialize writer
                PdfWriter writer = new PdfWriter(ms);

                //Initialize document
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document doc = new Document(pdfDoc);

                Paragraph header = new Paragraph("Movimientos de entrada por fecha")
                                   .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                                   .SetFontSize(14)
                                   .SetFontColor(ColorConstants.BLUE);
                doc.Add(header);


                // Crear tabla con 5 columnas 
                Table table = new Table(9, true);


                // los Encabezados
                table.AddHeaderCell("Nombre de Producto");
                table.AddHeaderCell("Comentario");
                table.AddHeaderCell("Cantidad");
                table.AddHeaderCell("Precio");
                table.AddHeaderCell("Tipo de movimiento");
                table.AddHeaderCell("Proveedor");
                table.AddHeaderCell("Nombre de usuario");
                table.AddHeaderCell("Creado");
                table.AddHeaderCell("Total");


                foreach (var item in lista)
                {

                    List proveedor = new List();
                    // Agregar datos a las celdas
                    table.AddCell(new Paragraph(item.Producto.Nombre));
                    table.AddCell(new Paragraph(item.Comentario));
                    table.AddCell(new Paragraph(item.Cantidad.ToString()));
                    table.AddCell(new Paragraph(item.Producto.Precio.ToString()));
                    table.AddCell(new Paragraph(item.TipoMovimiento.Tipo));

                    foreach (var p in item.Producto.Proveedor)
                    {
                        proveedor.Add(p.Nombre);
                    }
                    table.AddCell(proveedor);

                    table.AddCell(new Paragraph(item.Usuario.Nombre));
                    table.AddCell(new Paragraph(item.Creado.ToString()));
                    table.AddCell(new Paragraph(item.Total.ToString()));
                    proveedor = null;


                }
                doc.Add(table);



                // Colocar número de páginas
                int numberOfPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {

                    // Write aligned text to the specified by parameters point
                    doc.ShowTextAligned(new Paragraph(String.Format("pag {0} of {1}", i, numberOfPages)),
                            559, 826, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);
                }


                //Close document
                doc.Close();

                // Retorna un File
                return File(ms.ToArray(), "application/pdf", "reporte-entradas.pdf");


            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }

        public ActionResult CreatePdfSalidas(DateTime? date1, DateTime? date2)
        {
            //Ejemplos IText7 https://kb.itextpdf.com/home/it7kb/examples
            IEnumerable<Movimientos> lista = null;
            try
            {
                // Extraer informacion
                IServiceMovimientos _ServiceMovimientos = new ServiceMovimientos();


                if (date1 != null && date2 != null)
                {
                    lista = _ServiceMovimientos.GetMovimientosSalidasFechas(date1, date2);
                }
                else if (date1 != null && date2 == null)
                {
                    lista = _ServiceMovimientos.GetMovimientosSalidasFechas(date1, null);
                }

                // Crear stream para almacenar en memoria el reporte 
                MemoryStream ms = new MemoryStream();
                //Initialize writer
                PdfWriter writer = new PdfWriter(ms);

                //Initialize document
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document doc = new Document(pdfDoc);

                Paragraph header = new Paragraph("Movimientos de salida por fecha")
                                   .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                                   .SetFontSize(14)
                                   .SetFontColor(ColorConstants.BLUE);
                doc.Add(header);


                // Crear tabla con 5 columnas 
                Table table = new Table(8, true);


                // los Encabezados
                table.AddHeaderCell("Nombre de Producto");
                table.AddHeaderCell("Comentario");
                table.AddHeaderCell("Cantidad");
                table.AddHeaderCell("Precio");
                table.AddHeaderCell("Tipo de movimiento");
                table.AddHeaderCell("Proveedor");
                table.AddHeaderCell("Nombre de usuario");
                table.AddHeaderCell("Creado");
                table.AddHeaderCell("Total");


                foreach (var item in lista)
                {

                    List proveedor = new List();
                    // Agregar datos a las celdas
                    table.AddCell(new Paragraph(item.Producto.Nombre));
                    table.AddCell(new Paragraph(item.Comentario));
                    table.AddCell(new Paragraph(item.Cantidad.ToString()));
                    table.AddCell(new Paragraph(item.Producto.Precio.ToString()));
                    table.AddCell(new Paragraph(item.TipoMovimiento.Tipo));

                    foreach (var p in item.Producto.Proveedor)
                    {
                        proveedor.Add(p.Nombre);
                    }
                    table.AddCell(proveedor);

                    table.AddCell(new Paragraph(item.Usuario.Nombre));
                    table.AddCell(new Paragraph(item.Creado.ToString()));
                    table.AddCell(new Paragraph(item.Total.ToString()));
                    proveedor = null;


                }
                doc.Add(table);



                // Colocar número de páginas
                int numberOfPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {

                    // Write aligned text to the specified by parameters point
                    doc.ShowTextAligned(new Paragraph(String.Format("pag {0} of {1}", i, numberOfPages)),
                            559, 826, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);
                }


                //Close document
                doc.Close();
                // Retorna un File
                return File(ms.ToArray(), "application/pdf", "reporte-salidas.pdf");

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }

        public PartialViewResult ReportesEntradasPartial(DateTime? date1,DateTime? date2)
        {
            IEnumerable<Movimientos> movimientos = null;
            IServiceMovimientos _ServiceMovimientos = new ServiceMovimientos();
            ViewBag.Date1 = date1;
            ViewBag.Date2 = date2;



            if (date1 != null  && date2 != null)
            {
                movimientos = _ServiceMovimientos.GetMovimientosEntradaFechas(date1.Value,date2.Value);
            }
            else if (date1 != null && date2 == null)
            {
                movimientos = _ServiceMovimientos.GetMovimientosEntradaFechas(date1.Value,null);
            }
            else
            {
                movimientos = null;
            }


            return PartialView("_PartialViewReportesEntrada", movimientos);

        }

        public PartialViewResult ReportesSalidasPartial(DateTime? date1, DateTime? date2)
        {
            IEnumerable<Movimientos> movimientos = null;
            IServiceMovimientos _ServiceMovimientos = new ServiceMovimientos();
            ViewBag.Date1 = date1;
            ViewBag.Date2 = date2;

            if (date1 != null && date2 != null)
            {
                movimientos = _ServiceMovimientos.GetMovimientosSalidasFechas(date1, date2);
            }
            else if (date1 != null && date2 == null)
            {
                movimientos = _ServiceMovimientos.GetMovimientosSalidasFechas(date1, null);
            }
            else
            {
                movimientos = null;
            }

            return PartialView("_PartialViewReportesSalida", movimientos);

        }

        public ActionResult ReporteMayorCantidadSalidas()
        {
            IEnumerable<Movimientos> movimientos = null;
            IServiceMovimientos _ServiceMovimientos = new ServiceMovimientos();

            movimientos = _ServiceMovimientos.GetMovimientosMayorSalida();

            return View(movimientos.OrderByDescending(x => x.Cantidad).Take(3));
        }

        public ActionResult CreatePDFMayorCantidadSalidas()
        {
            //Ejemplos IText7 https://kb.itextpdf.com/home/it7kb/examples
            IEnumerable<Movimientos> lista = null;
            try
            {
                // Extraer informacion
                IServiceMovimientos _ServiceMovimientos = new ServiceMovimientos();
                lista = _ServiceMovimientos.GetMovimientosMayorSalida().OrderByDescending(x => x.Cantidad).Take(3);

                // Crear stream para almacenar en memoria el reporte 
                MemoryStream ms = new MemoryStream();
                //Initialize writer
                PdfWriter writer = new PdfWriter(ms);

                //Initialize document
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document doc = new Document(pdfDoc);

                Paragraph header = new Paragraph("Mayor Cantidad Salidas")
                                   .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                                   .SetFontSize(14)
                                   .SetFontColor(ColorConstants.BLUE);
                doc.Add(header);


                // Crear tabla con 5 columnas 
                Table table = new Table(5, true);


                // los Encabezados
                table.AddHeaderCell("Nombre de Producto");
                table.AddHeaderCell("Proveedor");
                table.AddHeaderCell("Tipo Movimiento");
                table.AddHeaderCell("Cantidad de productos despachados");
                table.AddHeaderCell("Cantidad de salidas registradas");


                foreach (var item in lista)
                {

                    List proveedor = new List();
                    // Agregar datos a las celdas
                    table.AddCell(new Paragraph(item.Producto.Nombre));
                    foreach (var p in item.Producto.Proveedor)
                    {
                        proveedor.Add(p.Nombre);
                    }
                    table.AddCell(proveedor);
                    table.AddCell(new Paragraph(item.TipoMovimiento.Tipo));
                    table.AddCell(new Paragraph(item.Cantidad.ToString()));
                    table.AddCell(new Paragraph(item.Id.ToString()));
                    proveedor = null;


                }
                doc.Add(table);



                // Colocar número de páginas
                int numberOfPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {

                    // Write aligned text to the specified by parameters point
                    doc.ShowTextAligned(new Paragraph(String.Format("pag {0} of {1}", i, numberOfPages)),
                            559, 826, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);
                }


                //Close document
                doc.Close();

                // Retorna un File
                return File(ms.ToArray(), "application/pdf", "mayor-cantidad-salidas.pdf");


            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }


    }
}
