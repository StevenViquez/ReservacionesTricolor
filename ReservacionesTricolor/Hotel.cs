//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReservacionesTricolor
{
    using System;
    using System.Collections.Generic;
    
    public partial class Hotel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hotel()
        {
            this.Habitacion = new HashSet<Habitacion>();
        }
    
        public int IdHotel { get; set; }
        public string NombreHotel { get; set; }
        public string Descripcion { get; set; }
        public int Estrellas { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public int IdAdministrador { get; set; }
        public int IdPais { get; set; }
        public int IdProvincia { get; set; }
        public int IdCanton { get; set; }
    
        public virtual Canton Canton { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Habitacion> Habitacion { get; set; }
        public virtual Pais Pais { get; set; }
        public virtual Provincia Provincia { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}