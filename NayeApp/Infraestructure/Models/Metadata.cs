using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    public class Metadata
    {
        internal partial class UsuarioMetadata
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Ubicacion { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [DataType(DataType.EmailAddress, ErrorMessage = "{0} No tiene un formato válido")]
            public string Correo { get; set; }

            [Required(ErrorMessage = "{0} es un dato requerido")]
            [DataType(DataType.Password)]
            public string Clave { get; set; }
            [Display(Name = "Rol de usuario")]
            public int Tipo { get; set; }

        }

        internal partial class ProductoMetadata
        {
            public int Id { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Nombre de Producto")]

            public string Nombre { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Detalle de Producto")]
            public string Detalle { get; set; }

            [Required(ErrorMessage = "{0} es un dato requerido")]
            public double Precio { get; set; }

            [Required(ErrorMessage = "{0} es un dato requerido")]
            public int Unidades { get; set; }

            public System.DateTime Creado { get; set; }

            [Display(Name = "Categoría")]
            public int Id_Categoria { get; set; }

            
            [Display(Name = "Bodega")]
            public int Id_Bodega { get; set; }
            public virtual Bodega Bodega { get; set; }
            public virtual Categoria Categoria { get; set; }
        }

        internal partial class MovimientosMetadata
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Comentario { get; set; }

            [Required(ErrorMessage = "{0} es un dato requerido")]
            public int Cantidad { get; set; }
            public System.DateTime Creado { get; set; }

            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Nombre de producto")]
            public int Id_Producto { get; set; }

            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Nombre de usuario")]
            public int Id_Usuario { get; set; }



            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Tipo Movimiento")]
            public int Id_TipoMovimiento { get; set; }
            public virtual Producto Producto { get; set; }

       
            public virtual TipoMovimiento TipoMovimiento { get; set; }
            public virtual Usuario Usuario { get; set; }
        }

        internal partial class ProveedorMetadata
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Direccion { get; set; }
            [RegularExpression(@"([0-9]{4}-[0-9]{4})$", ErrorMessage = "Este campo solo adminte números o falta un guíon.")]
            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Telefono { get; set; }


            [DataType(DataType.EmailAddress, ErrorMessage = "{0} No tiene un formato válido")]
            public string Correo { get; set; }

            

        }

        internal partial class TipoMovimientoMetadata
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Tipo { get; set; }
        }

        internal partial class CategoriaMetadata
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Detalle { get; set; }
        }

        internal partial class BodegaMetadata
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Direccion { get; set; }

            [RegularExpression(@"([0-9]{4}-[0-9]{4})$", ErrorMessage = "Este campo solo adminte números o falta un guíon.")]
            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Telefono { get; set; }
        }

        internal partial class ContactoMetadata
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Nombre { get; set; }

            [DataType(DataType.EmailAddress, ErrorMessage = "{0} No tiene un formato válido")]
            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Correo { get; set; }

            [RegularExpression(@"([0-9]{4}-[0-9]{4})$", ErrorMessage = "Este campo solo adminte números o falta un guíon.")]
            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Telefono { get; set; }



        }
    }
}
