using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibLCV {
    public static class CollectionHelper {
        public static int MaxIndex<T>(this ICollection<T> list) => list.Count-1;
        public static bool IndexExists<T>(this ICollection<T> list, int index) => index >= 0 && index < list.Count;
        public static int NextIndex<T>(this ICollection<T> list, int index) => index < list.MaxIndex() ? index+1 : list.MaxIndex();
        public static int PrevIndex<T>(this ICollection<T> list, int index) => index > 0 ? index-1 : 0;

        // https://stackoverflow.com/questions/43021/how-do-you-get-the-index-of-the-current-iteration-of-a-foreach-loop
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self) => self.Select((item, index) => (item, index));
    }
}
