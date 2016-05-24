using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage.Models
{
   public class Vehicle
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You need to specify a registration number")]
        [MaxLength(101, ErrorMessage = "Cannot be longer than 100 characters")]
        public string Registration { get; set; }

        public int VehicleTypeId { get; set; }



        [Required(ErrorMessage = "You need to specify a color")]
        [MaxLength(101, ErrorMessage = "Cannot be longer then 100 characters")]
        public string Color { get; set; }

        public virtual Member Member { get; set; }



    }
}