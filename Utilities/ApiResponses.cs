namespace AmiFlota.Utilities
{
    public class ApiResponses
    {

        public static readonly int success_code = 1;
        public static readonly int failure_code = 0;


        public static readonly string bookingConfirmed = "Booking Confirmed";
        public static readonly string bookingConfirmationError = "Error occured while confirming the booking";
        public static readonly string bookingDeleted = "Booking Deleted";
        public static readonly string bookingDeleteError = "Error occured while deleting the booking";
        public static readonly string bookingRejected = "Booking Rejected";
        public static readonly string bookingRejectionError = "Error occured while rejecting the booking";
        public static readonly string carTaken = "Car Taken";
        public static readonly string carTakingError = "Error occured while taking the car";
        public static readonly string carReturn = "Car Returned";
        public static readonly string carReturningError = "Error occured while returning the car";
    }
}
