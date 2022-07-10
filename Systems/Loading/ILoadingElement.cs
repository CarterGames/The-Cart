// ----------------------------------------------------------------------------
// ILoadingElement.cs
// 
// Description: An interface to handle a element that contributes to the
//              progress of the loading system.
// ----------------------------------------------------------------------------

using Scarlet.EventsSystem;

namespace Scarlet.Loading
{
    public interface ILoadingElement
    {
        float Progress { get; set; }
        Evt OnStart { get; set; }
        Evt OnProgressMade { get; set; }
        Evt OnLoaded { get; set; }
    }
}