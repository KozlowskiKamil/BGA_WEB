using System.ComponentModel.DataAnnotations;

namespace BGA.Entites
{
    public class Repair
    {
        public long Id { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public string Analysis { get; set; }

        public string? Comment { get; set; }

        public string LocationComponent { get; set; }

        public string Defect { get; set; }

        public string Client { get; set; }

        public string TesterProcess { get; set; }

        public string Machine { get; set; }

        public string RepairMethod { get; set; }
        public string Pass { get; set; }
        public string Fail { get; set; }


        [DataType(DataType.Date)]
        public DateTime LocalDate { get; set; }
    }
}