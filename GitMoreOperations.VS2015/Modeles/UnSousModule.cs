namespace GitMoreOperations.VS2015.Modeles
{
    public class UnSousModule
    {
        private string _lastHash;
        private string _nom;
        private string _branche;

        public string LastHash
        {
            get
            {
                return _lastHash;
            }

            set
            {
                _lastHash = value;
            }
        }

        public string Nom
        {
            get
            {
                return _nom;
            }

            set
            {
                _nom = value;
            }
        }

        public string Branche
        {
            get
            {
                return _branche;
            }

            set
            {
                _branche = value;
            }
        }
    }
}
