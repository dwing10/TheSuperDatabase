using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSuperDatabase
{
    class Character
    {
        public enum Alignment
        {
            NONE,
            GOOD,
            EVIL,
            NEUTRAL,
            ANTIHERO
        }

        public enum Universe
        {
            NONE,
            DC,
            MARVEL
        }

        #region fields

        private string _name;
        private string _realName;
        private string _gender;
        private string _homeworld;
        private Alignment _alignment;
        private Universe _universe;

        #endregion

        #region properties

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string RealName
        {
            get { return _realName; }
            set { _realName = value; }
        }

        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public string Homeworld
        {
            get { return _homeworld; }
            set { _homeworld = value; }
        }

        public Alignment Alignments
        {
            get { return _alignment; }
            set { _alignment = value; }
        }

        public Universe ComicUniverse
        {
            get { return _universe; }
            set { _universe = value; }
        }

        #endregion

        #region constructor

        public Character()
        {

        }

        #endregion
    }
}
