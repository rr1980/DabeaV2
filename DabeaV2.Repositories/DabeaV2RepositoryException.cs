using System;

namespace DabeaV2.Repositories
{
    public class DabeaV2RepositoryException : Exception
    {
        public DabeaV2RepositoryException(string message) : base(message)
        {

        }

        public DabeaV2RepositoryException(string message, Exception ex) : base("RepositoryException: " + message, ex)
        {

        }
    }
}
