using System.Text.RegularExpressions;
using Nancy.Routing.Constraints;

namespace ECommerceFX.Web.Tools.RouteSegmentConstraints
{
    public class EmailRouteSegmentConstraint : RouteSegmentConstraintBase<string>
    {
        protected override bool TryMatch(string constraint, string segment, out string matchedValue)
        {
            var matches = Regex.IsMatch(segment, @"^[A-Za-z0-9_.+]+@[a-z]{2,}\.[a-z]{2,}$");
            matchedValue = matches ? segment : null;
            return matches;
        }

        public override string Name
        {
            get { return "email"; }
        }
    }
}