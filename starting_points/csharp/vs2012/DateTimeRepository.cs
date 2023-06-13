using System;

namespace Tests {
    public class DateTimeRepository : IDateTimeRepository {
        private static readonly DateTimeRepository instance = new DateTimeRepository();

        private DateTimeRepository() {

        }

        public static DateTimeRepository Instance => instance;

        public DateTime GetCurrentDateTime() {
            return DateTime.Now;
        }
    }
}
