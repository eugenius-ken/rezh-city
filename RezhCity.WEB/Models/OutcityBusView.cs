using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RezhCity.WEB.Models
{
    public class BusView
    {
        public DateTime Time { get; set; }
        public bool IsNext { get; set; } = false;

        public BusView(DateTime time)
        {
            this.Time = time;
        }
    }

    public class OutcityBusView
    {
        private static DateTime _now = DateTime.Now.ToUniversalTime().AddHours(5); //get actual date no matter the timezone

        public string TimeClosest816ToEkb { get; set; }
        public string TimeClosest816ToRezh { get; set; }
        public string TimeClosest527ToEkb { get; set; }
        public string TimeClosest527ToRezh { get; set; }

        public IList<BusView> Buses816ToEkb { get; set; } = new List<BusView>()
        {
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 4, 20, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 4, 40, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 5, 20, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 5, 35, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 6, 10, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 6, 20, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 7, 55, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 8, 30, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 9, 0, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 9, 40, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 10, 30, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 11, 30, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 12, 30, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 13, 30, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 14, 15, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 15, 5, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 16, 0, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 16, 30, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 17, 35, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 19, 0, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 20, 20, 0))
        };

        public IList<BusView> Buses816ToRezh { get; set; } = new List<BusView>()
        {
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 6, 50, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 7, 15, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 8, 5, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 9, 5, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 9, 30, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 10, 25, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 10, 55, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 12, 30, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 13, 5, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 13, 25, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 14, 10, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 15, 5, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 16, 5, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 16, 30, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 17, 30, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 18, 35, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 19, 10, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 19, 35, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 21, 5, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 21, 35, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 23, 0, 0))
        };

        public IList<BusView> Buses527ToEkb { get; set; } = new List<BusView>()
        {
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 5, 5, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 6, 35, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 11, 15, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 13, 10, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 14, 40, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 17, 10, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 18, 15, 0))
        };

        public IList<BusView> Buses527ToRezh { get; set; } = new List<BusView>()
        {
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 8, 7, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 10, 47, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 14, 37, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 16, 07, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 17, 07, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 19, 52, 0)),
            new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 21, 07, 0))
        };

        private string GetClosest(IList<BusView> buses)
        {
            var result = String.Empty;

            for (int i = 0; i < buses.Count - 1; i++)
            {
                if (_now >= buses[i].Time && _now < buses[i + 1].Time)
                {
                    buses[i + 1].IsNext = true;
                    var diff = buses[i + 1].Time.Subtract(_now);
                    result = diff.Hours == 0 ?
                        $"ближайший отправится через {diff.Minutes} {GetStringEndForMinutes(diff)}" :
                        $"ближайший отправится через {diff.Hours} {GetStringEndForHours(diff)} {diff.Minutes} {GetStringEndForMinutes(diff)}";
                    break;
                }
            }
            if (_now >= buses.Last().Time)
            {
                buses[0].IsNext = true;
                var tommorowBus = buses[0].Time.AddDays(1);
                var diff = tommorowBus.Subtract(_now);
                result = $"ближайший отправится через {diff.Hours} {GetStringEndForHours(diff)} {diff.Minutes} {GetStringEndForMinutes(diff)}";
            }
            else if (_now < buses.First().Time)
            {
                buses[0].IsNext = true;
                var diff = buses.First().Time.Subtract(_now);
                result = diff.Hours == 0 ?
                        $"ближайший отправится через {diff.Minutes} {GetStringEndForMinutes(diff)}" :
                        $"ближайший отправится через {diff.Hours} {GetStringEndForHours(diff)} {diff.Minutes} {GetStringEndForMinutes(diff)}";
            }

            return result;
        }

        public OutcityBusView()
        {
            var dayOfWeek = DateTime.Now.DayOfWeek;
            if (dayOfWeek == DayOfWeek.Friday)
            {
                Buses816ToEkb.Insert(18, new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 16, 45, 0)));
                Buses816ToRezh.Insert(17, new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 19, 15, 0)));
            }
            if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
            {
                Buses816ToEkb.RemoveAt(0);
                Buses816ToRezh.RemoveAt(0);
                if (dayOfWeek == DayOfWeek.Sunday)
                {
                    Buses816ToEkb.Insert(Buses816ToEkb.Count - 2, new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 19, 30, 0)));
                    Buses816ToRezh.Insert(Buses816ToEkb.Count - 2, new BusView(new DateTime(_now.Year, _now.Month, _now.Day, 22, 30, 0)));
                }
            }

            TimeClosest816ToEkb = GetClosest(Buses816ToEkb);
            TimeClosest816ToRezh = GetClosest(Buses816ToRezh);
            TimeClosest527ToEkb = GetClosest(Buses527ToEkb);
            TimeClosest527ToRezh = GetClosest(Buses527ToRezh);
        }

        private string GetStringEndForMinutes(TimeSpan time)
        {
            var lastDigit = time.Minutes % 10;

            if (lastDigit == 1)
                return "минуту";
            else if (lastDigit > 1 && lastDigit < 5)
                return "минуты";
            else return "минут";
        }

        private string GetStringEndForHours(TimeSpan time)
        {
            var lastDigit = time.Hours % 10;

            if (lastDigit == 1)
                return "час";
            else if (lastDigit > 1 && lastDigit < 5)
                return "часа";
            else return "часов";
        }
    }
}