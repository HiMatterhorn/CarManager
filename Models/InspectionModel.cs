using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmiFlota.Models
{
    public class InspectionModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public ushort OilCheck { get; set; }
        public string OilRemarks { get; set; }
        public ushort TiresCheck { get; set; }
        public string TiresRemarks { get; set; }
        public ushort LightsCheck { get; set; }
        public string LightsRemarks { get; set; }
        public ushort WashersCheck { get; set; }
        public string WashersRemarks { get; set; }
        public ushort Damages { get; set; }
        public string DamagesRemarks { get; set; }
    }
}
