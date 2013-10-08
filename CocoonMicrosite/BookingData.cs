using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;


namespace CocoonMicrosite
{
    public class BookingData
    {
        private XDocument _data;
        private string _bookingFileLocation = "F:\\My Documents\\Clients\\CocoonMicrosite\\CocoonMicrosite\\bookings.xml";

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

        public Venue GetVenue(int id)
        {
            Venue venueFromFile = new Venue();
            XElement venueElement = _data.Root.Elements("venues").Elements("venue").FirstOrDefault(x => x.Attribute("id").Value == "1");

            if (venueElement != null)
            {
                venueFromFile = new Venue()
                                    {
                                        Name = venueElement.Element("name").Value,
                                    };
            }


            return venueFromFile;
        }


        //private string LoadBookingFileLocationFromWebConfig() 
        //{
        //    string location = String.Empty;

        //    System.Configuration.Configuration webConfig =
        //        System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);

        //    Object bookingsFileLocatonFromConfig = webConfig.AppSettings.Settings["BookingsFileLocation"];

        //    if (bookingsFileLocatonFromConfig != null)
        //    {
        //        location = bookingsFileLocatonFromConfig.ToString();
        //    }

        //    return location;
        //}
    }
}