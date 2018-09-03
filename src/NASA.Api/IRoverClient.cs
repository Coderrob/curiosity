using System.Collections.Generic;
using NASA.Api.Rovers;

namespace NASA.Api
{
    public interface IRoverClient
    {
        IEnumerable<IRover> GetRovers();
        IRover GetRover(string name);
    }
}