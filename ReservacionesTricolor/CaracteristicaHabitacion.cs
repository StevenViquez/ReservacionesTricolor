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
    
    public partial class CaracteristicaHabitacion
    {
        public int Id { get; set; }
        public int IdHabitacion { get; set; }
        public int IdCaracteristica { get; set; }
    
        public virtual Caracteristica Caracteristica { get; set; }
        public virtual Habitacion Habitacion { get; set; }
    }
}
