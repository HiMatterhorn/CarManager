namespace AmiFlota.Utilities
{
    public class Enums
    {
        public enum TrunkType
        {
            Hatchback = 1, Sedan = 2, combi = 3, van = 4, suv = 5
        }

        public enum UserRole
        {
            admin, manager, user
        }

        public enum BookingStatus
        {
            Pending = 0, Approved = 10, Active = 20, OnTheWay = 30, Finished = 50, Cancelled = 999
        }

        public enum Fuel
        {
            Gasoline, Diesel, LPG, Electric
        }
    }
}
