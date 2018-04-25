using Pricing.Models;
using System.Collections.Generic;

namespace Pricing.Helpers.Abstract
{
    public interface IUrlFormatter
    {
        string Format(IReadOnlyCollection<UrlParam> urlParams);
    }
}