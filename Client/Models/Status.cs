namespace Client.Models
{
    public enum Status
    {
        received,
        confirmed,
        courier_accepted,
        preparing,
        ready_for_pickup,
        in_transit,
        delivered
    }
}