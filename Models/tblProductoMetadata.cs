using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedidosComida.Models
{
    public partial class tblProducto
    {
        [NotMapped]
        public HttpPostedFileBase ImagenArchivo { get; set; }
    }
}
