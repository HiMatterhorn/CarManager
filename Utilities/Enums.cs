namespace AmiFlota.Utilities
{
    public class Enums
    {
        public enum TrunkType
        {
            hatchback, sedan, combi, van, suv
        }

        public enum UserRole
        {
            admin, manager, user
        }

        public enum BookingStatus
        {
            Pending = 0, Approved = 10, Active = 20, OnTheWay = 30, Finished = 50
        }

        public enum Fuel
        {
            Gasoline, Diesel, LPG, Electric
        }
    }
}
