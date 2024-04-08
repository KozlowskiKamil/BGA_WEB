using System.ComponentModel.DataAnnotations;

namespace BGA.Entites
{
    public class Rma
    {
        public long Id { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public string family { get; set; }

        public string? Comment { get; set; }

        public string Client { get; set; }

        public int duration { get; set; }


        [DataType(DataType.Date)]
        public DateTime LocalDate { get; set; }

    }
}
