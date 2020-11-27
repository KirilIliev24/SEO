using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SearchEngine.Model
{
    public class ResultComparer : IEqualityComparer<Result>
    {
        public bool Equals([AllowNull] Result x, [AllowNull] Result y) =>        
            x.Link.Equals(y.Link, StringComparison.InvariantCultureIgnoreCase);


        public int GetHashCode([DisallowNull] Result obj) =>
            obj.Link.GetHashCode();
    }
}
