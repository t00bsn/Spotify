using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify
{
    internal class songinformationen
    {
        public static int SongID { get; set; }
        public string TiteldesSongs { get; set; }
        public string Künstler { get; set; }
        public string Albumname { get; set; }
        public DateTime Erscheinungsjahr { get; set; }
        public int DauerSEK { get; set; }
        public string Song { get; set; }

        public songinformationen(int songID, string titeldesSongs, string künstler, string albumname, DateTime erscheinungsjahr, int dauerSEK, string song)
        {
            SongID = songID;
            TiteldesSongs = titeldesSongs;
            Künstler = künstler;
            Albumname = albumname;
            Erscheinungsjahr = erscheinungsjahr;
            DauerSEK = dauerSEK;
            Song = song;
        }



    }
}