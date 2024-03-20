using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Lib
{
    public class Entry
    {
        public bool Favourite { get; set; } = false;
        public string Description { get; set; } = string.Empty;

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int StartKM { get; set; }

        public int EndKM { get; set; }

        public int Distance => EndKM - StartKM;

        public string NumberPlate { get; set; }

        public string From { get; set; }

        public string To { get; set; }


        /// <summary>
        /// ID of an entry
        /// </summary>
        public string Id { get; set; }

        public Entry(DateTime start, DateTime end, int startKM, int endKM, string numberPlate, string from, string to, bool favourite, string id)
        {
            this.Start = start;
            this.End = end;
            this.StartKM = startKM;
            this.EndKM = endKM;
            this.NumberPlate = numberPlate;
            this.From = from;
            this.To = to;
            this.Id = id;
            this.Favourite = favourite;
        }

        public Entry(DateTime start, DateTime end, int startKM, int endKM, string numberPlate, string from, string to, bool favourite)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Start = start;
            this.End = end;
            this.StartKM = startKM;
            this.EndKM = endKM;
            this.NumberPlate = numberPlate;
            this.From = from;
            this.To = to;
            this.Favourite = favourite;
        }

        public override string ToString()
        {
            return string.Format("{0} nach {1}", From, To);
        }
    }
}
