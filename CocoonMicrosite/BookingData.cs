using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;


namespace CocoonMicrosite
{
    public class BookingData
    {
        #region Enums

        private enum EntityType
        {
            Seat,
            Venue,
            Bus
        }

        #endregion

        #region Private Declarations

        private XDocument _data;
        private string _bookingFileLocation = "F:\\My Documents\\Clients\\CocoonMicrosite\\CocoonMicrosite\\bookings.xml";

        #endregion

        #region Constructor

        public BookingData()
        {
            try
            {
                if (_bookingFileLocation != String.Empty) 
                {
                    _data = XDocument.Load(_bookingFileLocation);
                }
            }
            catch (Exception e)
            {

            }
        }

        #endregion

        #region Bus

        public List<Bus> GetAllBusses()
        {
            List<Bus> allBusses = new List<Bus>();

            List<XElement> busElements = _data.Root.Element("busses").Elements("bus").ToList();

            foreach (var busElement in busElements)
            {
                allBusses.Add(GetBusWithSeats(busElement.Attribute("id").Value));
            }

            return allBusses;
        }

        public Bus GetBus(string id) 
        {
            Bus bus = new Bus();
            XElement busElement = _data.Root.Elements("busses").Elements("bus").FirstOrDefault(b => b.Attribute("id").Value == id);

            if (busElement != null)
            {

                string venueId = busElement.Element("venue").Value;
                string timeId = busElement.Element("time").Value;

                Venue venue = GetVenue(venueId);

                bus = new Bus()
                {
                    Id = busElement.Attribute("id").Value,
                    Venue = GetVenue(venueId),
                    Time = GetTime(timeId)
                };
            }
            return bus;
        }

        public Bus GetBusWithSeats(string id)
        {
            Bus bus = GetBus(id);
            List<Seat> seats = new List<Seat>();

            if (!String.IsNullOrEmpty(bus.Id))  
            {
                // get bus element from file
                
                XElement busElement = _data.Root.Elements("busses").Elements("bus").FirstOrDefault(b => b.Attribute("id").Value == id);
                
                // get all seat elements, create seat objects for them and add to seats list

                if (busElement != null) 
                {
                    List<XElement> seatsFromXMLFile = busElement.Elements("seats").Elements("seat").ToList();

                    foreach (var seatElement in seatsFromXMLFile) 
                    {
                        seats.Add(new Seat()
                                      {
                                          Id = seatElement.Attribute("id").Value,
                                          BookedName = seatElement.Element("bookedName").Value,
                                          BookingReference = seatElement.Element("bookingRef").Value
                                      });
                    }

                    // add seats to bus object
                    
                    bus.Seats = seats;
                }
            }

            return bus;
        }

        public void SaveBus(Bus bus) 
        {
            try
            {
                int id = _data.Root.Elements("buses").Elements("bus").Count() + 1;

                XElement newBus = new XElement("bus",
                                        new XAttribute("id", id),
                                            new XElement("location", bus.Venue),
                                            new XElement("time", bus.Time));
                
                _data.Root.Elements("busses").FirstOrDefault().Add(newBus);
                _data.Save(_bookingFileLocation);
            }
            catch (Exception e)
            {
                // do something
            }
        }

        #endregion

        #region Seat

        public void SaveSeatBooking(string busId, Seat seat)
        {
            try
            {              
                XElement busElement = _data.Root.Elements("busses").Elements("bus").FirstOrDefault(b => b.Attribute("id").Value == busId);

                if (busElement != null)
                {
                    int id = busElement.Elements("seats").Elements("seat").Count() + 1;

                    XElement seatBooking = new XElement("seat",
                                            new XAttribute("id", id),
                                             new XElement("bookedName", seat.BookedName),
                                             new XElement("bookingRef", seat.BookingReference));

                    busElement.Element("seats").Add(seatBooking);
                    _data.Save(_bookingFileLocation);
                }

            }
            catch (Exception e)
            {
                // do something useful
            }
        }

        #endregion

        #region Venue

        /// <summary>
        /// Gets the Venue from the XML file with the supplied Id if it exists.
        /// </summary>
        /// <param name="id">The Id of the requested Venue</param>
        /// <returns>A Venue object with the matching Id if one exists</returns>
        public Venue GetVenue(string id)
        {
            Venue venueFromFile = new Venue();
            XElement venueElement = _data.Root.Elements("venues").Elements("venue").FirstOrDefault(x => x.Attribute("id").Value == id);

            if (venueElement != null)
            {
                venueFromFile = new Venue()
                {
                    Name = venueElement.Element("name").Value,
                    Street1 = venueElement.Element("street1").Value,
                    Street2 = venueElement.Element("street2").Value,
                    Street3 = venueElement.Element("street3").Value,
                    City = venueElement.Element("city").Value,
                    Postcode = venueElement.Element("postcode").Value,
                    PhoneNumber = venueElement.Element("phone").Value
                };
            }

            return venueFromFile;
        }

        /// <summary>
        /// Takes a Venue object and adds it the XML file.
        /// </summary>
        /// <param name="venue">The Venue to be persisted</param>
        public void SaveVenue(Venue venue)
        {
            try
            {
                int id = _data.Root.Elements("venues").Elements("venue").Count() + 1;

                XElement newVenue = new XElement("venue",
                                            new XAttribute("id", id),
                                             new XElement("name", venue.Name),
                                             new XElement("street1", venue.Street1),
                                             new XElement("street2", venue.Street2),
                                             new XElement("street3", venue.Street3),
                                             new XElement("city", venue.City),
                                             new XElement("postcode", venue.Postcode),
                                             new XElement("phone", venue.PhoneNumber)
                                             );

                _data.Root.Elements("venues").FirstOrDefault().Add(newVenue);
                _data.Save(_bookingFileLocation);
            }
            catch (Exception e)
            {
                // do something Holmes!
            }
        }

        #endregion

        #region Time

        private Time GetTime(string id)
        {
            Time returnTime = new Time();
            XElement timeFromXMLFile = _data.Root.Elements("times").Elements("time").FirstOrDefault(t => t.Attribute("id").Value == id);

            if (timeFromXMLFile != null) 
            {
                returnTime = new Time
                {
                    Id = timeFromXMLFile.Attribute("id").Value,
                    TimeSlot = timeFromXMLFile.Value
                };
            }

            return returnTime;
        }

        public List<Time> GetTimes()
        {
            List<Time> returnTimes = new List<Time>();
            List<XElement> timesFromXMLFile = _data.Root.Elements("times").Elements("time").ToList();

            foreach (var time in timesFromXMLFile)
            {
                returnTimes.Add(new Time(time.Attribute("id").Value, time.Value));
            }

            return returnTimes;
        }

        public void AddTime(string time)
        {
            try
            {
                int id = _data.Root.Elements("times").Elements("time").Count() + 1;

                XElement newTime = new XElement("time",
                                    new XAttribute("Id", id),
                                     new XElement("time", time));

                _data.Root.Elements("times").FirstOrDefault().Add(newTime);
                _data.Save(_bookingFileLocation);
            }
            catch (Exception e)
            {
                // do something
            }
        }

        #endregion
    }
}