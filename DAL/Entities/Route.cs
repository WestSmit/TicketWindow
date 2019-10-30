using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TrainName { get; set; }
        public string TrainNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public int DaysDuration { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string InternalData { get; set; }

        [NotMapped]
        public int[] Stations
        {
            get
            {
                return Array.ConvertAll(InternalData.Split(';'), Int32.Parse);
            }
            set
            {

                    InternalData = String.Join(";", value);
                
            }
        }


        [EditorBrowsable(EditorBrowsableState.Never)]
        public string InternalTimeOfStopPoints { get; set; }

        [NotMapped]
        public string[] TimeOfStopPoints
        {
            get
            {
                return InternalTimeOfStopPoints.Split(';');
            }
            set
            {

                InternalTimeOfStopPoints = string.Join(";", value);

            }
        }        

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string InternalDataDays { get; set; }

        [NotMapped]
        public string[] Days
        {
            get
            {
                return InternalDataDays.Split(';');
            }
            set
            {

                InternalDataDays = String.Join(";", value);

            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string InternalDataCarriages { get; set; }

        [NotMapped]
        public int[] CarriagesId
        {
            get
            {
                return Array.ConvertAll(InternalDataCarriages.Split(';'), Int32.Parse);
            }
            set
            {

                InternalDataCarriages = String.Join(";", value);

            }
        }



    }
}
