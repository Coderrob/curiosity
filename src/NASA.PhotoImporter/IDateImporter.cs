using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NASA.PhotoImporter
{
    public interface IDateImporter
    {
        Task<IEnumerable<DateTime>> GetDates();
    }
}